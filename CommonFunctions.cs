
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

/*
    Correo contaduria FALABELLA
    
    Email:      contaduria.falabella@gmail.com
    Password:   lucas184
    
 */

/*
    Correo Optica FALABELLA
    
    Email:      optica.falabella@gmail.com
    Password:   Agustinfalabella1
    
 */

/*
    Cuenta FIREBASE Optica FALABELLA
    
    Email:      optica.falabella@gmail.com
    Password:   Agustinfalabella1
    
 */

namespace Funciones
{
    public class Functions
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        static IFirebaseClient client; 

        //static string myWhatsAppNumber = "+14155238886";
        static string mySMS_Number = "+14232994571";
        public static SQLiteConnection miConexion;

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);//.ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeNowToUnixTimeStamp()
        {
            var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return Timestamp;
        }

        public static void Send_Email(string email_destino)
        {
            if(Data.Data_Optica.Computadora_Nro == 1)
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("optica.falabella@gmail.com", "Optica Falabella");
                mail.To.Add(email_destino);

                string dia, mes, anio;

                if (DateTime.Today.Day < 10)
                {
                    dia = "0" + DateTime.Today.Day.ToString();
                }
                else
                {
                    dia = DateTime.Today.Day.ToString();
                }

                if (DateTime.Today.Month < 10)
                {
                    mes = "0" + DateTime.Today.Month.ToString();
                }
                else
                {
                    mes = DateTime.Today.Month.ToString();
                }

                anio = DateTime.Today.Year.ToString();


                mail.Subject = "Balance de ventas " + dia + "/" + mes + "/" + anio;
                mail.Body = "Correo con archivo adjunto referente a las ventas del día de hoy (" + dia + "/" + mes + "/" + anio + ").";

                string path = @"C:\Temp\Ventas_" + dia + "-" + mes + "-" + anio + ".xlsx";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(path);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("optica.falabella@gmail.com", "Agustinfalabella1");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
        }

        public static void EnviarWhatsApp(string clientPhoneNumber, string msg) 
        {
            string from = "+5492215791386";
            //b8480e9x1fba00iaff6bb6p967d15bckab
            WhatsApp wa = new WhatsApp(from, "6fc4d860ea051c4dfec7ed033bab7cfd", Data.Data_Optica.Nombre, true);
            //MessageBox.Show("Entré a mandar un mensaje");

            wa.OnConnectSuccess += () =>
            {
                MessageBox.Show("Conectado a WhatsApp");

                wa.OnLoginSuccess += (phoneNumber, data) =>
                {
                    wa.SendMessage(clientPhoneNumber, msg);
                    MessageBox.Show("Mensaje enviado a {0}", clientPhoneNumber);
                };

                wa.OnLoginFailed += (data) =>
                {
                    MessageBox.Show("Fallo de inicio de sesión: {0}", data);
                };

                wa.Login();
            };

            wa.OnConnectFailed += (ex) =>
            {
                MessageBox.Show("Conexión fallida " + ex.Message);
            };

            /*
            string yourID = "ueQnywqTZEef9+tJnWjRV29wdGljYV9kb3RfZmFsYWJlbGxhX2F0X2dtYWlsX2RvdF9jb20=";
            string yourPhone = "+542215791386";
            //string mensaje = msg;

            try
            {
                string url = "https://NiceApi.net/API";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("X-APIId", yourID);
                request.Headers.Add("X-APIMobile", yourPhone);
                using (StreamWriter streamOut = new StreamWriter(request.GetRequestStream()))
                {
                    streamOut.WriteLine(msg);
                };

                using (StreamReader streamIn = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    Console.WriteLine(streamIn.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            
            var accountSid = "AC34ab5e47383574535aebcf93acb61116";
            var authToken = "de90e5d0feb1eaba992d95d63ea872b5";

            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:" + clientPhoneNumber));
            messageOptions.From = new PhoneNumber("whatsapp:" + myWhatsAppNumber);
            messageOptions.Body = msg;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            */
        }

        public static void EnviarSMS(string clientPhoneNumber, string msg)
        {
            var accountSid = "AC34ab5e47383574535aebcf93acb61116";
            var authToken = "de90e5d0feb1eaba992d95d63ea872b5";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber(mySMS_Number),
                body: msg,
                to: new Twilio.Types.PhoneNumber(clientPhoneNumber)
            );

            Console.WriteLine(message.Body);
        }

        public static void GenerateQRCode(PictureBox pictureBox, double timestamp, string type, string what)
        {
            QRCodeGenerator qr;
            QRCodeData data;
            QRCode code;

            string information = "";

            switch(type)
            {
                case "TRABAJO":

                    switch (what) 
                    {
                        case "PEDIDO":  information = "#QRCode-Pedido," + Data.DataTrabajos.DNI_Cliente + "," + Data.DataTrabajos.Nro_Orden + "," + Data.DataTrabajos.Nombre_Cliente + "," + Data.DataTrabajos.Apellido_Cliente + "," + Data.DataTrabajos.Telefono_Cliente + "," + Data.DataTrabajos.WhatsApp;
                                        qr = new QRCodeGenerator(); 
                                        data = qr.CreateQrCode(information, QRCodeGenerator.ECCLevel.Q);
                                        code = new QRCode(data);

                                        pictureBox.Image = code.GetGraphic(5);
                                        break;

                        case "RETIRO":  information = "#QRCode-Retiro," + Data.DataTrabajos.DNI_Cliente + "," + Data.DataTrabajos.Nro_Orden + "," + Data.DataTrabajos.Nombre_Cliente + "," + Data.DataTrabajos.Apellido_Cliente + "," + Data.DataTrabajos.Telefono_Cliente + "," + Data.DataTrabajos.WhatsApp;
                                        qr = new QRCodeGenerator();
                                        data = qr.CreateQrCode(information, QRCodeGenerator.ECCLevel.Q);
                                        code = new QRCode(data);

                                        pictureBox.Image = code.GetGraphic(5);
                                        break;
                    }
                    break;

                case "REPARACION":  break;

            } 
        }

        public static void Get_Info_Optica()
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            //DATOS_OPTICA (Nombre, Telefono_Fijo, Calle, Numero, Instagram, Facebook, Whatsapp, Website, Email, Horario1, Horario2, Horario3, Horario4, Tipo_Horario, EntreCalles
            sqlite_cmd.CommandText = "SELECT Nombre, Calle, Numero, EntreCalles, Horario1, Horario2, Horario3, Horario4, Instagram, Facebook, Whatsapp, Email, Website, Tipo_Horario FROM DATOS_OPTICA";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Data.Data_Optica.Nombre =  sqlite_datareader.GetValue(0).ToString();
                Data.Data_Optica.Domicilio = sqlite_datareader.GetValue(1).ToString() + " Nº " + Convert.ToInt16(sqlite_datareader.GetValue(2)).ToString() + " e/ " + sqlite_datareader.GetValue(3).ToString();

                if(sqlite_datareader.GetValue(13).ToString() == "CORRIDO")
                {
                    Data.Data_Optica.Horario = sqlite_datareader.GetValue(4).ToString() + " a " + sqlite_datareader.GetValue(5).ToString() + " hs";
                }
                else
                {
                    if(sqlite_datareader.GetValue(13).ToString() == "PARTIDO")
                    {
                        Data.Data_Optica.Horario = sqlite_datareader.GetValue(4).ToString() + " a " + sqlite_datareader.GetValue(5).ToString() + " y de " + sqlite_datareader.GetValue(6).ToString() + " a " + sqlite_datareader.GetValue(7).ToString() + " hs";
                    }
                }

                Data.Data_Optica.Instagram = sqlite_datareader.GetValue(8).ToString();
                Data.Data_Optica.Facebook = sqlite_datareader.GetValue(9).ToString();
                Data.Data_Optica.WhatsApp = sqlite_datareader.GetValue(10).ToString();
                Data.Data_Optica.Email = sqlite_datareader.GetValue(11).ToString();
                Data.Data_Optica.Website = sqlite_datareader.GetValue(12).ToString();
            }

            sqlite_datareader.Close();
            sqlite_datareader.DisposeAsync();

            sqlite_cmd.CommandText = "SELECT Computadora_Nro FROM COMPUTADORA";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();


            while (sqlite_datareader.Read())
            {
                Data.Data_Optica.Computadora_Nro = Convert.ToInt16(sqlite_datareader.GetValue(0));
            }

            miConexion.Close();
        }

        public static void SendBroadcastMessage(string message)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            
            sqlite_cmd.CommandText = "SELECT IP_Address FROM IPs";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress ip_address = IPAddress.Parse(sqlite_datareader.GetValue(0).ToString());

                byte[] sendbuf = Encoding.ASCII.GetBytes(message);
                IPEndPoint ep = new IPEndPoint(ip_address, 20021);

                s.SendTo(sendbuf, ep);
            }

            sqlite_datareader.Close();
            sqlite_datareader.DisposeAsync();

            miConexion.Close();
        }

        public static void GetBroadcastAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Data.Variables_Globales.IP_Local = ip.ToString();
                }
            }

            Data.Variables_Globales.IP_Local = Data.Variables_Globales.IP_Local.Substring(0, Data.Variables_Globales.IP_Local.LastIndexOf(".") + 1); // cuts of the last octet of the given IP 
            Data.Variables_Globales.IP_Broadcast = Data.Variables_Globales.IP_Local + "255"; // adds 255 which represents the local broadcast
        }

        public static void StartListener()
        {
            UdpClient listener = new UdpClient(20020);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 20021);

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Data.Variables_Globales.incoming_message = bytes;
                    Parser(Encoding.Default.GetString(Data.Variables_Globales.incoming_message));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }

        public static void ActualizarStock(string what)
        {
            string mensaje;
            switch (what)
            {
                case "CRISTALES":   mensaje = "STOCK/CRISTALES/";
                                    SendBroadcastMessage(mensaje);
                                    break;

                case "RECETA":      mensaje = "STOCK/RECETA/";
                                    SendBroadcastMessage(mensaje);
                                    break;

                case "SOL":         mensaje = "STOCK/SOL/";
                                    SendBroadcastMessage(mensaje);
                                    break;

                case "CONTACTO":    mensaje = "STOCK/CONTACTO/";
                                    SendBroadcastMessage(mensaje);
                                    break;

                case "LIQUIDO":     mensaje = "STOCK/LIQUIDO/";
                                    SendBroadcastMessage(mensaje);
                                    break;
            }        
        }

        public static void ParserQR(string msg)
        {
            /*
                Esta función está pensada para poder decodificar los códigos leídos por
                las pistolas QR de las computadoras. Deben leer códigos de cristales (los
                cuales debe descontar del stock), los armazones (los cuales debe descontar
                del stock) y algo más seguramente
            */

            string[] words = msg.Split('*');

            switch (words[0])
            {
                case "BCO":     //  CRISTAL BLANCO 

                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_CRISTALES";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {

                        if (sqlite_datareader.GetValue(0).ToString() == msg)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(miConexion))
                            {
                                command.CommandText = "update STOCK_CRISTALES set Cantidad_Disponible = :cant where Codigo=:mensaje";
                                command.Parameters.Add("cant", DbType.String).Value = Convert.ToInt32(sqlite_datareader.GetValue(0)) - 1;
                                command.Parameters.Add("mensaje", DbType.String).Value = msg;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    miConexion.Close();
                    break;

                case "BLUE":    //  CRISTAL BLUEMAX

                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader1;
                    SQLiteCommand sqlite_cmd1;
                    sqlite_cmd1 = miConexion.CreateCommand();
                    sqlite_cmd1.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_CRISTALES";
                    sqlite_cmd1.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader1 = sqlite_cmd1.ExecuteReader();

                    while (sqlite_datareader1.Read())
                    {

                        if (sqlite_datareader1.GetValue(0).ToString() == msg)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(miConexion))
                            {
                                command.CommandText = "update STOCK_CRISTALES set Cantidad_Disponible = :cant where Codigo=:mensaje";
                                command.Parameters.Add("cant", DbType.String).Value = Convert.ToInt32(sqlite_datareader1.GetValue(0)) - 1;
                                command.Parameters.Add("mensaje", DbType.String).Value = msg;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    miConexion.Close();
                    break;

                case "AR":      //  CRISTAL AR 

                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader2;
                    SQLiteCommand sqlite_cmd2;
                    sqlite_cmd2 = miConexion.CreateCommand();
                    sqlite_cmd2.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_CRISTALES";
                    sqlite_cmd2.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader2 = sqlite_cmd2.ExecuteReader();

                    while (sqlite_datareader2.Read())
                    {

                        if (sqlite_datareader2.GetValue(0).ToString() == msg)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(miConexion))
                            {
                                command.CommandText = "update STOCK_CRISTALES set Cantidad_Disponible = :cant where Codigo=:mensaje";
                                command.Parameters.Add("cant", DbType.String).Value = Convert.ToInt32(sqlite_datareader2.GetValue(0)) - 1;
                                command.Parameters.Add("mensaje", DbType.String).Value = msg;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    miConexion.Close();
                    break;

                case "ANT":     //  ANTEOJOS 

                    break;
            }
        }

        public static void Parser(string msg)
        {
            string[] words = msg.Split('/');

            // Nuevo Trabajo
            // "NT/Apellido/Nombre/DNI/Domicilio/Telefono/WhatsApp/Celular/Fecha_Nac_Cliente/AOIEL/AOICL/EjeOIL/AODEL/AODCL/EjeODL/Tipo_Cirstal_Lejos/AOIEC/AOICC/EjeOIC/AODEC/AODCC/EjeODC/Tipo_Cristal_Cerca/Fecha_Pedido/Fecha_Entregada_Pactada/Subido_A_Firebase"
            // "NT/FALABELLA/MARIA FLORENCIA/34948716/Av 66 N 2612/+5492216249597/si/si/02-01-1990/-1.25/-0.50/46/-1.50/0.25/21/AR///////" + timestamp + "/"
            // "NRO_ORDEN/12
            // "Nro_PC/Computadora_Nro
            // "Datos_Optica/Nombre/
            // "NR/Detalle Reparacion/Nro ORDEN/Apellido/Nombre/DNI/Telefono/Email/WhatsApp/Celular/Domicilio/Fecha_Pedido/Fecha_Entregada_Pactada/Fecha_Nac_Cliente"
            //  "STOCK/Codigo/Cantidad Descontada"

            switch (words[0])    //  Vino un Nuevo Mensaje
            {
                // Casos
                // 
                // NT: Nuevo Trabajo
                // Nro_PC: Es para pedido de los datos
                // NRO_ORDEN: Es para actualizar el número de orden entre las PCs del local
                // REPARACION: Es para informar una reparación
                // ACTUALIZAR: Es para mostrar a las demás máquinas que se lograron subir los datos a Firebase
                // STOCK: Es para descontar en las otras máquinas los productos vendidos y entregados

                case "NT":  // "Tipo Trabajo/NT/Apellido/Nombre/DNI/Domicilio/Telefono/WhatsApp/Celular/Fecha_Nac_Cliente/AOIEL/AOICL/EjeOIL/AODEL/AODCL/EjeODL/Tipo_Cirstal_Lejos/AOIEC/AOICC/EjeOIC/AODEC/AODCC/EjeODC/Tipo_Cristal_Cerca/Fecha_Pedido/Fecha_Entregada_Pactada/Subido_A_Firebase/Nro_Orden/Subtotal/Seña/Resta_Abonar"

                            //  Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, 
                            //  Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, 
                            //  AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, 
                            //  Armazon_Cerca, Subtotal, Seña, Resta_Abonar

                            int nro_orden = Convert.ToInt32(words[1]);
                            Data.DataTrabajos.Apellido_Cliente = words[2];
                            Data.DataTrabajos.Nombre_Cliente = words[3];
                            Data.DataTrabajos.DNI_Cliente = words[4];
                            Data.DataTrabajos.Telefono_Cliente = words[5];
                    
                            

                            if (words[6] == "si")
                            {
                                Data.DataTrabajos.WhatsApp = true;
                            }
                            else
                            {
                                Data.DataTrabajos.WhatsApp = false;
                            }

                            if (words[7] == "si")
                            {
                                Data.DataTrabajos.Celular = true;
                            }
                            else
                            {
                                Data.DataTrabajos.Celular = false;
                            }

                            Data.DataTrabajos.Domicilio_Cliente = words[8];

                            Data.DataTrabajos.Tipo_Cristal_Lejos = words[9];
                            Data.DataTrabajos.AOIEL = Convert.ToDouble(words[10]);
                            Data.DataTrabajos.AOICL = Convert.ToDouble(words[11]);
                            Data.DataTrabajos.Eje_OIL = Convert.ToDouble(words[12]);

                            Data.DataTrabajos.AODEL = Convert.ToDouble(words[13]);
                            Data.DataTrabajos.AODCL = Convert.ToDouble(words[14]);
                            Data.DataTrabajos.Eje_ODL = Convert.ToDouble(words[15]);

                            Data.DataTrabajos.Tipo_Cristal_Cerca = words[17];
                            Data.DataTrabajos.AOIEC = Convert.ToDouble(words[18]);
                            Data.DataTrabajos.AOICC = Convert.ToDouble(words[18]);
                            Data.DataTrabajos.Eje_OIC = Convert.ToDouble(words[20]);

                            Data.DataTrabajos.AODEC = Convert.ToDouble(words[21]);
                            Data.DataTrabajos.AODCC = Convert.ToDouble(words[22]);
                            Data.DataTrabajos.Eje_ODC = Convert.ToDouble(words[23]);
                            

                            Data.DataTrabajos.Fecha_Pedido = words[24];
                            Data.DataTrabajos.Fecha_Entrega_Pactada = words[25];
                            Data.DataTrabajos.Fecha_Nac_Cliente = words[26];

                            Data.DataTrabajos.Subido_A_Firebase = Convert.ToBoolean(words[27]);
                            Data.DataTrabajos.Email_Cliente = words[28];

                            Data.DataTrabajos.Armazon_Lejos = words[29];
                            Data.DataTrabajos.Armazon_Cerca = words[30];

                            Data.DataTrabajos.Subtotal = Convert.ToInt32(words[31]);
                            Data.DataTrabajos.Seña = Convert.ToInt32(words[32]);
                            Data.DataTrabajos.Resta_Abonar = Convert.ToInt32(words[33]);

                            string sql_NT = String.Empty;

                            sql_NT += "insert into TRABAJOS (Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, ";
                            sql_NT += "Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, ";
                            sql_NT += "AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, ";
                            sql_NT += "Armazon_Cerca, Subtotal, Seña, Resta_Abonar) ";
                            sql_NT += "values('" + "TRABAJO" + "', '" + nro_orden + "', '" + Data.DataTrabajos.Apellido_Cliente + "',";
                            sql_NT += "'" + Data.DataTrabajos.Nombre_Cliente + "', '" + Data.DataTrabajos.DNI_Cliente + "', '" + Data.DataTrabajos.Telefono_Cliente + "', '" + Data.DataTrabajos.WhatsApp + "',";
                            sql_NT += "'" + Data.DataTrabajos.Celular + "', '" + Data.DataTrabajos.Domicilio_Cliente + "', '" + Data.DataTrabajos.Tipo_Cristal_Lejos + "',";
                            sql_NT += "'" + Data.DataTrabajos.AOIEL + "', '" + Data.DataTrabajos.AOICL + "', '" + Data.DataTrabajos.Eje_OIL + "',";
                            sql_NT += "'" + Data.DataTrabajos.AODEL + "', '" + Data.DataTrabajos.AODCL + "', '" + Data.DataTrabajos.Eje_ODL + "', '" + Data.DataTrabajos.Tipo_Cristal_Cerca + "',";
                            sql_NT += "'" + Data.DataTrabajos.AOIEC + "', '" + Data.DataTrabajos.AOICC + "', '" + Data.DataTrabajos.Eje_OIC + "',";
                            sql_NT += "'" + Data.DataTrabajos.AODEC + "', '" + Data.DataTrabajos.AODCC + "', '" + Data.DataTrabajos.Eje_ODC + "',";
                            sql_NT += "'" + Data.DataTrabajos.Fecha_Pedido + "', '" + Data.DataTrabajos.Fecha_Entrega_Pactada + "', '" + Data.DataTrabajos.Fecha_Nac_Cliente + "', '" + Data.DataTrabajos.Subido_A_Firebase + "',";
                            sql_NT += "'" + Data.DataTrabajos.Email_Cliente + "', '" + Data.DataTrabajos.Armazon_Lejos + "', '" + Data.DataTrabajos.Armazon_Cerca + "',";
                            sql_NT += "'" + Data.DataTrabajos.Subtotal + "', '" + Data.DataTrabajos.Seña + "', '" + Data.DataTrabajos.Resta_Abonar + "')";

                            SQLiteConnection conn_NT = new SQLiteConnection(miConexion);
                            SQLiteCommand cmd_NT = new SQLiteCommand(sql_NT, conn_NT);
                            conn_NT.Open();
                            cmd_NT.ExecuteNonQuery();
                            cmd_NT.Dispose();
                            conn_NT.Close();
                    
                            break;

                case "NR":  // "NR/Detalle Reparacion/Nro ORDEN/Apellido/Nombre/DNI/Telefono/Email/WhatsApp/Celular/Domicilio/Fecha_Pedido/Fecha_Entregada_Pactada/Fecha_Nac_Cliente"
                    
                            Data.DataTrabajos.Detalle_Reparacion = words[1];
                            Data.DataTrabajos.Nro_Orden = Convert.ToInt32(words[2]);
                            Data.DataTrabajos.Apellido_Cliente = words[3];
                            Data.DataTrabajos.Nombre_Cliente = words[4];
                            Data.DataTrabajos.DNI_Cliente = words[5];
                            Data.DataTrabajos.Telefono_Cliente = words[6];
                            Data.DataTrabajos.Email_Cliente = words[7];

                            if (words[8] == "si")
                            {
                                Data.DataTrabajos.Celular = true;
                            }
                            else
                            {
                                Data.DataTrabajos.Celular = false;
                            }

                            if (words[9] == "si")
                            {
                                Data.DataTrabajos.WhatsApp = true;
                            }
                            else
                            {
                                Data.DataTrabajos.WhatsApp = false;
                            }

                            Data.DataTrabajos.Domicilio_Cliente = words[10];
                            Data.DataTrabajos.Fecha_Pedido = words[11].Replace("-", "/");
                            Data.DataTrabajos.Fecha_Entrega_Pactada = words[12].Replace("-", "/");
                            Data.DataTrabajos.Fecha_Nac_Cliente = words[13].Replace("-", "/");

                            string sql_NR = "INSERT INTO TRABAJOS (Tipo_Trabajo, Reparacion, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, Email, WhatsApp, Celular, Domicilio, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente) VALUES ('" + "REPARACION" + "', '" + Data.DataTrabajos.Detalle_Reparacion + "', '" + Data.DataTrabajos.Nro_Orden + "', '" + Data.DataTrabajos.Apellido_Cliente + "', '" + Data.DataTrabajos.Nombre_Cliente + "', '" + Data.DataTrabajos.DNI_Cliente + "', '" + Data.DataTrabajos.Telefono_Cliente + "', '" + Data.DataTrabajos.Email_Cliente + "', '" + Data.DataTrabajos.WhatsApp + "', '" + Data.DataTrabajos.Celular + "', '" + Data.DataTrabajos.Domicilio_Cliente + "', '" + Data.DataTrabajos.Fecha_Pedido + "', '" + Data.DataTrabajos.Fecha_Entrega_Pactada + "', '" + Data.DataTrabajos.Fecha_Nac_Cliente + "')";

                            SQLiteConnection conn_NR = new SQLiteConnection(miConexion);
                            SQLiteCommand cmd_NR = new SQLiteCommand(sql_NR, conn_NR);
                            conn_NR.Open();
                            cmd_NR.ExecuteNonQuery();
                            cmd_NR.Dispose();
                            conn_NR.Close();

                            break;

                case "NRO_PC":
                            
                            if(words[1] != "1")
                            {
                                Funciones.Functions.Get_Info_Optica();
                                if(Data.Data_Optica.Computadora_Nro == 1)// && Data.Data_Optica.Computadora_Nro != 0)
                                {
                                    //  Enviamos datos de la optica

                                    string mensaje;

                                    string nombre = string.Empty;
                                    string tel_fijo = string.Empty;
                                    string calle = string.Empty;
                                    string nro = string.Empty;
                                    string instagram = string.Empty;
                                    string facebook = string.Empty;
                                    string whatsapp = string.Empty;
                                    string website = string.Empty;
                                    string email = string.Empty;
                                    string inicio1 = string.Empty;
                                    string fin1 = string.Empty;
                                    string inicio2 = string.Empty;
                                    string fin2 = string.Empty;
                                    string tipo_horario = string.Empty;
                                    string entre_calles = string.Empty;

                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();

                                    SQLiteDataReader sqlite_datareader;
                                    SQLiteCommand sqlite_cmd;
                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT Nombre, Telefono_Fijo, Calle, Numero, Instagram, Facebook, Whatsapp, Website, Email, Horario1, Horario2, Horario3, Horario4, Tipo_Horario, EntreCalles FROM DATOS_OPTICA";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        nombre = sqlite_datareader.GetValue(0).ToString();
                                        tel_fijo = sqlite_datareader.GetValue(1).ToString();
                                        calle = sqlite_datareader.GetValue(2).ToString();
                                        nro = sqlite_datareader.GetValue(3).ToString();
                                        instagram = sqlite_datareader.GetValue(4).ToString();
                                        facebook = sqlite_datareader.GetValue(5).ToString();
                                        whatsapp = sqlite_datareader.GetValue(6).ToString();
                                        website = sqlite_datareader.GetValue(7).ToString();
                                        email = sqlite_datareader.GetValue(8).ToString();
                                        inicio1 = sqlite_datareader.GetValue(9).ToString();
                                        fin1 = sqlite_datareader.GetValue(10).ToString();
                                        inicio2 = sqlite_datareader.GetValue(11).ToString();
                                        fin2 = sqlite_datareader.GetValue(12).ToString();
                                        tipo_horario = sqlite_datareader.GetValue(13).ToString();
                                        entre_calles = sqlite_datareader.GetValue(14).ToString();
                                    }

                                    sqlite_cmd.Dispose();
                                    miConexion.Close();

                                    mensaje  = "DATOS_OPTICA/" + nombre + "/" + tel_fijo + "/" + calle + "/" + nro + "/" + instagram + "/" + facebook + "/" + whatsapp + "/";
                                    mensaje += website + "/" + email + "/" + inicio1 + "/" + fin1 + "/" + inicio2 + "/" + fin2 + "/" + tipo_horario + "/" + entre_calles; 

                                    SendBroadcastMessage(mensaje);

                                    //  Enviamos NRO_ORDEN
                                    miConexion.Open();

                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT NRO FROM NRO_ORDEN";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        Data.DataTrabajos.Nro_Orden = Convert.ToInt32(sqlite_datareader.GetValue(0));
                                    }

                                    miConexion.Close();
                                    Funciones.Functions.SendBroadcastMessage("NRO_ORDEN/" + Data.DataTrabajos.Nro_Orden);

                                    //  Enviamos datos de trabajos
                                    miConexion.Open();

                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, Email, WhatsApp, Celular, Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, AODEC, AODCC, Eje_ODC, Armazon_Lejos, Armazon_Cerca, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase, Subtotal, Seña, Resta_Abonar FROM TRABAJOS";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        int nro_orden1 = Convert.ToInt32(sqlite_datareader.GetValue(0));
                                        string apellido_cliente = sqlite_datareader.GetValue(1).ToString();
                                        string nombre_cliente = sqlite_datareader.GetValue(2).ToString();
                                        string dni_cliente = sqlite_datareader.GetValue(3).ToString();
                                        string domicilio_cliente = sqlite_datareader.GetValue(8).ToString();
                                        string telefono_cliente = sqlite_datareader.GetValue(4).ToString();
                                        bool whatsapp_cliente = Convert.ToBoolean(sqlite_datareader.GetValue(6));
                                        bool celular_cliente = Convert.ToBoolean(sqlite_datareader.GetValue(7));
                                        string fecha_nac_cliente = sqlite_datareader.GetValue(27).ToString();
                                        string email_cliente = sqlite_datareader.GetValue(5).ToString();

                                        string aoiel = String.Empty;
                                        string aoicl = String.Empty;
                                        int eje_oil = -1;

                                        string aodel = String.Empty;
                                        string aodcl = String.Empty;
                                        int eje_odl = -1;
                                
                                        string cristal_lejos = String.Empty;
                                        string armazon_lejos = String.Empty;

                                        string aoiec = String.Empty;
                                        string aoicc = String.Empty;
                                        int eje_oic = -1;

                                        string aodec = String.Empty;
                                        string aodcc = String.Empty;
                                        int eje_odc = -1;
                                
                                        string cristal_cerca = String.Empty;
                                        string armazon_cerca = String.Empty;

                                        if (sqlite_datareader.GetValue(10) != DBNull.Value)
                                        {
                                            aoiel = sqlite_datareader.GetValue(10).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(11) != DBNull.Value)
                                        {
                                            aoicl = sqlite_datareader.GetValue(11).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(12) != DBNull.Value)
                                        {
                                            eje_oil = Convert.ToInt32(sqlite_datareader.GetValue(12));
                                        }

                                        if (sqlite_datareader.GetValue(13) != DBNull.Value)
                                        {
                                            aodel = (sqlite_datareader.GetValue(13).ToString());
                                        }

                                        if (sqlite_datareader.GetValue(14) != DBNull.Value)
                                        {
                                            aodcl = (sqlite_datareader.GetValue(14).ToString());
                                        }

                                        if (sqlite_datareader.GetValue(15) != DBNull.Value)
                                        {
                                            eje_odl = Convert.ToInt32(sqlite_datareader.GetValue(15));
                                        }

                                        if (sqlite_datareader.GetValue(9) != DBNull.Value)
                                        {
                                            cristal_lejos = sqlite_datareader.GetValue(9).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(23) != DBNull.Value)
                                        {
                                            armazon_lejos = sqlite_datareader.GetValue(23).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(17) != DBNull.Value)
                                        {
                                            aoiec = sqlite_datareader.GetValue(17).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(18) != DBNull.Value)
                                        {
                                            aoicc = sqlite_datareader.GetValue(18).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(19) != DBNull.Value)
                                        {
                                            eje_oic = Convert.ToInt32(sqlite_datareader.GetValue(19));
                                        }

                                        if (sqlite_datareader.GetValue(20) != DBNull.Value)
                                        {
                                            aodec = sqlite_datareader.GetValue(20).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(21) != DBNull.Value)
                                        {
                                            aodcc = sqlite_datareader.GetValue(21).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(22) != DBNull.Value)
                                        {
                                            eje_odc = Convert.ToInt32(sqlite_datareader.GetValue(22));
                                        }

                                        if (sqlite_datareader.GetValue(16) != DBNull.Value)
                                        {
                                            cristal_cerca = sqlite_datareader.GetValue(16).ToString();
                                        }

                                        if (sqlite_datareader.GetValue(24) != DBNull.Value)
                                        {
                                            armazon_cerca = sqlite_datareader.GetValue(24).ToString();
                                        }

                                        string fecha_recepcion = sqlite_datareader.GetValue(25).ToString();
                                        string fecha_para_retiro = sqlite_datareader.GetValue(26).ToString();

                                        bool subido_a_firebase = Convert.ToBoolean(sqlite_datareader.GetValue(28));

                                        int subtotal = Convert.ToInt32(sqlite_datareader.GetValue(29));
                                        int seña = Convert.ToInt32(sqlite_datareader.GetValue(30));
                                        int resta_abonar = Convert.ToInt32(sqlite_datareader.GetValue(31));

                                        //  Tipo_Trabajo, Nro_Orden, Apellido_Cliente, Nombre_Cliente, DNI, Telefono, WhatsApp, Celular, 
                                        //  Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, 
                                        //  AODEC, AODCC, Eje_ODC, Fecha_Pedido, Fecha_Entrega_Pactada, Fecha_Nacimiento_Cliente, Subido_A_Firebase , Email, Armazon_Lejos, 
                                        //  Armazon_Cerca, Subtotal, Seña, Resta_Abonar

                                        string trabajo_completo = string.Empty;

                                        trabajo_completo = "NT/" + nro_orden1 + "/" + apellido_cliente + "/" + nombre_cliente + "/" + dni_cliente + "/";
                                        trabajo_completo += telefono_cliente + "/";

                                        if (whatsapp_cliente)
                                        {
                                            trabajo_completo += "si";
                                        }
                                        else
                                        {
                                            trabajo_completo += "no";
                                        }

                                        trabajo_completo += "/";

                                        if (celular_cliente)
                                        {
                                            trabajo_completo += "si";
                                        }
                                        else
                                        {
                                            trabajo_completo += "no";
                                        }

                                        trabajo_completo += "/" + domicilio_cliente + "/" + cristal_lejos + "/" + aoiel + "/" + aoicl + "/";
                                        trabajo_completo += eje_oil + "/" + aodel + "/" + aodcl + "/" + eje_odl + "/" + cristal_cerca + "/";
                                        trabajo_completo += aoiec + "/" + aoicc + "/" + eje_oic;
                                        trabajo_completo += "/" + aodec + "/" + aodcc + "/" + eje_odc;
                                        trabajo_completo += "/" + fecha_recepcion.Replace("/", "-") + "/" + fecha_para_retiro.Replace("/", "-") + "/" + fecha_nac_cliente.Replace("/", "-") + "/";
                                        trabajo_completo += subido_a_firebase + "/" + email_cliente + "/" + armazon_lejos + "/" + armazon_cerca + "/";
                                        trabajo_completo += subtotal + "/" + seña + "/" + resta_abonar;

                                        Funciones.Functions.SendBroadcastMessage(trabajo_completo);
                                    }

                                    miConexion.Close();

                                    //  Enviamos datos de stock
                                    SendBroadcastMessage("Aceptado");
                                }
                            }
                                                        
                            break;

                case "DATOS_OPTICA":        //  READY

                            string sql = "DELETE FROM DATOS_OPTICA";
                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();
                            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();

                            //  (Nombre varchar(30), Telefono_Fijo varchar(20), Calle varchar(50), Numero int, Instagram varchar(40), Facebook varchar(40), Whatsapp varchar(20), Website varchar(40), Email varchar(40), Horario1 varchar(5), Horario2 varchar(5), Horario3 varchar(5), Horario4 varchar(5), Tipo_Horario varchar(15), EntreCalles varchar(40)

                            sql = "insert into DATOS_OPTICA(Nombre, Telefono_Fijo, Calle, Numero, Instagram, Facebook, Whatsapp, Website, Email, Horario1, Horario2, Horario3, Horario4, Tipo_Horario, EntreCalles) values('" + words[1] + "', '" + words[2] + "', '" + words[3] + "', '" + Convert.ToInt16(words[4]) + "', '" + words[5] + "', '" + words[6] + "', '" + words[7] + "', '" + words[8] + "', '" + words[9] + "', '" + words[10] + "', '" + words[11] + "', '" + words[12] + "', '" + words[13] + "', '" + words[14] + "', '" + words[15] + "')";
                            command1 = new SQLiteCommand(sql, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();

                            break;

                case "NRO_ORDEN":       //  READY
                            ActualizarNroOrden(Convert.ToInt32(words[1]));
                            break;

                case "GIFT_CARD":       //  READY

                            ActualizarNroGiftCard(Convert.ToInt32(words[1]));
                            break;

                case "VENTA_GIFT_CARD":

                            miConexion = new SQLiteConnection("Data source=database.sqlite3");
                            miConexion.Open();

                            string sql2 = "insert into GIFT_CARDS(Nro, Valor) values('" + Convert.ToInt32(words[1]) + "', '" + Convert.ToInt32(words[2]) + "')";
                            command1 = new SQLiteCommand(sql2, miConexion);
                            command1.Prepare();
                            command1.ExecuteNonQuery();
                            command1.Dispose();
                            miConexion.Close();
                            break;

                case "FIREBASE":        //  FALTA CORREGIR

                            if(words[1] == "CRISTALES")
                            {
                                if (CheckEmptyTable("TIPOS_CRISTALES") == false)
                                {
                                    //  No hay datos

                                    string sqlCristales = "insert into TIPOS_CRISTALES (Tipo, Precio) values ('" + words[2] + "', '" + words[3] + "')";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandCristales = new SQLiteCommand(sqlCristales, miConexion);
                                    commandCristales.Prepare();
                                    commandCristales.ExecuteNonQuery();
                                    commandCristales.Dispose();
                                    miConexion.Close();
                                }
                                else
                                {
                                    //  Hay datos
                                    //  Actualizamos los valores de los cristales

                                    string sqlCristales = "update TIPOS_CRISTALES set Precio = '" + words[3] + "' where Tipo='" + words[2] + "'";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandCristales = new SQLiteCommand(sqlCristales, miConexion);
                                    commandCristales.Prepare();
                                    commandCristales.ExecuteNonQuery();
                                    commandCristales.Dispose();
                                    miConexion.Close();
                                }
                            }

                            if(words[1] == "AS")
                            {
                                if (CheckEmptyTable("CODIGOS") == false)
                                {
                                    //  No hay datos

                                    string sqlAS = "insert into CODIGOS (Codigo, Precio) values ('" + words[2] + "', '" + words[3] + "')";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandAS = new SQLiteCommand(sqlAS, miConexion);
                                    commandAS.Prepare();
                                    commandAS.ExecuteNonQuery();
                                    commandAS.Dispose();
                                    miConexion.Close();
                                }
                                else
                                {
                                    //  Actualizamos los valores de los armazones de sol

                                    string sqlAS = "update CODIGOS set Precio = '" + words[3] + "' where Codigo='" + words[2] + "'";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandAS = new SQLiteCommand(sqlAS, miConexion);
                                    commandAS.Prepare();
                                    commandAS.ExecuteNonQuery();
                                    commandAS.Dispose();
                                    miConexion.Close();
                                }
                            }                            

                            if(words[1] == "AR")
                            {
                                if (CheckEmptyTable("CODIGOS") == false)
                                {
                                    //  No hay datos

                                    string sqlAS = "insert into CODIGOS (Codigo, Precio) values ('" + words[2] + "', '" + words[3] + "')";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandAS = new SQLiteCommand(sqlAS, miConexion);
                                    commandAS.Prepare();
                                    commandAS.ExecuteNonQuery();
                                    commandAS.Dispose();
                                    miConexion.Close();
                                }
                                else
                                {
                                    //  Actualizamos los valores de los armazones de sol

                                    string sqlAS = "update CODIGOS set Precio = '" + words[3] + "' where Codigo='" + words[2] + "'";
                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();
                                    SQLiteCommand commandAS = new SQLiteCommand(sqlAS, miConexion);
                                    commandAS.Prepare();
                                    commandAS.ExecuteNonQuery();
                                    commandAS.Dispose();
                                    miConexion.Close();
                                }
                            }
           
                            break;

                case "DESCUENTO":

                            //  MENSAJE
                            //  DESCUENTO/CRISTALES/codigo/Cant
                            if (words[1] == "CRISTALES")
                            {
                                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                miConexion.Open();

                                string sql4 = "select Codigo, Cantidad_Disponible from STOCK_CRISTALES";

                                SQLiteCommand cmd2 = new SQLiteCommand(sql4, miConexion);
                                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                                while (rdr2.Read())
                                {
                                    string sql3 = "UPDATE STOCK_CRISTALES SET Cantidad_Disponible = '" + (Convert.ToInt32(rdr2.GetValue(1)) - Convert.ToInt32(words[3])) + "'" + " WHERE Codigo = '" + words[2] + "'";
                                    SQLiteCommand command = new SQLiteCommand(sql3, miConexion);
                                    command.ExecuteNonQuery();
                                    command.Dispose();

                                    //  Ahora resta la parte de actualizar FIREBASE
                                    //  Primero leemos la cantidad en la nube y con ese valor vamos a basar el descuento

                                    string[] values = new string[3];
                                    values = words[2].Split(new char[] { '*', '/' });

                                    string sentencia = "Optica Falabella Av 38" + "/" + "Stock" + "/" + "Cristales" + "/" + values[0] + "/" + words[2];

                                    Descontar_En_FIREBASE(sentencia, Convert.ToInt32(words[3]));
                                }

                                miConexion.Close();
                            }

                            if (words[1] == "AR")
                            {
                                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                miConexion.Open();

                                string sql4 = "select Codigo, Cantidad_Disponible from STOCK_ARMAZONES_RECETA";

                                SQLiteCommand cmd2 = new SQLiteCommand(sql4, miConexion);
                                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                                while (rdr2.Read())
                                {
                                    string sql3 = "UPDATE STOCK_ARMAZONES_RECETA SET Cantidad_Disponible = '" + (Convert.ToInt32(rdr2.GetValue(1)) - Convert.ToInt32(words[3])) + "'" + " WHERE Codigo = '" + words[2] + "'";
                                    SQLiteCommand command = new SQLiteCommand(sql3, miConexion);
                                    command.ExecuteNonQuery();
                                    command.Dispose();

                                    string sentencia = "Optica Falabella Av 38" + "/" + "Stock" + "/" + "AR" + "/" + words[2];

                                    Descontar_En_FIREBASE(sentencia, Convert.ToInt32(words[3]));
                                }

                                miConexion.Close();
                            }

                            if (words[1] == "AS")
                            {
                                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                miConexion.Open();

                                string sql4 = "select Codigo, Cantidad_Disponible from STOCK_ARMAZONES_SOL";

                                SQLiteCommand cmd2 = new SQLiteCommand(sql4, miConexion);
                                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                                while (rdr2.Read())
                                {
                                    string sql3 = "UPDATE STOCK_ARMAZONES_SOL SET Cantidad_Disponible = '" + (Convert.ToInt32(rdr2.GetValue(1)) - Convert.ToInt32(words[3])) + "'" + " WHERE Codigo = '" + words[2] + "'";
                                    SQLiteCommand command = new SQLiteCommand(sql3, miConexion);
                                    command.ExecuteNonQuery();
                                    command.Dispose();

                                    string sentencia = "Optica Falabella Av 38" + "/" + "Stock" + "/" + "AS" + "/" + words[2];

                                    Descontar_En_FIREBASE(sentencia, Convert.ToInt32(words[3]));
                                }

                                miConexion.Close();
                            }

                            break;

                case "ELIMINAR_GIFT_CARD":

                            int gift_card = Convert.ToInt32(words[2]);
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
                            break;

                case "VENTAS":

                            //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
                            //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

                            SaveDataToExcelFile(msg);
                            break;

                case "STOCK": 
                            
                            if(words[1] == "AR")
                            {
                                string[] valores_AR = words[2].Split("*");
                                string marca = valores_AR[0];
                                string modelo = valores_AR[1];
                                string color = valores_AR[2];
                                string codigo = words[2];
                                int cantidad = Convert.ToInt32(words[3]);

                                if (CheckEmptyTable("STOCK_ARMAZONES_RECETA") == false)
                                {
                                    //  No hay datos en la tabla

                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();

                                    string sqlAgregar_AR = "insert into STOCK_ARMAZONES_RECETA(Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                                    SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                    commandAgregar_AR.Prepare();
                                    commandAgregar_AR.ExecuteNonQuery();
                                    commandAgregar_AR.Dispose();
                                    miConexion.Close();

                                }
                                else
                                {
                                    //  Hay datos. Debo revisar si existe o si debo solo agregarlo
                            
                                    bool found = false;
                                    miConexion.Open();

                                    SQLiteDataReader sqlite_datareader;
                                    SQLiteCommand sqlite_cmd;
                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_ARMAZONES_RECETA";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        if (sqlite_datareader.GetValue(0).ToString() == codigo)
                                        {
                                            string sqlUpdate_AR = "UPDATE STOCK_ARMAZONES_RECETA SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                                            SQLiteCommand commandUpdate_AR = new SQLiteCommand(sqlUpdate_AR, miConexion);
                                            commandUpdate_AR.ExecuteNonQuery();
                                            commandUpdate_AR.Dispose();

                                            found = true;
                                        }
                                    }

                                    if (found == false)
                                    {
                                        string sqlAgregar_AR = "insert into STOCK_ARMAZONES_RECETA (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                                        SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                        commandAgregar_AR.ExecuteNonQuery();
                                        commandAgregar_AR.Dispose();
                                    }

                                    miConexion.Close();
                                }
                            }

                            if(words[1] == "AS")
                            {
                                string[] valores_AR = words[2].Split("*");
                                string marca = valores_AR[0];
                                string modelo = valores_AR[1];
                                string color = valores_AR[2];
                                string codigo = words[2];
                                int cantidad = Convert.ToInt32(words[3]);

                                if (CheckEmptyTable("STOCK_ARMAZONES_SOL") == false)
                                {
                                    //  No hay datos en la tabla

                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();

                                    string sqlAgregar_AR = "insert into STOCK_ARMAZONES_SOL(Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                                    SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                    commandAgregar_AR.Prepare();
                                    commandAgregar_AR.ExecuteNonQuery();
                                    commandAgregar_AR.Dispose();
                                    miConexion.Close();

                                }
                                else
                                {
                                    //  Hay datos. Debo revisar si existe o si debo solo agregarlo

                                    bool found = false;
                                    miConexion.Open();

                                    SQLiteDataReader sqlite_datareader;
                                    SQLiteCommand sqlite_cmd;
                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_ARMAZONES_SOL";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        if (sqlite_datareader.GetValue(0).ToString() == codigo)
                                        {
                                            string sqlUpdate_AR = "UPDATE STOCK_ARMAZONES_SOL SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                                            SQLiteCommand commandUpdate_AR = new SQLiteCommand(sqlUpdate_AR, miConexion);
                                            commandUpdate_AR.ExecuteNonQuery();
                                            commandUpdate_AR.Dispose();

                                            found = true;
                                        }
                                    }

                                    if (found == false)
                                    {
                                        string sqlAgregar_AR = "insert into STOCK_ARMAZONES_SOL (Marca, Modelo, Color, Codigo, Cantidad_Disponible) values('" + marca + "', '" + modelo + "', '" + color + "', '" + codigo + "', '" + cantidad + "')";
                                        SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                        commandAgregar_AR.ExecuteNonQuery();
                                        commandAgregar_AR.Dispose();
                                    }

                                    miConexion.Close();
                                }
                            }

                            if (words[1] == "CRISTALES")
                            {
                                string[] valores_AR = words[2].Split("*");
                                string tipo = valores_AR[0];
                                string esferico = valores_AR[1];
                                string cilindrico = valores_AR[2];
                                string codigo = words[2];
                                int cantidad = Convert.ToInt32(words[3]);

                                if (CheckEmptyTable("STOCK_CRISTALES") == false)
                                {
                                    //  No hay datos en la tabla

                                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                                    miConexion.Open();

                                    string sqlAgregar_AR = "insert into STOCK_CRISTALES(Tipo, Esferico, Cilindrico, Codigo, Cantidad_Disponible) values('" + tipo + "', '" + esferico + "', '" + cilindrico + "', '" + codigo + "', '" + cantidad + "')";
                                    SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                    commandAgregar_AR.Prepare();
                                    commandAgregar_AR.ExecuteNonQuery();
                                    commandAgregar_AR.Dispose();
                                    miConexion.Close();

                                }
                                else
                                {
                                    //  Hay datos. Debo revisar si existe o si debo solo agregarlo

                                    bool found = false;
                                    miConexion.Open();

                                    SQLiteDataReader sqlite_datareader;
                                    SQLiteCommand sqlite_cmd;
                                    sqlite_cmd = miConexion.CreateCommand();
                                    sqlite_cmd.CommandText = "SELECT Codigo, Cantidad_Disponible FROM STOCK_CRISTALES";
                                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                                    while (sqlite_datareader.Read())
                                    {
                                        if (sqlite_datareader.GetValue(0).ToString() == codigo)
                                        {
                                            string sqlUpdate_AR = "UPDATE STOCK_CRISTALES SET Cantidad_Disponible = '" + cantidad + "'" + " WHERE Codigo = '" + codigo + "'";
                                            SQLiteCommand commandUpdate_AR = new SQLiteCommand(sqlUpdate_AR, miConexion);
                                            commandUpdate_AR.ExecuteNonQuery();
                                            commandUpdate_AR.Dispose();

                                            found = true;
                                        }
                                    }

                                    if (found == false)
                                    {
                                        string sqlAgregar_AR = "insert into STOCK_CRISTALES(Tipo, Esferico, Cilindrico, Codigo, Cantidad_Disponible) values('" + tipo + "', '" + esferico + "', '" + cilindrico + "', '" + codigo + "', '" + cantidad + "')";
                                        SQLiteCommand commandAgregar_AR = new SQLiteCommand(sqlAgregar_AR, miConexion);
                                        commandAgregar_AR.ExecuteNonQuery();
                                        commandAgregar_AR.Dispose();
                                    }

                                    miConexion.Close();
                                }
                            }
                            break;
            }
        }
        
        public static void ActualizarNroOrden(int nro)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            if (nro == 0)    //  No viene de otra PC
            {
                Data.Variables_Globales.nro_orden += 1;
                string sql = "UPDATE NRO_ORDEN SET NRO = '" + Data.Variables_Globales.nro_orden + "'";
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

                SendBroadcastMessage("NRO_ORDEN/" + Data.Variables_Globales.nro_orden);
            }
            else            //  Viene de otra PC. Hay que actualizar a ese valor
            {
                Data.Variables_Globales.nro_orden = nro;
                string sql = "UPDATE NRO_ORDEN SET NRO = '" + Data.Variables_Globales.nro_orden + "'";
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }   //  READY

        public static void ActualizarNroGiftCard(int nro)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            if (nro == 0)    //  No viene de otra PC
            {
                Data.Variables_Globales.nro_giftcard += 1;
                string sql = "UPDATE Nro_GiftCard SET Nro = '" + Data.Variables_Globales.nro_giftcard + "'";
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

                SendBroadcastMessage("GIFT_CARD/" + Data.Variables_Globales.nro_giftcard);
            }
            else            //  Viene de otra PC. Hay que actualizar a ese valor
            {
                Data.Variables_Globales.nro_giftcard = nro;
                string sql = "UPDATE Nro_GiftCard SET Nro = '" + Data.Variables_Globales.nro_giftcard + "'";
                SQLiteConnection conn = new SQLiteConnection(miConexion);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }   //  READY

        public static bool CheckEmptyTable(string cual)
        {
            string sql = "SELECT COUNT(*) from " + cual;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            SQLiteConnection conn = new SQLiteConnection(miConexion);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();

            try
            {
                int result = int.Parse(cmd.ExecuteScalar().ToString());

                if (result == 0)
                {
                    return false; //is empty 
                }
                else
                {
                    return true;//is not empty
                }
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }   //  READY

        public static int Check_Precio(string codigo)
        {
            int precio_verificado = 0;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Codigo, Precio FROM CODIGOS";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (sqlite_datareader.GetValue(0).ToString() == codigo)
                {
                    precio_verificado = Convert.ToInt32(sqlite_datareader.GetValue(1));
                }
            }

            miConexion.Close();

            return precio_verificado;
        }

        public static void SubirCodigos(string tabla)
        {
            string tipo = "";
            
            if(tabla == "STOCK_ARMAZONES_RECETA")
            {
                tipo = "AR";
            }

            if (tabla == "STOCK_ARMAZONES_SOL")
            {
                tipo = "AS";
            }

            if (tabla == "LIQUIDOS_LENTES_CONTACTO")
            {
                tipo = "LIQ";
            }

            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Codigo, Precio FROM " + tabla;
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                string sql2 = "insert into CODIGOS (Tipo_Articulo, Codigo, Precio) values ('" + tipo + "', '" + sqlite_datareader.GetValue(0).ToString() + "', '" + Convert.ToInt32(sqlite_datareader.GetValue(1)) + "')";
                SQLiteCommand command2 = new SQLiteCommand(sql2, miConexion);
                command2.ExecuteNonQuery();
                command2.Dispose();
            }            

            miConexion.Close();
        }   //  READY

        public static void CrearBaseDeDatos()
        {
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                miConexion = new SQLiteConnection("Data source=database.sqlite3");

                string sql1 =  "create table TIPOS_CRISTALES (Tipo varchar(30), Precio int)";
                string sql2 =  "create table STOCK_CRISTALES (Tipo varchar(30), Cantidad_Disponible int, Esferico real, Cilindrico real, Codigo varchar(60))";
                string sql3 =  "create table STOCK_ARMAZONES_RECETA (Marca varchar(30), Modelo varchar(30), Cantidad_Disponible int, Codigo varchar(60), Color varchar(20))";
                string sql4 =  "create table STOCK_ARMAZONES_SOL (Marca varchar(30), Modelo varchar(30), Cantidad_Disponible int, Codigo varchar(60), Color varchar(20))";
                string sql5 =  "create table LENTES_CONTACTO (Marca varchar(30), Tipo varchar(30), Cantidad_Disponible int, Graducacion varchar(10), Codigo varchar(60), Color varchar(20))";
                string sql6 =  "create table LENTES_CONTACTO_CONVENCIONALES (Marca varchar(30), Tipo varchar(30), Cantidad_Disponible int, Graducacion varchar(10), Codigo varchar(60))";
                string sql7 =  "create table LENTES_CONTACTO_COSMETICA (Marca varchar(30), Cantidad_Disponible int, Color varchar(20), Codigo varchar(60))";
                string sql8 =  "create table LIQUIDOS_LENTES_CONTACTO (Marca varchar(30), Cantidad_Disponible int, Tamaño int, Unidad_Tamaño varchar(10), Codigo varchar(60))";
                string sql9 =  "create table TRABAJOS (Tipo_Trabajo varchar(20), Reparacion varchar(1000), Nro_Orden int, Apellido_Cliente varchar(30), Nombre_Cliente varchar(30), DNI varchar(15), Telefono varchar(20), Email varchar(50), WhatsApp bool, Celular bool, Domicilio varchar(50), Tipo_Cristal_Lejos varchar(20), AOIEL varchar(10), AOICL varchar(10), Eje_OIL int, AODEL varchar(10), AODCL varchar(10), Eje_ODL int, Tipo_Cristal_Cerca varchar(20), AOIEC varchar(10), AOICC varchar(10), Eje_OIC int, AODEC varchar(10), AODCC varchar(10), Eje_ODC int, Armazon_Lejos varchar(100), Armazon_Cerca varchar(100), Fecha_Pedido varchar(20), Fecha_Entrega_Pactada varchar(20), Fecha_Pedido_Finalizado varchar(20), Fecha_Entrega_Real varchar(20), Fecha_Nacimiento_Cliente varchar(20), Subido_A_Firebase bool, Subtotal int, Seña int, Resta_Abonar int)";
                string sql10 = "create table USUARIO (Usuario varchar(20), Password varchar(20))";
                string sql11 = "create table PROVEEDORES (Marca varchar(50), Telefono varchar(20), Email varchar(50), Contacto varchar(60))";
                string sql12 = "create table VENDEDORES (Nombre varchar(20))";
                string sql13 = "create table DATOS_OPTICA (Nombre varchar(30), Telefono_Fijo varchar(20), Calle varchar(50), Numero int, Instagram varchar(40), Facebook varchar(40), Whatsapp varchar(20), Website varchar(40), Email varchar(40), Horario1 varchar(5), Horario2 varchar(5), Horario3 varchar(5), Horario4 varchar(5), Tipo_Horario varchar(15), EntreCalles varchar(40))";
                string sql14 = "create table NRO_ORDEN (NRO int)";
                string sql15 = "create table CODIGOS (Tipo_Articulo varchar(20), Codigo varchar(60), Precio int)";
                string sql16 = "create table CLIENTES (Nombre varchar(30), Apellido varchar(30), DNI varchar(15), Telefono varchar(20), Email varchar(50), Domicilio varchar(50), Nro_ORDEN int, Fecha_Nac varchar(20), WhatsApp bool, Celular bool)";
                string sql17 = "create table COMPUTADORA (Computadora_Nro int)";
                string sql18 = "create table VENTAS (Nro_Orden int, DNI_Cliente varchar(15), Fecha_Pedido varchar(15), Fecha_Entrega varchar(15), Metodo_Pago varchar(15), Monto_Abonado_1 int, Monto_Abonado_2 int, Comision_Pago_1 real, Comision_Pago_2 real)";
                string sql19 = "create table COMISIONES_TARJETA (Tipo string, Comision real)";
                string sql20 = "create table GIFT_CARDS (Nro int, Valor int)";
                string sql21 = "create table Nro_GiftCard (Nro int)";
                string sql22 = "create table REPARACIONES (Tipo varchar(50), Precio int)";
                string sql23 = "create table IPs (IP_Address string)";

                string nro_orden = "insert into NRO_ORDEN(NRO) values('" + 1 + "')";
                string nro_giftcard = "insert into Nro_GiftCard(Nro) values('" + 1 + "')";

                SQLiteCommand command1  =  new SQLiteCommand(sql1, miConexion);
                SQLiteCommand command2  =  new SQLiteCommand(sql2, miConexion);
                SQLiteCommand command3  =  new SQLiteCommand(sql3, miConexion);
                SQLiteCommand command4  =  new SQLiteCommand(sql4, miConexion);
                SQLiteCommand command5  =  new SQLiteCommand(sql5, miConexion);
                SQLiteCommand command6  =  new SQLiteCommand(sql6, miConexion);
                SQLiteCommand command7  =  new SQLiteCommand(sql7, miConexion);
                SQLiteCommand command8  =  new SQLiteCommand(sql8, miConexion);
                SQLiteCommand command9  =  new SQLiteCommand(sql9, miConexion);
                SQLiteCommand command10 =  new SQLiteCommand(sql10, miConexion);
                SQLiteCommand command11 =  new SQLiteCommand(sql11, miConexion);
                SQLiteCommand command12 =  new SQLiteCommand(sql12, miConexion);
                SQLiteCommand command13 =  new SQLiteCommand(sql13, miConexion);
                SQLiteCommand command14 =  new SQLiteCommand(sql14, miConexion);
                SQLiteCommand command15 =  new SQLiteCommand(sql15, miConexion);
                SQLiteCommand command16 =  new SQLiteCommand(sql16, miConexion);
                SQLiteCommand command17 =  new SQLiteCommand(sql17, miConexion);
                SQLiteCommand command18 =  new SQLiteCommand(sql18, miConexion);
                SQLiteCommand command19 =  new SQLiteCommand(sql19, miConexion);
                SQLiteCommand command20 =  new SQLiteCommand(sql20, miConexion);
                SQLiteCommand command21 =  new SQLiteCommand(sql21, miConexion);
                SQLiteCommand command22 =  new SQLiteCommand(sql22, miConexion);
                SQLiteCommand command23 =  new SQLiteCommand(sql23, miConexion);
                SQLiteCommand command24 =  new SQLiteCommand(nro_orden, miConexion);
                SQLiteCommand command25 =  new SQLiteCommand(nro_giftcard, miConexion);

                miConexion.Open();

                command1.Prepare();
                command2.Prepare();
                command3.Prepare();
                command4.Prepare();
                command5.Prepare();
                command6.Prepare();
                command7.Prepare();
                command8.Prepare();
                command9.Prepare();
                command10.Prepare();
                command11.Prepare();
                command12.Prepare();
                command13.Prepare();
                command14.Prepare();
                command15.Prepare();
                command16.Prepare();
                command17.Prepare();
                command18.Prepare();
                command19.Prepare();
                command20.Prepare();
                command21.Prepare();
                command22.Prepare();
                command23.Prepare();
                command24.Prepare();
                command25.Prepare();

                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                command5.ExecuteNonQuery();
                command6.ExecuteNonQuery();
                command7.ExecuteNonQuery();
                command8.ExecuteNonQuery();
                command9.ExecuteNonQuery();
                command10.ExecuteNonQuery();
                command11.ExecuteNonQuery();
                command12.ExecuteNonQuery();
                command13.ExecuteNonQuery();
                command14.ExecuteNonQuery();
                command15.ExecuteNonQuery();
                command16.ExecuteNonQuery();
                command17.ExecuteNonQuery();
                command18.ExecuteNonQuery();
                command19.ExecuteNonQuery();
                command20.ExecuteNonQuery();
                command21.ExecuteNonQuery();
                command22.ExecuteNonQuery();
                command23.ExecuteNonQuery();
                command24.ExecuteNonQuery();
                command25.ExecuteNonQuery();

                command1.Dispose();
                command2.Dispose();
                command3.Dispose();
                command4.Dispose();
                command5.Dispose();
                command6.Dispose();
                command7.Dispose();
                command8.Dispose();
                command9.Dispose();
                command10.Dispose();
                command11.Dispose();
                command12.Dispose();
                command13.Dispose();
                command14.Dispose();
                command14.Dispose();
                command15.Dispose();
                command16.Dispose();
                command17.Dispose();
                command18.Dispose();
                command19.Dispose();
                command20.Dispose();
                command21.Dispose();
                command22.Dispose();
                command23.Dispose();
                command24.Dispose();
                command25.Dispose();

                miConexion.Close();
                miConexion.Dispose();
            }
        }   //  READY

        public static void CreateDaySales()
        {
            string day, month, year;

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

            string path = @"C:\temp\Ventas_" + day + "-" + month + "-" + year + ".xlsx";
            if (!File.Exists(path))
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel no está correctamente instalado.");
                    return;
                }

                object misValue = System.Reflection.Missing.Value;

                Workbook xlWorkBook = xlApp.Workbooks.Add();
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.Add();

                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[2, 2]  = "Nro de ORDEN";
                xlWorkSheet.Cells[2, 3]  = "Tipo de venta";
                xlWorkSheet.Cells[2, 4]  = "Armazón LEJOS";
                xlWorkSheet.Cells[2, 5]  = "Cantidad";
                xlWorkSheet.Cells[2, 6]  = "Valor de VENTA";
                xlWorkSheet.Cells[2, 7]  = "Cristal LEJOS";
                xlWorkSheet.Cells[2, 8]  = "Cantidad";
                xlWorkSheet.Cells[2, 9]  = "Valor de VENTA";
                xlWorkSheet.Cells[2, 10] = "Armazón CERCA";
                xlWorkSheet.Cells[2, 11] = "Cantidad";
                xlWorkSheet.Cells[2, 12] = "Valor de VENTA";
                xlWorkSheet.Cells[2, 13] = "Cristal CERCA";
                xlWorkSheet.Cells[2, 14] = "Cantidad";
                xlWorkSheet.Cells[2, 15] = "Valor de VENTA";
                xlWorkSheet.Cells[2, 16] = "Costo REPARACION";
                xlWorkSheet.Cells[2, 17] = "Artículo";
                xlWorkSheet.Cells[2, 18] = "Cantidad";
                xlWorkSheet.Cells[2, 19] = "Valor de VENTA";
                xlWorkSheet.Cells[2, 20] = "Subtotal";
                xlWorkSheet.Cells[2, 21] = "Seña";
                xlWorkSheet.Cells[2, 22] = "Descuento Aplicado";
                xlWorkSheet.Cells[2, 23] = "Resta abonar";
                xlWorkSheet.Cells[2, 24] = "Método de PAGO";
                xlWorkSheet.Cells[2, 25] = "Cant CUOTAS";
                xlWorkSheet.Cells[2, 26] = "Comisión de VENTA";
                xlWorkSheet.Cells[2, 27] = "Pago c/ DESCUENTOS";
                xlWorkSheet.Cells[2, 28] = "Concepto";
                xlWorkSheet.Cells[2, 29] = "Fecha de Referencia";

                //xlWorkSheet.Range["B:Z"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //xlWorkSheet.Columns.AutoFit();

                xlWorkBook.SaveAs(path);

                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
        }

        public static void SaveDataToExcelFile(string what)
        {
            object misValue = System.Reflection.Missing.Value;
            string day, month, year;

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

            string path = @"C:\Temp\Ventas_" + day + "-" + month + "-" + year + ".xlsx";
            string[] data = what.Split('/');

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Workbook xlWorkBook = xlWorkBook = xlApp.Workbooks.Open(path);
            Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

            var cellValue = (xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 2] as Microsoft.Office.Interop.Excel.Range).Value;

            while(cellValue != null)
            {
                Data.Variables_Globales.rowIndex++;
                cellValue = (xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 2] as Microsoft.Office.Interop.Excel.Range).Value;
            }

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 3]   = data[1];                          //  Tipo de venta
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 2]   = data[2];                          //  Nro de ORDEN
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 4]   = data[3];                          //  Armazon LEJOS
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 5]   = data[4];                          //  CANTIDAD
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 6]   = data[5];                          //  VALOR ARMAZON LEJOS

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 7]   = data[6];                          //  Cristal LEJOS
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 8]   = data[7];                          //  CANTIDAD
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 9]   = data[8];                          //  VALOR CRISTAL LEJOS

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 10]  = data[9];                          //  Armazon CERCA
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 11]  = data[10];                         //  CANTIDAD
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 12]  = data[11];                         //  VALOR ARMAZON CERCA

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 13]  = data[12];                         //  Cristal CERCA
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 14]  = data[13];                         //  CANTIDAD
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 15]  = data[14];                         //  VALOR CRISTAL CERCA

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 16]  = data[15];                         //  Costo REPARACION
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 17]  = data[16];                         //  Artículo
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 18]  = data[17];                         //  Cantidad
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 19]  = data[18];                         //  Valor de VENTA

            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 20]  = data[19];                         //  SUBTOTAL
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 21]  = data[20];                         //  SEÑA
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 22]  = data[21];                         //  DESCUENTO APLICADO
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 23]  = data[22];                         //  RESTA ABONAR
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 24]  = data[23];                         //  METODO DE PAGO
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 25]  = data[24];                         //  CANTIDAD DE CUOTAS
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 26]  = data[25];                         //  COMISION DE VENTA
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 27]  = data[26];                         //  SUBTOTAL CON DESCUENTOS
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 28]  = data[27];                         //  CONCEPTO
            xlWorkSheet.Cells[Data.Variables_Globales.rowIndex, 29]  = data[28].Replace('-', '/');       //  FECHA DE REFERENCIA

            Data.Variables_Globales.rowIndex++;

            xlWorkSheet.Range["B:AC"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.AutoFit();

            xlWorkBook.Save();
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }

        public static void PrintDocument(string path)
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(path);
            doc.Print();
            /*
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@path)
            {
                UseShellExecute = true
            };
            p.Start();

            
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.Verb = "print";
                psi.FileName = path;
                psi.UseShellExecute = false;
                System.Diagnostics.Process.Start(psi);
            */
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                string host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }   //  READY

        public static void Descontar_En_FIREBASE(string cadena, int cant)
        {
            FirebaseResponse response = client.Get(cadena);

            Dictionary<string, int> data1 = JsonConvert.DeserializeObject<Dictionary<string, int>>(response.Body.ToString());

            int cantidad_vieja;
            int cantidad_nueva;

            cantidad_vieja = data1.ElementAt(0).Value;
            cantidad_nueva = cantidad_vieja - cant;

            response = client.Set(cadena, cantidad_nueva);
        }

        public void Delete_GiftCard(int gift_card)
        {
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nro FROM GIFT_CARDS";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (Convert.ToInt32(sqlite_datareader.GetValue(0)) == gift_card)
                {
                    //  El codigo está en la base de datos. Lo actualizo

                    using (SQLiteCommand command1 = new SQLiteCommand(miConexion))
                    {
                        command1.CommandText = "DELETE FROM GIFT_CARDS WHERE Nro='" + gift_card + "'";
                        command1.ExecuteNonQuery();
                    }

                    SendBroadcastMessage("ELIMINAR_GIFT_CARD/" + gift_card);
                }
            }

            miConexion.Close();
        }
    }
}
