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
    public partial class GiftCard : Form
    {
        public SQLiteConnection miConexion;

        public GiftCard()
        {
            InitializeComponent();
        }

        private void GiftCard_Load(object sender, EventArgs e)
        {
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nro FROM Nro_GiftCard";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Data.DataTrabajos.Nro_GiftCard = Convert.ToInt32(sqlite_datareader.GetValue(0));
            }

            lbl_GiftCardNumber.Text = Data.DataTrabajos.Nro_GiftCard.ToString();

            miConexion.Close();
        }

        private void btn_Cancel_Clicked(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_Vender_Clicked(object sender, EventArgs e)
        {
            string forma_de_pago = String.Empty;
            string cuotas = String.Empty;
            double subtotal = 0.0f;
            double subtotal_c_desc;

            string what;
            string sqlDatos = "INSERT INTO GIFT_CARDS (Nro, Valor) VALUES ('" + lbl_GiftCardNumber.Text + "', '" + Convert.ToInt32(txt_PrecioGiftCard.Text) + "')";

            miConexion.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlDatos, miConexion);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            miConexion.Close();

            Funciones.Functions.SendBroadcastMessage("VENTA_GIFT_CARD/" + Data.DataTrabajos.Nro_GiftCard.ToString() + "/" + txt_PrecioGiftCard.Text);
            Data.DataTrabajos.Nro_GiftCard = Convert.ToInt32(lbl_GiftCardNumber.Text) + 1;
            Funciones.Functions.ActualizarNroGiftCard(Data.DataTrabajos.Nro_GiftCard);
            Funciones.Functions.SendBroadcastMessage("GIFT_CARD/" + Data.DataTrabajos.Nro_GiftCard.ToString());
            
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

            subtotal_c_desc = (subtotal * (100 - comision_tarjeta)) / 100;

            string day;
            string month;
            string fecha_referencia;

            if (dateTimePicker1.Value.Day < 10)
            {
                day = "0" + dateTimePicker1.Value.Day.ToString();
            }
            else
            {
                day = dateTimePicker1.Value.Day.ToString();
            }

            if (dateTimePicker1.Value.Month < 10)
            {
                month = "0" + dateTimePicker1.Value.Month.ToString();
            }
            else
            {
                month = dateTimePicker1.Value.Month.ToString();
            }

            fecha_referencia = day + "-" + month + "-" + dateTimePicker1.Value.Year;

            //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
            //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

            what = "VENTAS/GIFT CARD///////////////GIFT CARD/1/";
            what += Convert.ToInt32(txt_PrecioGiftCard.Text) + "/" + Convert.ToInt32(txt_PrecioGiftCard.Text);
            what += "////" + forma_de_pago + "/" + cuotas + "/" + comision_tarjeta + "/" + subtotal_c_desc + "/Total/" + fecha_referencia; 

            Funciones.Functions.SaveDataToExcelFile(what);

            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        

        private void GiftCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
