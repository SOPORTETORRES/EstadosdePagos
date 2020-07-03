namespace EstadosdePagos
{
    partial class Frm_Incobrable
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Btn_salir = new System.Windows.Forms.Button();
            this.Btn_Grabar = new System.Windows.Forms.Button();
            this.Tx_motivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tx_NroDoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmb_TipoDoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Lista = new System.Windows.Forms.GroupBox();
            this.Dtg_Datos = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.Lista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Datos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.Btn_salir);
            this.groupBox1.Controls.Add(this.Btn_Grabar);
            this.groupBox1.Controls.Add(this.Tx_motivo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Tx_NroDoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Cmb_TipoDoc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingreso de Datos";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(498, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 33);
            this.button2.TabIndex = 8;
            this.button2.Text = "Eliminar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // Btn_salir
            // 
            this.Btn_salir.Location = new System.Drawing.Point(569, 24);
            this.Btn_salir.Name = "Btn_salir";
            this.Btn_salir.Size = new System.Drawing.Size(56, 33);
            this.Btn_salir.TabIndex = 7;
            this.Btn_salir.Text = "Salir";
            this.Btn_salir.UseVisualStyleBackColor = true;
            this.Btn_salir.Click += new System.EventHandler(this.Btn_salir_Click);
            // 
            // Btn_Grabar
            // 
            this.Btn_Grabar.Location = new System.Drawing.Point(416, 24);
            this.Btn_Grabar.Name = "Btn_Grabar";
            this.Btn_Grabar.Size = new System.Drawing.Size(56, 33);
            this.Btn_Grabar.TabIndex = 6;
            this.Btn_Grabar.Text = "Grabar";
            this.Btn_Grabar.UseVisualStyleBackColor = true;
            this.Btn_Grabar.Click += new System.EventHandler(this.Btn_Grabar_Click);
            // 
            // Tx_motivo
            // 
            this.Tx_motivo.Location = new System.Drawing.Point(23, 96);
            this.Tx_motivo.Multiline = true;
            this.Tx_motivo.Name = "Tx_motivo";
            this.Tx_motivo.Size = new System.Drawing.Size(602, 38);
            this.Tx_motivo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ingrese el Motivo";
            // 
            // Tx_NroDoc
            // 
            this.Tx_NroDoc.Location = new System.Drawing.Point(173, 41);
            this.Tx_NroDoc.Name = "Tx_NroDoc";
            this.Tx_NroDoc.Size = new System.Drawing.Size(82, 20);
            this.Tx_NroDoc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nro. Documento";
            // 
            // Cmb_TipoDoc
            // 
            this.Cmb_TipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_TipoDoc.FormattingEnabled = true;
            this.Cmb_TipoDoc.Location = new System.Drawing.Point(22, 41);
            this.Cmb_TipoDoc.Name = "Cmb_TipoDoc";
            this.Cmb_TipoDoc.Size = new System.Drawing.Size(105, 21);
            this.Cmb_TipoDoc.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo Documento";
            // 
            // Lista
            // 
            this.Lista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lista.Controls.Add(this.Dtg_Datos);
            this.Lista.Location = new System.Drawing.Point(2, 163);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(634, 265);
            this.Lista.TabIndex = 1;
            this.Lista.TabStop = false;
            this.Lista.Text = "Lista";
            // 
            // Dtg_Datos
            // 
            this.Dtg_Datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dtg_Datos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dtg_Datos.Location = new System.Drawing.Point(3, 16);
            this.Dtg_Datos.Name = "Dtg_Datos";
            this.Dtg_Datos.Size = new System.Drawing.Size(628, 246);
            this.Dtg_Datos.TabIndex = 0;
            // 
            // Frm_Incobrable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 431);
            this.ControlBox = false;
            this.Controls.Add(this.Lista);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Incobrable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario para dejar documento Incobrable";
            this.Load += new System.EventHandler(this.Frm_Incobrable_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Lista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Datos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Tx_motivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tx_NroDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Cmb_TipoDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Lista;
        private System.Windows.Forms.DataGridView Dtg_Datos;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Btn_salir;
        private System.Windows.Forms.Button Btn_Grabar;
    }
}