namespace OpticaFalabella
{
    partial class RetiroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetiroForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Escaneo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_DNI_Cliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_NroORDEN = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_Cuotas = new System.Windows.Forms.ComboBox();
            this.chk_Credito = new System.Windows.Forms.CheckBox();
            this.chk_Debito = new System.Windows.Forms.CheckBox();
            this.chk_Efectivo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Subtotal = new System.Windows.Forms.TextBox();
            this.txt_Total = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_Seña = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btn_TomarPago = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resultado de Escaneo";
            // 
            // txt_Escaneo
            // 
            this.txt_Escaneo.Location = new System.Drawing.Point(12, 32);
            this.txt_Escaneo.Name = "txt_Escaneo";
            this.txt_Escaneo.Size = new System.Drawing.Size(449, 27);
            this.txt_Escaneo.TabIndex = 1;
            this.txt_Escaneo.TextChanged += new System.EventHandler(this.txt_Escaneo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "DNI Cliente";
            // 
            // txt_DNI_Cliente
            // 
            this.txt_DNI_Cliente.Location = new System.Drawing.Point(12, 98);
            this.txt_DNI_Cliente.Name = "txt_DNI_Cliente";
            this.txt_DNI_Cliente.Size = new System.Drawing.Size(183, 27);
            this.txt_DNI_Cliente.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nº de ORDEN";
            // 
            // txt_NroORDEN
            // 
            this.txt_NroORDEN.Location = new System.Drawing.Point(306, 98);
            this.txt_NroORDEN.Name = "txt_NroORDEN";
            this.txt_NroORDEN.Size = new System.Drawing.Size(154, 27);
            this.txt_NroORDEN.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_Cuotas);
            this.groupBox2.Controls.Add(this.chk_Credito);
            this.groupBox2.Controls.Add(this.chk_Debito);
            this.groupBox2.Controls.Add(this.chk_Efectivo);
            this.groupBox2.Location = new System.Drawing.Point(12, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 147);
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
            this.combo_Cuotas.Location = new System.Drawing.Point(120, 103);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Subtotal);
            this.groupBox1.Controls.Add(this.txt_Total);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txt_Seña);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(12, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 125);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            // 
            // txt_Subtotal
            // 
            this.txt_Subtotal.Location = new System.Drawing.Point(120, 23);
            this.txt_Subtotal.Name = "txt_Subtotal";
            this.txt_Subtotal.Size = new System.Drawing.Size(101, 27);
            this.txt_Subtotal.TabIndex = 71;
            this.txt_Subtotal.Text = "0";
            this.txt_Subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Total
            // 
            this.txt_Total.Location = new System.Drawing.Point(120, 89);
            this.txt_Total.Name = "txt_Total";
            this.txt_Total.Size = new System.Drawing.Size(101, 27);
            this.txt_Total.TabIndex = 73;
            this.txt_Total.Text = "0";
            this.txt_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(35, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(79, 20);
            this.label27.TabIndex = 38;
            this.label27.Text = "SUBTOTAL:";
            // 
            // txt_Seña
            // 
            this.txt_Seña.Location = new System.Drawing.Point(120, 55);
            this.txt_Seña.Name = "txt_Seña";
            this.txt_Seña.Size = new System.Drawing.Size(101, 27);
            this.txt_Seña.TabIndex = 72;
            this.txt_Seña.Text = "0";
            this.txt_Seña.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(65, 58);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 20);
            this.label28.TabIndex = 39;
            this.label28.Text = "SEÑA:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(2, 92);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(112, 20);
            this.label29.TabIndex = 40;
            this.label29.Text = "Resta ABONAR:";
            // 
            // btn_TomarPago
            // 
            this.btn_TomarPago.BackColor = System.Drawing.Color.Green;
            this.btn_TomarPago.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.btn_TomarPago.ForeColor = System.Drawing.Color.White;
            this.btn_TomarPago.Location = new System.Drawing.Point(371, 367);
            this.btn_TomarPago.Name = "btn_TomarPago";
            this.btn_TomarPago.Size = new System.Drawing.Size(94, 71);
            this.btn_TomarPago.TabIndex = 121;
            this.btn_TomarPago.Text = "Tomar PAGO";
            this.btn_TomarPago.UseVisualStyleBackColor = false;
            this.btn_TomarPago.Click += new System.EventHandler(this.btn_TomarPago_Clicked);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(345, 310);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(250, 27);
            this.dateTimePicker1.TabIndex = 122;
            this.dateTimePicker1.Visible = false;
            // 
            // RetiroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 450);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_TomarPago);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txt_NroORDEN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_DNI_Cliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Escaneo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RetiroForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retiro de Trabajo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RetiroTrabajo_FormClosing);
            this.Load += new System.EventHandler(this.RetiroForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Escaneo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DNI_Cliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_NroORDEN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_Cuotas;
        private System.Windows.Forms.CheckBox chk_Credito;
        private System.Windows.Forms.CheckBox chk_Debito;
        private System.Windows.Forms.CheckBox chk_Efectivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Subtotal;
        private System.Windows.Forms.TextBox txt_Total;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_Seña;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btn_TomarPago;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}