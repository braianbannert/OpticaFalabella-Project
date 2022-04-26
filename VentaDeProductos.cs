using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Extensions;
using System.Data.SQLite;

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class VentaDeProductos : Form
    {
        double subtotal = 0.0f;
        double total = 0.0f;
        double valor_final = 0.0f;
        int valor_gift_card = 0;
        string previous_readQR = String.Empty;

        public SQLiteConnection miConexion;
        int item = 1;
        //int total = 0;

        public VentaDeProductos()
        {
            InitializeComponent();
        }

        private void VentaDeProductos_FormLoad(object sender, EventArgs e)
        {
            txt_NroGiftCard.Visible = false;
            groupBox4.Visible = false;
            groupBox_Descuento.Visible = false;

            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            lbl_Codigo.Text = String.Empty;
            txt_Codigo.Text = String.Empty;

            DataGridViewColumn column0 = new DataGridViewColumn();
            column0.Name = "Item";
            column0.HeaderText = "Item";
            DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
            column0.CellTemplate = dgvcell;
            dataGridView1.Columns.Add(column0);

            DataGridViewColumn column1 = new DataGridViewColumn();
            column1.Name = "Producto";
            column1.HeaderText = "Producto";
            column1.CellTemplate = dgvcell;
            dataGridView1.Columns.Add(column1);

            DataGridViewColumn column2 = new DataGridViewColumn();
            column2.Name = "Cantidad";
            column2.HeaderText = "Cantidad";
            column2.CellTemplate = dgvcell;
            dataGridView1.Columns.Add(column2);

            DataGridViewColumn column3 = new DataGridViewColumn();
            column3.Name = "Precio";
            column3.HeaderText = "Precio";
            column3.CellTemplate = dgvcell;
            dataGridView1.Columns.Add(column3);

            DataGridViewColumn column5 = new DataGridViewColumn();
            column5.Name = "Codigo";
            column5.HeaderText = "Código";
            column5.CellTemplate = dgvcell;
            dataGridView1.Columns.Add(column5);

            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            txt_Codigo.Enabled = true;
            txt_Codigo.Visible = true;

            txt_Codigo.Focus();
        }

        private void VentaDeProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }

        private void btn_VenderClicked(object sender, EventArgs e)
        {
            int     filas = dataGridView1.Rows.Count;
            string  codigo;
            double  precio;
            int     cantidad;
            string  what;
            string  tipo_articulo;

            string forma_de_pago = String.Empty;
            string cuotas = String.Empty;
            
            double subtotal_parcial;
            double subtotal_c_desc;

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

            for (int k = 0; k < filas - 1; k++)
            { 
                codigo = dataGridView1.Rows[k].Cells["Codigo"].Value.ToString();
                precio = Convert.ToInt32(dataGridView1.Rows[k].Cells["Precio"].Value);
                cantidad = Convert.ToInt32(dataGridView1.Rows[k].Cells["Cantidad"].Value);

                subtotal_parcial = precio * cantidad;

                what = "VENTAS/FINAL///////////////" + codigo + "/" + cantidad.ToString() + "/" + precio.ToString() + "/" + string.Format("{0:,0.00}", subtotal_parcial) + "/" + "/";
                what += "/" + "/" + "/" + "/" + "/" + "/" + "/" + "Item individual" + "/" + fecha_referencia;

                Funciones.Functions.SaveDataToExcelFile(what);
                Funciones.Functions.SendBroadcastMessage(what);

                subtotal += precio * cantidad;

                if(dataGridView1.Rows[k].Cells["Producto"].Value.ToString() == "Armazón RECETA")
                {
                    string tipo = string.Empty;
                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                    SQLiteConnection conn = new SQLiteConnection(miConexion);
                    conn.Open();
                    string sql = "select Codigo, Cantidad_Disponible from STOCK_ARMAZONES_RECETA";

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (dataGridView1.Rows[k].Cells["Codigo"].Value.ToString() == rdr.GetValue(0).ToString())
                        {
                            using (SQLiteCommand command = new SQLiteCommand(miConexion))
                            {
                                command.CommandText = "update STOCK_ARMAZONES_RECETA set Cantidad_Disponible = :cant where Codigo=:codigo";
                                command.Parameters.Add("cant", DbType.String).Value = Convert.ToInt32(rdr.GetValue(1)) - 1;
                                command.Parameters.Add("codigo", DbType.String).Value = dataGridView1.Rows[k].Cells["Codigo"].Value.ToString();
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    rdr.Close();
                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
                else
                {
                    if (dataGridView1.Rows[k].Cells["Producto"].Value.ToString() == "Armazón SOL")
                    {
                        string tipo = string.Empty;
                        miConexion = new SQLiteConnection("Data source=database.sqlite3");
                        SQLiteConnection conn = new SQLiteConnection(miConexion);
                        conn.Open();
                        string sql = "select Codigo, Cantidad_Disponible from STOCK_ARMAZONES_SOL";

                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        SQLiteDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            if (dataGridView1.Rows[k].Cells["Codigo"].Value.ToString() == rdr.GetValue(0).ToString())
                            {
                                using (SQLiteCommand command = new SQLiteCommand(miConexion))
                                {
                                    command.CommandText = "update STOCK_ARMAZONES_SOL set Cantidad_Disponible = :cant where Codigo=:codigo";
                                    command.Parameters.Add("cant", DbType.String).Value = Convert.ToInt32(rdr.GetValue(1)) - 1;
                                    command.Parameters.Add("codigo", DbType.String).Value = dataGridView1.Rows[k].Cells["Codigo"].Value.ToString();
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        rdr.Close();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                    }
                    else
                    {
                        if (dataGridView1.Rows[k].Cells["Producto"].Value.ToString() == "Líquido Limpia Cristales")
                        {

                        }
                        else
                        {
                            if (dataGridView1.Rows[k].Cells["Producto"].Value.ToString() == "Lente Contacto")
                            {

                            }
                            else
                            {
                                if (dataGridView1.Rows[k].Cells["Producto"].Value.ToString() == "Líquido Lente Contacto")
                                {

                                }
                            }
                        }
                    }
                }
                
            }

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

            if (chk_GiftCard.Checked)
            {
                subtotal += total;
            }

            subtotal_c_desc = (subtotal * (100 - comision_tarjeta)) / 100;

            //  VENTAS/Trabajo/Nro ORDEN/Ar LEJOS/Cant/Valor/Cristal LEJOS/Cant/Valor/Ar CERCA/Cant/Valor/Cristal CERCA/Cant/Valor/Costo REPARACION/Articulo/Cant/Valor/Subtotal/
            //  Metodo PAGO/Cant CUOTAS/Comision VENTA/Subtotal con DESCUENTOS/Concepto/Fecha de Referencia

            what = "VENTAS/FINAL///////////////" + "/" + "/" + "/" + "/" + "/" + "/" + subtotal + "/" + forma_de_pago;
            what += "/" + cuotas + "/" + comision_tarjeta.ToString() + "/" + string.Format("{0:,0.00}", subtotal_c_desc) + "/" + "Total items anteriores" + "/" + fecha_referencia;

            Funciones.Functions.SaveDataToExcelFile(what);
            Funciones.Functions.SendBroadcastMessage(what);

            if (chk_GiftCard.Checked)
            {
                int gift_card = Convert.ToInt32(txt_NroGiftCard.Text);
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

                Funciones.Functions.SendBroadcastMessage("ELIMINAR_GIFT_CARD/" + gift_card);
            }
        }

        private void chk_Credito_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Credito.Checked == true)
            {
                combo_Cuotas.Visible = true;
            }
            else
            {
                combo_Cuotas.Visible = false;
            }
        }   //  READY

        private void chk_GiftCard_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_GiftCard.Checked == true)
            {
                txt_NroGiftCard.Visible = true;
            }
            else
            {
                txt_NroGiftCard.Visible = false;
            }
        }

        private void txt_NroGiftCard_LostFocus(object sender, EventArgs e)
        {
            total = 0;
            miConexion = new SQLiteConnection("Data source=database.sqlite3");

            miConexion.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = miConexion.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Nro, Valor FROM GIFT_CARDS";
            sqlite_cmd.CommandType = System.Data.CommandType.Text;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                if (Convert.ToInt32(sqlite_datareader.GetValue(0)) == Convert.ToInt32(txt_NroGiftCard.Text))
                {
                    valor_gift_card = Convert.ToInt32(sqlite_datareader.GetValue(1));
                    total -= Convert.ToInt32(sqlite_datareader.GetValue(1));
                }
            }

            miConexion.Close();
        }

        private void txt_Codigo_TextChanged(object sender, EventArgs e)
        {
            string articulo = String.Empty;

            if (previous_readQR == String.Empty)
            {
                previous_readQR = txt_Codigo.Text;
                lbl_Codigo.Text = String.Empty;
                lbl_Codigo.Text = previous_readQR;

                miConexion = new SQLiteConnection("Data source=database.sqlite3");
                miConexion.Open();

                string sql2 = "select Tipo_Articulo, Codigo, Precio from CODIGOS";

                SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (txt_Codigo.Text == rdr2.GetValue(1).ToString())
                    {
                        switch (rdr2.GetValue(0).ToString())
                        {
                            case "AR":
                                articulo = "Armazón RECETA";
                                break;

                            case "AS":
                                articulo = "Armazón SOL";
                                break;

                            case "LIQ":
                                articulo = "Líquido Limpia Cristales";
                                break;

                            case "LC":
                                articulo = "Lente Contacto";
                                break;

                            case "LIQ-LC":
                                articulo = "Líquido Lente Contacto";
                                break;
                        }

                        dataGridView1.Rows.Add(item, articulo, 1, Convert.ToInt32(rdr2.GetValue(2)), lbl_Codigo.Text);

                        total += Convert.ToInt32(rdr2.GetValue(2));
                        item++;
                    }
                }

                subtotal = total;

                txt_Total1.Text = total.ToString();
                valor_final = total;
                lbl_ValorFinal.Text = "$ " + valor_final.ToString();

                miConexion.Close();
                timer1.Enabled = true;
            }
            else
            {
                if ((previous_readQR != txt_Codigo.Text) && (txt_Codigo.Text != String.Empty))
                {
                    previous_readQR = txt_Codigo.Text;
                    lbl_Codigo.Text = String.Empty;
                    lbl_Codigo.Text = previous_readQR;

                    miConexion = new SQLiteConnection("Data source=database.sqlite3");
                    miConexion.Open();

                    string sql2 = "select Tipo_Articulo, Codigo, Precio from CODIGOS";

                    SQLiteCommand cmd2 = new SQLiteCommand(sql2, miConexion);
                    SQLiteDataReader rdr2 = cmd2.ExecuteReader();

                    while (rdr2.Read())
                    {
                        if (txt_Codigo.Text == rdr2.GetValue(1).ToString())
                        {
                            switch (rdr2.GetValue(0).ToString())
                            {
                                case "AR":
                                    articulo = "Armazón RECETA";
                                    break;

                                case "AS":
                                    articulo = "Armazón SOL";
                                    break;

                                case "LIQ":
                                    articulo = "Líquido Limpia Cristales";
                                    break;

                                case "LC":
                                    articulo = "Lente Contacto";
                                    break;

                                case "LIQ-LC":
                                    articulo = "Líquido Lente Contacto";
                                    break;
                            }

                            dataGridView1.Rows.Add(item, articulo, 1, Convert.ToInt32(rdr2.GetValue(2)), lbl_Codigo.Text);

                            total += Convert.ToInt32(rdr2.GetValue(2));
                            item++;
                        }
                    }

                    txt_Total1.Text = total.ToString();
                    valor_final = total;
                    lbl_ValorFinal.Text = "$ " + valor_final.ToString();

                    miConexion.Close();
                    timer1.Enabled = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            txt_Codigo.Text = String.Empty;
        }

        private void VentaDeProductos_FormActivated(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            txt_Codigo.Focus();
        }

        private void dataGridView_RowRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int total = 0;

            for(int i=0; i< dataGridView1.RowCount; i++)
            {
                total += Convert.ToInt32(dataGridView1.Rows[i].Cells["Precio"].Value);
            }

            txt_Total1.Text = total.ToString();
            valor_final = total;
            lbl_ValorFinal.Text = "$ " + valor_final.ToString();
            txt_Codigo.Focus();
        }

        private void chk_Efectivo_CheckChanged(object sender, EventArgs e)
        {
            if (chk_Efectivo.Checked == true)
            {
                groupBox4.Visible = true;
                groupBox_Descuento.Visible = true;

                if (txt_Descuento.Text != String.Empty)
                {
                    valor_final = valor_final * (100 - Convert.ToDouble(txt_Descuento.Text)) / 100;

                    lbl_ValorFinal.Text = "$ " + valor_final.ToString();
                }
            }
            else
            {
                groupBox4.Visible = false;
                groupBox_Descuento.Visible = false;
            }
        }
    }
}
