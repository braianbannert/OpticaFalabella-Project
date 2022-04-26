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

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class NuevoTrabajoForm : Form
    {
        public double subtotal_Excel = 0.0f;
        public double descuento_Excel = 0.0f;
        public double seña_Excel = 0.0f;
        public double resta_abonar_Excel = 0.0f;

        public int valor_GiftCard = 0;

        public string tipo_descuento;
        public System.Windows.Forms.Button printButton;
        public System.Drawing.Font printFont;
        public StreamReader streamToPrint;

        string previus_readQR = "";
        string telefono_client = "";
        bool whatsapp = false;
        bool internet_connection = false;
        bool subido_a_firebase = false;

        public SQLiteConnection miConexion;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public NuevoTrabajoForm()
        {
            InitializeComponent();
        }

        private void NuevoTrabajoForm_Load(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox_Descuento.Visible = false;

            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            txt_DNI1.Focus();
            combo_Cuotas.Visible = false;
            txt_NroGiftCard.Visible = false;

            txt_Descuento.Enabled = true;
            txt_Descuento.Visible = true;

            cmb_Concepto.Enabled = true;
            cmb_Concepto.Visible = true;

            if (Data.DataTrabajos.PROCEDENCIA == "BUSQUEDA")
            {
                SQLiteDataReader sqlite_datareader2;
                SQLiteCommand sqlite_cmd2;
                sqlite_cmd2 = miConexion.CreateCommand();
                sqlite_cmd2.CommandText = "SELECT Tipo, Precio FROM TIPOS_CRISTALES";
                sqlite_cmd2.CommandType = System.Data.CommandType.Text;

                if (Data.DataTrabajos.anteojo_lejos == true)
                {
                    checkBox2.Checked = true;

                    txt_EsfericoOIL.Enabled = true;
                    txt_CilindricoOIL.Enabled = true;
                    txt_EjeOIL.Enabled = true;

                    txt_EsfericoODL.Enabled = true;
                    txt_CilindricoODL.Enabled = true;
                    txt_EjeODL.Enabled = true;

                    txt_CristalLejos.Enabled = true;
                    txt_ArmazonLejos.Enabled = true;
                    comboBox_CristalLejos.Enabled = true;

                    txt_EsfericoOIL.Text = Data.DataTrabajos.AOIEL.ToString();
                    txt_CilindricoOIL.Text = Data.DataTrabajos.AOICL.ToString();
                    txt_EjeOIL.Text = Data.DataTrabajos.Eje_OIL.ToString();

                    txt_EsfericoODL.Text = Data.DataTrabajos.AODEL.ToString();
                    txt_CilindricoODL.Text = Data.DataTrabajos.AODCL.ToString();
                    txt_EjeODL.Text = Data.DataTrabajos.Eje_ODL.ToString();

                    comboBox_CristalLejos.Text = Data.DataTrabajos.Tipo_Cristal_Lejos.ToString();

                    miConexion.Open();                   

                    sqlite_datareader2 = sqlite_cmd2.ExecuteReader();

                    while (sqlite_datareader2.Read())
                    {
                        if (sqlite_datareader2.GetValue(0).ToString() == comboBox_CristalLejos.Text)
                        {
                            txt_CristalLejos.Text = sqlite_datareader2.GetValue(1).ToString();
                        }
                    }

                    miConexion.Close();
                }
                else
                {
                    checkBox2.Checked = false;

                    txt_EsfericoOIL.Enabled = false;
                    txt_CilindricoOIL.Enabled = false;
                    txt_EjeOIL.Enabled = false;

                    txt_EsfericoODL.Enabled = false;
                    txt_CilindricoODL.Enabled = false;
                    txt_EjeODL.Enabled = false;

                    txt_CristalLejos.Enabled = false;
                    txt_ArmazonLejos.Enabled = false;
                    comboBox_CristalLejos.Enabled = false;
                }

                if(Data.DataTrabajos.anteojo_cerca == true)
                {
                    checkBox3.Checked = true;

                    txt_EsfericoOIC.Enabled = true;
                    txt_CilindricoOIC.Enabled = true;
                    txt_EjeOIC.Enabled = true;

                    txt_EsfericoODC.Enabled = true;
                    txt_CilindricoODC.Enabled = true;
                    txt_EjeODC.Enabled = true;

                    txt_CristalCerca.Enabled = true;
                    txt_ArmazonCerca.Enabled = true;
                    comboBox_CristalCerca.Enabled = true;

                    txt_EsfericoOIC.Text = Data.DataTrabajos.AOIEC.ToString();
                    txt_CilindricoOIC.Text = Data.DataTrabajos.AOICC.ToString();
                    txt_EjeOIC.Text = Data.DataTrabajos.Eje_OIC.ToString();

                    txt_EsfericoODC.Text = Data.DataTrabajos.AODEC.ToString();
                    txt_CilindricoODC.Text = Data.DataTrabajos.AODCC.ToString();
                    txt_EjeODC.Text = Data.DataTrabajos.Eje_ODC.ToString();

                    comboBox_CristalCerca.Text = Data.DataTrabajos.Tipo_Cristal_Cerca.ToString();

                    miConexion.Open();

                    sqlite_datareader2 = sqlite_cmd2.ExecuteReader();

                    while (sqlite_datareader2.Read())
                    {
                        if (sqlite_datareader2.GetValue(0).ToString() == comboBox_CristalCerca.Text)
                        {
                            txt_CristalCerca.Text = sqlite_datareader2.GetValue(1).ToString();
                        }
                    }

                    miConexion.Close();
                }
                else
                {
                    checkBox3.Checked = false;

                    txt_EsfericoOIC.Enabled = false;
                    txt_CilindricoOIC.Enabled = false;
                    txt_EjeOIC.Enabled = false;

                    txt_EsfericoODC.Enabled = false;
                    txt_CilindricoODC.Enabled = false;
                    txt_EjeODC.Enabled = false;

                    txt_CristalCerca.Enabled = false;
                    txt_ArmazonCerca.Enabled = false;
                    comboBox_CristalCerca.Enabled = false;
                }

                txt_ApellidoCliente1.Text = Data.DataTrabajos.Apellido_Cliente;
                txt_NombreCliente1.Text = Data.DataTrabajos.Nombre_Cliente;
                txt_DNI1.Text = Data.DataTrabajos.DNI_Cliente;

                int cant_leidos = 0;
                string[] telefono = new string[2];
                string[] fecha = new string[3];

                miConexion.Open();

                SQLiteDataReader sqlite_datareader1;
                SQLiteCommand sqlite_cmd1;
                sqlite_cmd1 = miConexion.CreateCommand();
                sqlite_cmd1.CommandText = "SELECT DNI, Apellido, Nombre, Telefono, Email, Domicilio, Fecha_Nac, WhatsApp, Celular FROM CLIENTES";
                sqlite_cmd1.CommandType = System.Data.CommandType.Text;

                sqlite_datareader1 = sqlite_cmd1.ExecuteReader();

                while (sqlite_datareader1.Read())
                {
                    if (sqlite_datareader1.GetValue(0).ToString() == txt_DNI1.Text)
                    {
                        // Debo traer todos los datos del cliente
                        //  Nombre varchar(30), Apellido varchar(30), DNI varchar(12), Telefono varchar(20), Email varchar(50), Domicilio varchar(50)
                        // SELECT Nombre, Apellido, Telefono, Email, Domicilio FROM CLIENTES WHERE DNI = '" + txt_DNI1.Text + "'

                        txt_ApellidoCliente1.Text = sqlite_datareader1.GetValue(1).ToString();
                        txt_NombreCliente1.Text = sqlite_datareader1.GetValue(2).ToString();
                        txt_DomicilioCliente.Text = sqlite_datareader1.GetValue(5).ToString();
                        telefono = sqlite_datareader1.GetValue(3).ToString().Split("-");
                        txt_EmailCliente.Text = sqlite_datareader1.GetValue(4).ToString();
                        fecha = sqlite_datareader1.GetValue(6).ToString().Split("-");
                        checkBox1.Checked = Convert.ToBoolean(sqlite_datareader1.GetValue(7));
                        ckbox_Celular.Checked = Convert.ToBoolean(sqlite_datareader1.GetValue(8));
                        cant_leidos++;
                    }
                }

                miConexion.Close();

                if (cant_leidos > 0)
                {
                    txt_TelPrefijo.Text = telefono[0].ToString();
                    txt_TelResto.Text = telefono[1].ToString();
                    dt_FechaNacimiento.Value = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                }
            }
            else
            {
                txt_EsfericoOIL.Enabled = false;
                txt_CilindricoOIL.Enabled = false;
                txt_EjeOIL.Enabled = false;

                txt_EsfericoODL.Enabled = false;
                txt_CilindricoODL.Enabled = false;
                txt_EjeODL.Enabled = false;

                txt_CristalLejos.Enabled = false;
                txt_ArmazonLejos.Enabled = false;
                comboBox_CristalLejos.Enabled = false;

                txt_EsfericoOIC.Enabled = false;
                txt_CilindricoOIC.Enabled = false;
                txt_EjeOIC.Enabled = false;

                txt_EsfericoODC.Enabled = false;
                txt_CilindricoODC.Enabled = false;
                txt_EjeODC.Enabled = false;

                txt_CristalCerca.Enabled = false;
                txt_ArmazonCerca.Enabled = false;
                comboBox_CristalCerca.Enabled = false;

                txt_ApellidoCliente1.Text = "";
                txt_NombreCliente1.Text = "";
                txt_DNI1.Text = "";
                txt_DomicilioCliente.Text = "";

                lbl_FechaRetiro.Text = "";

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;

            }

            label49.Visible = true;
            comboBox_Vendedor.Visible = true;

            client = new FireSharp.FirebaseClient(config);
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            dtp_FechaRecepcion.Enabled = false;
            dtp_FechaRecepcion.MinDate = DateTime.Today;

            dtp_FechaEntrega.MinDate = DateTime.Today;
            
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Tipo FROM TIPOS_CRISTALES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                comboBox_CristalLejos.Items.Add(sqlite_datareader.GetValue(0));
                comboBox_CristalCerca.Items.Add(sqlite_datareader.GetValue(0));
            }

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT NRO FROM NRO_ORDEN";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Data.DataTrabajos.Nro_Orden = Convert.ToInt32(sqlite_datareader.GetValue(0));
            }

            lbl_NroOrden1.Text = Data.DataTrabajos.Nro_Orden.ToString();

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nombre FROM VENDEDORES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                comboBox_Vendedor.Items.Add(sqlite_datareader.GetValue(0));
            }

            miConexion.Close();
        }

        private async void btn_Imprimir_Click(object sender, EventArgs e)
        {
            double valorFinal = 0.0f;
            double subtotal_final = 0.0f;
            double totalizado = 0.0f;

            if ((chk_Credito.Checked == false) && (chk_Debito.Checked == false) && (chk_Efectivo.Checked == false))
            {
                MessageBox.Show("Debe ingresar un método de pago.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                Data.Variables_Globales.AOIEL = 0.0F;
                Data.Variables_Globales.AOICL = 0.0F;
                Data.Variables_Globales.Eje_OIL = 0.0F;

                Data.Variables_Globales.AODEL = 0.0F;
                Data.Variables_Globales.AODCL = 0.0F;
                Data.Variables_Globales.Eje_ODL = 0.0F;

                Data.Variables_Globales.AOIEC = 0.0F;
                Data.Variables_Globales.AOICC = 0.0F;
                Data.Variables_Globales.Eje_OIC = 0.0F;

                Data.Variables_Globales.AODEC = 0.0F;
                Data.Variables_Globales.AODCC = 0.0F;
                Data.Variables_Globales.Eje_ODC = 0.0F;

                btn_Imprimir.Visible = false;
                btn_Borrar.Visible = false;

                txt_ApellidoCliente1.Text = txt_ApellidoCliente1.Text.ToUpper();
                txt_NombreCliente1.Text = txt_NombreCliente1.Text.ToUpper();

                Data.DataTrabajos.Apellido_Cliente = txt_ApellidoCliente1.Text;
                Data.DataTrabajos.Nombre_Cliente = txt_NombreCliente1.Text;
                Data.DataTrabajos.DNI_Cliente = txt_DNI1.Text;

                telefono_client = txt_TelPrefijo.Text + "-" + txt_TelResto.Text;

                if (checkBox2.Checked == true)
                {
                    if (txt_CilindricoODL.Text != "")
                    {
                        if (txt_CilindricoODL.Text.Contains("."))
                        {
                            Data.Variables_Globales.AODCL = Convert.ToDouble(txt_CilindricoODL.Text);
                            Data.Variables_Globales.AODCL = Data.Variables_Globales.AODCL / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AODCL = Convert.ToDouble(txt_CilindricoODL.Text);
                        }
                    }

                    if (txt_CilindricoOIL.Text != "")
                    {
                        if (txt_CilindricoOIL.Text.Contains("."))
                        {
                            Data.Variables_Globales.AOICL = Convert.ToDouble(txt_CilindricoOIL.Text);
                            Data.Variables_Globales.AOICL = Data.Variables_Globales.AOICL / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AOICL = Convert.ToDouble(txt_CilindricoOIL.Text);
                        }
                    }

                    if (txt_EsfericoODL.Text != "")
                    {
                        if (txt_EsfericoODL.Text.Contains("."))
                        {
                            Data.Variables_Globales.AODEL = Convert.ToDouble(txt_EsfericoODL.Text);
                            Data.Variables_Globales.AODEL = Data.Variables_Globales.AODEL / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AODEL = Convert.ToDouble(txt_EsfericoODL.Text);
                        }
                    }

                    if (txt_EsfericoOIL.Text != "")
                    {
                        if (txt_EsfericoOIL.Text.Contains("."))
                        {
                            Data.Variables_Globales.AOIEL = Convert.ToDouble(txt_EsfericoOIL.Text);
                            Data.Variables_Globales.AOIEL = Data.Variables_Globales.AOIEL / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AOIEL = Convert.ToDouble(txt_EsfericoOIL.Text);
                        }
                    }
                }   //  Revisamos que esté habilitado el campo de LEJOS

                if (checkBox3.Checked == true)
                {
                    if (txt_CilindricoODC.Text != "")
                    {
                        if (txt_CilindricoODC.Text.Contains("."))
                        {
                            Data.Variables_Globales.AODCC = Convert.ToDouble(txt_CilindricoODC.Text);
                            Data.Variables_Globales.AODCC = Data.Variables_Globales.AODCC / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AODCC = Convert.ToDouble(txt_CilindricoODC.Text);
                        }
                    }

                    if (txt_CilindricoOIC.Text != "")
                    {
                        if (txt_CilindricoOIC.Text.Contains("."))
                        {
                            Data.Variables_Globales.AOICC = Convert.ToDouble(txt_CilindricoOIC.Text);
                            Data.Variables_Globales.AOICC = Data.Variables_Globales.AOICC / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AOICC = Convert.ToDouble(txt_CilindricoOIC.Text);
                        }
                    }

                    if (txt_EsfericoODC.Text != "")
                    {
                        if (txt_EsfericoODC.Text.Contains("."))
                        {
                            Data.Variables_Globales.AODEC = Convert.ToDouble(txt_EsfericoODC.Text);
                            Data.Variables_Globales.AODEC = Data.Variables_Globales.AODEC / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AODEC = Convert.ToDouble(txt_EsfericoODC.Text);
                        }
                    }

                    if (txt_EsfericoOIC.Text != "")
                    {
                        if (txt_EsfericoOIC.Text.Contains("."))
                        {
                            Data.Variables_Globales.AOIEC = Convert.ToDouble(txt_EsfericoOIC.Text);
                            Data.Variables_Globales.AOIEC = Data.Variables_Globales.AOIEC / 100;
                        }
                        else
                        {
                            Data.Variables_Globales.AOIEC = Convert.ToDouble(txt_EsfericoOIC.Text);
                        }
                    }
                }   //  Revisamos que esté habilitado el campo de CERCA

                Data.Variables_Globales.Inv_AOIEL = Data.Variables_Globales.AOIEL + Data.Variables_Globales.AOICL;
                Data.Variables_Globales.Inv_AOICL = (-1) * Data.Variables_Globales.AOICL;

                Data.Variables_Globales.Inv_AODEL = Data.Variables_Globales.AODEL + Data.Variables_Globales.AODCL;
                Data.Variables_Globales.Inv_AODCL = (-1) * Data.Variables_Globales.AODCL;

                Data.Variables_Globales.Inv_AOIEC = Data.Variables_Globales.AOIEC + Data.Variables_Globales.AOICC;
                Data.Variables_Globales.Inv_AOICC = (-1) * Data.Variables_Globales.AOICC;

                Data.Variables_Globales.Inv_AODEC = Data.Variables_Globales.AODEC + Data.Variables_Globales.AODCC;
                Data.Variables_Globales.Inv_AODCC = (-1) * Data.Variables_Globales.AODCC;

                string day;
                string month;
                string fecha_nac;
                string fecha_recepcion;
                string fecha_para_retiro;

                if (dt_FechaNacimiento.Value.Day < 10)
                {
                    day = "0" + dt_FechaNacimiento.Value.Day.ToString();
                }
                else
                {
                    day = dt_FechaNacimiento.Value.Day.ToString();
                }

                if (dt_FechaNacimiento.Value.Month < 10)
                {
                    month = "0" + dt_FechaNacimiento.Value.Month.ToString();
                }
                else
                {
                    month = dt_FechaNacimiento.Value.Month.ToString();
                }

                fecha_nac = day + "-" + month + "-" + dt_FechaNacimiento.Value.Year;

                if (dtp_FechaRecepcion.Value.Day < 10)
                {
                    day = "0" + dtp_FechaRecepcion.Value.Day.ToString();
                }
                else
                {
                    day = dtp_FechaRecepcion.Value.Day.ToString();
                }

                if (dtp_FechaRecepcion.Value.Month < 10)
                {
                    month = "0" + dtp_FechaRecepcion.Value.Month.ToString();
                }
                else
                {
                    month = dtp_FechaRecepcion.Value.Month.ToString();
                }

                fecha_recepcion = day + "-" + month + "-" + dtp_FechaRecepcion.Value.Year;

                if (dtp_FechaEntrega.Value.Day < 10)
                {
                    day = "0" + dtp_FechaEntrega.Value.Day.ToString();
                }
                else
                {
                    day = dtp_FechaEntrega.Value.Day.ToString();
                }

                if (dtp_FechaEntrega.Value.Month < 10)
                {
                    month = "0" + dtp_FechaEntrega.Value.Month.ToString();
                }
                else
                {
                    month = dtp_FechaEntrega.Value.Month.ToString();
                }

                fecha_para_retiro = day + "-" + month + "-" + dtp_FechaEntrega.Value.Year;

                string aux = "";

                if (txt_EsfericoOIL.Text != String.Empty)
                {
                    if (txt_EsfericoOIL.Text.Contains("."))
                    {
                        txt_EsfericoOIL.Text.Replace(".", ",");
                    }
                }

                if (txt_CilindricoOIL.Text != String.Empty)
                {
                    if (txt_CilindricoOIL.Text.Contains("."))
                    {
                        txt_CilindricoOIL.Text.Replace(".", ",");
                    }
                }

                if (txt_EsfericoODL.Text != String.Empty)
                {
                    if (txt_EsfericoODL.Text.Contains("."))
                    {
                        txt_EsfericoODL.Text.Replace(".", ",");
                    }
                }

                if (txt_CilindricoODL.Text != String.Empty)
                {
                    if (txt_CilindricoODL.Text.Contains("."))
                    {
                        txt_CilindricoODL.Text.Replace(".", ",");
                    }
                }

                if (txt_EsfericoOIC.Text != String.Empty)
                {
                    if (txt_EsfericoOIC.Text.Contains("."))
                    {
                        txt_EsfericoOIC.Text.Replace(".", ",");
                    }
                }

                if (txt_CilindricoOIC.Text != String.Empty)
                {
                    if (txt_CilindricoOIC.Text.Contains("."))
                    {
                        txt_CilindricoOIC.Text.Replace(".", ",");
                    }
                }

                if (txt_EsfericoODC.Text != String.Empty)
                {
                    if (txt_EsfericoODC.Text.Contains("."))
                    {
                        txt_EsfericoODC.Text.Replace(".", ",");
                    }
                }

                if (txt_CilindricoODC.Text != String.Empty)
                {
                    if (txt_CilindricoODC.Text.Contains("."))
                    {
                        txt_CilindricoODC.Text.Replace(".", ",");
                    }
                }

                if (txt_EjeOIL.Text != string.Empty)
                {
                    if (txt_EjeOIL.Text.Contains("."))
                    {
                        aux = txt_EjeOIL.Text;
                        aux = aux.Replace(".", ",");
                        txt_EjeOIL.Text = aux;
                    }
                }
                else
                {
                    txt_EjeOIL.Text = "0";
                }

                if (txt_EjeODL.Text != string.Empty)
                {
                    if (txt_EjeODL.Text.Contains("."))
                    {
                        aux = txt_EjeODL.Text;
                        aux = aux.Replace(".", ",");
                        txt_EjeODL.Text = aux;
                    }
                }
                else
                {
                    txt_EjeODL.Text = "0";
                }

                if (txt_EjeOIC.Text != string.Empty)
                {
                    if (txt_EjeOIC.Text.Contains("."))
                    {
                        aux = txt_EjeOIC.Text;
                        aux = aux.Replace(".", ",");
                        txt_EjeOIC.Text = aux;
                    }
                }
                else
                {
                    txt_EjeOIC.Text = "0";
                }

                if (txt_EjeODC.Text != string.Empty)
                {
                    if (txt_EjeODC.Text.Contains("."))
                    {
                        aux = txt_EjeODC.Text;
                        aux = aux.Replace(".", ",");
                        txt_EjeODC.Text = aux;
                    }
                }
                else
                {
                    txt_EjeODC.Text = "0";
                }

                var info_to_firebase = new info_firebase
                {
                    Ap = txt_ApellidoCliente1.Text,
                    No = txt_NombreCliente1.Text,
                    Em = txt_EmailCliente.Text,
                    Te = telefono_client,
                    DNI = txt_DNI1.Text,
                    Do = txt_DomicilioCliente.Text,
                    FN = fecha_nac,
                    Cel = ckbox_Celular.Checked,
                    What = whatsapp,
                    AOIEL = Data.Variables_Globales.AOIEL,
                    AOICL = Data.Variables_Globales.AOICL,
                    EOIL = Convert.ToDouble(txt_EjeOIL.Text),
                    AODEL = Data.Variables_Globales.AODEL,
                    AODCL = Data.Variables_Globales.AODCL,
                    EODL = Convert.ToDouble(txt_EjeODL.Text),
                    TCL = comboBox_CristalLejos.Text,
                    AL = lbl_ArmazonLejos.Text,
                    AOIEC = Data.Variables_Globales.AOIEC,
                    AOICC = Data.Variables_Globales.AOICC,
                    EOIC = Convert.ToDouble(txt_EjeOIC.Text),
                    AODEC = Data.Variables_Globales.AODEC,
                    AODCC = Data.Variables_Globales.AODCC,
                    EODC = Convert.ToDouble(txt_EjeODC.Text),
                    TCC = comboBox_CristalCerca.Text,
                    AC = lbl_ArmazonCerca.Text,
                    Nro_Orden = lbl_NroOrden1.Text,
                    FRec = fecha_recepcion,
                    FPRet = fecha_para_retiro,
                    FQRet = "",
                };

                if (txt_Descuento.Text.Contains('.'))
                {
                    txt_Descuento.Text.Replace('.', ',');
                }

                internet_connection = Funciones.Functions.CheckInternetConnection();
                if (internet_connection)
                {
                    SetResponse response = await client.SetAsync("Optica Falabella Av 38" + "/" + "Clientes" + "/" + txt_DNI1.Text + "/" + info_to_firebase.Nro_Orden, info_to_firebase);
                    subido_a_firebase = true;

                    //  Tomamos los datos de la óptica para el armado del archivo que se va a imprimir
                    Funciones.Functions.Get_Info_Optica();

                    Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument();
                    document.PageSettings.Size = Syncfusion.Pdf.PdfPageSize.A4;
                    document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Portrait;
                    Syncfusion.Pdf.PdfPageBase page = document.Pages.Add();
                    PdfQRBarcode qRBarcode = new PdfQRBarcode();

                    qRBarcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
                    qRBarcode.XDimension = 2.5F;

                    Data.DataTrabajos.DNI_Cliente = txt_DNI1.Text;
                    Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text);
                    Data.DataTrabajos.Nombre_Cliente = txt_NombreCliente1.Text;
                    Data.DataTrabajos.Apellido_Cliente = txt_ApellidoCliente1.Text;
                    Data.DataTrabajos.Telefono_Cliente = txt_TelPrefijo.Text + txt_TelResto.Text;
                    Data.DataTrabajos.WhatsApp = whatsapp;

                    //  REVISAR INFO DE QR
                    qRBarcode.Text = "#QRCode-Pedido," + Data.DataTrabajos.DNI_Cliente + "*" + Data.DataTrabajos.Nro_Orden;
                    qRBarcode.Draw(page, new PointF(280, 325));
                    //page.Graphics.DrawString(txt_NombreCliente1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 0));

                    Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text);

                    PdfBitmap image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Logo grande.png");
                    page.Graphics.DrawImage(image, 0, 0, 174, 25);

                    //  Nombre Optica Talón
                    //page.Graphics.DrawString(Data.Data_Optica.Nombre, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 0));

                    //  Numero de ORDEN
                    page.Graphics.DrawString("Nº de Orden: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(250, 7));
                    page.Graphics.DrawString(lbl_NroOrden1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(340, 5));

                    //  APELLIDO DEL CLIENTE
                    page.Graphics.DrawString("Apellido: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 38));
                    page.Graphics.DrawString(txt_ApellidoCliente1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 38));

                    //  NOMBRE DEL CLIENTE
                    page.Graphics.DrawString("Nombre: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 55));
                    page.Graphics.DrawString(txt_NombreCliente1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 55));

                    //  DNI DEL CLIENTE
                    page.Graphics.DrawString("DNI: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(250, 38));

                    string dni_original = txt_DNI1.Text;
                    if (txt_DNI1.TextLength == 8)
                    {
                        dni_original = dni_original.Insert(2, ".");
                        dni_original = dni_original.Insert(6, ".");
                    }
                    else
                    {
                        if (txt_DNI1.TextLength == 7)
                        {
                            dni_original = dni_original.Insert(1, ".");
                            dni_original = dni_original.Insert(5, ".");
                        }
                    }

                    page.Graphics.DrawString(dni_original, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(280, 38));

                    //  Email DEL CLIENTE
                    page.Graphics.DrawString("Email: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 72));
                    page.Graphics.DrawString(txt_EmailCliente.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 72));
                    
                    //  VENDEDOR
                    page.Graphics.DrawString("Vendedor: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(250, 72));
                    page.Graphics.DrawString(comboBox_Vendedor.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 72));

                    //  Telefono DEL CLIENTE
                    page.Graphics.DrawString("Teléfono: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 89));
                    page.Graphics.DrawString(txt_TelPrefijo.Text + "-" + txt_TelResto.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 89));

                    //  Titulos Esferico y Cilindro
                    page.Graphics.DrawString("   Esférico          Cilindro            Eje", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(80, 108));

                    PdfPen pen = new PdfPen(PdfBrushes.Black, 1f);

                    //Create the rectangle points
                    PointF point1 = new PointF(0, 126);
                    PointF point2 = new PointF(480, 126);
                    PointF point3 = new PointF(480, 195);
                    PointF point4 = new PointF(0, 195);

                    //Draw the rectangle on PDF document
                    page.Graphics.DrawLine(pen, point1, point2);
                    page.Graphics.DrawLine(pen, point2, point3);
                    page.Graphics.DrawLine(pen, point3, point4);
                    page.Graphics.DrawLine(pen, point4, point1);

                    //  Vision de LEJOS AUMENTOS
                    page.Graphics.DrawString("VL", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(15, 144));
                    page.Graphics.DrawString("OI", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 153));
                    page.Graphics.DrawString(txt_EsfericoOIL.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(98, 153));
                    page.Graphics.DrawString(txt_CilindricoOIL.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(166, 153));
                    page.Graphics.DrawString(txt_EjeOIL.Text + "º", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(234, 153));
                    page.Graphics.DrawString("OD", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 135));
                    page.Graphics.DrawString(txt_EsfericoODL.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(98, 135));
                    page.Graphics.DrawString(txt_CilindricoODL.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(166, 135));
                    page.Graphics.DrawString(txt_EjeODL.Text + "º", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(234, 135));
                    page.Graphics.DrawString("Cristal", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(300, 108));
                    page.Graphics.DrawString(comboBox_CristalLejos.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(280, 144));
                    page.Graphics.DrawString("Precio", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(410, 108));
                    page.Graphics.DrawString("$ " + txt_CristalLejos.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(410, 144));

                    //  Armazon Lejos
                    page.Graphics.DrawString("Armazón: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 181));
                    page.Graphics.DrawString(lbl_ArmazonLejos.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(70, 181));
                    page.Graphics.DrawString("$ " + txt_ArmazonLejos.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(410, 181));

                    page.Graphics.DrawString("   Esférico          Cilindro            Eje", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(80, 207));

                    //Create the rectangle points
                    PointF point5 = new PointF(0, 225);
                    PointF point6 = new PointF(480, 225);
                    PointF point7 = new PointF(480, 294);
                    PointF point8 = new PointF(0, 294);

                    //Draw the rectangle on PDF document
                    page.Graphics.DrawLine(pen, point5, point6);
                    page.Graphics.DrawLine(pen, point6, point7);
                    page.Graphics.DrawLine(pen, point7, point8);
                    page.Graphics.DrawLine(pen, point8, point5);

                    //  Vision de CERCA AUMENTOS
                    page.Graphics.DrawString("VP", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(15, 243));
                    page.Graphics.DrawString("OI", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 252));
                    page.Graphics.DrawString(txt_EsfericoOIC.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(98, 252));
                    page.Graphics.DrawString(txt_CilindricoOIC.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(166, 252));
                    page.Graphics.DrawString(txt_EjeOIC.Text + "º", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(234, 252));
                    page.Graphics.DrawString("OD", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 234));
                    page.Graphics.DrawString(txt_EsfericoODC.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(98, 234));
                    page.Graphics.DrawString(txt_CilindricoODC.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(166, 234));
                    page.Graphics.DrawString(txt_EjeODC.Text + "º", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(234, 234));
                    page.Graphics.DrawString("Cristal", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(300, 207));
                    page.Graphics.DrawString(comboBox_CristalCerca.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(280, 243));
                    page.Graphics.DrawString("Precio", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(410, 108));
                    page.Graphics.DrawString("$ " + txt_CristalCerca.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(410, 243));

                    //  Armazon Cerca
                    page.Graphics.DrawString("Armazón: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 280));
                    page.Graphics.DrawString(lbl_ArmazonCerca.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(70, 280));
                    page.Graphics.DrawString("$ " + txt_ArmazonCerca.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(410, 280));

                    //  Fecha de RECEPCION
                    string fecha_rec = fecha_recepcion.Replace("-", "/");
                    page.Graphics.DrawString("Fecha de Recepción: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 305));
                    page.Graphics.DrawString(fecha_rec, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(160, 303));

                    //  Fecha de ENTREGA
                    string fecha_entrega = fecha_para_retiro.Replace("-", "/");
                    page.Graphics.DrawString("Fecha de Entrega: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 330));
                    page.Graphics.DrawString(fecha_entrega, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(160, 328));

                    double original;
                    int enteros;
                    int decimales;
                    double descuento;

                    //Create the rectangle points
                    PointF point9 = new PointF(0, 355);
                    PointF point10 = new PointF(240, 355);
                    PointF point11 = new PointF(240, 425);
                    PointF point12 = new PointF(0, 425);

                    //Draw the rectangle on PDF document
                    page.Graphics.DrawLine(pen, point9, point10);
                    page.Graphics.DrawLine(pen, point10, point11);
                    page.Graphics.DrawLine(pen, point11, point12);
                    page.Graphics.DrawLine(pen, point12, point9);

                    //  SUBTOTAL
                    page.Graphics.DrawString("Subtotal: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 363));
                    
                    original = Convert.ToInt32(txt_Subtotal1.Text);
                    subtotal_Excel = original;

                    if (tipo_descuento != "")
                    {
                        if(tipo_descuento == "CONOCIDO_$")
                        {
                            original -= Convert.ToDouble(txt_Descuento.Text);
                            descuento = Convert.ToDouble(txt_Descuento.Text);
                            descuento_Excel = descuento;
                            subtotal_final = original;
                        }
                        else
                        {
                            if (tipo_descuento == "CONOCIDO_PORCENTUAL")
                            {
                                descuento = (original * Convert.ToDouble(txt_Descuento.Text)) / 100;
                                original = (original * (100 - Convert.ToDouble(txt_Descuento.Text))) / 100;
                                descuento_Excel = descuento;
                                subtotal_final = original;
                            }
                            else
                            {
                                if (tipo_descuento == "EFECTIVO")
                                {
                                    descuento = (original * Convert.ToDouble(txt_Descuento.Text)) / 100;
                                    original = (original * (100 - Convert.ToDouble(txt_Descuento.Text))) / 100;
                                    descuento_Excel = descuento;
                                    subtotal_final = original;
                                }
                                else
                                {
                                    if (tipo_descuento == "OBRA SOCIAL")
                                    {
                                        original -= Convert.ToDouble(txt_Descuento.Text);
                                        descuento = Convert.ToDouble(txt_Descuento.Text);
                                        descuento_Excel = descuento;
                                        subtotal_final = original;
                                    }
                                    else
                                    {
                                        descuento = 0.0f;
                                        descuento_Excel = descuento;
                                        subtotal_final = original;
                                    }
                                }
                            }
                        }
                    }

                    if (original >= 1000)
                    {
                        enteros = Convert.ToInt32(subtotal_final) / 1000;
                        decimales = Convert.ToInt32(subtotal_final) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 361));
                        }
                        else
                        {
                            if (decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 361));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 361));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 361));
                                }
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + subtotal_final.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 361));
                    }

                    //  SEÑA
                    page.Graphics.DrawString("Seña: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 383));

                    original = Convert.ToInt32(txt_Seña1.Text);
                    seña_Excel = original;

                    if (original >= 1000)
                    {
                        enteros = Convert.ToInt32(original) / 1000;
                        decimales = Convert.ToInt32(original) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 381));
                        }
                        else
                        {
                            if (decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 381));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 381));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 381));
                                } 
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 381));
                    }

                    //  TOTAL
                    page.Graphics.DrawString("Resta ABONAR: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 403));

                    //resta_abonar_Excel = Convert.ToDouble(txt_Total1.Text);
                    totalizado = subtotal_final - Convert.ToInt32(txt_Seña1.Text);
                    resta_abonar_Excel = totalizado;

                    if (totalizado >= 1000)
                    {
                        enteros = Convert.ToInt32(totalizado) / 1000;
                        decimales = Convert.ToInt32(totalizado) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 401));
                        }
                        else
                        {
                            if (decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 401));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 401));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 401));
                                }
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + totalizado.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 401));
                    }

                    //  Zona de corte para talón
                    page.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 450));

                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Logo grande.png");
                    page.Graphics.DrawImage(image, 0, 465, 174, 25);

                    //  Nombre Optica Talón
                    //page.Graphics.DrawString(Data.Data_Optica.Nombre, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 475));

                    //  Numero de ORDEN Talón
                    page.Graphics.DrawString("Nº de Orden: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 500));
                    page.Graphics.DrawString(lbl_NroOrden1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(95, 498));

                    //  APELLIDO DEL CLIENTE Talón
                    page.Graphics.DrawString("Apellido: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 523));
                    page.Graphics.DrawString(txt_ApellidoCliente1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 523));

                    //  NOMBRE DEL CLIENTE Talón
                    page.Graphics.DrawString("Nombre: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 540));
                    page.Graphics.DrawString(txt_NombreCliente1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 540));

                    //  DNI DEL CLIENTE Talón
                    page.Graphics.DrawString("DNI: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 557));
                    page.Graphics.DrawString(dni_original, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 557));

                    //  Fecha de ENTREGA Talón
                    page.Graphics.DrawString("Fecha de Entrega: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(240, 470));
                    page.Graphics.DrawString(fecha_entrega, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(400, 468));

                    //  REVISAR INFO DE QR
                    qRBarcode.Text = "#QRCode-Retiro*" + Data.DataTrabajos.DNI_Cliente + "*" + Data.DataTrabajos.Nro_Orden;
                    qRBarcode.Draw(page, new PointF(280, 505));

                    //Create the rectangle points
                    PointF point13 = new PointF(0, 580);
                    PointF point14 = new PointF(240, 580);
                    PointF point15 = new PointF(240, 650);
                    PointF point16 = new PointF(0, 650);

                    //Draw the rectangle on PDF document
                    page.Graphics.DrawLine(pen, point13, point14);
                    page.Graphics.DrawLine(pen, point14, point15);
                    page.Graphics.DrawLine(pen, point15, point16);
                    page.Graphics.DrawLine(pen, point16, point13);

                    //  SUBTOTAL
                    page.Graphics.DrawString("Subtotal: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 588));

                    //original = Convert.ToInt32(txt_Subtotal1.Text);

                    if (subtotal_final >= 1000)
                    {
                        enteros = Convert.ToInt32(subtotal_final) / 1000;
                        decimales = Convert.ToInt32(subtotal_final) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 586));
                        }
                        else
                        {
                            if (decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 586));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 586));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 586));
                                }
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + subtotal_final.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 586));
                    }

                    //  SEÑA
                    page.Graphics.DrawString("Seña: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 608));

                    original = Convert.ToInt32(txt_Seña1.Text);
                    if (original >= 1000)
                    {
                        enteros = Convert.ToInt32(original) / 1000;
                        decimales = Convert.ToInt32(original) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 606));
                        }
                        else
                        {
                            if(decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 606));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 606));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 606));
                                }
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 606));
                    }

                    //  TOTAL
                    page.Graphics.DrawString("Resta ABONAR: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 628));

                    //original = Convert.ToInt32(txt_Total1.Text);
                    if (totalizado >= 1000)
                    {
                        enteros = Convert.ToInt32(totalizado) / 1000;
                        decimales = Convert.ToInt32(totalizado) - enteros * 1000;
                        if (decimales == 0)
                        {
                            page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 626));
                        }
                        else
                        {
                            if (decimales >= 10 && decimales < 100)
                            {
                                page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 626));
                            }
                            else
                            {
                                if(decimales < 10)
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + ".00" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 626));
                                }
                                else
                                {
                                    page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 626));
                                }                                
                            }
                        }
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + totalizado.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 626));
                    }

                    // Add location
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Marker_96px.png");
                    page.Graphics.DrawImage(image, 0, 665, 20, 20);

                    page.Graphics.DrawString(Data.Data_Optica.Domicilio, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 667));

                    // Add Working Hours
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Clock_100px.png");
                    page.Graphics.DrawImage(image, 280, 667, 15, 15);

                    page.Graphics.DrawString(Data.Data_Optica.Horario, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 667));

                    // Add WhatsApp
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\WhatsApp_100px.png");
                    page.Graphics.DrawImage(image, 2, 692, 15, 15);

                    page.Graphics.DrawString(Data.Data_Optica.WhatsApp, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 692));

                    // Add Facebook
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Facebook_100px.png");
                    page.Graphics.DrawImage(image, 280, 692, 15, 15);

                    page.Graphics.DrawString(Data.Data_Optica.Facebook, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 692));

                    // Add Instagram
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Instagram_100px.png");
                    page.Graphics.DrawImage(image, 0, 717, 20, 20);

                    page.Graphics.DrawString(Data.Data_Optica.Instagram, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 718));

                    // Add Email
                    image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Message_100px.png");
                    page.Graphics.DrawImage(image, 280, 719, 15, 15);

                    page.Graphics.DrawString(Data.Data_Optica.Email, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 717));

                    // Add Website

                    if (Data.Data_Optica.Website != String.Empty)
                    {
                        image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Domain_100px.png");
                        page.Graphics.DrawImage(image, 2, 744, 15, 15);

                        page.Graphics.DrawString(Data.Data_Optica.Website, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 742));
                    }

                    document.Save("C:\\Temp\\Trabajo_" + lbl_NroOrden1.Text + ".pdf");
                    document.Close(true);

                    string path = "C:\\Temp\\Trabajo_" + lbl_NroOrden1.Text + ".pdf";

                    Funciones.Functions.PrintDocument(path);

                    string trabajo_completo = string.Empty;

                    //  Chequeo de variables en CERO
                    if (checkBox2.Checked == false)
                    {
                        if (checkBox3.Checked == false)
                        {
                            //  NO HAY DATOS PARA SUBIR
                            //  NO GUARDAMOS NADA
                        }
                        else
                        {
                            //  HAY SOLO DATOS DE CERCA

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += txt_EsfericoOIC.Text + "/" + txt_CilindricoOIC.Text + "/" + txt_EjeOIC.Text;
                            trabajo_completo += "/" + txt_EsfericoODC.Text + "/" + txt_CilindricoODC.Text + "/" + txt_EjeODC.Text;
                            trabajo_completo += "/" + comboBox_CristalCerca.Text + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + "/" + "/" + lbl_ArmazonCerca.Text + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();

                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, ";
                            sql += "AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, ";
                            sql += "Armazon_Cerca, Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "',";
                            sql += "'" + comboBox_CristalCerca.Text + "',";
                            sql += "'" + txt_EsfericoOIC.Text + "', '" + txt_CilindricoOIC.Text + "', '" + Convert.ToInt32(txt_EjeOIC.Text) + "',";
                            sql += "'" + txt_EsfericoODC.Text + "', '" + txt_CilindricoODC.Text + "', '" + Convert.ToInt32(txt_EjeODC.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonCerca.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == false)
                        {
                            //  HAY SOLO DATOS DE LEJOS

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + comboBox_CristalLejos.Text + "/" + txt_EsfericoOIL.Text + "/" + txt_CilindricoOIL.Text + "/";
                            trabajo_completo += txt_EjeOIL.Text + "/" + txt_EsfericoODL.Text + "/" + txt_CilindricoODL.Text + "/" + txt_EjeODL.Text + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + lbl_ArmazonLejos.Text + "/" + "/" + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();

                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, ";
                            sql += "Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, ";
                            sql += "Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "', '" + comboBox_CristalLejos.Text + "',";
                            sql += "'" + txt_EsfericoOIL.Text + "', '" + txt_CilindricoOIL.Text + "', '" + Convert.ToInt32(txt_EjeOIL.Text) + "',";
                            sql += "'" + txt_EsfericoODL.Text + "', '" + txt_CilindricoODL.Text + "', '" + Convert.ToInt32(txt_EjeODL.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonLejos.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                        else
                        {
                            //  HAY DATOS DE LEJOS Y DE CERCA

                            //  Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, 
                            //  Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, 
                            //  AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, 
                            //  Armazon_Cerca, Subtotal, Seña, Resta_Abonar

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + comboBox_CristalLejos.Text + "/" + txt_EsfericoOIL.Text + "/" + txt_CilindricoOIL.Text + "/";
                            trabajo_completo += txt_EjeOIL.Text + "/" + txt_EsfericoODL.Text + "/" + txt_CilindricoODL.Text + "/" + txt_EjeODL.Text + "/";
                            trabajo_completo += txt_EsfericoOIC.Text + "/" + txt_CilindricoOIC.Text + "/" + txt_EjeOIC.Text;
                            trabajo_completo += "/" + txt_EsfericoODC.Text + "/" + txt_CilindricoODC.Text + "/" + txt_EjeODC.Text;
                            trabajo_completo += "/" + comboBox_CristalCerca.Text + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + lbl_ArmazonLejos.Text + "/" + lbl_ArmazonCerca.Text + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();

                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, ";
                            sql += "AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, ";
                            sql += "Armazon_Cerca, Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "', '" + comboBox_CristalLejos.Text + "',";
                            sql += "'" + txt_EsfericoOIL.Text + "', '" + txt_CilindricoOIL.Text + "', '" + Convert.ToInt32(txt_EjeOIL.Text) + "',";
                            sql += "'" + txt_EsfericoODL.Text + "', '" + txt_CilindricoODL.Text + "', '" + Convert.ToInt32(txt_EjeODL.Text) + "', '" + comboBox_CristalCerca.Text + "',";
                            sql += "'" + txt_EsfericoOIC.Text + "', '" + txt_CilindricoOIC.Text + "', '" + Convert.ToInt32(txt_EjeOIC.Text) + "',";
                            sql += "'" + txt_EsfericoODC.Text + "', '" + txt_CilindricoODC.Text + "', '" + Convert.ToInt32(txt_EjeODC.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonLejos.Text + "', '" + lbl_ArmazonCerca.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                    }

                    // "NT/25/FALABELLA/MARIA FLORENCIA/34948716/Av 66 N 2612/221/6249597/si/si/02-01-1990/-1.25/-0.50/46/-1.50/0.25/21/AR///////" + timestamp + "/"
                    // "NT/Nro_Orden/Apellido/Nombre/DNI/Domicilio/Telefono/WhatsApp/Celular/Fecha_Nac_Cliente/AOIEL/AOICL/EjeOIL/AODEL/AODCL/EjeODL/Tipo_Cirstal_Lejos/AOIEC/AOICC/EjeOIC/AODEC/AODCC/EjeODC/Tipo_Cristal_Cerca/Fecha_Pedido/Fecha_Entregada_Pactada/Subido_A_Firebase"

                    string forma_de_pago = String.Empty;
                    string cuotas = String.Empty;
                    int cant_armazones_lejos = 0;
                    int cant_armazones_cerca = 0;
                    int valor_armazon_lejos = 0;
                    int valor_armazon_cerca = 0;
                    int cant_cristales_lejos = 0;
                    int cant_cristales_cerca = 0;
                    int valor_cristal_lejos = 0;
                    int valor_cristal_cerca = 0;

                    if (checkBox2.Checked)
                    {
                        cant_cristales_lejos = 1;
                        valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);
                    }

                    if (checkBox3.Checked)
                    {
                        cant_cristales_cerca = 1;
                        valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);
                    }

                    if (checkBox4.Checked)
                    {
                        cant_armazones_lejos = 1;
                        valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);
                    }

                    if (checkBox5.Checked)
                    {
                        cant_armazones_cerca = 1;
                        valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);
                    }

                    int subtotal = 0;
                    int seña = Convert.ToInt32(txt_Seña1.Text);

                    subtotal += cant_armazones_lejos * valor_armazon_lejos;
                    subtotal += cant_armazones_cerca * valor_armazon_cerca;
                    subtotal += cant_cristales_lejos * valor_cristal_lejos;
                    subtotal += cant_cristales_cerca * valor_cristal_cerca;

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

                    string concepto_pago;

                    if (txt_Total1.Text != "0")
                    {
                        concepto_pago = "Seña";
                    }
                    else
                    {
                        concepto_pago = "Total";
                    }

                    double monto_total = Convert.ToDouble(txt_Subtotal1.Text);

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
                        if(forma_de_pago == "Debito")
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

                    double subtotal_c_desc = 0.0f;

                    if (concepto_pago == "Seña")
                    {
                        subtotal_c_desc = (seña * (100 - comision_tarjeta)) / 100;
                    }
                    else
                    {
                        if (concepto_pago == "Total")
                        {
                            subtotal_c_desc = ((subtotal - descuento_Excel) * (100 - comision_tarjeta)) / 100;
                        }
                    }

                    if(totalizado == 0)
                    {
                        concepto_pago = "Total";
                    }

                    //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
                    //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

                    string what = String.Empty;
                    what += "VENTAS/TRABAJO/" + lbl_NroOrden1.Text + "/" + lbl_ArmazonLejos.Text + "/" + cant_armazones_lejos.ToString() + "/" + valor_armazon_lejos.ToString();
                    what += "/" + comboBox_CristalLejos.Text + "/" + cant_cristales_lejos.ToString() + "/" + valor_cristal_lejos.ToString();
                    what += "/" + lbl_ArmazonCerca.Text + "/" + cant_armazones_cerca.ToString() + "/" + valor_armazon_cerca.ToString();
                    what += "/" + comboBox_CristalCerca.Text + "/" + cant_cristales_cerca.ToString() + "/" + valor_cristal_cerca.ToString() + "/////";
                    what += subtotal_Excel.ToString() + "/" + seña_Excel.ToString() + "/" + descuento_Excel.ToString() + "/" + totalizado.ToString() + "/" + forma_de_pago;
                    what += "/" + cuotas + "/" + comision_tarjeta.ToString() + "/" + string.Format("{0:,0.00}", subtotal_c_desc) + "/" + concepto_pago + "/" + fecha_recepcion;

                    Funciones.Functions.SaveDataToExcelFile(what);
                    Funciones.Functions.SendBroadcastMessage(what);

                    //  Inserto los datos del cliente en la respectiva base de datos
                    string sql2 = "insert into CLIENTES (Apellido, Nombre, DNI, Email, Telefono, Domicilio, Nro_ORDEN, Fecha_Nac, WhatsApp, Celular) values ('" + txt_ApellidoCliente1.Text + "', '" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + txt_EmailCliente.Text + "', '" + telefono_client + "', '" + txt_DomicilioCliente.Text + "', '" + lbl_NroOrden1.Text + "', '" + fecha_nac + "', '" + checkBox1.Checked + "', '" + ckbox_Celular.Checked + "')";

                    miConexion.Open();
                    SQLiteCommand command2 = new SQLiteCommand(sql2, miConexion);
                    command2.Prepare();
                    command2.ExecuteNonQuery();
                    command2.Dispose();
                    miConexion.Close();

                    Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text) + 1;
                    Funciones.Functions.ActualizarNroOrden(Data.DataTrabajos.Nro_Orden);
                    Funciones.Functions.SendBroadcastMessage("NRO_ORDEN/" + Data.DataTrabajos.Nro_Orden);
                }
                else
                {
                    subido_a_firebase = false;
                    string trabajo_completo = string.Empty;

                    double original;
                    double descuento = 0.0f;

                    original = Convert.ToInt32(txt_Subtotal1.Text);
                    subtotal_Excel = original;

                    if (tipo_descuento != "")
                    {
                        if (tipo_descuento == "CONOCIDO_$")
                        {
                            original -= Convert.ToDouble(txt_Descuento.Text);
                            descuento = Convert.ToDouble(txt_Descuento.Text);
                            descuento_Excel = descuento;
                            subtotal_final = original;
                        }
                        else
                        {
                            if (tipo_descuento == "CONOCIDO_PORCENTUAL")
                            {
                                descuento = (original * Convert.ToDouble(txt_Descuento.Text)) / 100;
                                original = (original * (100 - Convert.ToDouble(txt_Descuento.Text))) / 100;
                                descuento_Excel = descuento;
                                subtotal_final = original;
                            }
                            else
                            {
                                if (tipo_descuento == "EFECTIVO")
                                {
                                    descuento = (original * Convert.ToDouble(txt_Descuento.Text)) / 100;
                                    original = (original * (100 - Convert.ToDouble(txt_Descuento.Text))) / 100;
                                    descuento_Excel = descuento;
                                    subtotal_final = original;
                                }
                                else
                                {
                                    if (tipo_descuento == "OBRA SOCIAL")
                                    {
                                        original -= Convert.ToDouble(txt_Descuento.Text);
                                        descuento = Convert.ToDouble(txt_Descuento.Text);
                                        descuento_Excel = descuento;
                                        subtotal_final = original;
                                    }
                                    else
                                    {
                                        descuento = 0.0f;
                                        descuento_Excel = descuento;
                                        subtotal_final = original;
                                    }
                                }
                            }
                        }
                    }

                    //  Chequeo de variables en CERO
                    if (checkBox2.Checked == false)
                    {
                        if (checkBox3.Checked == false)
                        {
                            //  NO HAY DATOS PARA SUBIR
                            //  NO GUARDAMOS NADA
                        }
                        else
                        {
                            //  HAY SOLO DATOS DE CERCA

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += txt_EsfericoOIC.Text + "/" + txt_CilindricoOIC.Text + "/" + txt_EjeOIC.Text;
                            trabajo_completo += "/" + txt_EsfericoODC.Text + "/" + txt_CilindricoODC.Text + "/" + txt_EjeODC.Text;
                            trabajo_completo += "/" + comboBox_CristalCerca.Text + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + "/" + "/" + lbl_ArmazonCerca.Text + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();

                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, ";
                            sql += "AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, ";
                            sql += "Armazon_Cerca, Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "',";
                            sql += "'" + comboBox_CristalCerca.Text + "',";
                            sql += "'" + txt_EsfericoOIC.Text + "', '" + txt_CilindricoOIC.Text + "', '" + Convert.ToInt32(txt_EjeOIC.Text) + "',";
                            sql += "'" + txt_EsfericoODC.Text + "', '" + txt_CilindricoODC.Text + "', '" + Convert.ToInt32(txt_EjeODC.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonCerca.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == false)
                        {
                            //  HAY SOLO DATOS DE LEJOS

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + comboBox_CristalLejos.Text + "/" + txt_EsfericoOIL.Text + "/" + txt_CilindricoOIL.Text + "/";
                            trabajo_completo += txt_EjeOIL.Text + "/" + txt_EsfericoODL.Text + "/" + txt_CilindricoODL.Text + "/" + txt_EjeODL.Text + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + "/" + "/" + "/";
                            trabajo_completo += "/" + "/" + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + lbl_ArmazonLejos.Text + "/" + "/" + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();
                            
                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, ";
                            sql += "Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, ";
                            sql += "Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "', '" + comboBox_CristalLejos.Text + "',";
                            sql += "'" + txt_EsfericoOIL.Text + "', '" + txt_CilindricoOIL.Text + "', '" + Convert.ToInt32(txt_EjeOIL.Text) + "',";
                            sql += "'" + txt_EsfericoODL.Text + "', '" + txt_CilindricoODL.Text + "', '" + Convert.ToInt32(txt_EjeODL.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonLejos.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                        else
                        {
                            //  HAY DATOS DE LEJOS Y DE CERCA

                            //  Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, 
                            //  Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, 
                            //  AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, 
                            //  Armazon_Cerca, Subtotal, Seña, Resta_Abonar

                            trabajo_completo = "NT/" + Data.DataTrabajos.Nro_Orden + "/" + txt_ApellidoCliente1.Text + "/" + txt_NombreCliente1.Text + "/" + txt_DNI1.Text + "/";
                            trabajo_completo += telefono_client + "/";

                            if (whatsapp)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/";

                            if (ckbox_Celular.Checked)
                            {
                                trabajo_completo += "si";
                            }
                            else
                            {
                                trabajo_completo += "no";
                            }

                            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + comboBox_CristalLejos.Text + "/" + txt_EsfericoOIL.Text + "/" + txt_CilindricoOIL.Text + "/";
                            trabajo_completo += txt_EjeOIL.Text + "/" + txt_EsfericoODL.Text + "/" + txt_CilindricoODL.Text + "/" + txt_EjeODL.Text + "/";
                            trabajo_completo += txt_EsfericoOIC.Text + "/" + txt_CilindricoOIC.Text + "/" + txt_EjeOIC.Text;
                            trabajo_completo += "/" + txt_EsfericoODC.Text + "/" + txt_CilindricoODC.Text + "/" + txt_EjeODC.Text;
                            trabajo_completo += "/" + comboBox_CristalCerca.Text + "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac.Replace("/", "-") + "/";
                            trabajo_completo += subido_a_firebase + "/" + txt_EmailCliente.Text + "/" + lbl_ArmazonLejos.Text + "/" + lbl_ArmazonCerca.Text + "/";
                            trabajo_completo += subtotal_final.ToString() + "/" + txt_Seña1.Text + "/" + totalizado.ToString();

                            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

                            string sql = String.Empty;

                            sql += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql += "Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, ";
                            sql += "AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, ";
                            sql += "Armazon_Cerca, Subtotal, Seña, Resta_Abonar) ";
                            sql += "values('" + "TRABAJO" + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_ApellidoCliente1.Text + "',";
                            sql += "'" + txt_NombreCliente1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + whatsapp + "',";
                            sql += "'" + ckbox_Celular.Checked + "', '" + txt_DomicilioCliente.Text + "', '" + comboBox_CristalLejos.Text + "',";
                            sql += "'" + txt_EsfericoOIL.Text + "', '" + txt_CilindricoOIL.Text + "', '" + Convert.ToInt32(txt_EjeOIL.Text) + "',";
                            sql += "'" + txt_EsfericoODL.Text + "', '" + txt_CilindricoODL.Text + "', '" + Convert.ToInt32(txt_EjeODL.Text) + "', '" + comboBox_CristalCerca.Text + "',";
                            sql += "'" + txt_EsfericoOIC.Text + "', '" + txt_CilindricoOIC.Text + "', '" + Convert.ToInt32(txt_EjeOIC.Text) + "',";
                            sql += "'" + txt_EsfericoODC.Text + "', '" + txt_CilindricoODC.Text + "', '" + Convert.ToInt32(txt_EjeODC.Text) + "',";
                            sql += "'" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + subido_a_firebase + "',";
                            sql += "'" + txt_EmailCliente.Text + "', '" + lbl_ArmazonLejos.Text + "', '" + lbl_ArmazonCerca.Text + "',";
                            sql += "'" + Convert.ToInt32(subtotal_final) + "', '" + Convert.ToInt32(txt_Seña1.Text) + "', '" + Convert.ToInt32(totalizado) + "')";

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                        }
                    }

                    string forma_de_pago = String.Empty;
                    string cuotas = String.Empty;
                    int cant_armazones_lejos = 0;
                    int cant_armazones_cerca = 0;
                    int valor_armazon_lejos = 0;
                    int valor_armazon_cerca = 0;
                    int cant_cristales_lejos = 0;
                    int cant_cristales_cerca = 0;
                    int valor_cristal_lejos = 0;
                    int valor_cristal_cerca = 0;

                    if (checkBox2.Checked)
                    {
                        cant_cristales_lejos = 1;
                        valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);
                    }

                    if (checkBox3.Checked)
                    {
                        cant_cristales_cerca = 1;
                        valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);
                    }

                    if (checkBox4.Checked)
                    {
                        cant_armazones_lejos = 1;
                        valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);
                    }

                    if (checkBox5.Checked)
                    {
                        cant_armazones_cerca = 1;
                        valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);
                    }

                    int subtotal = 0;
                    int seña = Convert.ToInt32(txt_Seña1.Text);

                    subtotal += cant_armazones_lejos * valor_armazon_lejos;
                    subtotal += cant_armazones_cerca * valor_armazon_cerca;
                    subtotal += cant_cristales_lejos * valor_cristal_lejos;
                    subtotal += cant_cristales_cerca * valor_cristal_cerca;

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

                    string concepto_pago;

                    if (txt_Total1.Text != "0")
                    {
                        concepto_pago = "Seña";
                    }
                    else
                    {
                        concepto_pago = "Total";
                    }

                    double monto_total = Convert.ToDouble(txt_Subtotal1.Text);

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

                    double subtotal_c_desc = 0.0f;

                    if (concepto_pago == "Seña")
                    {
                        subtotal_c_desc = (seña * (100 - comision_tarjeta)) / 100;
                    }
                    else
                    {
                        if (concepto_pago == "Total")
                        {
                            subtotal_c_desc = ((subtotal - descuento_Excel) * (100 - comision_tarjeta)) / 100;
                        }
                    }

                    if (totalizado == 0)
                    {
                        concepto_pago = "Total";
                    }

                    //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
                    //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

                    string what = String.Empty;
                    what += "VENTAS/TRABAJO/" + lbl_NroOrden1.Text + "/" + lbl_ArmazonLejos.Text + "/" + cant_armazones_lejos.ToString() + "/" + valor_armazon_lejos.ToString();
                    what += "/" + comboBox_CristalLejos.Text + "/" + cant_cristales_lejos.ToString() + "/" + valor_cristal_lejos.ToString();
                    what += "/" + lbl_ArmazonCerca.Text + "/" + cant_armazones_cerca.ToString() + "/" + valor_armazon_cerca.ToString();
                    what += "/" + comboBox_CristalCerca.Text + "/" + cant_cristales_cerca.ToString() + "/" + valor_cristal_cerca.ToString() + "/////";
                    what += subtotal_Excel.ToString() + "/" + seña_Excel.ToString() + "/" + descuento_Excel.ToString() + "/" + totalizado.ToString() + "/" + forma_de_pago;
                    what += "/" + cuotas + "/" + comision_tarjeta.ToString() + "/" + string.Format("{0:,0.00}", subtotal_c_desc) + "/" + concepto_pago + "/" + fecha_recepcion;

                    Funciones.Functions.SaveDataToExcelFile(what);
                    Funciones.Functions.SendBroadcastMessage(what);

                    Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text) + 1;
                    Funciones.Functions.ActualizarNroOrden(Data.DataTrabajos.Nro_Orden);
                    Funciones.Functions.SendBroadcastMessage("NRO_ORDEN/" + Data.DataTrabajos.Nro_Orden);
                }

                if (chk_GiftCard.Checked)
                {
                    int gift_card = Convert.ToInt32(txt_NroGiftCard.Text);
                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader3;
                    SQLiteCommand sqlite_cmd3;
                    sqlite_cmd3 = miConexion.CreateCommand();
                    sqlite_cmd3.CommandText = "SELECT Nro FROM GIFT_CARDS";
                    sqlite_cmd3.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader3 = sqlite_cmd3.ExecuteReader();

                    while (sqlite_datareader3.Read())
                    {
                        if (Convert.ToInt32(sqlite_datareader3.GetValue(0)) == gift_card)
                        {
                            //  El codigo está en la base de datos. Lo borro

                            using (SQLiteCommand command3 = new SQLiteCommand(miConexion))
                            {
                                command3.CommandText = "DELETE FROM GIFT_CARDS WHERE Nro='" + gift_card + "'";
                                command3.ExecuteNonQuery();
                            }
                        }
                    }

                    miConexion.Close();

                    Funciones.Functions.SendBroadcastMessage("ELIMINAR_GIFT_CARD/" + gift_card);
                }

                Form1 form1 = new Form1();
                this.Hide();
                this.Dispose();
                form1.Show();
                this.Close();
            }            
        }

        private void btn_Borrar_Click(object sender, EventArgs e)
        {
            txt_ApellidoCliente1.Text = "";
            txt_NombreCliente1.Text = "";
            txt_TelPrefijo.Text = "";
            txt_TelResto.Text = "";
            txt_DNI1.Text = "";
            txt_DomicilioCliente.Text = "";

            txt_EjeODC.Text = "";
            txt_EjeODL.Text = "";
            txt_EjeOIC.Text = "";
            txt_EjeOIL.Text = "";

            txt_EsfericoODC.Text = "";
            txt_EsfericoODL.Text = "";
            txt_EsfericoOIC.Text = "";
            txt_EsfericoOIL.Text = "";

            txt_CilindricoODC.Text = "";
            txt_CilindricoODL.Text = "";
            txt_CilindricoOIC.Text = "";
            txt_CilindricoOIL.Text = "";

            txt_CristalLejos.Text = "";
            txt_CristalCerca.Text = "";

            txt_ArmazonLejos.Text = "";
            txt_ArmazonCerca.Text = "";

            txt_Seña1.Text = "";
            txt_Subtotal1.Text = "";
            txt_Total1.Text = "";

            lbl_ArmazonLejos.Text = "";
            lbl_ArmazonCerca.Text = "";

            txt_Descuento.Text = "";
        }

        private void txt_ApellidoCliente_TextChanged(object sender, EventArgs e)
        {
            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
        }

        private void txt_NombreCliente_TextChanged(object sender, EventArgs e)
        {
            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
        }

        private void txt_TelPrefijo_TextChanged(object sender, EventArgs e)
        {
            telefono_client = txt_TelPrefijo.Text + txt_TelResto.Text;
            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
        }

        private void txt_TelResto_TextChanged(object sender, EventArgs e)
        {
            telefono_client = txt_TelPrefijo.Text + txt_TelResto.Text;
            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            whatsapp = checkBox1.Checked;
            double time = Funciones.Functions.DateTimeNowToUnixTimeStamp();
        }

        private void NuevoTrabajoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                txt_EsfericoOIL.Enabled = true;
                txt_CilindricoOIL.Enabled = true;
                txt_EjeOIL.Enabled = true;

                txt_EsfericoODL.Enabled = true;
                txt_CilindricoODL.Enabled = true;
                txt_EjeODL.Enabled = true;

                txt_CristalLejos.Enabled = true;
                txt_ArmazonLejos.Enabled = true;
                comboBox_CristalLejos.Enabled = true;
            }
            else
            {
                txt_EsfericoOIL.Enabled = false;
                txt_CilindricoOIL.Enabled = false;
                txt_EjeOIL.Enabled = false;

                txt_EsfericoODL.Enabled = false;
                txt_CilindricoODL.Enabled = false;
                txt_EjeODL.Enabled = false;

                txt_CristalLejos.Enabled = false;
                txt_ArmazonLejos.Enabled = false;
                comboBox_CristalLejos.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked == true)
            {
                txt_EsfericoOIC.Enabled = true;
                txt_CilindricoOIC.Enabled = true;
                txt_EjeOIC.Enabled = true;

                txt_EsfericoODC.Enabled = true;
                txt_CilindricoODC.Enabled = true;
                txt_EjeODC.Enabled = true;

                txt_CristalCerca.Enabled = true;
                txt_ArmazonCerca.Enabled = true;
                comboBox_CristalCerca.Enabled = true;
            }
            else
            {
                txt_EsfericoOIC.Enabled = false;
                txt_CilindricoOIC.Enabled = false;
                txt_EjeOIC.Enabled = false;

                txt_EsfericoODC.Enabled = false;
                txt_CilindricoODC.Enabled = false;
                txt_EjeODC.Enabled = false;

                txt_CristalCerca.Enabled = false;
                txt_ArmazonCerca.Enabled = false;
                comboBox_CristalCerca.Enabled = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                txt_QRRead.Enabled = true;
                txt_QRRead.Focus();
            }
        }

        private void txt_QRRead_TextChanged(object sender, EventArgs e)
        {
            if(previus_readQR == "")
            {
                previus_readQR = txt_QRRead.Text;

                if(checkBox4.Checked)
                {
                    lbl_ArmazonLejos.Text = "";
                    lbl_ArmazonLejos.Text = txt_QRRead.Text;

                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                    miConexion.Open();

                    string sql2 = "select Codigo, Precio from CODIGOS";

                    SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                    SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                    while (rdr2.Read())
                    {
                        if (lbl_ArmazonLejos.Text == rdr2.GetValue(0).ToString())
                        {
                            txt_ArmazonLejos.Text = rdr2.GetValue(1).ToString();
                        }
                    }

                    miConexion.Close();
                }
                else
                {
                    if(checkBox5.Checked)
                    {
                        lbl_ArmazonCerca.Text = "";
                        lbl_ArmazonCerca.Text = txt_QRRead.Text;

                        miConexion = new SQLiteConnection("Data source=database.sqlite3");
                        miConexion.Open();

                        string sql2 = "select Codigo, Precio from CODIGOS";

                        SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                        SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                        while (rdr2.Read())
                        {
                            if (lbl_ArmazonCerca.Text == rdr2.GetValue(0).ToString())
                            {
                                txt_ArmazonCerca.Text = rdr2.GetValue(1).ToString();
                            }
                        }

                        miConexion.Close();
                    }
                }
            }
            else
            {
                if((previus_readQR != txt_QRRead.Text) && (txt_QRRead.Text != ""))
                {
                    previus_readQR = txt_QRRead.Text;
                    timer1.Enabled = true;

                    if(checkBox4.Checked)
                    {
                        lbl_ArmazonLejos.Text = "";
                        lbl_ArmazonLejos.Text = txt_QRRead.Text;

                        miConexion = new SQLiteConnection("Data source=database.sqlite3");
                        miConexion.Open();

                        string sql2 = "select Codigo, Precio from CODIGOS";

                        SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                        SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                        while (rdr2.Read())
                        {
                            if(lbl_ArmazonLejos.Text == rdr2.GetValue(0).ToString())
                            {
                                txt_ArmazonLejos.Text = rdr2.GetValue(1).ToString();
                            }
                        }

                        miConexion.Close();
                    }
                    else
                    {
                        if(checkBox5.Checked)
                        {
                            lbl_ArmazonCerca.Text = "";
                            lbl_ArmazonCerca.Text = txt_QRRead.Text;

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();

                            string sql2 = "select Codigo, Precio from CODIGOS";

                            SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                            SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                            while (rdr2.Read())
                            {
                                if (lbl_ArmazonCerca.Text == rdr2.GetValue(0).ToString())
                                {
                                    txt_ArmazonCerca.Text = rdr2.GetValue(1).ToString();
                                }
                            }

                            miConexion.Close();
                        }
                    } 
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked)
            {
                txt_QRRead.Enabled = true;
                txt_QRRead.Focus();
            }            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            txt_QRRead.Text = "";
        }

        private void RevisarCristales()
        {
            //  Estructura STOCK_CRISTALES
            //      Tipo varchar(30)
            //      Cantidad int
            //      Esferico real
            //      Cilindrico real

            /*
                Revisaremos que el cristal esté en Stock
                En caso contrario se procederá a armar una lista
                de todos los cristales que deberán ser pedidos
             */

            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Tipo, Esferico, Cilindrico, Cantidad FROM STOCK_CRISTALES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                /*
                    AEOIL = 0;
                    ACOIL = 0;
                    AEODL = 0;
                    ACODL = 0;

                    AEOIC = 0;
                    ACOIC = 0;
                    AEODC = 0;
                    ACODC = 0;

                    AEOIL_Inv = 0;
                    ACOIL_Inv = 0;
                    AEODL_Inv = 0;
                    ACODL_Inv = 0;

                    AEOIC_Inv = 0;
                    ACOIC_Inv = 0;
                    double AEODC_Inv = 0;
                    double ACODC_Inv = 0;
                 
                 */
                if (sqlite_datareader.GetValue(0).ToString() == comboBox_CristalLejos.Text)
                {
                    //if(Convert.ToDouble(sqlite_datareader.GetValue(1)) == )
                }

                comboBox_CristalLejos.Items.Add(sqlite_datareader.GetValue(0));
                comboBox_CristalCerca.Items.Add(sqlite_datareader.GetValue(0));
            }

            miConexion.Close();

        }

        private void dtp_FechaEntrega_ValueChanged(object sender, EventArgs e)
        {
            string day;
            string month;

            if (dtp_FechaEntrega.Value.Day < 10)
            {
                day = "0" + dtp_FechaEntrega.Value.Day.ToString();
            }
            else 
            {
                day = dtp_FechaEntrega.Value.Day.ToString();
            }

            if (dtp_FechaEntrega.Value.Month < 10)
            {
                month = "0" + dtp_FechaEntrega.Value.Month.ToString();
            }
            else 
            {
                month = dtp_FechaEntrega.Value.Month.ToString();
            }

            lbl_FechaRetiro.Text = day + "/" + month + "/" + dtp_FechaEntrega.Value.Year;
        }

        public void Calcular()
        {
            try
            {
                int val1 = Convert.ToInt32(txt_CristalLejos.Text);
                int val2 = Data.Valores_Calculo.armazon_lejos;
                int val3 = Convert.ToInt32(txt_CristalCerca.Text);
                int val4 = Data.Valores_Calculo.armazon_cerca;

                Data.Valores_Calculo.subtotal = val1 + val2 + val3 + val4;
                Data.Valores_Calculo.seña     = Convert.ToInt32(txt_Seña1.Text);
                Data.Valores_Calculo.total    = Data.Valores_Calculo.subtotal - Data.Valores_Calculo.seña;
                txt_Subtotal1.Text            = Convert.ToString(val1 + val2 + val3 + val4);
                txt_Total1.Text               = Convert.ToString(Data.Valores_Calculo.total);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Calcular2()
        {
            int val5 = Data.Valores_Calculo.subtotal;
            int val6 = Data.Valores_Calculo.seña;

            Data.Valores_Calculo.total = val5 - val6;
            txt_Total1.Text            = Convert.ToString(Data.Valores_Calculo.total);
        }

        private void CristalLejos_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
            Data.Valores_Calculo.total = Data.Valores_Calculo.subtotal - Data.Valores_Calculo.seña;
        }

        private void ArmazonLejos_ValueChanged(object sender, EventArgs e)
        {
            if (txt_ArmazonLejos.Text != "")
            {
                Data.Valores_Calculo.armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);
                Calcular();
            }
            else
            {
                if (txt_ArmazonLejos.Text == "")
                {
                    Data.Valores_Calculo.armazon_lejos = 0;
                    Calcular();
                }
            }
        }

        private void CristalCerca_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
            Data.Valores_Calculo.total = Data.Valores_Calculo.subtotal - Data.Valores_Calculo.seña;
        }

        private void ArmazonCerca_ValueChanged(object sender, EventArgs e)
        {
            if (txt_ArmazonCerca.Text != "")
            {
                Data.Valores_Calculo.armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);
                Calcular();
            }
            else
            {
                if (txt_ArmazonCerca.Text == "")
                {
                    Data.Valores_Calculo.armazon_cerca = 0;
                    Calcular();
                }
            }
        }

        private void Seña_ValueChanged(object sender, EventArgs e)
        {
            if(txt_Seña1.Text != "")
            {
                Data.Valores_Calculo.seña = Convert.ToInt32(txt_Seña1.Text);
                Calcular2();
            }
            else
            {
                if(txt_Seña1.Text == "")
                {
                    Data.Valores_Calculo.seña = 0;
                    Calcular2();
                }
            }
        }

        private void TipoCristalLejos_Changed(object sender, EventArgs e)
        {
            string tipo = string.Empty;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            SQLiteConnection conn = new SQLiteConnection(miConexion);
            conn.Open();
            string sql = "select Tipo, Precio from TIPOS_CRISTALES";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (comboBox_CristalLejos.Text == rdr.GetValue(0).ToString())
                {
                    txt_CristalLejos.Text = rdr.GetValue(1).ToString();
                }
            }

            rdr.Close();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        private void TipoCristalCerca_Changed(object sender, EventArgs e)
        {
            string tipo = string.Empty;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            SQLiteConnection conn = new SQLiteConnection(miConexion);
            conn.Open();
            string sql = "select Tipo, Precio from TIPOS_CRISTALES";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (comboBox_CristalCerca.Text == rdr.GetValue(0).ToString())
                {
                    txt_CristalCerca.Text = rdr.GetValue(1).ToString();
                }
            }

            rdr.Close();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        private void Seña_LostFocus(object sender, EventArgs e)
        {
            if(txt_Seña1.Text == "")
            {
                txt_Seña1.Text = "0";
            }
        }

        private void txt_DNI_LostFocus(object sender, EventArgs e)
        {
            int cant_leidos = 0;
            string[] telefono = new string[2];
            string[] fecha = new string[3];
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT DNI, Apellido, Nombre, Telefono, Email, Domicilio, Fecha_Nac, WhatsApp, Celular FROM CLIENTES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (sqlite_datareader.GetValue(0).ToString() == txt_DNI1.Text)
                {
                    // Debo traer todos los datos del cliente
                    // Nombre varchar(30), Apellido varchar(30), DNI varchar(12), Telefono varchar(20), Email varchar(50), Domicilio varchar(50)
                    // SELECT Nombre, Apellido, Telefono, Email, Domicilio FROM CLIENTES WHERE DNI = '" + txt_DNI1.Text + "'

                    txt_ApellidoCliente1.Text = sqlite_datareader.GetValue(1).ToString();
                    txt_NombreCliente1.Text   = sqlite_datareader.GetValue(2).ToString();
                    txt_DomicilioCliente.Text = sqlite_datareader.GetValue(5).ToString();
                    telefono                  = sqlite_datareader.GetValue(3).ToString().Split("-");
                    txt_EmailCliente.Text     = sqlite_datareader.GetValue(4).ToString();
                    fecha                     = sqlite_datareader.GetValue(6).ToString().Split("-");
                    checkBox1.Checked         = Convert.ToBoolean(sqlite_datareader.GetValue(7));
                    ckbox_Celular.Checked     = Convert.ToBoolean(sqlite_datareader.GetValue(8));
                    cant_leidos++;
                }
            }

            miConexion.Close();

            if(cant_leidos > 0)
            {
                txt_TelPrefijo.Text = telefono[0].ToString();
                txt_TelResto.Text = telefono[1].ToString();
                dt_FechaNacimiento.Value = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
            }
        }

        private void chk_Credito_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Credito.Checked == true)
            {
                combo_Cuotas.Visible = true;
            }
            else
            {
                combo_Cuotas.Visible = false;
            }
        }

        private void chk_GiftCard_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_GiftCard.Checked == true)
            {
                txt_NroGiftCard.Visible = true;
            }
            else
            {
                txt_NroGiftCard.Visible = false;
            }
        }   //  READY

        private void txt_NroGiftCard_LostFocus(object sender, EventArgs e)
        {
            int seña = Convert.ToInt32(txt_Seña1.Text);
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nro, Valor FROM GIFT_CARDS";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (Convert.ToInt32(sqlite_datareader.GetValue(0)) == Convert.ToInt32(txt_NroGiftCard.Text))
                {
                    seña += Convert.ToInt32(sqlite_datareader.GetValue(1));
                    txt_Seña1.Text = seña.ToString();
                }
            }

            miConexion.Close();
        }

        private void chk_Efectivo_CheckChanged(object sender, EventArgs e)
        {
            double valor_final = 0.0f;
            int valor_armazon_lejos = 0;
            int valor_armazon_cerca = 0;
            int valor_cristal_lejos = 0;
            int cant_cristal_lejos = 0;
            int valor_cristal_cerca = 0;
            int cant_cristal_cerca = 0;

            if (chk_Efectivo.Checked == true)
            {
                groupBox4.Visible = true;
                groupBox_Descuento.Visible = true;

                if (checkBox2.Checked)
                {
                    cant_cristal_lejos = 1;
                    valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);

                    valor_final += valor_cristal_lejos * cant_cristal_lejos;
                }

                if (checkBox3.Checked)
                {
                    cant_cristal_cerca = 1;
                    valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);

                    valor_final += valor_cristal_cerca * cant_cristal_cerca;
                }

                if (checkBox4.Checked)
                {
                    valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);

                    valor_final += valor_armazon_lejos;
                }

                if (checkBox5.Checked)
                {
                    valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);

                    valor_final += valor_armazon_cerca;
                }

                if (chk_GiftCard.Checked)
                {
                    valor_final -= valor_GiftCard;
                }

                if (txt_Descuento.Text != String.Empty)
                {
                    valor_final = valor_final * (100 - Convert.ToDouble(txt_Descuento.Text)) / 100;

                    lbl_ValorFinal.Text = "$ " + valor_final.ToString();
                }                
            }
            else
            {
                groupBox4.Visible = false;
                groupBox_Descuento.Visible = false;

                if (checkBox2.Checked)
                {
                    cant_cristal_lejos = 1;
                    valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);

                    valor_final += valor_cristal_lejos * cant_cristal_lejos;
                }

                if (checkBox3.Checked)
                {
                    cant_cristal_cerca = 1;
                    valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);

                    valor_final += valor_cristal_cerca * cant_cristal_cerca;
                }

                if (checkBox4.Checked)
                {
                    valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);

                    valor_final += valor_armazon_lejos;
                }

                if (checkBox5.Checked)
                {
                    valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);

                    valor_final += valor_armazon_cerca;
                }

                if (chk_GiftCard.Checked)
                {
                    valor_final -= valor_GiftCard;
                }

                lbl_ValorFinal.Text = "$ " + valor_final.ToString();
            }
        }

        private void cmb_Concepto_TextChanged(object sender, EventArgs e)
        {
            /*
             
                Descuento a conocido $
                Descuento a conocido Procentual
                Descuento en Efectivo (Porcentual)
                Descuento por Obra Social $
             
             */

            if(cmb_Concepto.Text == "Descuento a conocido ($)")
            {
                //  Acá el descuento es en pesos

                tipo_descuento = "CONOCIDO_$";
            }
            else
            {
                if(cmb_Concepto.Text == "Descuento a conocido (Porcentual)")
                {
                    //  Acá el descuento es porcentual

                     tipo_descuento = "CONOCIDO_PORCENTUAL";
                }
                else
                {
                    if(cmb_Concepto.Text == "Descuento en Efectivo (Porcentual)")
                    {
                        //  Acá el descuento es en efectivo

                        tipo_descuento = "EFECTIVO";
                    }
                    else
                    {
                        if(cmb_Concepto.Text == "Descuento por Obra Social ($)")
                        {
                            //  Acá el descuento es por Obra Social

                            tipo_descuento = "OBRA SOCIAL";
                        }
                        else
                        {
                            //  Acá no se ingresó ninguna opción y debe saltar el cartel de alerta

                            MessageBox.Show("Debe ingresar una de las opciones listadas.\r\n\r\nGracias.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void chk_Debito_CheckChanged(object sender, EventArgs e)
        {
            double valor_final = 0.0f;
            int valor_armazon_lejos = 0;
            int valor_armazon_cerca = 0;
            int valor_cristal_lejos = 0;
            int cant_cristal_lejos = 0;
            int valor_cristal_cerca = 0;
            int cant_cristal_cerca = 0;

            if (chk_Debito.Checked == true)
            {
                groupBox4.Visible = true;
                groupBox_Descuento.Visible = true;

                if (checkBox2.Checked)
                {
                    cant_cristal_lejos = 1;
                    valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);

                    valor_final += valor_cristal_lejos * cant_cristal_lejos;
                }

                if (checkBox3.Checked)
                {
                    cant_cristal_cerca = 1;
                    valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);

                    valor_final += valor_cristal_cerca * cant_cristal_cerca;
                }

                if (checkBox4.Checked)
                {
                    valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);

                    valor_final += valor_armazon_lejos;
                }

                if (checkBox5.Checked)
                {
                    valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);

                    valor_final += valor_armazon_cerca;
                }

                if (chk_GiftCard.Checked)
                {
                    valor_final -= valor_GiftCard;
                }

                if (txt_Descuento.Text != String.Empty)
                {
                    valor_final = valor_final * (100 - Convert.ToDouble(txt_Descuento.Text)) / 100;

                    lbl_ValorFinal.Text = "$ " + valor_final.ToString();
                }
            }
            else
            {
                groupBox4.Visible = false;
                groupBox_Descuento.Visible = false;

                if (checkBox2.Checked)
                {
                    cant_cristal_lejos = 1;
                    valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);

                    valor_final += valor_cristal_lejos * cant_cristal_lejos;
                }

                if (checkBox3.Checked)
                {
                    cant_cristal_cerca = 1;
                    valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);

                    valor_final += valor_cristal_cerca * cant_cristal_cerca;
                }

                if (checkBox4.Checked)
                {
                    valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);

                    valor_final += valor_armazon_lejos;
                }

                if (checkBox5.Checked)
                {
                    valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);

                    valor_final += valor_armazon_cerca;
                }

                if (chk_GiftCard.Checked)
                {
                    valor_final -= valor_GiftCard;
                }

                lbl_ValorFinal.Text = "$ " + valor_final.ToString();
            }
        }

        private void txt_Descuento_TextChanged(object sender, EventArgs e)
        {
            double valor_final = 0.0f;
            int valor_armazon_lejos = 0;
            int valor_armazon_cerca = 0;
            int valor_cristal_lejos = 0;
            int cant_cristal_lejos = 0;
            int valor_cristal_cerca = 0;
            int cant_cristal_cerca = 0;

            if (checkBox2.Checked)
            {
                cant_cristal_lejos = 1;
                valor_cristal_lejos = Convert.ToInt32(txt_CristalLejos.Text);

                valor_final += valor_cristal_lejos * cant_cristal_lejos;
            }

            if (checkBox3.Checked)
            {
                cant_cristal_cerca = 1;
                valor_cristal_cerca = Convert.ToInt32(txt_CristalCerca.Text);

                valor_final += valor_cristal_cerca * cant_cristal_cerca;
            }

            if (checkBox4.Checked)
            {
                valor_armazon_lejos = Convert.ToInt32(txt_ArmazonLejos.Text);

                valor_final += valor_armazon_lejos;
            }

            if (checkBox5.Checked)
            {
                valor_armazon_cerca = Convert.ToInt32(txt_ArmazonCerca.Text);

                valor_final += valor_armazon_cerca;
            }

            if (chk_GiftCard.Checked)
            {
                valor_final -= valor_GiftCard;
            }

            valor_final = valor_final * (100 - Convert.ToDouble(txt_Descuento.Text)) / 100;

            lbl_ValorFinal.Text = "$ " + valor_final.ToString();
        }

        private void chk_NroGIFTCARD_CheckChanged(object sender, EventArgs e)
        {
            if(txt_NroGiftCard.Text != String.Empty)
            {
                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                miConexion.Open();

                string sql2 = "select Nro, Valor from GIFT_CARDS";

                SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (txt_NroGiftCard.Text == rdr2.GetValue(0).ToString())
                    {
                        valor_GiftCard = Convert.ToInt32(rdr2.GetValue(1));
                    }
                }

                miConexion.Close();
            }
        }
    }
}

internal class DatosVENTAS
{
    public double Income { get; set; }
    public double Outcome { get; set; }
    public double startDate { get; set; }
    public double endDate { get; set; }

}

/*
 
SQLiteCommand cmd;
cmd = miConexion.CreateCommand();
cmd.CommandText = "update STOCK_CRISTALES set Cantidad ='" + precio_cristal + "' where Esferico = '" + tipo_cristal + "' AND Cilindrico = '" + tipo_cristal + "'";
cmd.CommandType = System.Data.CommandType.Text;
 






00132064447@BANNERT@BRAIAN DEMIAN@M@33688441@A@07/03/1988@27/08/2012




 */
