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
    public partial class DatosOptica : Form
    {
        public SQLiteConnection miConexion;

        public DatosOptica()
        {
            InitializeComponent();
        }

        private void btn_Guardar_ItemClicked(object sender, EventArgs e)
        {
            string Whatsapp;
            string NombreOptica;
            string Telefono;
            string Facebook;
            string Instagram;
            string telRestoModificado;
            string tipo_horario = string.Empty;

            NombreOptica = txt_NombreOptica.Text;
            Facebook = txt_Facebook.Text;
            Instagram = txt_Instagram.Text;
            telRestoModificado = txt_WhatsappResto.Text.Insert(3, "-");
            Whatsapp = txt_WhatsappPrefijo.Text + "-" + telRestoModificado;
            telRestoModificado = txt_TelefonoRestoOptica.Text.Insert(3, "-");
            Telefono = "0-" + txt_TelefonoPrefijoOptica.Text + "-" + telRestoModificado;

            if(combo_Horario.Text == "CORRIDO")
            {
                tipo_horario = "CORRIDO";
            }
            else
            {
                if(combo_Horario.Text == "PARTIDO")
                {
                    tipo_horario = "PARTIDO";
                }
            }

            string sql = "DELETE FROM DATOS_OPTICA";
            miConexion = new SQLiteConnection("Data source=database.sqlite3");
            miConexion.Open();
            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
            command1.Prepare();
            command1.ExecuteNonQuery();
            command1.Dispose();

            //  (Nombre varchar(30), Telefono_Fijo varchar(20), Calle varchar(50), Numero int, Instagram varchar(40), Facebook varchar(40), Whatsapp varchar(20), Website varchar(40), Email varchar(40), Horario1 varchar(5), Horario2 varchar(5), Horario3 varchar(5), Horario4 varchar(5), Tipo_Horario varchar(15), EntreCalles varchar(40)

            sql = "insert into DATOS_OPTICA(Nombre, Telefono_Fijo, Calle, Numero, Instagram, Facebook, Whatsapp, Website, Email, Horario1, Horario2, Horario3, Horario4, Tipo_Horario, EntreCalles) values('" + NombreOptica + "', '" + Telefono + "', '" + txt_DomicilioOptica.Text + "', '" + Convert.ToInt16(txt_Numero.Text) + "', '" + Instagram + "', '" + Facebook + "', '" + Whatsapp + "', '" + txt_Website.Text + "', '" + txt_Email.Text + "', '" + txt_Inicio1.Text + "', '" + txt_Fin1.Text + "', '" + txt_Inicio2.Text + "', '" + txt_Fin2.Text + "', '" + tipo_horario + "', '" + txt_EntreCalles.Text + "')";
            command1 = new SQLiteCommand(sql, miConexion);
            command1.Prepare();
            command1.ExecuteNonQuery();
            command1.Dispose();
            miConexion.Close();

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
            string tipo_horario2 = string.Empty;
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
            miConexion.Dispose();

            mensaje = "DATOS_OPTICA/" + nombre + "/" + tel_fijo + "/" + calle + "/" + nro + "/" + instagram + "/" + facebook + "/" + whatsapp + "/";
            mensaje += website + "/" + email + "/" + inicio1 + "/" + fin1 + "/" + inicio2 + "/" + fin2 + "/" + tipo_horario2 + "/" + entre_calles;

            Funciones.Functions.SendBroadcastMessage(mensaje);

            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_Cancelar_ItemClicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void DatosOpticaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void Horario_ItemChanged(object sender, EventArgs e)
        {

        }

        private void DatosOptica_Load(object sender, EventArgs e)
        {
            string[] tel = new string[4];
            string[] whatsapp = new string[3];

            if (Funciones.Functions.CheckEmptyTable("DATOS_OPTICA"))
            {
                //  (Nombre varchar(30), Telefono_Fijo varchar(20), Calle varchar(50), Numero int, Instagram varchar(40), Facebook varchar(40), Whatsapp varchar(20), Website varchar(40), Email varchar(40), Horario1 varchar(5), Horario2 varchar(5), Horario3 varchar(5), Horario4 varchar(5), Tipo_Horario varchar(15), EntreCalles varchar(40)
                
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
                    txt_NombreOptica.Text = sqlite_datareader.GetValue(0).ToString();
                    
                    tel = sqlite_datareader.GetValue(1).ToString().Split("-");
                    txt_TelefonoPrefijoOptica.Text = tel[1].ToString();
                    txt_TelefonoRestoOptica.Text = tel[2].ToString() + tel[3].ToString();

                    txt_DomicilioOptica.Text = sqlite_datareader.GetValue(2).ToString();
                    txt_Numero.Text = sqlite_datareader.GetValue(3).ToString();
                    txt_Instagram.Text = sqlite_datareader.GetValue(4).ToString();
                    txt_Facebook.Text = sqlite_datareader.GetValue(5).ToString();

                    whatsapp = sqlite_datareader.GetValue(6).ToString().Split("-");
                    txt_WhatsappPrefijo.Text = whatsapp[0].ToString();
                    txt_WhatsappResto.Text = whatsapp[1].ToString() + whatsapp[2].ToString();

                    txt_Website.Text = sqlite_datareader.GetValue(7).ToString();
                    txt_Email.Text = sqlite_datareader.GetValue(8).ToString();
                    txt_Inicio1.Text = sqlite_datareader.GetValue(9).ToString();
                    txt_Fin1.Text = sqlite_datareader.GetValue(10).ToString();
                    txt_Inicio2.Text = sqlite_datareader.GetValue(11).ToString();
                    txt_Fin2.Text = sqlite_datareader.GetValue(12).ToString();
                    combo_Horario.SelectedItem = sqlite_datareader.GetValue(13).ToString();
                    txt_EntreCalles.Text = sqlite_datareader.GetValue(14).ToString();                    
                }

                sqlite_cmd.Dispose();
                miConexion.Close();
                miConexion.Dispose();
            }            
        }
    }
}
