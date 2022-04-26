using System;
using System.Windows.Forms;
using System.Data.SQLite;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Extensions;

namespace OpticaFalabella
{
    public partial class NuevoProveedor : Form
    {
        public SQLiteConnection miConexion;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AJSFlcSrhvKqrKFhIjcIwzM5Av8nw9oNN80GrJq5",
            BasePath = "https://rtd-optica-falabella-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public NuevoProveedor()
        {
            InitializeComponent();
        }

        private void NuevoProveedor_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            txt_Marca.Text = "";
            txt_Telefono.Text = "";
            txt_Email.Text = "";
            txt_Contacto.Text = "";
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (txt_Email.Text.Contains("@") == true) //    CORREGIR
            {
                client = new FireSharp.FirebaseClient(config);
                miConexion = new SQLiteConnection("Data source=database.sqlite3");

                string sqlDatos = "INSERT INTO PROVEEDORES(Marca, Telefono, Email, Contacto) VALUES('" + txt_Marca.Text + "', '" + txt_Telefono.Text + "', '" + txt_Email.Text + "', '" + txt_Contacto.Text + "')";

                miConexion.Open();

                SQLiteCommand cmd = new SQLiteCommand(sqlDatos, miConexion);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                miConexion.Close();

                txt_Marca.Text = "";
                txt_Telefono.Text = "";
                txt_Email.Text = "";
                txt_Contacto.Text = "";
                this.Close();
            }
            else 
            {
                MessageBox.Show("El email debe ser un correo electrónico válido.");
            }
        }

        private void btn_Borrar_Click(object sender, EventArgs e)
        {
            txt_Marca.Text = "";
            txt_Telefono.Text = "";
            txt_Email.Text = "";
        }

        private void NuevoProveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
