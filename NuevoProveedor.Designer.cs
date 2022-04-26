
namespace OpticaFalabella
{
    partial class NuevoProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoProveedor));
            this.btn_Agregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Marca = new System.Windows.Forms.TextBox();
            this.txt_Telefono = new System.Windows.Forms.TextBox();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.btn_Borrar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Contacto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Agregar
            // 
            this.btn_Agregar.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btn_Agregar, "btn_Agregar");
            this.btn_Agregar.ForeColor = System.Drawing.Color.Green;
            this.btn_Agregar.Name = "btn_Agregar";
            this.btn_Agregar.UseVisualStyleBackColor = false;
            this.btn_Agregar.Click += new System.EventHandler(this.btn_Agregar_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txt_Marca
            // 
            resources.ApplyResources(this.txt_Marca, "txt_Marca");
            this.txt_Marca.Name = "txt_Marca";
            // 
            // txt_Telefono
            // 
            resources.ApplyResources(this.txt_Telefono, "txt_Telefono");
            this.txt_Telefono.Name = "txt_Telefono";
            // 
            // txt_Email
            // 
            resources.ApplyResources(this.txt_Email, "txt_Email");
            this.txt_Email.Name = "txt_Email";
            // 
            // btn_Borrar
            // 
            this.btn_Borrar.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btn_Borrar, "btn_Borrar");
            this.btn_Borrar.ForeColor = System.Drawing.Color.Red;
            this.btn_Borrar.Name = "btn_Borrar";
            this.btn_Borrar.UseVisualStyleBackColor = false;
            this.btn_Borrar.Click += new System.EventHandler(this.btn_Borrar_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_Contacto
            // 
            resources.ApplyResources(this.txt_Contacto, "txt_Contacto");
            this.txt_Contacto.Name = "txt_Contacto";
            // 
            // NuevoProveedor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_Contacto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Borrar);
            this.Controls.Add(this.txt_Email);
            this.Controls.Add(this.txt_Telefono);
            this.Controls.Add(this.txt_Marca);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Agregar);
            this.Name = "NuevoProveedor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NuevoProveedor_FormClosed);
            this.Load += new System.EventHandler(this.NuevoProveedor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Agregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Marca;
        private System.Windows.Forms.TextBox txt_Telefono;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.Button btn_Borrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Contacto;
    }
}