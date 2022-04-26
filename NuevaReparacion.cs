using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Data.SQLite;

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

namespace OpticaFalabella
{
    public partial class NuevaReparacion : Form
    {
        //private Button printButton = new Button();
        //private PrintDocument printDocument1 = new PrintDocument();

        public SQLiteConnection miConexion;
        string telefono_client = "";
        bool whatsapp = false;
        bool celular = false;
        string fecha_pedido = string.Empty;
        string fecha_nacimiento = string.Empty;

        public NuevaReparacion()
        {
            InitializeComponent();
        }

        private void NuevaReparacion_Load(object sender, EventArgs e)
        {
            txt_Subtotal1.Text = "0";
            txt_Seña1.Text = "0";

            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            dtp_FechaRecepcion.Enabled = false;
            dtp_FechaRecepcion.MinDate = DateTime.Today;
            dtp_FechaEntrega.MinDate = DateTime.Today;

            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT NRO FROM NRO_ORDEN";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Data.DataTrabajos.Nro_Orden = Convert.ToInt32(sqlite_datareader.GetValue(0));
            }

            lbl_NroOrden1.Text = Data.DataTrabajos.Nro_Orden.ToString();

            sqlite_datareader.Close();
            miConexion.Close();

            GetTipoREPARACIONES();            
        }

        public void GetTipoREPARACIONES()
        {
            miConexion.Open();

            SQLiteDataReader sqlite_datareader2;
            SQLiteCommand sqlite_cmd2;

            sqlite_cmd2 = miConexion.CreateCommand();
            sqlite_cmd2.CommandText = "SELECT Tipo FROM REPARACIONES";
            sqlite_cmd2.CommandType = System.Data.CommandType.Text;

            sqlite_datareader2 = sqlite_cmd2.ExecuteReader();

            while (sqlite_datareader2.Read())
            {
                cmb_REPARACION.Items.Add(sqlite_datareader2.GetValue(0).ToString());
            }

            sqlite_datareader2.Close();
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

            //lbl_FechaRetiro.Text = day + "/" + month + "/" + dtp_FechaEntrega.Value.Year;
        }

        private void btn_Imprimir_ItemClicked(object sender, EventArgs e)
        {
            //  Falta agregar la parte de imprimir

            string day;
            string month;

            txt_Apellido1.Text = txt_Apellido1.Text.ToUpper();
            txt_Nombre1.Text = txt_Nombre1.Text.ToUpper();

            telefono_client = txt_TelPrefijo.Text + "-" + txt_TelResto.Text;

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

            fecha_recepcion = day + "-" + month + "-" + dtp_FechaRecepcion.Value.Year.ToString();

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

            Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Size = PdfPageSize.A4;
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            PdfPageBase page = document.Pages.Add();
            PdfQRBarcode qRBarcode = new PdfQRBarcode();

            qRBarcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
            qRBarcode.XDimension = 2.5F;

            //  REVISAR INFO DE QR
            qRBarcode.Text = "#QRCode-Pedido*" + Data.DataTrabajos.DNI_Cliente + "*" + Data.DataTrabajos.Nro_Orden;
            qRBarcode.Draw(page, new PointF(280, 325));
            //page.Graphics.DrawString(txt_NombreCliente1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 0));

            Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text);

            //  Logo 
            PdfBitmap image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Logo grande.png");
            page.Graphics.DrawImage(image, 0, 0, 174, 25);

            //  Nombre Optica Talón
            //page.Graphics.DrawString(Data.Data_Optica.Nombre, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 0));

