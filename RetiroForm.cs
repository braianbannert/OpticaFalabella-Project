using System;
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
using System.Linq;

using System.Threading;

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class RetiroForm : Form
    {
        string previous_readQR = "";

        string[] valores;
        public SQLiteConnection miConexion;
        string day, month;
        string fecha_de_retiro;
        string fecha_pedido = String.Empty;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public RetiroForm()
        {
            InitializeComponent();
        }

        private void RetiroForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker1.MinDate = DateTime.Today;

            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            txt_DNI_Cliente.Text = "";
            txt_Escaneo.Text =  "";
            txt_NroORDEN.Text = "";

            txt_Seña.Text = "0";
            txt_Subtotal.Text = "0";
            txt_Total.Text = "0";   

            txt_Escaneo.Focus();

            if (dateTimePicker1.Value.Day < 10)
            {
                day = "0" + dateTimePicker1.Value.Day.ToString();
            }
            else
            {
                day = dateTimePicker1.Value.Month.ToString();
            }

            if (dateTimePicker1.Value.Month < 10)
            {
                month = "0" + dateTimePicker1.Value.Day.ToString();
            }
            else
            {
                month = dateTimePicker1.Value.Month.ToString();
            }

            fecha_de_retiro = day + "/" + month + "/" + dateTimePicker1.Value.Year;
        }        

        private void RetiroTrabajo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }   //  READY

        private void txt_Escaneo_TextChanged(object sender, EventArgs e)
        {
            if (previous_readQR == "")
            {
                previous_readQR = txt_Escaneo.Text;
            }
            else
            {
                if ((previous_readQR != txt_Escaneo.Text) && (txt_Escaneo.Text != ""))
                {
                    previous_readQR = txt_Escaneo.Text;
                }
            }

            if (txt_Escaneo.Text.Contains("#QRCode-Retiro"))
            {
                valores = txt_Escaneo.Text.Split('*');

                miConexion.Open();

                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = miConexion.CreateCommand();

                // TRABAJOS (Nro_Orden, DNI, Subtotal, Seña, Resta_Abonar)";
                sqlite_cmd.CommandText = "SELECT Nro_Orden, DNI, Subtotal, Seña, Resta_Abonar, Fecha_Pedido FROM TRABAJOS";
                sqlite_cmd.CommandType = System.Data.CommandType.Text;

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    if (Convert.ToInt32(sqlite_datareader.GetValue(0)) == Convert.ToInt32(valores[2]))
                    {
                        txt_DNI_Cliente.Text = valores[1].ToString();
                        txt_Subtotal.Text = sqlite_datareader.GetValue(2).ToString();
                        txt_Seña.Text = sqlite_datareader.GetValue(3).ToString();
                        txt_Total.Text = sqlite_datareader.GetValue(4).ToString();
                        fecha_pedido = sqlite_datareader.GetValue(5).ToString();
                    }
                }

                miConexion.Close();
            }
            
        }

        private void btn_TomarPago_Clicked(object sender, EventArgs e)
        {
            string day, month, year, fecha_de_retiro;

            if(dateTimePicker1.Value.Day < 10)
            {
                day = "0" + dateTimePicker1.Value.Day.ToString();
            }
            else
            {
                day = dateTimePicker1.Value.Day.ToString();
            }

            if (dateTimePicker1.Value.Month < 10)
            {
                month = "0" + dateTimePicker1.Value.Month.ToString();
            }
            else
            {
                month = dateTimePicker1.Value.Month.ToString();
            }

            year = dateTimePicker1.Value.Year.ToString();

            fecha_de_retiro = day + "-" + month + "-" + year;

            /**********************************************************************/
            /***                                                                ***/
            /***              Cargamos la fecha de retiro en SQLite             ***/
            /***                                                                ***/
            /**********************************************************************/

            miConexion.Open();

            //  TRABAJOS (Fecha_Entrega_Real)";

            using (SQLiteCommand command = new SQLiteCommand(miConexion))
            {
                command.CommandText = "update TRABAJOS set Fecha_Entrega_Real =:fecha where Nro_Orden=:nro_orden";
                command.Parameters.Add("fecha", DbType.String).Value = fecha_de_retiro;
                command.Parameters.Add("nro_orden", DbType.String).Value = valores[2];
                command.ExecuteNonQuery();
            }

            miConexion.Close();

            /**********************************************************************/
            /***                                                                ***/
            /***      Leemos los armazones para poder descontarlos del stock    ***/
            /***                                                                ***/
            /**********************************************************************/

            SQLiteDataReader sqlite_datareader2;
            SQLiteCommand sqlite_cmd2;
            sqlite_cmd2 = miConexion.CreateCommand();
            sqlite_cmd2.CommandText = "SELECT Nro_Orden, DNI, Armazon_Lejos, Armazon_Cerca FROM TIPOS_CRISTALES";
            sqlite_cmd2.CommandType = System.Data.CommandType.Text;

            miConexion.Open();

            sqlite_datareader2 = sqlite_cmd2.ExecuteReader();

            while (sqlite_datareader2.Read())
            {
                if ((Convert.ToInt32(sqlite_datareader2.GetValue(0)) == Convert.ToInt32(txt_NroORDEN.Text)) && (sqlite_datareader2.GetValue(1).ToString() == txt_DNI_Cliente.Text))
                {
                    string que = String.Empty;
                    string armazon_lejos = sqlite_datareader2.GetValue(2).ToString();
                    string armazon_cerca = sqlite_datareader2.GetValue(3).ToString();

                    if (armazon_lejos == "")
                    {
                        que = "DESCUENTO/AR/" + armazon_lejos + "/" + "1";

                        Funciones.Functions.SendBroadcastMessage(que);

                        //  Ahora resta la parte de actualizar FIREBASE
                        //  Primero leemos la cantidad en la nube y con ese valor vamos a basar el descuento

                        FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/AR/" + armazon_lejos);

                        Dictionary<string, int> data1 = JsonConvert.DeserializeObject<Dictionary<string, int>>(response.Body.ToString());

                        int cantidad_vieja;
                        int cantidad_nueva;

                        cantidad_vieja = data1.ElementAt(0).Value;
                        cantidad_nueva = cantidad_vieja - 1;

                        response = client.Set("Optica Falabella Av 38" + "/" + "Stock" + "/" + "AR" + "/" + armazon_lejos, cantidad_nueva);
                    }

                    if (armazon_cerca == "")
                    {
                        que = "DESCUENTO/AR/" + armazon_cerca +"/" + "1";

                        Funciones.Functions.SendBroadcastMessage(que);

                        //  Ahora resta la parte de actualizar FIREBASE
                        //  Primero leemos la cantidad en la nube y con ese valor vamos a basar el descuento

                        FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/AR/" + armazon_cerca);

                        Dictionary<string, int> data1 = JsonConvert.DeserializeObject<Dictionary<string, int>>(response.Body.ToString());

                        int cantidad_vieja;
                        int cantidad_nueva;

                        cantidad_vieja = data1.ElementAt(0).Value;
                        cantidad_nueva = cantidad_vieja - 1;

                        response = client.Set("Optica Falabella Av 38" + "/" + "Stock" + "/" + "AR" + "/" + armazon_cerca, cantidad_nueva);
                    }

                    FirebaseResponse response1 = client.Set(@"Optica Falabella Av 38/Finalizados/" + txt_DNI_Cliente.Text + "/" + Convert.ToInt32(txt_NroORDEN.Text), fecha_de_retiro);
                }
            }

            miConexion.Close();

            int seña = Convert.ToInt32(txt_Seña.Text);
            string forma_de_pago = String.Empty;
            string cuotas = String.Empty;

            if (chk_Debito.Checked == true)
            {
                forma_de_pago = "Debito";
                cuotas = "0";
            }
            else
            {
                if (chk_Credito.Checked == true)
                {
                    forma_de_pago = "Credito";
                    cuotas = combo_Cuotas.Text;
                }
                else
                {
                    if (chk_Efectivo.Checked == true)
                    {
                        forma_de_pago = "Efectivo";
                        cuotas = "0";
                    }
                }
            }

            string concepto_pago = "Total";

            string tarjeta = String.Empty;
            double comision_tarjeta = 0.0f;

            if (forma_de_pago == "Credito")
            {
                switch (cuotas)
                {
                    case "1":
                        tarjeta = forma_de_pago + "_1";
                        break;
                    case "3":
                        tarjeta = forma_de_pago + "_3";
                        break;
                    case "6":
                        tarjeta = forma_de_pago + "_6";
                        break;
                    case "12":
                        tarjeta = forma_de_pago + "_12";
                        break;
                }

                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                miConexion.Open();

                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = miConexion.CreateCommand();
                sqlite_cmd.CommandText = "SELECT Tipo, Comision FROM COMISIONES_TARJETA";
                sqlite_cmd.CommandType = System.Data.CommandType.Text;

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    if (sqlite_datareader.GetValue(0).ToString() == tarjeta)
                    {
                        comision_tarjeta = Convert.ToDouble(sqlite_datareader.GetValue(1));
                    }
                }

                miConexion.Close();
            }
            else
            {
                if (forma_de_pago == "Debito")
                {
                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Tipo, Comision FROM COMISIONES_TARJETA";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {
                        if (sqlite_datareader.GetValue(0).ToString() == forma_de_pago)
                        {
                            comision_tarjeta = Convert.ToDouble(sqlite_datareader.GetValue(1));
                        }
                    }

                    miConexion.Close();
                }
                else
                {
                    comision_tarjeta = 0.0f;
                }
            }

            int subtotal = Convert.ToInt32(txt_Subtotal.Text);
            double resta_abonar = subtotal - seña;

            double subtotal_c_desc = (resta_abonar * (100 - comision_tarjeta)) / 100;

            string what = String.Empty;
            what += "VENTAS/TRABAJO/" + txt_NroORDEN.Text + "/" + "/" + "/" + "/" + "/" + "/";
            what += "/" + "/" + "/" + "/" + "/" + "/";
            what += "/" + "/" + "/" + "/" + "/" + "/";
            what += "/" + "/" + "/" + "/" + "/" + "/" + "/////";
            what += subtotal + "/" + txt_Seña.Text + "/" + txt_Total.Text + "/" + forma_de_pago + "/" + cuotas + "/" + comision_tarjeta.ToString() + "/" + subtotal_c_desc + "/" + concepto_pago + "/";
            what += fecha_pedido;

            Funciones.Functions.SendBroadcastMessage(what);
            Funciones.Functions.SaveDataToExcelFile(what);
        }
    }
}
