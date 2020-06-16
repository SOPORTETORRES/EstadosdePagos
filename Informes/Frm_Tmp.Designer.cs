namespace EstadosdePagos.Informes
{
    partial class Frm_Tmp
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
            this.Btn_Buscar = new System.Windows.Forms.Button();
            this.DTG = new System.Windows.Forms.DataGridView();
            this.Btn_Imprimir = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Tx_codigo = new System.Windows.Forms.TextBox();
            this.Lbl_msg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Lbl_NroAtencion = new System.Windows.Forms.Label();
            this.lbl_NombreObra = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DTG)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Buscar
            // 
            this.Btn_Buscar.Location = new System.Drawing.Point(222, 12);
            this.Btn_Buscar.Name = "Btn_Buscar";
            this.Btn_Buscar.Size = new System.Drawing.Size(97, 44);
            this.Btn_Buscar.TabIndex = 0;
            this.Btn_Buscar.Text = "Buscar";
            this.Btn_Buscar.UseVisualStyleBackColor = true;
            this.Btn_Buscar.Click += new System.EventHandler(this.Btn_Buscar_Click);
            // 
            // DTG
            // 
            this.DTG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DTG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTG.Location = new System.Drawing.Point(12, 79);
            this.DTG.Name = "DTG";
            this.DTG.Size = new System.Drawing.Size(1155, 333);
            this.DTG.TabIndex = 1;
            this.DTG.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DTG_CellDoubleClick);
            // 
            // Btn_Imprimir
            // 
            this.Btn_Imprimir.Location = new System.Drawing.Point(325, 12);
            this.Btn_Imprimir.Name = "Btn_Imprimir";
            this.Btn_Imprimir.Size = new System.Drawing.Size(97, 44);
            this.Btn_Imprimir.TabIndex = 2;
            this.Btn_Imprimir.Text = "Imprimir IT";
            this.Btn_Imprimir.UseVisualStyleBackColor = true;
            this.Btn_Imprimir.Click += new System.EventHandler(this.Btn_Imprimir_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(428, 12);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(97, 44);
            this.Btn_Salir.TabIndex = 3;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código IT";
            // 
            // Tx_codigo
            // 
            this.Tx_codigo.Location = new System.Drawing.Point(102, 25);
            this.Tx_codigo.Name = "Tx_codigo";
            this.Tx_codigo.Size = new System.Drawing.Size(100, 20);
            this.Tx_codigo.TabIndex = 5;
            // 
            // Lbl_msg
            // 
            this.Lbl_msg.AutoSize = true;
            this.Lbl_msg.Location = new System.Drawing.Point(242, 60);
            this.Lbl_msg.Name = "Lbl_msg";
            this.Lbl_msg.Size = new System.Drawing.Size(35, 13);
            this.Lbl_msg.TabIndex = 6;
            this.Lbl_msg.Text = "label2";
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(766, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 44);
            this.button1.TabIndex = 7;
            this.button1.Text = "Repara";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_NombreObra);
            this.groupBox1.Controls.Add(this.Lbl_NroAtencion);
            this.groupBox1.Controls.Add(this.Btn_OK);
            this.groupBox1.Location = new System.Drawing.Point(876, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 75);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(233, 13);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(41, 28);
            this.Btn_OK.TabIndex = 9;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Lbl_NroAtencion
            // 
            this.Lbl_NroAtencion.AutoSize = true;
            this.Lbl_NroAtencion.Location = new System.Drawing.Point(7, 20);
            this.Lbl_NroAtencion.Name = "Lbl_NroAtencion";
            this.Lbl_NroAtencion.Size = new System.Drawing.Size(35, 13);
            this.Lbl_NroAtencion.TabIndex = 10;
            this.Lbl_NroAtencion.Text = "label2";
            // 
            // lbl_NombreObra
            // 
            this.lbl_NombreObra.AutoSize = true;
            this.lbl_NombreObra.Location = new System.Drawing.Point(7, 43);
            this.lbl_NombreObra.Name = "lbl_NombreObra";
            this.lbl_NombreObra.Size = new System.Drawing.Size(35, 13);
            this.lbl_NombreObra.TabIndex = 11;
            this.lbl_NombreObra.Text = "label3";
            // 
            // Frm_Tmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 413);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Lbl_msg);
            this.Controls.Add(this.Tx_codigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Imprimir);
            this.Controls.Add(this.DTG);
            this.Controls.Add(this.Btn_Buscar);
            this.Name = "Frm_Tmp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario para Impresion de PL";
            this.Activated += new System.EventHandler(this.Frm_Tmp_Activated);
            this.Load += new System.EventHandler(this.Frm_Tmp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTG)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Buscar;
        private System.Windows.Forms.DataGridView DTG;
        private System.Windows.Forms.Button Btn_Imprimir;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tx_codigo;
        private System.Windows.Forms.Label Lbl_msg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_NombreObra;
        private System.Windows.Forms.Label Lbl_NroAtencion;
        private System.Windows.Forms.Button Btn_OK;
    }
}