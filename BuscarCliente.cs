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
    public partial class BuscarCliente : Form
    {
        public SQLiteConnection miConexion;

        public BuscarCliente()
        {
            InitializeComponent();
        }

        private void BuscarCliente_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            comboBox1.Text = "";
            txt_Busqueda.Text = "";
            txt_Apellido.Text = "";
            txt_DNI.Text = "";
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Email.Text = "";
            txt_Domicilio.Text = "";

            dataGridView1.Columns.Add("Nro_Orden", "Nº Orden");
            dataGridView1.Columns.Add("Apellido", "Apellido");
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("DNI", "DNI");
            dataGridView1.Columns.Add("Fecha_Realizacion", "Fecha");
            dataGridView1.Columns.Add("TCL", "Cristal LEJOS");

            dataGridView1.Columns.Add("AODEL", "Esf");
            dataGridView1.Columns.Add("AODCL", "Cil");
            dataGridView1.Columns.Add("EjeOD_L", "Eje");
            dataGridView1.Columns.Add("AOIEL", "Esf");
            dataGridView1.Columns.Add("AOICL", "Cil");
            dataGridView1.Columns.Add("EjeOI_L", "Eje");

            dataGridView1.Columns.Add("TCC", "Cristal CERCA");
            
            dataGridView1.Columns.Add("AODEC", "Esf");
            dataGridView1.Columns.Add("AODCC", "Cil");
            dataGridView1.Columns.Add("EjeOD_C", "Eje");
            dataGridView1.Columns.Add("AOIEC", "Esf");
            dataGridView1.Columns.Add("AOICC", "Cil");
            dataGridView1.Columns.Add("EjeOI_C", "Eje");

        }

        private void btn_BorrarClicked(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            txt_Busqueda.Text = "";
            txt_Apellido.Text = "";
            txt_DNI.Text = "";
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Email.Text = "";
            txt_Domicilio.Text = "";
        }

        private void btn_CancelarClicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void BuscarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_BuscarCliente_Clicked(object sender, EventArgs e)
        {
            //  CLIENTES (Nombre varchar(30), Apellido varchar(30), DNI varchar(12), Telefono varchar(20), Email varchar(50), Domicilio varchar(50))

            if (comboBox1.Text == "DNI")
            {
                miConexion.Open();

                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = miConexion.CreateCommand();
                sqlite_cmd.CommandText = "SELECT Nro_Orden, DNI, Apellido_Cliente, Nombre_Cliente, Telefono, Email, Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, AODEC, AODCC, Eje_ODC, Fecha_Pedido FROM TRABAJOS";
                sqlite_cmd.CommandType = System.Data.CommandType.Text;

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    if (sqlite_datareader.GetValue(1).ToString() == txt_Busqueda.Text)
                    {
                        txt_Apellido.Text = sqlite_datareader.GetValue(2).ToString();
                        txt_Nombre.Text = sqlite_datareader.GetValue(3).ToString();
                        txt_Domicilio.Text = sqlite_datareader.GetValue(6).ToString();
                        txt_Telefono.Text = sqlite_datareader.GetValue(4).ToString();
                        txt_Email.Text = sqlite_datareader.GetValue(5).ToString();
                        txt_DNI.Text = sqlite_datareader.GetValue(1).ToString();
                        dataGridView1.Rows.Add(sqlite_datareader.GetValue(0).ToString(), txt_Apellido.Text, txt_Nombre.Text, txt_DNI.Text, sqlite_datareader.GetValue(21).ToString(), sqlite_datareader.GetValue(7).ToString(), sqlite_datareader.GetValue(8).ToString(), sqlite_datareader.GetValue(9).ToString(), sqlite_datareader.GetValue(10).ToString(), sqlite_datareader.GetValue(11).ToString(), sqlite_datareader.GetValue(12).ToString(), sqlite_datareader.GetValue(13).ToString(), sqlite_datareader.GetValue(14).ToString(), sqlite_datareader.GetValue(15).ToString(), sqlite_datareader.GetValue(16).ToString(), sqlite_datareader.GetValue(17).ToString(), sqlite_datareader.GetValue(18).ToString(), sqlite_datareader.GetValue(19).ToString(), sqlite_datareader.GetValue(20).ToString());
                    }
                }

                miConexion.Close();
            }
            else
            {
                if (comboBox1.Text == "Apellido")
                {
                    miConexion.Open();

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = miConexion.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT Nro_Orden, DNI, Apellido_Cliente, Nombre_Cliente, Telefono, Email, Domicilio, Tipo_Cristal_Lejos, AOIEL, AOICL, Eje_OIL, AODEL, AODCL, Eje_ODL, Tipo_Cristal_Cerca, AOIEC, AOICC, Eje_OIC, AODEC, AODCC, Eje_ODC, Fecha_Pedido FROM TRABAJOS";
                    sqlite_cmd.CommandType = System.Data.CommandType.Text;

                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    while (sqlite_datareader.Read())
                    {

                        if (sqlite_datareader.GetValue(2).ToString() == txt_Busqueda.Text.ToUpper())
                        {
                            txt_Apellido.Text = sqlite_datareader.GetValue(2).ToString();
                            txt_Nombre.Text = sqlite_datareader.GetValue(3).ToString();
                            txt_Domicilio.Text = sqlite_datareader.GetValue(6).ToString();
                            txt_Telefono.Text = sqlite_datareader.GetValue(4).ToString();
                            txt_Email.Text = sqlite_datareader.GetValue(5).ToString();
                            txt_DNI.Text = sqlite_datareader.GetValue(1).ToString();
                            dataGridView1.Rows.Add(sqlite_datareader.GetValue(0).ToString(), txt_Apellido.Text, txt_Nombre.Text, txt_DNI.Text, sqlite_datareader.GetValue(21).ToString(), sqlite_datareader.GetValue(7).ToString(), sqlite_datareader.GetValue(8).ToString(), sqlite_datareader.GetValue(9).ToString(), sqlite_datareader.GetValue(10).ToString(), sqlite_datareader.GetValue(11).ToString(), sqlite_datareader.GetValue(12).ToString(), sqlite_datareader.GetValue(13).ToString(), sqlite_datareader.GetValue(14).ToString(), sqlite_datareader.GetValue(15).ToString(), sqlite_datareader.GetValue(16).ToString(), sqlite_datareader.GetValue(17).ToString(), sqlite_datareader.GetValue(18).ToString(), sqlite_datareader.GetValue(19).ToString(), sqlite_datareader.GetValue(20).ToString());
                        }
                    }

                    miConexion.Close();
                }
                else
                {
                    MessageBox.Show("Ingrese un parámetro de búsqueda válido.");
                }
            }
        }

        private void btn_RehacerTrabajo_Clicked(object sender, EventArgs e)
        {
            //  Verificamos primero cuál de todas las filas es la que está seleccionada
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            Data.DataTrabajos.PROCEDENCIA = "BUSQUEDA";

            Data.DataTrabajos.Apellido_Cliente = dataGridView1.Rows[selectedRowCount-1].Cells["Apellido"].Value.ToString();
            Data.DataTrabajos.Nombre_Cliente = dataGridView1.Rows[selectedRowCount-1].Cells["Nombre"].Value.ToString();
            Data.DataTrabajos.DNI_Cliente = dataGridView1.Rows[selectedRowCount - 1].Cells["DNI"].Value.ToString();
            Data.DataTrabajos.Fecha_Pedido = dataGridView1.Rows[selectedRowCount - 1].Cells["Fecha_Realizacion"].Value.ToString();

            try
            {
                Data.DataTrabajos.Tipo_Cristal_Lejos = dataGridView1.Rows[selectedRowCount - 1].Cells["TCL"].Value.ToString();
                Data.DataTrabajos.AOIEL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AOIEL"].Value.ToString())/100;
                Data.DataTrabajos.AOICL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AOICL"].Value.ToString())/100;
                Data.DataTrabajos.Eje_OIL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["EjeOI_L"].Value.ToString());

                Data.DataTrabajos.AODEL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AODEL"].Value.ToString())/100;
                Data.DataTrabajos.AODCL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AODCL"].Value.ToString())/100;
                Data.DataTrabajos.Eje_ODL = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["EjeOD_L"].Value.ToString());

                if((Data.DataTrabajos.AOIEL != 0.00f) || (Data.DataTrabajos.AOICL != 0.00f) || (Data.DataTrabajos.AODEL != 0.00f) || (Data.DataTrabajos.AODCL != 0.00f))
                {
                    Data.DataTrabajos.anteojo_lejos = true;
                }
            }
            catch 
            { 
            
            }

            try
            {
                Data.DataTrabajos.Tipo_Cristal_Cerca = dataGridView1.Rows[selectedRowCount - 1].Cells["TCC"].Value.ToString();
                Data.DataTrabajos.AOIEC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AOIEC"].Value.ToString())/100;
                Data.DataTrabajos.AOICC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AOICC"].Value.ToString())/100;
                Data.DataTrabajos.Eje_OIC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["EjeOI_C"].Value.ToString());

                Data.DataTrabajos.AODEC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AODEC"].Value.ToString())/100;
                Data.DataTrabajos.AODCC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["AODCC"].Value.ToString())/100;
                Data.DataTrabajos.Eje_ODC = Convert.ToDouble(dataGridView1.Rows[selectedRowCount - 1].Cells["EjeOD_C"].Value.ToString());

                if ((Data.DataTrabajos.AOIEC != 0.00f) || (Data.DataTrabajos.AOICC != 0.00f) || (Data.DataTrabajos.AODEC != 0.00f) || (Data.DataTrabajos.AODCC != 0.00f))
                {
                    Data.DataTrabajos.anteojo_cerca = true;
                }
            }
            catch
            {

            }

            Form nuevoTrabajo = new NuevoTrabajoForm();
            nuevoTrabajo.Show();
            this.Hide();
            this.Dispose();
            this.Close();
        }
    }
}
