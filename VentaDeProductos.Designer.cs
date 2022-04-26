namespace OpticaFalabella
{
    partial class VentaDeProductos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentaDeProductos));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Codigo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_Cuotas = new System.Windows.Forms.ComboBox();
            this.chk_Credito = new System.Windows.Forms.CheckBox();
            this.chk_Debito = new System.Windows.Forms.CheckBox();
            this.chk_Efectivo = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_NroGiftCard = new System.Windows.Forms.TextBox();
            this.chk_GiftCard = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Total1 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_Codigo = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmb_Concepto = new System.Windows.Forms.ComboBox();
            this.groupBox_Descuento = new System.Windows.Forms.GroupBox();
            this.txt_Descuento = new System.Windows.Forms.TextBox();
            this.lbl_ValorFinal = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_Descuento.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código: ";
            // 
            // lbl_Codigo
            // 
            this.lbl_Codigo.AutoSize = true;
            this.lbl_Codigo.Location = new System.Drawing.Point(101, 20);
            this.lbl_Codigo.Name = "lbl_Codigo";
            this.lbl_Codigo.Size = new System.Drawing.Size(0, 20);
            this.lbl_Codigo.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(874, 302);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowRemoved);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(723, 557);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 44);
            this.button1.TabIndex = 3;
            this.button1.Text = "VENDER";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btn_VenderClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_Cuotas);
            this.groupBox2.Controls.Add(this.chk_Credito);
            this.groupBox2.Controls.Add(this.chk_Debito);
            this.groupBox2.Controls.Add(this.chk_Efectivo);
            this.groupBox2.Location = new System.Drawing.Point(337, 461);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 147);
            this.groupBox2.TabIndex = 119;
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
            this.combo_Cuotas.Location = new System.Drawing.Point(123, 94);
            this.combo_Cuotas.Name = "combo_Cuotas";
            this.combo_Cuotas.Size = new System.Drawing.Size(84, 28);
            this.combo_Cuotas.TabIndex = 121;
            // 
            // chk_Credito
            // 
            this.chk_Credito.AutoSize = true;
            this.chk_Credito.Location = new System.Drawing.Point(32, 96);
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
            this.chk_Debito.Location = new System.Drawing.Point(32, 68);
            this.chk_Debito.Name = "chk_Debito";
            this.chk_Debito.Size = new System.Drawing.Size(77, 24);
            this.chk_Debito.TabIndex = 119;
            this.chk_Debito.Text = "Débito";
            this.chk_Debito.UseVisualStyleBackColor = true;
            // 
            // chk_Efectivo
            // 
            this.chk_Efectivo.AutoSize = true;
            this.chk_Efectivo.Location = new System.Drawing.Point(33, 40);
            this.chk_Efectivo.Name = "chk_Efectivo";
            this.chk_Efectivo.Size = new System.Drawing.Size(84, 24);
            this.chk_Efectivo.TabIndex = 118;
            this.chk_Efectivo.Text = "Efectivo";
            this.chk_Efectivo.UseVisualStyleBackColor = true;
            this.chk_Efectivo.CheckedChanged += new System.EventHandler(this.chk_Efectivo_CheckChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(882, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(250, 27);
            this.dateTimePicker1.TabIndex = 120;
            this.dateTimePicker1.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.txt_NroGiftCard);
            this.groupBox3.Controls.Add(this.chk_GiftCard);
            this.groupBox3.Location = new System.Drawing.Point(30, 461);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 147);
            this.groupBox3.TabIndex = 123;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gift Card";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(50, 96);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 20);
            this.label24.TabIndex = 122;
            this.label24.Text = "Nº:";
            // 
            // txt_NroGiftCard
            // 
            this.txt_NroGiftCard.Location = new System.Drawing.Point(87, 93);
            this.txt_NroGiftCard.Name = "txt_NroGiftCard";
            this.txt_NroGiftCard.Size = new System.Drawing.Size(88, 27);
            this.txt_NroGiftCard.TabIndex = 122;
            this.txt_NroGiftCard.Leave += new System.EventHandler(this.txt_NroGiftCard_LostFocus);
            // 
            // chk_GiftCard
            // 
            this.chk_GiftCard.AutoSize = true;
            this.chk_GiftCard.Location = new System.Drawing.Point(71, 49);
            this.chk_GiftCard.Name = "chk_GiftCard";
            this.chk_GiftCard.Size = new System.Drawing.Size(87, 24);
            this.chk_GiftCard.TabIndex = 120;
            this.chk_GiftCard.Text = "¿Aplica?";
            this.chk_GiftCard.UseVisualStyleBackColor = true;
            this.chk_GiftCard.CheckedChanged += new System.EventHandler(this.chk_GiftCard_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Total1);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(654, 461);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 73);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            // 
            // txt_Total1
            // 
            this.txt_Total1.Location = new System.Drawing.Point(113, 26);
            this.txt_Total1.Name = "txt_Total1";
            this.txt_Total1.Size = new System.Drawing.Size(101, 27);
            this.txt_Total1.TabIndex = 73;
            this.txt_Total1.Text = "0";
            this.txt_Total1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(34, 29);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 20);
            this.label29.TabIndex = 40;
            this.label29.Text = "TOTAL:";
            // 
            // txt_Codigo
            // 
            this.txt_Codigo.Location = new System.Drawing.Point(503, 21);
            this.txt_Codigo.Name = "txt_Codigo";
            this.txt_Codigo.Size = new System.Drawing.Size(365, 27);
            this.txt_Codigo.TabIndex = 125;
            this.txt_Codigo.TextChanged += new System.EventHandler(this.txt_Codigo_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmb_Concepto);
            this.groupBox4.Location = new System.Drawing.Point(30, 384);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(487, 70);
            this.groupBox4.TabIndex = 127;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Concepto Descuento";
            // 
            // cmb_Concepto
            // 
            this.cmb_Concepto.FormattingEnabled = true;
            this.cmb_Concepto.Items.AddRange(new object[] {
            "Descuento a conocido $",
            "Descuento a conocido Porcentual",
            "Descuento en Efectivo (Porcentual)",
            "Descuento por Obra Social $"});
            this.cmb_Concepto.Location = new System.Drawing.Point(47, 28);
            this.cmb_Concepto.Name = "cmb_Concepto";
            this.cmb_Concepto.Size = new System.Drawing.Size(399, 28);
            this.cmb_Concepto.TabIndex = 0;
            // 
            // groupBox_Descuento
            // 
            this.groupBox_Descuento.Controls.Add(this.txt_Descuento);
            this.groupBox_Descuento.Location = new System.Drawing.Point(523, 384);
            this.groupBox_Descuento.Name = "groupBox_Descuento";
            this.groupBox_Descuento.Size = new System.Drawing.Size(381, 70);
            this.groupBox_Descuento.TabIndex = 126;
            this.groupBox_Descuento.TabStop = false;
            this.groupBox_Descuento.Text = "Descuento";
            // 
            // txt_Descuento
            // 
            this.txt_Descuento.Location = new System.Drawing.Point(131, 26);
            this.txt_Descuento.Name = "txt_Descuento";
            this.txt_Descuento.Size = new System.Drawing.Size(151, 27);
            this.txt_Descuento.TabIndex = 0;
            this.txt_Descuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_ValorFinal
            // 
            this.lbl_ValorFinal.AutoSize = true;
            this.lbl_ValorFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_ValorFinal.ForeColor = System.Drawing.Color.Red;
            this.lbl_ValorFinal.Location = new System.Drawing.Point(318, 647);
            this.lbl_ValorFinal.Name = "lbl_ValorFinal";
            this.lbl_ValorFinal.Size = new System.Drawing.Size(0, 29);
            this.lbl_ValorFinal.TabIndex = 129;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(197, 653);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(96, 20);
            this.label38.TabIndex = 128;
            this.label38.Text = "VALOR FINAL";
            // 
            // VentaDeProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 710);
            this.Controls.Add(this.lbl_ValorFinal);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox_Descuento);
            this.Controls.Add(this.txt_Codigo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_Codigo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VentaDeProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VENTA de PRODUCTOS";
            this.Activated += new System.EventHandler(this.VentaDeProductos_FormActivated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VentaDeProductos_FormClosed);
            this.Load += new System.EventHandler(this.VentaDeProductos_FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox_Descuento.ResumeLayout(false);
            this.groupBox_Descuento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Codigo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_Cuotas;
        private System.Windows.Forms.CheckBox chk_Credito;
        private System.Windows.Forms.CheckBox chk_Debito;
        private System.Windows.Forms.CheckBox chk_Efectivo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_NroGiftCard;
        private System.Windows.Forms.CheckBox chk_GiftCard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Total1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_Codigo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmb_Concepto;
        private System.Windows.Forms.GroupBox groupBox_Descuento;
        private System.Windows.Forms.TextBox txt_Descuento;
        private System.Windows.Forms.Label lbl_ValorFinal;
        private System.Windows.Forms.Label label38;
    }
}