            //  Numero de ORDEN
            page.Graphics.DrawString("Nº de Orden: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 30));
            page.Graphics.DrawString(lbl_NroOrden1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(95, 28));

            //  APELLIDO DEL CLIENTE
            page.Graphics.DrawString("Apellido: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 53));
            page.Graphics.DrawString(txt_Apellido1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 53));

            //  NOMBRE DEL CLIENTE
            page.Graphics.DrawString("Nombre: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 70));
            page.Graphics.DrawString(txt_Nombre1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 70));

            //  DNI DEL CLIENTE
            page.Graphics.DrawString("DNI: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(250, 48));

            string dni_original = txt_DNI1.Text;

            if (txt_DNI1.TextLength == 8)   //  XX.XXX.XXX
            {
                dni_original = dni_original.Insert(2, ".");
                dni_original = dni_original.Insert(6, ".");
            }
            else
            {
                if (txt_DNI1.TextLength == 7)   //  X.XXX.XXX
                {
                    dni_original = dni_original.Insert(1, ".");
                    dni_original = dni_original.Insert(5, ".");
                }
                else
                {
                    if (txt_DNI1.TextLength == 9)   //  XXX.XXX.XXX
                    {
                        dni_original = dni_original.Insert(3, ".");
                        dni_original = dni_original.Insert(7, ".");
                    }
                }
            }

            page.Graphics.DrawString(dni_original, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(280, 48));

            //  Email DEL CLIENTE
            page.Graphics.DrawString("Email: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 87));
            page.Graphics.DrawString(txt_EmailCliente.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 87));

            //  Telefono DEL CLIENTE
            page.Graphics.DrawString("Teléfono: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 104));
            page.Graphics.DrawString(txt_TelPrefijo.Text + "-" + txt_TelResto.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 104));

            PdfPen pen = new PdfPen(PdfBrushes.Black, 1f);

            //Create the rectangle points
            PointF point1 = new PointF(0, 142);
            PointF point2 = new PointF(505, 142);
            PointF point3 = new PointF(505, 305);
            PointF point4 = new PointF(0, 305);

            //Draw the rectangle on PDF document
            page.Graphics.DrawLine(pen, point1, point2);
            page.Graphics.DrawLine(pen, point2, point3);
            page.Graphics.DrawLine(pen, point3, point4);
            page.Graphics.DrawLine(pen, point4, point1);

            //  Texto de DETALLE
            page.Graphics.DrawString("DETALLE", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 122));

            int length_Detalle = richTextBox1.TextLength;
            string detalle = richTextBox1.Text;
            int const_impresion = 73;
            int fila = 0;
            int startIndex = 0;
            int endIndex = const_impresion - 1;
            
            while(length_Detalle > const_impresion)
            {
                page.Graphics.DrawString(detalle.Substring(startIndex, const_impresion), new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(2, 147 + fila * 17));
                startIndex = endIndex + 1;
                length_Detalle -= const_impresion;
                endIndex = startIndex + const_impresion - 1;
                fila++;
            }

            page.Graphics.DrawString(detalle.Substring(startIndex, length_Detalle), new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(2, 147 + fila * 17));

            //  Fecha de RECEPCION
            string fecha_rec = fecha_recepcion.Replace("-", "/");
            page.Graphics.DrawString("Fecha de Recepción: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 317));
            page.Graphics.DrawString(fecha_rec, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(160, 315));

            //  Fecha de ENTREGA
            string fecha_entrega = fecha_para_retiro.Replace("-", "/");
            page.Graphics.DrawString("Fecha de Entrega: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 342));
            page.Graphics.DrawString(fecha_entrega, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(160, 340));

            //Create the rectangle points
            PointF point5 = new PointF(0, 380);
            PointF point6 = new PointF(240, 380);
            PointF point7 = new PointF(240, 450);
            PointF point8 = new PointF(0, 450);

            //Draw the rectangle on PDF document
            page.Graphics.DrawLine(pen, point5, point6);
            page.Graphics.DrawLine(pen, point6, point7);
            page.Graphics.DrawLine(pen, point7, point8);
            page.Graphics.DrawLine(pen, point8, point5);

            int original;
            int enteros;
            int decimales;

            //  SUBTOTAL
            page.Graphics.DrawString("Subtotal: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 388));

            original = Convert.ToInt32(txt_Subtotal1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 386));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 386));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 386));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 386));
            }

            //  SEÑA
            page.Graphics.DrawString("Seña: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 408));

            original = Convert.ToInt32(txt_Seña1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 406));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 406));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 406));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 406));
            }

            //  TOTAL
            page.Graphics.DrawString("Resta ABONAR: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 428));

            original = Convert.ToInt32(txt_Total1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 426));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 426));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 426));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 426));
            }

            //  Zona de corte para talón
            page.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 460));

            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Logo grande.png");
            page.Graphics.DrawImage(image, 0, 475, 174, 25);

            //  Nombre Optica Talón
            //page.Graphics.DrawString(Data.Data_Optica.Nombre, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(0, 475));

            //  Numero de ORDEN Talón
            page.Graphics.DrawString("Nº de Orden: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(0, 510));
            page.Graphics.DrawString(lbl_NroOrden1.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(95, 508));

            //  APELLIDO DEL CLIENTE Talón
            page.Graphics.DrawString("Apellido: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 533));
            page.Graphics.DrawString(txt_Apellido1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 533));

            //  NOMBRE DEL CLIENTE Talón
            page.Graphics.DrawString("Nombre: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 550));
            page.Graphics.DrawString(txt_Nombre1.Text.ToUpper(), new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(50, 550));

            //  DNI DEL CLIENTE Talón
            page.Graphics.DrawString("DNI: ", new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Underline), PdfBrushes.Black, new PointF(0, 567));
            page.Graphics.DrawString(dni_original, new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 567));

            //  Fecha de ENTREGA Talón
            page.Graphics.DrawString("Fecha de Entrega: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(240, 480));
            page.Graphics.DrawString(fecha_entrega, new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(400, 478));

            //  REVISAR INFO DE QR
            qRBarcode.Text = "#QRCode-Retiro*" + Data.DataTrabajos.DNI_Cliente + "*" + Data.DataTrabajos.Nro_Orden;
            qRBarcode.Draw(page, new PointF(280, 515));

            //Create the rectangle points
            PointF point9 = new PointF(0, 590);
            PointF point10 = new PointF(240, 590);
            PointF point11 = new PointF(240, 660);
            PointF point12 = new PointF(0, 660);

            //Draw the rectangle on PDF document
            page.Graphics.DrawLine(pen, point9, point10);
            page.Graphics.DrawLine(pen, point10, point11);
            page.Graphics.DrawLine(pen, point11, point12);
            page.Graphics.DrawLine(pen, point12, point9);

            //  SUBTOTAL
            page.Graphics.DrawString("Subtotal: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 598));

            original = Convert.ToInt32(txt_Subtotal1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 596));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 596));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 596));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 596));
            }

            //  SEÑA
            page.Graphics.DrawString("Seña: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 618));

            original = Convert.ToInt32(txt_Seña1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 616));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 616));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 616));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 616));
            }

            //  TOTAL
            page.Graphics.DrawString("Resta ABONAR: ", new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Regular), PdfBrushes.Black, new PointF(15, 638));

            original = Convert.ToInt32(txt_Total1.Text);
            if (original >= 1000)
            {
                enteros = original / 1000;
                decimales = original - enteros * 1000;
                if (decimales == 0)
                {
                    page.Graphics.DrawString("$ " + enteros.ToString() + ".000", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 636));
                }
                else
                {
                    if(decimales < 100)
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + ".0" + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 636));
                    }
                    else
                    {
                        page.Graphics.DrawString("$ " + enteros.ToString() + "." + decimales.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 636));
                    }
                }
            }
            else
            {
                page.Graphics.DrawString("$ " + original.ToString(), new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(134, 636));
            }

            // Add location
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Marker_96px.png");
            page.Graphics.DrawImage(image, 0, 675, 20, 20);

            page.Graphics.DrawString(Data.Data_Optica.Domicilio, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 677));

            // Add Working Hours
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Clock_100px.png");
            page.Graphics.DrawImage(image, 280, 677, 15, 15);

            page.Graphics.DrawString(Data.Data_Optica.Horario, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 677));

            // Add WhatsApp
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\WhatsApp_100px.png");
            page.Graphics.DrawImage(image, 2, 702, 15, 15);

            page.Graphics.DrawString(Data.Data_Optica.WhatsApp, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 702));

            // Add Facebook
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Facebook_100px.png");
            page.Graphics.DrawImage(image, 280, 702, 15, 15);

            page.Graphics.DrawString(Data.Data_Optica.Facebook, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 702));

            // Add Instagram
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Instagram_100px.png");
            page.Graphics.DrawImage(image, 0, 727, 20, 20);

            page.Graphics.DrawString(Data.Data_Optica.Instagram, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(30, 728));

            // Add Email
            image = new PdfBitmap("C:\\Optica Falabella\\Pictures\\Message_100px.png");
            page.Graphics.DrawImage(image, 280, 729, 15, 15);

            page.Graphics.DrawString(Data.Data_Optica.Email, new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(310, 727));

            string path = "C:\\Temp\\Reparacion_" + lbl_NroOrden1.Text + ".pdf";
            document.Save(path);
            document.Close(true);

            Funciones.Functions.PrintDocument(path);

            //  Carga de los datos de la REPARACION a la base de datos SQLite

            //  Tipo_Trabajo varchar(20), Reparacion varchar(1000), Nro_Orden int, Apellido_Cliente varchar(30), Nombre_Cliente varchar(30),
            //  DNI varchar(12), Telefono varchar(20), Email varchar(50), WhatsApp bool, Celular bool, Domicilio varchar(50),
            //  Fecha_Pedido varchar(20), Fecha_Entrega_Pactada varchar(20), Fecha_Nacimiento_Cliente varchar(20)

            string sqlDatos = "INSERT INTO TRABAJOS (Tipo_Trabajo, Reparacion, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, Email, WhatsApp, Celular, Domicilio, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subtotal, Seña, Resta_Abonar) VALUES ('" + "REPARACION" + "', '" + richTextBox1.Text + "', '" + Convert.ToInt32(lbl_NroOrden1.Text) + "', '" + txt_Apellido1.Text + "', '" + txt_Nombre1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + txt_EmailCliente.Text + "', '" + whatsapp + "', '" + celular + "', '" + txt_DomicilioCliente.Text + "', '" + fecha_recepcion + "', '" + fecha_para_retiro + "', '" + fecha_nac + "', '" + txt_Subtotal1.Text + "', '" + txt_Seña1.Text + "', '" + txt_Total1.Text + "')";

            miConexion.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlDatos, miConexion);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            miConexion.Close();

            Data.DataTrabajos.Nro_Orden = Convert.ToInt32(lbl_NroOrden1.Text) + 1;
            Funciones.Functions.ActualizarNroOrden(Data.DataTrabajos.Nro_Orden);
            Funciones.Functions.SendBroadcastMessage("NRO_ORDEN/" + Data.DataTrabajos.Nro_Orden);

            // "NR/Detalle Reparacion/Nro ORDEN/Apellido/Nombre/DNI/Telefono/Email/WhatsApp/Celular/Domicilio/Fecha_Pedido/Fecha_Entregada_Pactada/Fecha_Nac_Cliente"

            string trabajo_completo;

            trabajo_completo = "NR/" + richTextBox1.Text + "/" + lbl_NroOrden1.Text + "/" + txt_Apellido1.Text + "/" + txt_Nombre1.Text + "/" + txt_DNI1.Text + "/";
            trabajo_completo += telefono_client + "/" + txt_EmailCliente.Text + "/";

            if (whatsapp)
            {
                trabajo_completo += "si";
            }
            else
            {
                trabajo_completo += "no";
            }

            trabajo_completo += "/";

            if (celular)
            {
                trabajo_completo += "si";
            }
            else
            {
                trabajo_completo += "no";
            }

            trabajo_completo += "/" + txt_DomicilioCliente.Text + "/" + fecha_recepcion + "/" + fecha_para_retiro;
            trabajo_completo += "/" + fecha_nac + "/" + txt_Subtotal1.Text + "/" + txt_Seña1.Text + "/" + txt_Total1.Text + "/";

            //  Enviamos el mensaje por la red para que se actualicen las otras PCs
            Funciones.Functions.SendBroadcastMessage(trabajo_completo);

            string forma_de_pago = String.Empty;
            string cuotas = String.Empty;

            int subtotal = Convert.ToInt32(txt_Subtotal1.Text);
            int seña = Convert.ToInt32(txt_Seña1.Text);

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
                if(concepto_pago == "Total")
                {
                    subtotal_c_desc = (subtotal * (100 - comision_tarjeta)) / 100;
                }
            }

            //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
            //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

            string what = String.Empty;
            what += "VENTAS/REPARACION/" + lbl_NroOrden1.Text + "/////////////" + Convert.ToInt32(txt_Subtotal1.Text) + "////";
            what += subtotal + "/" + txt_Seña1.Text + "//" + txt_Total1.Text + "/" + forma_de_pago + "/" + cuotas + "/" + comision_tarjeta.ToString() + "/" + subtotal_c_desc + "/" + concepto_pago + "/";
            what += fecha_recepcion;

            Funciones.Functions.SaveDataToExcelFile(what);
            Funciones.Functions.SendBroadcastMessage(what);

            //  Inserto los datos del cliente en la respectiva base de datos
            string sql2 = "insert into CLIENTES (Nombre, Apellido, DNI, Telefono, Email, Domicilio, Nro_ORDEN, Fecha_Nac) values ('" + txt_Nombre1.Text + "', '" + txt_Apellido1.Text + "', '" + txt_DNI1.Text + "', '" + telefono_client + "', '" + txt_EmailCliente.Text + "', '" + txt_DomicilioCliente.Text + "', '" + lbl_NroOrden1.Text + "', '" + fecha_nac + "')";

            miConexion.Open();
            SQLiteCommand command2 = new SQLiteCommand(sql2, miConexion);
            command2.Prepare();
            command2.ExecuteNonQuery();
            command2.Dispose();
            miConexion.Close();

            //  Cerramos el Form
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_Borrar_ItemClicked(object sender, EventArgs e)
        {
            txt_Apellido1.Text = "";
            txt_Nombre1.Text = "";
            txt_DNI1.Text = "";
            txt_TelPrefijo.Text = "";
            txt_TelResto.Text = "";
            txt_EmailCliente.Text = "";
            txt_DomicilioCliente.Text = "";
            chk_WhatsApp.Checked = false;
            ckbox_Celular.Checked = false;
        }

        private void NuevaReparacionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void chk_Whatsapp_CheckedChanged(object sender, EventArgs e)
        {
            whatsapp = chk_WhatsApp.Checked;
        }

        private void chk_Celular_CheckedChanged(object sender, EventArgs e)
        {
            celular = ckbox_Celular.Checked;
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
            sqlite_cmd.CommandText = "SELECT DNI, Apellido, Nombre, Telefono, Email, Domicilio, Fecha_Nac FROM CLIENTES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (sqlite_datareader.GetValue(0).ToString() == txt_DNI1.Text)
                {
                    // Debo traer todos los datos del cliente
                    //  Nombre varchar(30), Apellido varchar(30), DNI varchar(12), Telefono varchar(20), Email varchar(50), Domicilio varchar(50)
                    // SELECT Nombre, Apellido, Telefono, Email, Domicilio FROM CLIENTES WHERE DNI = '" + txt_DNI1.Text + "'

                    txt_Apellido1.Text = sqlite_datareader.GetValue(1).ToString();
                    txt_Nombre1.Text = sqlite_datareader.GetValue(2).ToString();
                    txt_DomicilioCliente.Text = sqlite_datareader.GetValue(5).ToString();
                    telefono = sqlite_datareader.GetValue(3).ToString().Split("-");
                    txt_EmailCliente.Text = sqlite_datareader.GetValue(4).ToString();
                    fecha = sqlite_datareader.GetValue(6).ToString().Split("-");
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

        private void chk_Credito_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Credito.Checked == true)
            {
                combo_Cuotas.Visible = true;
            }
            else
            {
                combo_Cuotas.Visible = false;
            }
        }   // READY

        public void Calcular2()
        {
            int val5 = Data.Valores_Calculo.subtotal;
            int val6 = Data.Valores_Calculo.seña;

            Data.Valores_Calculo.total = val5 - val6;
            txt_Total1.Text = Convert.ToString(Data.Valores_Calculo.total);
        }   //  READY

        private void txt_Subtotal_TextChanged(object sender, EventArgs e)
        {
            if (txt_Subtotal1.Text != "")
            {
                Data.Valores_Calculo.subtotal = Convert.ToInt32(txt_Subtotal1.Text);
                Calcular2();
            }
            else
            {
                if (txt_Subtotal1.Text == "")
                {
                    Data.Valores_Calculo.subtotal = 0;
                    Calcular2();
                }
            }
        }   //  READY

        private void txt_Seña_TextChanged(object sender, EventArgs e)
        {
            if (txt_Seña1.Text != "")
            {
                Data.Valores_Calculo.seña = Convert.ToInt32(txt_Seña1.Text);
                Calcular2();
            }
            else
            {
                if (txt_Seña1.Text == "")
                {
                    Data.Valores_Calculo.seña = 0;
                    Calcular2();
                }
            }
        }   //  READY

        private void cmb_TipoReparacion_Changed(object sender, EventArgs e)
        {
            richTextBox1.Text = "Tipo de reparación a realizar: " + cmb_REPARACION.Text;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            SQLiteConnection conn = new SQLiteConnection(miConexion);
            conn.Open();
            string sql = "select Tipo, Precio from REPARACIONES";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (cmb_REPARACION.Text == rdr.GetValue(0).ToString())
                {
                    txt_Subtotal1.Text = rdr.GetValue(1).ToString();
                }
            }

            rdr.Close();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }
}
