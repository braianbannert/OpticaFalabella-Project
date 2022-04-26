namespace OpticaFalabella
{
    partial class VerificarPrecio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerificarPrecio));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_QRRead = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Precio = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_Codigo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código:";
            // 
            // txt_QRRead
            // 
            this.txt_QRRead.Location = new System.Drawing.Point(352, 22);
            this.txt_QRRead.Name = "txt_QRRead";
            this.txt_QRRead.Size = new System.Drawing.Size(1, 27);
            this.txt_QRRead.TabIndex = 2;
            this.txt_QRRead.Visible = false;
            this.txt_QRRead.TextChanged += new System.EventHandler(this.txt_QRRead_TextChanged2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(56, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio:";
            // 
            // lbl_Precio
            // 
            this.lbl_Precio.AutoSize = true;
            this.lbl_Precio.Font = new System.Drawing.Font("Segoe UI", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lbl_Precio.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbl_Precio.Location = new System.Drawing.Point(137, 51);
            this.lbl_Precio.Name = "lbl_Precio";
            this.lbl_Precio.Size = new System.Drawing.Size(0, 38);
            this.lbl_Precio.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_Codigo
            // 
            this.lbl_Codigo.AutoSize = true;
            this.lbl_Codigo.Location = new System.Drawing.Point(79, 25);
            this.lbl_Codigo.Name = "lbl_Codigo";
            this.lbl_Codigo.Size = new System.Drawing.Size(0, 20);
            this.lbl_Codigo.TabIndex = 5;
            // 
            // VerificarPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 112);
            this.Controls.Add(this.lbl_Codigo);
            this.Controls.Add(this.lbl_Precio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_QRRead);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerificarPrecio";
            this.Text = "Verificador de Precios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VerificadorPrecio_Closed);
            this.Load += new System.EventHandler(this.VerificarPrecio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_QRRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Precio;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_Codigo;
    }
}