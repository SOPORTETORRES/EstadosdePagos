namespace EstadosdePagos
{
    partial class Frm_ContratosObra
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
            this.label8 = new System.Windows.Forms.Label();
            this.Tx_PU = new System.Windows.Forms.TextBox();
            this.Btn_Eliminar = new System.Windows.Forms.Button();
            this.Tx_Servicio = new System.Windows.Forms.TextBox();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btn_Grabar = new System.Windows.Forms.Button();
            this.Cmb_Unidades = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Tx_CantidadTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Tx_Obra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Dtg_Datos = new System.Windows.Forms.DataGridView();
            this.Tx_ImporteTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Datos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Tx_ImporteTotal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Tx_PU);
            this.groupBox1.Controls.Add(this.Btn_Eliminar);
            this.groupBox1.Controls.Add(this.Tx_Servicio);
            this.groupBox1.Controls.Add(this.Btn_Salir);
            this.groupBox1.Controls.Add(this.Btn_Grabar);
            this.groupBox1.Controls.Add(this.Cmb_Unidades);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Tx_CantidadTotal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Tx_Obra);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(786, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Obra y Contrato";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "$ PU";
            // 
            // Tx_PU
            // 
            this.Tx_PU.Location = new System.Drawing.Point(363, 84);
            this.Tx_PU.Name = "Tx_PU";
            this.Tx_PU.Size = new System.Drawing.Size(40, 20);
            this.Tx_PU.TabIndex = 30;
            this.Tx_PU.Text = "0";
            this.Tx_PU.Leave += new System.EventHandler(this.Tx_PU_Leave);
            this.Tx_PU.Validating += new System.ComponentModel.CancelEventHandler(this.Tx_PU_Validating);
            // 
            // Btn_Eliminar
            // 
            this.Btn_Eliminar.Location = new System.Drawing.Point(718, 62);
            this.Btn_Eliminar.Name = "Btn_Eliminar";
            this.Btn_Eliminar.Size = new System.Drawing.Size(62, 42);
            this.Btn_Eliminar.TabIndex = 29;
            this.Btn_Eliminar.Text = "Eliminar";
            this.Btn_Eliminar.UseVisualStyleBackColor = true;
            this.Btn_Eliminar.Click += new System.EventHandler(this.Btn_Eliminar_Click);
            // 
            // Tx_Servicio
            // 
            this.Tx_Servicio.BackColor = System.Drawing.Color.White;
            this.Tx_Servicio.Location = new System.Drawing.Point(124, 51);
            this.Tx_Servicio.Name = "Tx_Servicio";
            this.Tx_Servicio.Size = new System.Drawing.Size(475, 20);
            this.Tx_Servicio.TabIndex = 28;
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(720, 14);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(60, 42);
            this.Btn_Salir.TabIndex = 27;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Btn_Grabar
            // 
            this.Btn_Grabar.Location = new System.Drawing.Point(641, 14);
            this.Btn_Grabar.Name = "Btn_Grabar";
            this.Btn_Grabar.Size = new System.Drawing.Size(62, 42);
            this.Btn_Grabar.TabIndex = 26;
            this.Btn_Grabar.Text = "Grabar";
            this.Btn_Grabar.UseVisualStyleBackColor = true;
            this.Btn_Grabar.Click += new System.EventHandler(this.Btn_Grabar_Click);
            // 
            // Cmb_Unidades
            // 
            this.Cmb_Unidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Unidades.FormattingEnabled = true;
            this.Cmb_Unidades.Location = new System.Drawing.Point(257, 84);
            this.Cmb_Unidades.Name = "Cmb_Unidades";
            this.Cmb_Unidades.Size = new System.Drawing.Size(53, 21);
            this.Cmb_Unidades.TabIndex = 25;
            this.Cmb_Unidades.SelectedIndexChanged += new System.EventHandler(this.Cmb_Unidades_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(210, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Unidad";
            // 
            // Tx_CantidadTotal
            // 
            this.Tx_CantidadTotal.Location = new System.Drawing.Point(124, 83);
            this.Tx_CantidadTotal.Name = "Tx_CantidadTotal";
            this.Tx_CantidadTotal.Size = new System.Drawing.Size(74, 20);
            this.Tx_CantidadTotal.TabIndex = 5;
            this.Tx_CantidadTotal.Leave += new System.EventHandler(this.Tx_CantidadTotal_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantidad ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Indique el  Servicio";
            // 
            // Tx_Obra
            // 
            this.Tx_Obra.BackColor = System.Drawing.Color.White;
            this.Tx_Obra.Location = new System.Drawing.Point(124, 21);
            this.Tx_Obra.Name = "Tx_Obra";
            this.Tx_Obra.ReadOnly = true;
            this.Tx_Obra.Size = new System.Drawing.Size(475, 20);
            this.Tx_Obra.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Obra Seleccionada";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Dtg_Datos);
            this.groupBox2.Location = new System.Drawing.Point(6, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos  Ingresados ";
            // 
            // Dtg_Datos
            // 
            this.Dtg_Datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dtg_Datos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dtg_Datos.Location = new System.Drawing.Point(3, 16);
            this.Dtg_Datos.Name = "Dtg_Datos";
            this.Dtg_Datos.Size = new System.Drawing.Size(782, 280);
            this.Dtg_Datos.TabIndex = 0;
            this.Dtg_Datos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dtg_Datos_CellContentClick);
            // 
            // Tx_ImporteTotal
            // 
            this.Tx_ImporteTotal.Location = new System.Drawing.Point(508, 83);
            this.Tx_ImporteTotal.Name = "Tx_ImporteTotal";
            this.Tx_ImporteTotal.ReadOnly = true;
            this.Tx_ImporteTotal.Size = new System.Drawing.Size(91, 20);
            this.Tx_ImporteTotal.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Importe Total";
            // 
            // Frm_ContratosObra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 426);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_ContratosObra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Ingreso de contratos a Obra.";
            this.Load += new System.EventHandler(this.Frm_ContratosObra_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Datos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tx_Obra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tx_CantidadTotal;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Grabar;
        private System.Windows.Forms.ComboBox Cmb_Unidades;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView Dtg_Datos;
        private System.Windows.Forms.TextBox Tx_Servicio;
        private System.Windows.Forms.Button Btn_Eliminar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Tx_PU;
        private System.Windows.Forms.TextBox Tx_ImporteTotal;
        private System.Windows.Forms.Label label4;
    }
}