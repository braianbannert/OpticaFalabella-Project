namespace OpticaFalabella
{
    partial class GiftCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiftCard));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_GiftCardNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PrecioGiftCard = new System.Windows.Forms.TextBox();
            this.btn_Vender = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_Cuotas = new System.Windows.Forms.ComboBox();
            this.chk_Credito = new System.Windows.Forms.CheckBox();
            this.chk_Debito = new System.Windows.Forms.CheckBox();
            this.chk_Efectivo = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gift Card Nº:";
            // 
            // lbl_GiftCardNumber
            // 
            this.lbl_GiftCardNumber.AutoSize = true;
            this.lbl_GiftCardNumber.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lbl_GiftCardNumber.Location = new System.Drawing.Point(120, 20);
            this.lbl_GiftCardNumber.Name = "lbl_GiftCardNumber";
            this.lbl_GiftCardNumber.Size = new System.Drawing.Size(0, 25);
            this.lbl_GiftCardNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto:";
            // 
            // txt_PrecioGiftCard
            // 
            this.txt_PrecioGiftCard.Location = new System.Drawing.Point(120, 64);
            this.txt_PrecioGiftCard.Name = "txt_PrecioGiftCard";
            this.txt_PrecioGiftCard.Size = new System.Drawing.Size(125, 27);
            this.txt_PrecioGiftCard.TabIndex = 3;
            // 
            // btn_Vender
            // 
            this.btn_Vender.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Vender.Font = new System.Drawing.Font("Segoe UI Black", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Vender.ForeColor = System.Drawing.Color.White;
            this.btn_Vender.Location = new System.Drawing.Point(20, 290);
            this.btn_Vender.Name = "btn_Vender";
            this.btn_Vender.Size = new System.Drawing.Size(131, 47);
            this.btn_Vender.TabIndex = 4;
            this.btn_Vender.Text = "VENDER";
            this.btn_Vender.UseVisualStyleBackColor = false;
            this.btn_Vender.Click += new System.EventHandler(this.btn_Vender_Clicked);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Red;
            this.btn_Cancel.Font = new System.Drawing.Font("Segoe UI Black", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(179, 290);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(131, 47);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "CANCELAR";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Clicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_Cuotas);
            this.groupBox2.Controls.Add(this.chk_Credito);
            this.groupBox2.Controls.Add(this.chk_Debito);
            this.groupBox2.Controls.Add(this.chk_Efectivo);
            this.groupBox2.Location = new System.Drawing.Point(58, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 147);
            this.groupBox2.TabIndex = 120;
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
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(245, 21);
            this.dateTimePicker1.MinDate = new System.DateTime(2022, 4, 14, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(250, 27);
            this.dateTimePicker1.TabIndex = 121;
            this.dateTimePicker1.Visible = false;
            // 
            // GiftCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 358);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Vender);
            this.Controls.Add(this.txt_PrecioGiftCard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_GiftCardNumber);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GiftCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gift Card";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GiftCard_FormClosing);
            this.Load += new System.EventHandler(this.GiftCard_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_GiftCardNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PrecioGiftCard;
        private System.Windows.Forms.Button btn_Vender;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_Cuotas;
        private System.Windows.Forms.CheckBox chk_Credito;
        private System.Windows.Forms.CheckBox chk_Debito;
        private System.Windows.Forms.CheckBox chk_Efectivo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}