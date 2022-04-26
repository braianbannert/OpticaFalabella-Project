using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SQLite;

namespace OpticaFalabella
{
    public partial class VerificarPrecio : Form
    {
        string previous_readQR = String.Empty;

        public static SQLiteConnection miConexion;

        public VerificarPrecio()
        {
            InitializeComponent();
        }

        private void VerificarPrecio_Load(object sender, EventArgs e)
        {
            txt_QRRead.Enabled = true;
            txt_QRRead.Visible = true;
            txt_QRRead.Text = String.Empty;
            txt_QRRead.Focus();
        }

        private void VerificadorPrecio_Closed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            txt_QRRead.Text = String.Empty;
        }

        private void txt_QRRead_TextChanged2(object sender, EventArgs e)
        {
            if(previous_readQR == String.Empty)
            {
                previous_readQR = txt_QRRead.Text;
                lbl_Codigo.Text = String.Empty;
                lbl_Codigo.Text = previous_readQR;

                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                miConexion.Open();

                string sql2 = "select Codigo, Precio from CODIGOS";

                SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (txt_QRRead.Text == rdr2.GetValue(0).ToString())
                    {
                        lbl_Precio.Text = "$ " + rdr2.GetValue(1).ToString();
                    }
                }

                miConexion.Close();
                timer1.Enabled = true;
            }
            else
            {
                if((previous_readQR != txt_QRRead.Text) && (txt_QRRead.Text != String.Empty))
                {
                    previous_readQR = txt_QRRead.Text;
                    lbl_Codigo.Text = String.Empty;
                    lbl_Codigo.Text = previous_readQR;

                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                    miConexion.Open();

                    string sql2 = "select Codigo, Precio from CODIGOS";

                    SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                    SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                    while (rdr2.Read())
                    {
                        if (txt_QRRead.Text == rdr2.GetValue(0).ToString())
                        {
                            lbl_Precio.Text = "$ " + rdr2.GetValue(1).ToString();
                        }
                    }

                    miConexion.Close();
                    timer1.Enabled = true;
                }
            }
        }
    }
}
