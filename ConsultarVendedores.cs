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
    public partial class ConsultarVendedores : Form
    {
        public SQLiteConnection miConexion;

        public ConsultarVendedores()
        {
            InitializeComponent();
        }

        private void ConsultarVendedores_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nombre FROM VENDEDORES";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                cmb_Vendedor.Items.Add(sqlite_datareader.GetValue(0));
            }

            miConexion.Close();
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btnBorrar_Clicked(object sender, EventArgs e)
        {
            miConexion.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(miConexion))
            {
                cmd.CommandText = "DELETE FROM VENDEDORES WHERE Nombre='" + cmb_Vendedor.Text + "'";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            miConexion.Close();

            MessageBox.Show("El vendedor " + cmb_Vendedor.Text + " ha sido borrado");
            cmb_Vendedor.Items.Remove(cmb_Vendedor.Text);
            cmb_Vendedor.Text = "";
        }

        private void ConsultarVendedores_Closed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
