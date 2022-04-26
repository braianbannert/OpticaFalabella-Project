
namespace OpticaFalabella
{
    partial class SendEmail
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_CorreoOrigen = new System.Windows.Forms.TextBox();
            this.richTXT_Mensaje = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Enviar = new System.Windows.Forms.Button();
            this.txt_CorreoDestino = new System.Windows.Forms.TextBox();
            this.txt_Titulo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Proveedor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "desde:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "para:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Título:";
            // 
            // txt_CorreoOrigen
            // 
            this.txt_CorreoOrigen.Location = new System.Drawing.Point(98, 49);
            this.txt_CorreoOrigen.Name = "txt_CorreoOrigen";
            this.txt_CorreoOrigen.Size = new System.Drawing.Size(334, 27);
            this.txt_CorreoOrigen.TabIndex = 4;
            // 
            // richTXT_Mensaje
            // 
            this.richTXT_Mensaje.Location = new System.Drawing.Point(12, 208);
            this.richTXT_Mensaje.Name = "richTXT_Mensaje";
            this.richTXT_Mensaje.Size = new System.Drawing.Size(608, 453);
            this.richTXT_Mensaje.TabIndex = 5;
            this.richTXT_Mensaje.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mensaje:";
            // 
            // btn_Enviar
            // 
            this.btn_Enviar.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_Enviar.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Enviar.ForeColor = System.Drawing.Color.White;
            this.btn_Enviar.Location = new System.Drawing.Point(473, 37);
            this.btn_Enviar.Name = "btn_Enviar";
            this.btn_Enviar.Size = new System.Drawing.Size(94, 39);
            this.btn_Enviar.TabIndex = 7;
            this.btn_Enviar.Text = "Enviar";
            this.btn_Enviar.UseVisualStyleBackColor = false;
            this.btn_Enviar.Click += new System.EventHandler(this.EnviarEmail_ItemClicked);
            // 
            // txt_CorreoDestino
            // 
            this.txt_CorreoDestino.Location = new System.Drawing.Point(98, 88);
            this.txt_CorreoDestino.Name = "txt_CorreoDestino";
            this.txt_CorreoDestino.Size = new System.Drawing.Size(334, 27);
            this.txt_CorreoDestino.TabIndex = 8;
            // 
            // txt_Titulo
            // 
            this.txt_Titulo.Location = new System.Drawing.Point(98, 130);
            this.txt_Titulo.Name = "txt_Titulo";
            this.txt_Titulo.Size = new System.Drawing.Size(334, 27);
            this.txt_Titulo.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.OrangeRed;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(473, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 39);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Cancelar_ItemClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Proveedor:";
            // 
            // combo_Proveedor
            // 
            this.combo_Proveedor.FormattingEnabled = true;
            this.combo_Proveedor.Location = new System.Drawing.Point(98, 10);
            this.combo_Proveedor.Name = "combo_Proveedor";
            this.combo_Proveedor.Size = new System.Drawing.Size(333, 28);
            this.combo_Proveedor.TabIndex = 12;
            // 
            // SendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 673);
            this.Controls.Add(this.combo_Proveedor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_Titulo);
            this.Controls.Add(this.txt_CorreoDestino);
            this.Controls.Add(this.btn_Enviar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTXT_Mensaje);
            this.Controls.Add(this.txt_CorreoOrigen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SendEmail";
            this.Text = "Email";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SendEmailForm_FormClosed);
            this.Load += new System.EventHandler(this.SendEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_CorreoOrigen;
        private System.Windows.Forms.RichTextBox richTXT_Mensaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Enviar;
        private System.Windows.Forms.TextBox txt_CorreoDestino;
        private System.Windows.Forms.TextBox txt_Titulo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_Proveedor;
    }
}