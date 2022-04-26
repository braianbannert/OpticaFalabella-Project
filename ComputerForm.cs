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
    public partial class ComputerForm : Form
    {
        public SQLiteConnection miConexion;

        public ComputerForm()
        {
            InitializeComponent();
        }

        private void btn_Agregar_Clicked(object sender, EventArgs e)
        {
            miConexion.Open();
            string sql = "insert into COMPUTADORA(Computadora_Nro) values('" + Convert.ToInt16(txt_NroComputadora.Text) + "')";

            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
            command1.Prepare();
            command1.ExecuteNonQuery();
            command1.Dispose();
            miConexion.Close();
            miConexion.Dispose();

            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_Cancelar_Clicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void ComputerForm_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
        }

        private void Computadora_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
