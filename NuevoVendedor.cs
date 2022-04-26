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
    public partial class NuevoVendedor : Form
    {
        public SQLiteConnection miConexion;

        public NuevoVendedor()
        {
            InitializeComponent();
        }

        private void NuevoVendedor_Load(object sender, EventArgs e)
        {
            txt_Vendedor.Text = "";
        }

        private void btnArgregar_Clicked(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            string sql = "insert into VENDEDORES (Nombre) values('" + txt_Vendedor.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, miConexion);
            miConexion.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            miConexion.Close();

            MessageBox.Show("Vendedor: " + txt_Vendedor.Text + " agregado.");

            txt_Vendedor.Text = "";
        }

        private void btnCancelar__Clicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void NuevoVendedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
