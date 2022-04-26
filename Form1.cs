
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Diagnostics;
using System.Net.NetworkInformation;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Extensions;
using System.Data.SQLite;
using Google.Cloud.Firestore;
using System.IO;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using QRCoder;
using System.Threading;

using Spire.Pdf;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;

using WhatsAppApi;
using WhatsAppApi.Account;
using WhatsAppApi.Helper;
using WhatsAppApi.Parser;
using WhatsAppApi.Register;
using WhatsAppApi.Response;
using WhatsAppApi.Settings;

using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class Form1 : Form
    {
        public SQLiteConnection miConexion;
        public bool existo_solo = false;

        // Twilio WhatsApp identifiers
        const string accountSID = "AC34ab5e47383574535aebcf93acb61116";
        const string authToken = "de90e5d0feb1eaba992d95d63ea872b5";

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        string QR_Text;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            QR_Text = "";

            txt_QR_Read.Focus();
            Data.Valores_Calculo.seña = 0;
            Data.Valores_Calculo.subtotal = 0;
            Data.Valores_Calculo.total = 0;

            Data.DataTrabajos.PROCEDENCIA = "";

            if (!Data.Variables_Globales.unica_vez)
            {
                Thread udpListener = new Thread(new ThreadStart(Funciones.Functions.StartListener));
                udpListener.Start();
                Data.Variables_Globales.unica_vez = true;

                Funciones.Functions.CreateDaySales();
            }

            var timestamp = Funciones.Functions.DateTimeNowToUnixTimeStamp();

            client = new FireSharp.FirebaseClient(config);
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            Funciones.Functions.GetBroadcastAddress();
            Funciones.Functions.CrearBaseDeDatos();            
            Funciones.Functions.Get_Info_Optica();
            Funciones.Functions.SendBroadcastMessage("NRO_PC/" + Data.Data_Optica.Computadora_Nro.ToString());
        }

        public async void SubirDatosAFirebase()
        {
            var infoTrabajos = new Data.InfoTrabajos
            {
                Nro_Orden = 0,
                Apellido_Cliente = "",
                Nombre_Cliente = "",
                DNI_Cliente = "",
                //Telefono_Cliente = "",
                Domicilio_Cliente = "",
                Tipo_Cristal = "",
                AODCC = 0,
                AODCL = 0,
                AODEC = 0,
                AODEL = 0,
                AOEIL = 0,
                AOICC = 0,
                AOICL = 0,
                AOIEC = 0,
                Eje_ODC = 0,
                Eje_ODL = 0,
                Eje_OIC = 0,
                Eje_OIL = 0,
                Fecha_Pedido = "",
                Fecha_Entrega_Pactada = "",
                Fecha_Entrega_Real = ""
            };

            infoTrabajos.Nro_Orden = 32;
            infoTrabajos.Apellido_Cliente = "Bannert";
            infoTrabajos.Nombre_Cliente = "Braian";
            infoTrabajos.DNI_Cliente = "33688441";
            //infoTrabajos.Telefono_Cliente = "2214545648";
            infoTrabajos.Domicilio_Cliente = "Avenida 66 Nº 2612";

            infoTrabajos.Tipo_Cristal = "Foto AR";
            infoTrabajos.AODCC = 0.25;
            infoTrabajos.AODCL = 0;

            infoTrabajos.AODEC = -2;
            infoTrabajos.AODEL = 0.50;

            infoTrabajos.AOEIL = 1.25;
            infoTrabajos.AOICC = -0.75;

            infoTrabajos.AOICL = 0.25;
            infoTrabajos.AOIEC = -1.75;

            infoTrabajos.Eje_ODC = 15;
            infoTrabajos.Eje_ODL = 0;
            infoTrabajos.Eje_OIC = 23;
            infoTrabajos.Eje_OIL = -45;

            infoTrabajos.Fecha_Pedido = "1615997265";
            infoTrabajos.Fecha_Entrega_Pactada = "23-03-2021";
            infoTrabajos.Fecha_Entrega_Real = "01-04-2021";

            SetResponse response = await client.SetAsync("Optica Falabella Av 38" + "/Clientes/" + Data.DataTrabajos.DNI_Cliente + "/" + infoTrabajos.Fecha_Pedido, infoTrabajos);
        }

        public async void RetrieveDataFromFirebase()
        {
            FirebaseResponse response = await client.GetAsync("Optica Falabella Av 38" + "/33688441/15-03-2021");
            //FirebaseResponse response2 = await client.GetAsync("Apellido");

            var statusCode = response.StatusCode.ToJson();
            //MessageBox.Show("Status: " + statusCode);

            if (statusCode == "200")
            {
                Data.DataTrabajos obj = response.ResultAs<Data.DataTrabajos>();

                /*
                MessageBox.Show("Apellido: " + obj.Apellido_Cliente + "\n\r" +
                                "Nombre: " + obj.Nombre_Cliente + "\n\r" +
                                "DNI: " + obj.DNI_Cliente + "\n\r" +
                                "Telefono: " + obj.Telefono_Cliente + "\n\r" +
                                "Domicilio: " + obj.Domicilio_Cliente + "\n\r" +
                                "Tipo Cristal: " + obj.Tipo_Cristal + "\n\r"
               );
                */
            }
        }

        private void NuevoReparacionItemClick(object sender, EventArgs e)
        {
            Form nuevaReparacionForm = new NuevaReparacion();
            nuevaReparacionForm.Show();
            this.Hide();
        }

        private void NuevoTrabajo_ItemClicked(object sender, EventArgs e)
        {
            Form nuevoTrabajoForm = new NuevoTrabajoForm();
            nuevoTrabajoForm.Show();
            this.Hide();
        }

        private void NuevaReparacion_ItemClicked(object sender, EventArgs e)
        {
            Form nuevaReparacion = new NuevaReparacion();
            nuevaReparacion.Show();
            this.Hide();
        }

        private void BuscarCliente_ItemClicked(object sender, EventArgs e)
        {
            Form buscarCliente = new BuscarCliente();
            buscarCliente.Show();
            this.Hide();
        }

        private void BuscarTrabajo_ItemClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Cliqueé en Buscar Trabajo");
        }

        private void InsertarTipoCristal_ItemClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Cliqueé en Insertar Tipo de Cristal");
        }

        private void InsertarProveedor_ItemClicked(object sender, EventArgs e)
        {
            Form nuevoProveedor = new NuevoProveedor();
            nuevoProveedor.Show();
            this.Hide();
        }

        private void Pedido_ItemClicked(object sender, EventArgs e)
        {
            Form pedido = new SendEmail();
            pedido.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Data.Data_Optica.Computadora_Nro == 1)
            {
                Funciones.Functions.CreateDaySales();
                Funciones.Functions.Send_Email("contaduria.falabella@gmail.com");
            }

            foreach (var process in Process.GetProcessesByName("OpticaFalabella"))
            {
                process.Kill();
            }
        }   //  READY

        private void Verificar_Precio_ItemClicked(object sender, EventArgs e)
        {
            Form verificarPrecio = new VerificarPrecio();
            verificarPrecio.Show();
            this.Hide();
        }

        private void DatosOptica_ItemClicked(object sender, EventArgs e)
        {
            Form datosoptica = new DatosOptica();
            datosoptica.Show();
            this.Hide();
        }   //  READY

        private void btn_SincronizarCristales_Clicked(object sender, EventArgs e)
        {
            // Revision de STOCK de CRISTALES
            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/Cristales");
            Dictionary<string, DatosCristales> data = JsonConvert.DeserializeObject<Dictionary<string, DatosCristales>>(response.Body.ToString());
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            miConexion.Open();

            foreach (var item in data)
            {
                response = client.Get(@"Optica Falabella Av 38/Stock/Cristales/" + item.Key.ToString());
                Dictionary<string, string> data1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

                string[] values;
                string tipo;
                string codigo;
                double cilindro;
                double esferico;
                int cantidad;

                if (!Funciones.Functions.CheckEmptyTable("STOCK_CRISTALES"))
                {
                    // Está vacía la tabla
                    for (int i = 0; i < data1.Keys.Count; i++)
                    {
                        values = data1.ElementAt(i).Key.Split("*");
                        tipo = values[0];
                        esferico = Convert.ToDouble(values[1]);
                        cilindro = Convert.ToDouble(values[2]);
                        codigo = data1.ElementAt(i).Key;
                        cantidad = Convert.ToInt32(data1.ElementAt(i).Value);

                        string sql = "insert into STOCK_CRISTALES (Tipo, Esferico, Cilindrico, Codigo, Cantidad_Disponible) values('" + tipo + "', '" + esferico + "', '" + cilindro + "', '" + codigo + "', '" + cantidad + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);

                        command.ExecuteNonQuery();
                        command.Dispose();

                        Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + codigo + "/" + cantidad);
                    }
                }
                else
                {
                    bool found = false;

                    // Tiene datos, hay que actualizarlos
                    for (int i = 0; i < data1.Keys.Count; i++)
                    {
                        values = data1.ElementAt(i).Key.Split("*");
                        tipo = values[0];
                        esferico = Convert.ToDouble(values[1]);
                        cilindro = Convert.ToDouble(values[2]);
                        codigo = data1.ElementAt(i).Key;
                        cantidad = Convert.ToInt32(data1.ElementAt(i).Value);

                        SQLiteDataReader sqlite_datareader;
                        SQLiteCommand sqlite_cmd;
                        sqlite_cmd = miConexion.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_CRISTALES";
                        sqlite_cmd.CommandType = System.Data.CommandType.Text;

                        sqlite_datareader = sqlite_cmd.ExecuteReader();

                        while (sqlite_datareader.Read())
                        {
                            if (sqlite_datareader.GetValue(0).ToString() == data1.ElementAt(i).Key)
                            {
                                string sql = "UPDATE STOCK_CRISTALES SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                                SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                                command.ExecuteNonQuery();
                                command.Dispose();

                                found = true;

                                Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + codigo + "/" + cantidad);
                            }
                        }

                        if (found == false)
                        {
                            string sql = "insert into STOCK_CRISTALES (Tipo, Esferico, Cilindrico, Codigo, Cantidad_Disponible) values('" + tipo + "', '" + esferico + "', '" + cilindro + "', '" + codigo + "', '" + cantidad + "')";
                            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                            command.ExecuteNonQuery();
                            command.Dispose();

                            Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + codigo + "/" + cantidad);
                        }
                    }
                }
            }

            miConexion.Close();

            //Funciones.Functions.SubirCodigos("STOCK_ARMAZONES_RECETA");
            MessageBox.Show("Base de datos actualizada");
        }   //  READY

        private void btn_SincronizarArmazonesReceta_Clicked(object sender, EventArgs e)
        {
            //  Sincronización de ARMAZONES DE RECETA
            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/AR");
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            string[] values;
            string marca;
            string modelo;
            string color;
            string codigo;
            int cantidad;

            if (!Funciones.Functions.CheckEmptyTable("CODIGOS"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    values = data.ElementAt(i).Key.Split("*");
                    marca = values[0];
                    modelo = values[1];
                    color = values[2];
                    codigo = data.ElementAt(i).Key;
                    cantidad = Convert.ToInt32(data.ElementAt(i).Value);

;                   string sql = "insert into STOCK_ARMAZONES_RECETA (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("STOCK/AR/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                bool found = false;
                miConexion.Open();

                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    values = data.ElementAt(i).Key.Split("*");
                    marca = values[0];
                    modelo = values[1];
                    color = values[2];
                    codigo = data.ElementAt(i).Key;
                    cantidad = Convert.ToInt32(data.ElementAt(i).Value);

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_ARMAZONES_RECETA";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {
                        if (sqlite_datareader.GetValue(0).ToString() == data.ElementAt(i).Key)
                        {
                            string sql = "UPDATE STOCK_ARMAZONES_RECETA SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                            command.ExecuteNonQuery();
                            command.Dispose();

                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        string sql = "insert into STOCK_ARMAZONES_RECETA (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    Funciones.Functions.SendBroadcastMessage("STOCK/AR/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }

                miConexion.Close();
            }

            //Funciones.Functions.SubirCodigos("STOCK_ARMAZONES_RECETA");
            MessageBox.Show("Base de datos actualizada");
        }   //  READY

        private void btn_SincronizarArmazonesSOL_Clicked(object sender, EventArgs e)
        {
            //  Sincronización de ARMAZONES DE RECETA
            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/Stock/AS");
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            string[] values;
            string marca;
            string modelo;
            string color;
            string codigo;
            int cantidad;

            if (!Funciones.Functions.CheckEmptyTable("CODIGOS"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    values = data.ElementAt(i).Key.Split("*");
                    marca = values[0];
                    modelo = values[1];
                    color = values[2];
                    codigo = data.ElementAt(i).Key;
                    cantidad = Convert.ToInt32(data.ElementAt(i).Value);

                    ; string sql = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("STOCK/AS/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                bool found = false;
                miConexion.Open();

                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    values = data.ElementAt(i).Key.Split("*");
                    marca = values[0];
                    modelo = values[1];
                    color = values[2];
                    codigo = data.ElementAt(i).Key;
                    cantidad = Convert.ToInt32(data.ElementAt(i).Value);

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_ARMAZONES_SOL";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {
                        if (sqlite_datareader.GetValue(0).ToString() == data.ElementAt(i).Key)
                        {
                            string sql = "UPDATE STOCK_ARMAZONES_SOL SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                            command.ExecuteNonQuery();
                            command.Dispose();

                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        string sql = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    Funciones.Functions.SendBroadcastMessage("STOCK/AS/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }

                miConexion.Close();
            }

            //Funciones.Functions.SubirCodigos("STOCK_ARMAZONES_SOL");
            MessageBox.Show("Base de datos actualizada");
        }   //  READY

        private void btn_SincronizarLC_Clicked(object sender, EventArgs e)
        {
            //LIQUIDOS_LENTES_CONTACTO (Marca varchar(30), Tipo varchar(30), Cantidad_Disponible int, Tamaño real, Unidad_Tamaño varchar(10), Precio int, Codigo varchar(30))

            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/PRECIOS/AS");

            Dictionary<string, Data.Firebase_AR> data = JsonConvert.DeserializeObject<Dictionary<string, Data.Firebase_AR>>(response.Body.ToString());
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            foreach (var item in data)
            {
                response = client.Get(@"Optica Falabella Av 38/PRECIOS/AS/" + item.Key);
                data = JsonConvert.DeserializeObject<Dictionary<string, Data.Firebase_AR>>(response.Body.ToString());
                foreach (var item2 in data)
                {
                    response = client.Get(@"Optica Falabella Av 38/PRECIOS/AS/" + item.Key + "/" + item2.Key);
                    DatosAR obj = response.ResultAs<DatosAR>();

                    if (!Funciones.Functions.CheckEmptyTable("STOCK_ARMAZONES_SOL"))
                    {
                        //  (Marca varchar(30), Modelo varchar(30), Material varchar(30), Cantidad_Disponible int, Precio int, Codigo varchar(30))

                        string sql = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Material, Cantidad_Disponible, Precio, Codigo) values('" + item.Key.ToString() + "', '" + obj.Mod.ToString() + "', '" + obj.Material.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        miConexion.Open();
                        command.ExecuteNonQuery();
                        command.Dispose();
                        miConexion.Close();
                    }
                    else
                    {
                        //  Chequeo que el número de código no esté en la base de datos. 
                        //  Si no está, lo inserto
                        //  Si llegara a estar en la base de datos lo actualizo

                        miConexion.Open();

                        SQLiteDataReader sqlite_datareader;
                        SQLiteCommand sqlite_cmd;
                        sqlite_cmd = miConexion.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT Codigo FROM STOCK_ARMAZONES_SOL";
                        sqlite_cmd.CommandType = System.Data.CommandType.Text;

                        sqlite_datareader = sqlite_cmd.ExecuteReader();

                        while (sqlite_datareader.Read())
                        {
                            if (sqlite_datareader.GetValue(0).ToString() == obj.Cod.ToString())
                            {
                                //  El codigo está en la base de datos. Lo actualizo

                                using (SQLiteCommand command1 = new SQLiteCommand(miConexion))
                                {
                                    command1.CommandText = "DELETE FROM STOCK_ARMAZONES_SOL WHERE Codigo='" + obj.Cod.ToString() + "'";
                                    command1.ExecuteNonQuery();

                                    command1.CommandText = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Material, Cantidad_Disponible, Precio, Codigo) values('" + item.Key.ToString() + "', '" + obj.Mod.ToString() + "', '" + obj.Material.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                                    command1.ExecuteNonQuery();
                                    command1.Dispose();
                                }
                            }
                            else
                            {
                                using (SQLiteCommand command1 = new SQLiteCommand(miConexion))
                                {
                                    command1.CommandText = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Material, Cantidad_Disponible, Precio, Codigo) values('" + item.Key.ToString() + "', '" + obj.Mod.ToString() + "', '" + obj.Material.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                                    command1.ExecuteNonQuery();
                                    command1.Dispose();
                                }
                            }
                        }

                        miConexion.Close();
                    }
                }
            }

            MessageBox.Show("Base de datos actualizada");
        }       //  Not READY

        private void btn_SincronizarLiquidos_Clicked(object sender, EventArgs e)
        {
            //LIQUIDOS_LENTES_CONTACTO (Marca varchar(30), Tipo varchar(30), Cantidad_Disponible int, Tamaño real, Unidad_Tamaño varchar(10), Precio int, Codigo varchar(30))

            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/PRECIOS/LIQ");

            Dictionary<string, Data.Firebase_Liq> data = JsonConvert.DeserializeObject<Dictionary<string, Data.Firebase_Liq>>(response.Body.ToString());
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            foreach (var item in data)
            {
                response = client.Get(@"Optica Falabella Av 38/PRECIOS/LIQ/" + item.Key);
                data = JsonConvert.DeserializeObject<Dictionary<string, Data.Firebase_Liq>>(response.Body.ToString());

                foreach (var item2 in data)
                {
                    response = client.Get(@"Optica Falabella Av 38/PRECIOS/LIQ/" + item.Key + "/" + item2.Key);
                    DatosLiq obj = response.ResultAs<DatosLiq>();

                    if (!Funciones.Functions.CheckEmptyTable("LIQUIDOS_LENTES_CONTACTO"))
                    {
                        //LIQUIDOS_LENTES_CONTACTO (Marca varchar(30), Cantidad_Disponible int, Tamaño real, Unidad_Tamaño varchar(10), Precio int, Codigo varchar(30))

                        string sql = "insert into LIQUIDOS_LENTES_CONTACTO (Marca, Cantidad_Disponible, Tamaño, Unidad_Tamaño, Precio, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Tam.ToString()) + "', '" + obj.Uni_Tam.ToString() + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        miConexion.Open();
                        command.ExecuteNonQuery();
                        command.Dispose();

                        miConexion.Close();
                    }
                    else
                    {
                        //  Chequeo que el número de código no esté en la base de datos. 
                        //  Si no está, lo inserto
                        //  Si llegara a estar en la base de datos lo actualizo

                        miConexion.Open();

                        SQLiteDataReader sqlite_datareader;
                        SQLiteCommand sqlite_cmd;
                        sqlite_cmd = miConexion.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT Codigo FROM LIQUIDOS_LENTES_CONTACTO";
                        sqlite_cmd.CommandType = System.Data.CommandType.Text;

                        sqlite_datareader = sqlite_cmd.ExecuteReader();

                        while (sqlite_datareader.Read())
                        {
                            if (sqlite_datareader.GetValue(0).ToString() == obj.Cod.ToString())
                            {
                                //  El codigo está en la base de datos. Lo actualizo

                                using (SQLiteCommand command1 = new SQLiteCommand(miConexion))
                                {
                                    command1.CommandText = "DELETE FROM LIQUIDOS_LENTES_CONTACTO WHERE Codigo='" + obj.Cod.ToString() + "'";
                                    command1.ExecuteNonQuery();

                                    command1.CommandText = "insert into LIQUIDOS_LENTES_CONTACTO (Marca, Cantidad_Disponible, Tamaño, Unidad_Tamaño, Precio, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Tam.ToString()) + "', '" + obj.Uni_Tam.ToString() + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                                    command1.ExecuteNonQuery();
                                    command1.Dispose();
                                }
                            }
                            else
                            {
                                using (SQLiteCommand command1 = new SQLiteCommand(miConexion))
                                {
                                    command1.CommandText = "insert into LIQUIDOS_LENTES_CONTACTO (Marca, Cantidad_Disponible, Tamaño, Unidad_Tamaño, Precio, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(obj.Cant) + "', '" + Convert.ToInt32(obj.Tam.ToString()) + "', '" + obj.Uni_Tam.ToString() + "', '" + Convert.ToInt32(obj.Precio) + "', '" + obj.Cod.ToString() + "')";
                                    command1.ExecuteNonQuery();
                                    command1.Dispose();
                                }
                            }
                        }

                        miConexion.Close();
                    }
                }
            }

            //Funciones.Functions.SubirCodigos("LIQUIDOS_LENTES_CONTACTO");
            MessageBox.Show("Base de datos actualizada");
        }   //  READY

        private async void ChequeoDatosSubidos_timer1Tick(object sender, EventArgs e)
        {
            bool internet_connection = Funciones.Functions.CheckInternetConnection();
            if (internet_connection)
            {
                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                conn.Open();
                string sql = "select Subido_A_Firebase from TRABAJOS";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (Convert.ToBoolean(rdr.GetValue(0)) == false)
                    {
                        SQLiteDataReader sqlite_datareader;
                        
                        string sqlDatos = "SELECT (Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Email) FROM TRABAJOS";
                        SQLiteCommand cmdDatos = new SQLiteCommand(sqlDatos, conn);
                        sqlite_datareader = cmdDatos.ExecuteReader();

                        string DNI;
                        int nro_orden;

                        while (sqlite_datareader.Read())
                        {
                            DNI = sqlite_datareader.GetValue(3).ToString();
                            nro_orden = Convert.ToInt32(sqlite_datareader.GetValue(0));

                            var info_to_firebase = new info_firebase
                            {
                                Ap = sqlite_datareader.GetValue(1).ToString(),
                                No = sqlite_datareader.GetValue(2).ToString(),
                                Em = sqlite_datareader.GetValue(25).ToString(),
                                Te = sqlite_datareader.GetValue(4).ToString(),
                                DNI = sqlite_datareader.GetValue(3).ToString(),
                                Do = sqlite_datareader.GetValue(7).ToString(),
                                FN = sqlite_datareader.GetValue(24).ToString(),
                                Cel = Convert.ToBoolean(sqlite_datareader.GetValue(6)),
                                What = Convert.ToBoolean(sqlite_datareader.GetValue(5)),
                                AOIEL = Convert.ToDouble(sqlite_datareader.GetValue(9)),
                                AOICL = Convert.ToDouble(sqlite_datareader.GetValue(10)),
                                EOIL = Convert.ToDouble(sqlite_datareader.GetValue(11)),
                                AODEL = Convert.ToDouble(sqlite_datareader.GetValue(12)),
                                AODCL = Convert.ToDouble(sqlite_datareader.GetValue(13)),
                                EODL = Convert.ToDouble(sqlite_datareader.GetValue(14)),
                                TCL = sqlite_datareader.GetValue(8).ToString(),
                                AOIEC = Convert.ToDouble(sqlite_datareader.GetValue(16)),
                                AOICC = Convert.ToDouble(sqlite_datareader.GetValue(17)),
                                EOIC = Convert.ToDouble(sqlite_datareader.GetValue(18)),
                                AODEC = Convert.ToDouble(sqlite_datareader.GetValue(19)),
                                AODCC = Convert.ToDouble(sqlite_datareader.GetValue(20)),
                                EODC = Convert.ToDouble(sqlite_datareader.GetValue(21)),
                                TCC = sqlite_datareader.GetValue(15).ToString(),
                                Nro_Orden = sqlite_datareader.GetValue(0).ToString(),
                                FRec = sqlite_datareader.GetValue(22).ToString(),           // fecha_recepcion,
                                FPRet = sqlite_datareader.GetValue(23).ToString(),          //fecha_para_retiro,
                                FQRet = ""
                            };

                            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
                            SetResponse response = await client.SetAsync("Optica Falabella Av 38" + "/" + "Clientes" + "/" + DNI + "/" + time, info_to_firebase);

                            Funciones.Functions.SendBroadcastMessage("ACTUALIZAR/" + nro_orden + "/" + DNI);
                        }
                        
                    }
                }

                rdr.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        private void Vendedor_ItemClicked(object sender, EventArgs e)
        {
            Form nuevoVendedor = new NuevoVendedor();
            nuevoVendedor.Show();
            this.Hide();
        }   //  READY

        private void ConsultarVendedor_Clicked(object sender, EventArgs e)
        {
            Form consultarVendedor = new ConsultarVendedores();
            consultarVendedor.Show();
            this.Hide();
        }   //  READY

        private void VerProveedores_Clicked(object sender, EventArgs e)
        {

        }

        private void txt_QR_Read_TextChanged(object sender, EventArgs e)
        {
            if (QR_Text == "")
            {
                QR_Text = txt_QR_Read.Text;
            }
            else
            {
                if ((QR_Text != txt_QR_Read.Text) && (txt_QR_Read.Text != ""))
                {
                    QR_Text = txt_QR_Read.Text;
                }

                Funciones.Functions.ParserQR(QR_Text);
            }
        }

        private void btn_SincronizarPrecios_Clicked(object sender, EventArgs e)
        {
            //  Sincronización de CRISTALES
            FirebaseResponse response = client.Get(@"Optica Falabella Av 38/PRECIOS/CRIS");
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            if (!Funciones.Functions.CheckEmptyTable("TIPOS_CRISTALES"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "insert into TIPOS_CRISTALES (Tipo, Precio) values('" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("FIREBASE/CRISTALES/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "UPDATE TIPOS_CRISTALES SET Precio = '" + data.ElementAt(i).Value + "'" + " WHERE Tipo = '" + data.ElementAt(i).Key + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("FIREBASE/CRISTALES/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }

            //  Sincronización de COMISIONES DE TARJETA
            response = client.Get(@"Optica Falabella Av 38/Comisiones_Tarjeta");
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            if (!Funciones.Functions.CheckEmptyTable("COMISIONES_TARJETA"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "insert into COMISIONES_TARJETA (Tipo, Comision) values ('" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("TARJETA/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "UPDATE COMISIONES_TARJETA SET Comision = '" + data.ElementAt(i).Value + "'" + " WHERE Tipo = '" + data.ElementAt(i).Key + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("TARJETA/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }

            //  Sincronización de Valores de REPARACIONES
            response = client.Get(@"Optica Falabella Av 38/PRECIOS/REPARACIONES");
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            if (!Funciones.Functions.CheckEmptyTable("REPARACIONES"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "insert into REPARACIONES (Tipo, Precio) values ('" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("REPARACIONES/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "UPDATE REPARACIONES SET Precio = '" + data.ElementAt(i).Value + "'" + " WHERE Tipo = '" + data.ElementAt(i).Key + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("REPARACIONES/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }

            //  Sincronización de ARMAZONES DE RECETA
            response = client.Get(@"Optica Falabella Av 38/PRECIOS/AR");
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            if (!Funciones.Functions.CheckEmptyTable("CODIGOS"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "insert into CODIGOS (Tipo_Articulo, Codigo, Precio) values('" + "AR" + "', '" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("FIREBASE/AR/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                bool found = false;
                miConexion.Open();

                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Tipo_Articulo, Codigo, Precio FROM CODIGOS";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {
                        if ((sqlite_datareader.GetValue(1).ToString() == data.ElementAt(i).Key) && (sqlite_datareader.GetValue(0).ToString() == "AR"))
                        {
                            string sql = "UPDATE CODIGOS SET Precio = '" + data.ElementAt(i).Value + "'" + " WHERE Codigo = '" + data.ElementAt(i).Key + "' and Tipo_Articulo = '" + "AR" + "'";
                            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                            command.ExecuteNonQuery();
                            command.Dispose();

                            found = true;
                        }
                    }

                    if(found == false)
                    {
                        string sql = "insert into CODIGOS (Tipo_Articulo, Codigo, Precio) values('" + "AR" + "', '" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    Funciones.Functions.SendBroadcastMessage("FIREBASE/AR/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }

                miConexion.Close();
            }

            //  Sincronización de ARMAZONES DE SOL
            response = client.Get(@"Optica Falabella Av 38/PRECIOS/AS");
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Body.ToString());

            if (!Funciones.Functions.CheckEmptyTable("CODIGOS"))
            {
                // Está vacía la tabla
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    string sql = "insert into CODIGOS (Tipo_Articulo, Codigo, Precio) values('" + "AS" + "', '" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                    miConexion.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("FIREBASE/AS/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }
            }
            else
            {
                bool found = false;
                miConexion.Open();

                // Tiene datos, hay que actualizarlos
                for (int i = 0; i < data.Keys.Count; i++)
                {
                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Tipo_Articulo, Codigo, Precio FROM CODIGOS";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {
                        if ((sqlite_datareader.GetValue(1).ToString() == data.ElementAt(i).Key) && (sqlite_datareader.GetValue(0).ToString() == "AS"))
                        {
                            string sql = "UPDATE CODIGOS SET Precio = '" + data.ElementAt(i).Value + "'" + " WHERE Codigo = '" + data.ElementAt(i).Key + "' and Tipo_Articulo = '" + "AS" + "'";
                            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                            command.ExecuteNonQuery();
                            command.Dispose();

                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        string sql = "insert into CODIGOS (Tipo_Articulo, Codigo, Precio) values('" + "AS" + "', '" + data.ElementAt(i).Key + "', '" + data.ElementAt(i).Value + "')";
                        SQLiteCommand command = new SQLiteCommand(sql, miConexion);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    
                    Funciones.Functions.SendBroadcastMessage("FIREBASE/AS/" + data.ElementAt(i).Key + "/" + data.ElementAt(i).Value);
                }

                miConexion.Close();
            }

            MessageBox.Show("Lista de precios actualizada");
        }

        private void Computadora_ItemClicked(object sender, EventArgs e)
        {
            Form computerForm = new ComputerForm();
            computerForm.Show();
            this.Hide();
        }

        private void btn_VentaDeProductos_Clicked(object sender, EventArgs e)
        {
            Form nuevoVentaDeProducto = new VentaDeProductos();
            nuevoVentaDeProducto.Show();
            this.Hide();
        }

        private void btn_GiftCard_Clicked(object sender, EventArgs e)
        {
            Form nuevoGiftCard = new GiftCard();
            nuevoGiftCard.Show();
            this.Hide();
        }

        private void btn_Retiro_ItemClicked(object sender, EventArgs e)
        {
            Form retiro = new RetiroForm();
            retiro.Show();
            this.Hide();
        }

        private void btn_BajaSTOCK_ItemClicked(object sender, EventArgs e)
        {
            Form bajaStock = new BajaStock();
            bajaStock.Show();
            this.Hide();
        }

        private void btn_CajaDIARIA_ItemClicked(object sender, EventArgs e)
        {
            int row_index = 3;
            string concepto_pago = String.Empty;
            double monto_abonado_efectivo = 0.0f;
            double monto_abonado_debito = 0.0f;
            double monto_abonado_credito = 0.0f;

            object misValue = System.Reflection.Missing.Value;
            string day, month, year, date;

            double original;
            int enteros;
            int decimales;

            if (DateTime.Today.Day < 10)
            {
                day = "0" + DateTime.Today.Day.ToString();
            }
            else
            {
                day = DateTime.Today.Day.ToString();
            }

            if (DateTime.Today.Month < 10)
            {
                month = "0" + DateTime.Today.Month.ToString();
            }
            else
            {
                month = DateTime.Today.Month.ToString();
            }

            year = DateTime.Today.Year.ToString();
            date = day + "/" + month + "/" + year;

            string path = @"C:\Temp\Ventas_" + day + "-" + month + "-" + year + ".xlsx";
         
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Workbook xlWorkBook = xlWorkBook = xlApp.Workbooks.Open(path);
            Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

            var cellValue = (xlWorkSheet.Cells[row_index, 2] as Microsoft.Office.Interop.Excel.Range).Value;

            while (cellValue != null)
            {
                cellValue = (xlWorkSheet.Cells[row_index, 24] as Microsoft.Office.Interop.Excel.Range).Value;

                if(cellValue == "Efectivo")
                {
                    monto_abonado_efectivo += Convert.ToDouble((xlWorkSheet.Cells[row_index, 21] as Microsoft.Office.Interop.Excel.Range).Value);
                }

                if (cellValue == "Debito")
                {
                    monto_abonado_debito += Convert.ToDouble((xlWorkSheet.Cells[row_index, 21] as Microsoft.Office.Interop.Excel.Range).Value);
                }

                if (cellValue == "Credito")
                {
                    monto_abonado_credito += Convert.ToDouble((xlWorkSheet.Cells[row_index, 21] as Microsoft.Office.Interop.Excel.Range).Value);
                }

                row_index++;
            }

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Size = Syncfusion.Pdf.PdfPageSize.A4;
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Portrait;
            Syncfusion.Pdf.PdfPageBase page = document.Pages.Add();

            PdfBitmap image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Logo grande.png");
            page.Graphics.DrawImage(image, 0, 0, 174, 25);

            //  Caja Día
            page.Graphics.DrawString("CAJA DIA: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(250, 7));
            page.Graphics.DrawString(date, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(340, 5));

            //  EFECTIVO
            page.Graphics.DrawString("EFECTIVO: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 45));

            if (monto_abonado_efectivo >= 1000)
            {
                enteros = Convert.ToInt32(monto_abonado_efectivo) / 1000;
                decimales = Convert.ToInt32(monto_abonado_efectivo) - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 43));
                }
                else
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 43));
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + monto_abonado_efectivo.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 43));
            }

            //  DEBITO
            page.Graphics.DrawString("DEBITO: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 65));

            if (monto_abonado_debito >= 1000)
            {
                enteros = Convert.ToInt32(monto_abonado_debito) / 1000;
                decimales = Convert.ToInt32(monto_abonado_debito) - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 63));
                }
                else
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 63));
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + monto_abonado_debito.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 63));
            }

            //  CREDITO
            page.Graphics.DrawString("CREDITO: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 85));

            if (monto_abonado_credito >= 1000)
            {
                enteros = Convert.ToInt32(monto_abonado_credito) / 1000;
                decimales = Convert.ToInt32(monto_abonado_credito) - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 83));
                }
                else
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 83));
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + monto_abonado_credito.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 83));
            }

            document.Save("C:\\Temp\\CajaDiaria_" + date.Replace("/", "-") + ".pdf");
            document.Close(true);

            path = "C:\\Temp\\CajaDiaria_" + date.Replace("/", "-") + ".pdf";

            Funciones.Functions.PrintDocument(path);
        }

        private void btn_InsertarIPs_ItemClicked(object sender, EventArgs e)
        {
            Form nuevoIP = new IPS();
            nuevoIP.Show();
            this.Hide();
        }
    }

    internal class DatosAR
    {
        public string Cant { get; set; }
        public string Cod { get; set; }
        public string Material { get; set; }
        public string Mod { get; set; }
        public string Precio { get; set; }
    }

    internal class DatosLiq
    {
        public string Cant { get; set; }
        public string Cod { get; set; }
        public string Precio { get; set; }
        public string Uni_Tam { get; set; }
        public int Tam { get; set; }

    }

    internal class DatosCristales
    {
        public string Cant { get; set; }
        public string Cod { get; set; }
    }
}







