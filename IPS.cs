using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Extensions;
using System.Data.SQLite;

using System.Threading;

using QRCoder;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

using Newtonsoft.Json;

namespace OpticaFalabella
{
    public partial class IPS : Form
    {
        public SQLiteConnection miConexion;

        public IPS()
        {
            InitializeComponent();
        }

        private void IPS_Load(object sender, EventArgs e)
        {
            txt_IP.Text = "";
            txt_IP.Focus();

            miConexion = new SQLiteConnection("Data source=database.sqlite3");
        }

        private void btn_Add_Clicked(object sender, EventArgs e)
        {
            miConexion.Open();
            string sql = "insert into IPs(IP_Address) values('" + txt_IP.Text + "')";

            SQLiteCommand command1 = new SQLiteCommand(sql, miConexion);
            command1.Prepare();
            command1.ExecuteNonQuery();
            command1.Dispose();
            miConexion.Close();
            miConexion.Dispose();
        }

        private void IP_Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            this.Dispose();
            form1.Show();
            this.Close();
        }
    }
}
