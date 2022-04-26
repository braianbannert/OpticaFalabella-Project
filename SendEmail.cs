using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net;
using System.Net.Mail;

namespace OpticaFalabella
{
    public partial class SendEmail : Form
    {
        public SQLiteConnection miConexion;

        public SendEmail()
        {
            InitializeComponent();
        }

        private void SendEmail_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            SQLiteConnection conn = new SQLiteConnection(miConexion);

            string sql = "select Email from DATOS_OPTICA";

            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                txt_CorreoOrigen.Text = Convert.ToString(rdr.GetValue(0));
            }

            rdr.Close();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            string line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"C:\temp\Pedido-" + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + ".txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    richTXT_Mensaje.Text += line + "\n";
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void EnviarEmail_ItemClicked(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress(txt_CorreoOrigen.Text, "Optica Falabella");
            var toAddress = new MailAddress(txt_CorreoDestino.Text, "Braian Bannert");
            const string fromPassword = "Agustinfalabella1";
            string titulo = txt_Titulo.Text;
            string mensaje = richTXT_Mensaje.Text;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = titulo,
                Body = mensaje
            })
            {
                smtp.Send(message);
            }

            this.Hide();

            MessageBox.Show("Correo enviado a " + txt_CorreoDestino.Text);

            this.Close();
        }

        private void Cancelar_ItemClicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void SendEmailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
