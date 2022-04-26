using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Extensions;
using System.Data.SQLite;

using System.Threading;
using System.Linq;

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class BajaStock : Form
    {
        string previous_readQR = "";

        public SQLiteConnection miConexion;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public BajaStock()
        {
            InitializeComponent();
        }

        private async void txt_Codigo_TextChanged(object sender, EventArgs e)
        {
            if (previous_readQR == "")
            {
                previous_readQR = txt_Codigo.Text;
            }
            else
            {
                if ((previous_readQR != txt_Codigo.Text) && (txt_Codigo.Text != ""))
                {
                    previous_readQR = txt_Codigo.Text;
                }
            }

            if(previous_readQR != "")
            {
                string[] values = new string[3];

                previous_readQR.Replace('/', '*');
                values = previous_readQR.Split(new char[] { '*', '/' });

                string tipo = string.Empty;
                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                conn.Open();
                string sql = "select Codigo from CODIGOS";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (previous_readQR == rdr.GetValue(0).ToString())
                    {
                        //  Debo chequear de qué tabla viene

                        if ((values[0] == "AR") || (values[0] == "BCO") || (values[0] == "BLUE"))
                        {
                            //  El código pertenece a un cristal

                            string sql2 = "select Codigo, Cantidad_Disponible from STOCK_CRISTALES";

                            SQLiteCommand cmd2 = new SQLiteCommand(sql2, conn);
                            SQLiteDataReader rdr2 = cmd.ExecuteReader();

                            while (rdr2.Read())
                            {
                                if (rdr2.GetValue(0).ToString() == previous_readQR)
                                {
                                    //  Tuve coincidencia de cristales

                                    DialogResult d = MessageBox.Show("¿Desea quitar del inventario a:\r\n\r\n" + previous_readQR + "\r\n\r\n", "Alerta", MessageBoxButtons.YesNo);

                                    if(d == DialogResult.OK)
                                    {
                                        //  El usuario aceptó descontar el cristal de las bases de datos
                                        //  Descuento el cristal de la base de datos local y envío mensaje de BROADCAST para que la otra máquina lo descuente también
                                        string sql3 = "UPDATE STOCK_CRISTALES SET Cantidad_Disponible = '" + (Convert.ToInt32(rdr.GetValue(1)) - 1) + "'" + " WHERE Codigo = '" + previous_readQR + "'";
                                        SQLiteCommand command = new SQLiteCommand(sql3, miConexion);
                                        command.ExecuteNonQuery();
                                        command.Dispose();

                                        string codigo = previous_readQR;
                                        string what;

                                        what = "DESCUENTO/CRISTALES/" + codigo + "/" + "1";

                                        Funciones.Functions.SendBroadcastMessage(what);

                                        //  Ahora resta la parte de actualizar FIREBASE
                                        //  Primero leemos la cantidad en la nube y con ese valor vamos a basar el descuento

                                        FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/Cristales/" + codigo);

                                        Dictionary<string, int> data1 = JsonConvert.DeserializeObject<Dictionary<string, int>>(response.Body.ToString());

                                        int cantidad_vieja;
                                        int cantidad_nueva;

                                        cantidad_vieja = data1.ElementAt(0).Value;
                                        cantidad_nueva = cantidad_vieja - 1;

                                        response = await client.SetAsync("Optica Falabella Av 38" + "/" + "Stock" + "/" + "Cristales" + "/" + values[0] + "/" + codigo, cantidad_nueva);
                                    }
                                    
                                }
                            }
                        }
                    }
                }

                rdr.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        private void BajaStock_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            client = new FireSharp.FirebaseClient(config);

            txt_Codigo.Text = "";
            txt_Codigo.Focus();
        }

        private void BajaStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
