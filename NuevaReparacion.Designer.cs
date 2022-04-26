
namespace OpticaFalabella
{
    partial class NuevaReparacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaReparacion));
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dtp_FechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.dtp_FechaRecepcion = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.btn_Borrar = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.ckbox_Celular = new System.Windows.Forms.CheckBox();
            this.dt_FechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label44 = new System.Windows.Forms.Label();
            this.txt_EmailCliente = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_DomicilioCliente = new System.Windows.Forms.TextBox();
            this.txt_DNI1 = new System.Windows.Forms.TextBox();
            this.txt_Nombre1 = new System.Windows.Forms.TextBox();
            this.txt_Apellido1 = new System.Windows.Forms.TextBox();
            this.txt_TelResto = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txt_TelPrefijo = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.chk_WhatsApp = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbl_NroOrden1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Subtotal1 = new System.Windows.Forms.TextBox();
            this.txt_Total1 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_Seña1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_Cuotas = new System.Windows.Forms.ComboBox();
            this.chk_Credito = new System.Windows.Forms.CheckBox();
            this.chk_Debito = new System.Windows.Forms.CheckBox();
            this.chk_Efectivo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_REPARACION = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_Titulo.Location = new System.Drawing.Point(17, 2);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(274, 39);
            this.lbl_Titulo.TabIndex = 0;
            this.lbl_Titulo.Text = "Optica FALABELLA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Firebrick;
            this.label4.Location = new System.Drawing.Point(23, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Descripción del problema:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(26, 386);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(501, 242);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // dtp_FechaEntrega
            // 
            this.dtp_FechaEntrega.Location = new System.Drawing.Point(169, 225);
            this.dtp_FechaEntrega.Name = "dtp_FechaEntrega";
            this.dtp_FechaEntrega.Size = new System.Drawing.Size(293, 27);
            this.dtp_FechaEntrega.TabIndex = 41;
            this.dtp_FechaEntrega.ValueChanged += new System.EventHandler(this.dtp_FechaEntrega_ValueChanged);
            // 
            // dtp_FechaRecepcion
            // 
            this.dtp_FechaRecepcion.Location = new System.Drawing.Point(169, 192);
            this.dtp_FechaRecepcion.Name = "dtp_FechaRecepcion";
            this.dtp_FechaRecepcion.Size = new System.Drawing.Size(293, 27);
            this.dtp_FechaRecepcion.TabIndex = 40;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label26.Location = new System.Drawing.Point(32, 229);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(132, 20);
            this.label26.TabIndex = 39;
            this.label26.Text = "Fecha de Entrega:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label25.Location = new System.Drawing.Point(14, 197);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(149, 20);
            this.label25.TabIndex = 38;
            this.label25.Text = "Fecha de Recepción:";
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Imprimir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Imprimir.ForeColor = System.Drawing.Color.White;
            this.btn_Imprimir.Location = new System.Drawing.Point(739, 57);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(116, 46);
            this.btn_Imprimir.TabIndex = 77;
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.UseVisualStyleBackColor = false;
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_ItemClicked);
            // 
            // btn_Borrar
            // 
            this.btn_Borrar.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_Borrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Borrar.ForeColor = System.Drawing.Color.White;
            this.btn_Borrar.Location = new System.Drawing.Point(739, 111);
            this.btn_Borrar.Name = "btn_Borrar";
            this.btn_Borrar.Size = new System.Drawing.Size(116, 46);
            this.btn_Borrar.TabIndex = 78;
            this.btn_Borrar.Text = "Borrar";
            this.btn_Borrar.UseVisualStyleBackColor = false;
            this.btn_Borrar.Click += new System.EventHandler(this.btn_Borrar_ItemClicked);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Segoe UI Black", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label50.Location = new System.Drawing.Point(392, 7);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(159, 31);
            this.label50.TabIndex = 104;
            this.label50.Text = "Nº de Orden:";
            // 
            // ckbox_Celular
            // 
            this.ckbox_Celular.AutoSize = true;
            this.ckbox_Celular.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ckbox_Celular.Location = new System.Drawing.Point(376, 158);
            this.ckbox_Celular.Name = "ckbox_Celular";
            this.ckbox_Celular.Size = new System.Drawing.Size(86, 24);
            this.ckbox_Celular.TabIndex = 130;
            this.ckbox_Celular.Text = "Celular?";
            this.ckbox_Celular.UseVisualStyleBackColor = true;
            this.ckbox_Celular.CheckedChanged += new System.EventHandler(this.chk_Celular_CheckedChanged);
            // 
            // dt_FechaNacimiento
            // 
            this.dt_FechaNacimiento.Location = new System.Drawing.Point(496, 120);
            this.dt_FechaNacimiento.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dt_FechaNacimiento.Name = "dt_FechaNacimiento";
            this.dt_FechaNacimiento.Size = new System.Drawing.Size(184, 27);
            this.dt_FechaNacimiento.TabIndex = 129;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label44.Location = new System.Drawing.Point(396, 123);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(84, 20);
            this.label44.TabIndex = 128;
            this.label44.Text = "Fecha Nac:";
            // 
            // txt_EmailCliente
            // 
            this.txt_EmailCliente.Location = new System.Drawing.Point(103, 121);
            this.txt_EmailCliente.Name = "txt_EmailCliente";
            this.txt_EmailCliente.Size = new System.Drawing.Size(246, 27);
            this.txt_EmailCliente.TabIndex = 127;
            this.txt_EmailCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label43.Location = new System.Drawing.Point(42, 124);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(51, 20);
            this.label43.TabIndex = 126;
            this.label43.Text = "Email:";
            // 
            // txt_DomicilioCliente
            // 
            this.txt_DomicilioCliente.Location = new System.Drawing.Point(497, 86);
            this.txt_DomicilioCliente.Name = "txt_DomicilioCliente";
            this.txt_DomicilioCliente.Size = new System.Drawing.Size(183, 27);
            this.txt_DomicilioCliente.TabIndex = 125;
            this.txt_DomicilioCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_DNI1
            // 
            this.txt_DNI1.Location = new System.Drawing.Point(497, 51);
            this.txt_DNI1.Name = "txt_DNI1";
            this.txt_DNI1.Size = new System.Drawing.Size(183, 27);
            this.txt_DNI1.TabIndex = 124;
            this.txt_DNI1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_DNI1.Leave += new System.EventHandler(this.txt_DNI_LostFocus);
            // 
            // txt_Nombre1
            // 
            this.txt_Nombre1.Location = new System.Drawing.Point(103, 87);
            this.txt_Nombre1.Name = "txt_Nombre1";
            this.txt_Nombre1.Size = new System.Drawing.Size(246, 27);
            this.txt_Nombre1.TabIndex = 123;
            this.txt_Nombre1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Apellido1
            // 
            this.txt_Apellido1.Location = new System.Drawing.Point(103, 52);
            this.txt_Apellido1.Name = "txt_Apellido1";
            this.txt_Apellido1.Size = new System.Drawing.Size(246, 27);
            this.txt_Apellido1.TabIndex = 122;
            this.txt_Apellido1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_TelResto
            // 
            this.txt_TelResto.Location = new System.Drawing.Point(230, 156);
            this.txt_TelResto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_TelResto.Name = "txt_TelResto";
            this.txt_TelResto.Size = new System.Drawing.Size(119, 27);
            this.txt_TelResto.TabIndex = 121;
            this.txt_TelResto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(187, 156);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(34, 27);
            this.textBox5.TabIndex = 120;
            this.textBox5.Text = "15";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_TelPrefijo
            // 
            this.txt_TelPrefijo.Location = new System.Drawing.Point(130, 156);
            this.txt_TelPrefijo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_TelPrefijo.Name = "txt_TelPrefijo";
            this.txt_TelPrefijo.Size = new System.Drawing.Size(49, 27);
            this.txt_TelPrefijo.TabIndex = 119;
            this.txt_TelPrefijo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(103, 156);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(20, 27);
            this.textBox7.TabIndex = 118;
            this.textBox7.Text = "0";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(406, 89);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 18);
            this.label14.TabIndex = 117;
            this.label14.Text = "Domicilio:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(449, 54);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 18);
            this.label15.TabIndex = 116;
            this.label15.Text = "DNI:";
            // 
            // chk_WhatsApp
            // 
            this.chk_WhatsApp.AutoSize = true;
            this.chk_WhatsApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chk_WhatsApp.Location = new System.Drawing.Point(475, 158);
            this.chk_WhatsApp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_WhatsApp.Name = "chk_WhatsApp";
            this.chk_WhatsApp.Size = new System.Drawing.Size(115, 22);
            this.chk_WhatsApp.TabIndex = 115;
            this.chk_WhatsApp.Text = "WhatsApp?";
            this.chk_WhatsApp.UseVisualStyleBackColor = true;
            this.chk_WhatsApp.CheckedChanged += new System.EventHandler(this.chk_Whatsapp_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(16, 159);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 18);
            this.label16.TabIndex = 114;
            this.label16.Text = "Teléfono:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(24, 90);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 18);
            this.label17.TabIndex = 113;
            this.label17.Text = "Nombre:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(24, 55);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 18);
            this.label18.TabIndex = 112;
            this.label18.Text = "Apellido:";
            // 
            // lbl_NroOrden1
            // 
            this.lbl_NroOrden1.AutoSize = true;
            this.lbl_NroOrden1.Font = new System.Drawing.Font("Segoe UI Black", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lbl_NroOrden1.Location = new System.Drawing.Point(557, 7);
            this.lbl_NroOrden1.Name = "lbl_NroOrden1";
            this.lbl_NroOrden1.Size = new System.Drawing.Size(0, 31);
            this.lbl_NroOrden1.TabIndex = 131;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Subtotal1);
            this.groupBox1.Controls.Add(this.txt_Total1);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txt_Seña1);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(596, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 125);
            this.groupBox1.TabIndex = 145;
            this.groupBox1.TabStop = false;
            // 
            // txt_Subtotal1
            // 
            this.txt_Subtotal1.Location = new System.Drawing.Point(120, 23);
            this.txt_Subtotal1.Name = "txt_Subtotal1";
            this.txt_Subtotal1.Size = new System.Drawing.Size(101, 27);
            this.txt_Subtotal1.TabIndex = 71;
            this.txt_Subtotal1.Text = "0";
            this.txt_Subtotal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Subtotal1.TextChanged += new System.EventHandler(this.txt_Subtotal_TextChanged);
            // 
            // txt_Total1
            // 
            this.txt_Total1.Location = new System.Drawing.Point(120, 89);
            this.txt_Total1.Name = "txt_Total1";
            this.txt_Total1.Size = new System.Drawing.Size(101, 27);
            this.txt_Total1.TabIndex = 73;
            this.txt_Total1.Text = "0";
            this.txt_Total1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label27.Location = new System.Drawing.Point(24, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(87, 20);
            this.label27.TabIndex = 38;
            this.label27.Text = "SUBTOTAL:";
            // 
            // txt_Seña1
            // 
            this.txt_Seña1.Location = new System.Drawing.Point(120, 55);
            this.txt_Seña1.Name = "txt_Seña1";
            this.txt_Seña1.Size = new System.Drawing.Size(101, 27);
            this.txt_Seña1.TabIndex = 72;
            this.txt_Seña1.Text = "0";
            this.txt_Seña1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Seña1.TextChanged += new System.EventHandler(this.txt_Seña_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(57, 58);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(52, 20);
            this.label28.TabIndex = 39;
            this.label28.Text = "SEÑA:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label29.Location = new System.Drawing.Point(53, 92);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 20);
            this.label29.TabIndex = 40;
            this.label29.Text = "TOTAL:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_Cuotas);
            this.groupBox2.Controls.Add(this.chk_Credito);
            this.groupBox2.Controls.Add(this.chk_Debito);
            this.groupBox2.Controls.Add(this.chk_Efectivo);
            this.groupBox2.Location = new System.Drawing.Point(596, 481);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 147);
            this.groupBox2.TabIndex = 146;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PAGO";
            // 
            // combo_Cuotas
            // 
            this.combo_Cuotas.FormattingEnabled = true;
            this.combo_Cuotas.Items.AddRange(new object[] {
            "1",
            "3",
            "6",
            "12"});
            this.combo_Cuotas.Location = new System.Drawing.Point(121, 103);
            this.combo_Cuotas.Name = "combo_Cuotas";
            this.combo_Cuotas.Size = new System.Drawing.Size(84, 28);
            this.combo_Cuotas.TabIndex = 121;
            // 
            // chk_Credito
            // 
            this.chk_Credito.AutoSize = true;
            this.chk_Credito.Location = new System.Drawing.Point(30, 105);
            this.chk_Credito.Name = "chk_Credito";
            this.chk_Credito.Size = new System.Drawing.Size(80, 24);
            this.chk_Credito.TabIndex = 120;
            this.chk_Credito.Text = "Crédito";
            this.chk_Credito.UseVisualStyleBackColor = true;
            this.chk_Credito.CheckedChanged += new System.EventHandler(this.chk_Credito_CheckedChanged);
            // 
            // chk_Debito
            // 
            this.chk_Debito.AutoSize = true;
            this.chk_Debito.Location = new System.Drawing.Point(30, 77);
            this.chk_Debito.Name = "chk_Debito";
            this.chk_Debito.Size = new System.Drawing.Size(77, 24);
            this.chk_Debito.TabIndex = 119;
            this.chk_Debito.Text = "Débito";
            this.chk_Debito.UseVisualStyleBackColor = true;
            // 
            // chk_Efectivo
            // 
            this.chk_Efectivo.AutoSize = true;
            this.chk_Efectivo.Location = new System.Drawing.Point(31, 49);
            this.chk_Efectivo.Name = "chk_Efectivo";
            this.chk_Efectivo.Size = new System.Drawing.Size(84, 24);
            this.chk_Efectivo.TabIndex = 118;
            this.chk_Efectivo.Text = "Efectivo";
            this.chk_Efectivo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 147;
            this.label1.Text = "Tipo de REPARACION: ";
            // 
            // cmb_REPARACION
            // 
            this.cmb_REPARACION.FormattingEnabled = true;
            this.cmb_REPARACION.Location = new System.Drawing.Point(187, 286);
            this.cmb_REPARACION.Name = "cmb_REPARACION";
            this.cmb_REPARACION.Size = new System.Drawing.Size(340, 28);
            this.cmb_REPARACION.TabIndex = 148;
            this.cmb_REPARACION.SelectedIndexChanged += new System.EventHandler(this.cmb_TipoReparacion_Changed);
            // 
            // NuevaReparacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 701);
            this.Controls.Add(this.cmb_REPARACION);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_NroOrden1);
            this.Controls.Add(this.ckbox_Celular);
            this.Controls.Add(this.dt_FechaNacimiento);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.txt_EmailCliente);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.txt_DomicilioCliente);
            this.Controls.Add(this.txt_DNI1);
            this.Controls.Add(this.txt_Nombre1);
            this.Controls.Add(this.txt_Apellido1);
            this.Controls.Add(this.txt_TelResto);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.txt_TelPrefijo);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.chk_WhatsApp);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.btn_Borrar);
            this.Controls.Add(this.btn_Imprimir);
            this.Controls.Add(this.dtp_FechaEntrega);
            this.Controls.Add(this.dtp_FechaRecepcion);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_Titulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NuevaReparacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva Reparación";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NuevaReparacionForm_FormClosed);
            this.Load += new System.EventHandler(this.NuevaReparacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DateTimePicker dtp_FechaEntrega;
        private System.Windows.Forms.DateTimePicker dtp_FechaRecepcion;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btn_Imprimir;
        private System.Windows.Forms.Button btn_Borrar;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.CheckBox ckbox_Celular;
        private System.Windows.Forms.DateTimePicker dt_FechaNacimiento;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_EmailCliente;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txt_DomicilioCliente;
        private System.Windows.Forms.TextBox txt_DNI1;
        private System.Windows.Forms.TextBox txt_Nombre1;
        private System.Windows.Forms.TextBox txt_Apellido1;
        private System.Windows.Forms.TextBox txt_TelResto;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox txt_TelPrefijo;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chk_WhatsApp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbl_NroOrden1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Subtotal1;
        private System.Windows.Forms.TextBox txt_Total1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_Seña1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_Cuotas;
        private System.Windows.Forms.CheckBox chk_Credito;
        private System.Windows.Forms.CheckBox chk_Debito;
        private System.Windows.Forms.CheckBox chk_Efectivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_REPARACION;
    }
}