/*
            //  Revision de STOCK de CRISTALES

            FirebaseResponse response1 = client.Get(@"Optica Falabella Av 38/Stock/Cristales");
            Dictionary<string, DatosCristales> data1 = JsonConvert.DeserializeObject<Dictionary<string, DatosCristales>>(response1.Body.ToString());

            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            foreach (var item in data1)
            {
                response1 = client.Get(@"Optica Falabella Av 38/Stock/Cristales/" + item.Key);
                data1 = JsonConvert.DeserializeObject<Dictionary<string, DatosCristales>>(response1.Body.ToString());
                foreach (var item2 in data1)
                {
                    response1 = client.Get(@"Optica Falabella Av 38/Stock/Cristales/" + item.Key + "/" + item2.Key);
                    data1 = JsonConvert.DeserializeObject<Dictionary<string, DatosCristales>>(response1.Body.ToString());

                    foreach(var item3 in data1)
                    {
                        response1 = client.Get(@"Optica Falabella Av 38/Stock/Cristales/" + item.Key + "/" + item2.Key + "/" + item3.Key);
                        DatosCristales obj = response1.ResultAs<DatosCristales>();

                        var datos = new DatosCristales
                        {
                            Cant = obj.Cant.ToString(),
                            Cod = obj.Cod.ToString(),
                            //Esferico = item3.Key.Replace(",", "."),
                            //Cilindrico = item2.Key.Replace(",", ".")
                        };

                        //double esf = Convert.ToDouble(datos.Esferico)/100;
                        //double cil = Convert.ToDouble(datos.Cilindrico)/100;

                        if (!Funciones.Functions.CheckEmptyTable("STOCK_CRISTALES"))
                        {
                            //  STOCK_CRISTALES (Tipo varchar(30), Cantidad_Disponible int, Esferico real, Cilindrico real, Codigo varchar(30))

                            string sql = "insert into STOCK_CRISTALES (Tipo, Cantidad_Disponible, Esferico, Cilindrico, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(datos.Cant) + "', '" + esf + "', '" + cil + "', '" + datos.Cod + "')";
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            miConexion.Open();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                        else
                        {
                            //  Chequeo que el número de código no esté en la base de datos. 
                            //  Si no está, lo inserto
                            //  Si llegara a estar en la base de datos lo actualizo

                            miConexion.Open();

                            SQLiteDataReader sqlite_datareader;
                            SQLiteCommand sqlite_cmd;
                            sqlite_cmd = miConexion.CreateCommand();
                            sqlite_cmd.CommandText = "SELECT Codigo FROM STOCK_CRISTALES";
                            sqlite_cmd.CommandType = System.Data.CommandType.Text;

                            sqlite_datareader = sqlite_cmd.ExecuteReader();

                            while (sqlite_datareader.Read())
                            {
                                if (sqlite_datareader.GetValue(0).ToString() == obj.Cod.ToString())
                                {
                                //  El codigo está en la base de datos. Lo actualizo

                                    using (SQLiteCommand command2 = new SQLiteCommand(miConexion))
                                    {
                                        command2.CommandText = "DELETE FROM STOCK_CRISTALES WHERE Codigo='" + obj.Cod.ToString() + "'";
                                        command2.ExecuteNonQuery();

                                        command2.CommandText = "insert into STOCK_CRISTALES (Tipo, Cantidad_Disponible, Esferico, Cilindrico, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(datos.Cant) + "', '" + esf + "', '" + cil + "', '" + datos.Cod + "')";
                                        command2.ExecuteNonQuery();
                                        command2.Dispose();

                                        //Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + item.Key.ToString() + "/" + Convert.ToInt32(datos.Cant) + "/" + esf + "/" + cil + "/" + datos.Cod);
                                    }
                                }
                                else
                                {
                                    using (SQLiteCommand command3 = new SQLiteCommand(miConexion))
                                    {
                                        command3.CommandText = "insert into STOCK_CRISTALES (Tipo, Cantidad_Disponible, Esferico, Cilindrico, Codigo) values('" + item.Key.ToString() + "', '" + Convert.ToInt32(datos.Cant) + "', '" + esf + "', '" + cil + "', '" + datos.Cod + "')";
                                        command3.ExecuteNonQuery();
                                        command3.Dispose();

                                        //Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + item.Key.ToString() + "/" + Convert.ToInt32(datos.Cant) + "/" + esf + "/" + cil + "/" + datos.Cod);
                                    }
                                } 
                            }

                            miConexion.Close();
                        }

                        Funciones.Functions.SendBroadcastMessage("STOCK/CRISTALES/" + item.Key.ToString() + "/" + Convert.ToInt32(datos.Cant) + "/" + esf + "/" + cil + "/" + datos.Cod);
                    }  
                }
            }
            */
