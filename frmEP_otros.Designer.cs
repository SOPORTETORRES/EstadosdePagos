namespace EstadosdePagos
{
    partial class frmEP_otros
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
            this.Cmb_Concepto = new System.Windows.Forms.ComboBox();
            this.Cmb_Unidades = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Rb_AdPuntual = new System.Windows.Forms.RadioButton();
            this.Rb_Adicional = new System.Windows.Forms.RadioButton();
            this.Rb_Descuento = new System.Windows.Forms.RadioButton();
            this.Tx_saldo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Tx_CantTotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Tx_cantidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Tx_Unidad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Tx_PU = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Tx_IdEP = new System.Windows.Forms.TextBox();
            this.Tx_total = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Tx_Id = new System.Windows.Forms.TextBox();
            this.Btn_eliminar = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btn_Grabar = new System.Windows.Forms.Button();
            this.Tx_Importe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tx_obra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Tx_Concepto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Dtg_Resultado = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Resultado)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Cmb_Concepto);
            this.groupBox1.Controls.Add(this.Cmb_Unidades);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Tx_cantidad);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Tx_Unidad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Tx_PU);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Tx_IdEP);
            this.groupBox1.Controls.Add(this.Tx_total);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Tx_Id);
            this.groupBox1.Controls.Add(this.Btn_eliminar);
            this.groupBox1.Controls.Add(this.Btn_Salir);
            this.groupBox1.Controls.Add(this.Btn_Grabar);
            this.groupBox1.Controls.Add(this.Tx_Importe);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Tx_obra);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Tx_Concepto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1071, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingreso de Datos";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Cmb_Concepto
            // 
            this.Cmb_Concepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Concepto.FormattingEnabled = true;
            this.Cmb_Concepto.Location = new System.Drawing.Point(7, 90);
            this.Cmb_Concepto.Name = "Cmb_Concepto";
            this.Cmb_Concepto.Size = new System.Drawing.Size(140, 21);
            this.Cmb_Concepto.TabIndex = 24;
            this.Cmb_Concepto.SelectedIndexChanged += new System.EventHandler(this.Cmb_Concepto_SelectedIndexChanged);
            // 
            // Cmb_Unidades
            // 
            this.Cmb_Unidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Unidades.FormattingEnabled = true;
            this.Cmb_Unidades.Location = new System.Drawing.Point(495, 88);
            this.Cmb_Unidades.Name = "Cmb_Unidades";
            this.Cmb_Unidades.Size = new System.Drawing.Size(48, 21);
            this.Cmb_Unidades.TabIndex = 23;
            this.Cmb_Unidades.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Rb_AdPuntual);
            this.groupBox3.Controls.Add(this.Rb_Adicional);
            this.groupBox3.Controls.Add(this.Rb_Descuento);
            this.groupBox3.Controls.Add(this.Tx_saldo);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.Tx_CantTotal);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(640, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 98);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones";
            // 
            // Rb_AdPuntual
            // 
            this.Rb_AdPuntual.AutoSize = true;
            this.Rb_AdPuntual.Location = new System.Drawing.Point(81, 23);
            this.Rb_AdPuntual.Name = "Rb_AdPuntual";
            this.Rb_AdPuntual.Size = new System.Drawing.Size(80, 17);
            this.Rb_AdPuntual.TabIndex = 27;
            this.Rb_AdPuntual.Text = "Ad. Puntual";
            this.Rb_AdPuntual.UseVisualStyleBackColor = true;
            this.Rb_AdPuntual.CheckedChanged += new System.EventHandler(this.Rb_AdPuntual_CheckedChanged);
            // 
            // Rb_Adicional
            // 
            this.Rb_Adicional.AutoSize = true;
            this.Rb_Adicional.Checked = true;
            this.Rb_Adicional.Location = new System.Drawing.Point(6, 22);
            this.Rb_Adicional.Name = "Rb_Adicional";
            this.Rb_Adicional.Size = new System.Drawing.Size(68, 17);
            this.Rb_Adicional.TabIndex = 26;
            this.Rb_Adicional.TabStop = true;
            this.Rb_Adicional.Text = "Adicional";
            this.Rb_Adicional.UseVisualStyleBackColor = true;
            this.Rb_Adicional.CheckedChanged += new System.EventHandler(this.Rb_Adicional_CheckedChanged);
            // 
            // Rb_Descuento
            // 
            this.Rb_Descuento.AutoSize = true;
            this.Rb_Descuento.Location = new System.Drawing.Point(169, 23);
            this.Rb_Descuento.Name = "Rb_Descuento";
            this.Rb_Descuento.Size = new System.Drawing.Size(77, 17);
            this.Rb_Descuento.TabIndex = 1;
            this.Rb_Descuento.Text = "Descuento";
            this.Rb_Descuento.UseVisualStyleBackColor = true;
            this.Rb_Descuento.CheckedChanged += new System.EventHandler(this.Rb_Descuento_CheckedChanged);
            // 
            // Tx_saldo
            // 
            this.Tx_saldo.Enabled = false;
            this.Tx_saldo.Location = new System.Drawing.Point(181, 66);
            this.Tx_saldo.Name = "Tx_saldo";
            this.Tx_saldo.Size = new System.Drawing.Size(62, 20);
            this.Tx_saldo.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(141, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Saldo";
            // 
            // Tx_CantTotal
            // 
            this.Tx_CantTotal.Enabled = false;
            this.Tx_CantTotal.Location = new System.Drawing.Point(65, 66);
            this.Tx_CantTotal.Name = "Tx_CantTotal";
            this.Tx_CantTotal.Size = new System.Drawing.Size(70, 20);
            this.Tx_CantTotal.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 36);
            this.label10.TabIndex = 22;
            this.label10.Text = "Cantidad Total";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(565, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Cantidad";
            // 
            // Tx_cantidad
            // 
            this.Tx_cantidad.Location = new System.Drawing.Point(565, 88);
            this.Tx_cantidad.Name = "Tx_cantidad";
            this.Tx_cantidad.Size = new System.Drawing.Size(47, 20);
            this.Tx_cantidad.TabIndex = 19;
            this.Tx_cantidad.Text = "0";
            this.Tx_cantidad.Validating += new System.ComponentModel.CancelEventHandler(this.Tx_cantidad_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(498, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Unidad";
            // 
            // Tx_Unidad
            // 
            this.Tx_Unidad.Location = new System.Drawing.Point(373, 68);
            this.Tx_Unidad.Name = "Tx_Unidad";
            this.Tx_Unidad.Size = new System.Drawing.Size(40, 20);
            this.Tx_Unidad.TabIndex = 17;
            this.Tx_Unidad.Text = "Kg";
            this.Tx_Unidad.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(439, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "$ PU";
            // 
            // Tx_PU
            // 
            this.Tx_PU.Location = new System.Drawing.Point(438, 88);
            this.Tx_PU.Name = "Tx_PU";
            this.Tx_PU.Size = new System.Drawing.Size(40, 20);
            this.Tx_PU.TabIndex = 15;
            this.Tx_PU.Text = "0";
            this.Tx_PU.Validating += new System.ComponentModel.CancelEventHandler(this.Tx_PU_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(498, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Id EP";
            // 
            // Tx_IdEP
            // 
            this.Tx_IdEP.Location = new System.Drawing.Point(496, 37);
            this.Tx_IdEP.Name = "Tx_IdEP";
            this.Tx_IdEP.ReadOnly = true;
            this.Tx_IdEP.Size = new System.Drawing.Size(40, 20);
            this.Tx_IdEP.TabIndex = 13;
            // 
            // Tx_total
            // 
            this.Tx_total.Enabled = false;
            this.Tx_total.Location = new System.Drawing.Point(558, 37);
            this.Tx_total.Name = "Tx_total";
            this.Tx_total.Size = new System.Drawing.Size(66, 20);
            this.Tx_total.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(573, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Total";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Id Otros";
            // 
            // Tx_Id
            // 
            this.Tx_Id.Location = new System.Drawing.Point(436, 37);
            this.Tx_Id.Name = "Tx_Id";
            this.Tx_Id.ReadOnly = true;
            this.Tx_Id.Size = new System.Drawing.Size(40, 20);
            this.Tx_Id.TabIndex = 9;
            // 
            // Btn_eliminar
            // 
            this.Btn_eliminar.Location = new System.Drawing.Point(990, 11);
            this.Btn_eliminar.Name = "Btn_eliminar";
            this.Btn_eliminar.Size = new System.Drawing.Size(75, 46);
            this.Btn_eliminar.TabIndex = 8;
            this.Btn_eliminar.Text = "Eliminar";
            this.Btn_eliminar.UseVisualStyleBackColor = true;
            this.Btn_eliminar.Click += new System.EventHandler(this.button2_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(993, 68);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(72, 46);
            this.Btn_Salir.TabIndex = 7;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Btn_Grabar
            // 
            this.Btn_Grabar.Location = new System.Drawing.Point(896, 11);
            this.Btn_Grabar.Name = "Btn_Grabar";
            this.Btn_Grabar.Size = new System.Drawing.Size(75, 46);
            this.Btn_Grabar.TabIndex = 6;
            this.Btn_Grabar.Text = "Grabar";
            this.Btn_Grabar.UseVisualStyleBackColor = true;
            this.Btn_Grabar.Click += new System.EventHandler(this.Btn_Grabar_Click);
            // 
            // Tx_Importe
            // 
            this.Tx_Importe.Location = new System.Drawing.Point(902, 88);
            this.Tx_Importe.Name = "Tx_Importe";
            this.Tx_Importe.Size = new System.Drawing.Size(73, 20);
            this.Tx_Importe.TabIndex = 5;
            this.Tx_Importe.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(899, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total Concepto";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Tx_obra
            // 
            this.Tx_obra.Location = new System.Drawing.Point(6, 38);
            this.Tx_obra.Name = "Tx_obra";
            this.Tx_obra.ReadOnly = true;
            this.Tx_obra.Size = new System.Drawing.Size(412, 20);
            this.Tx_obra.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Obra ";
            // 
            // Tx_Concepto
            // 
            this.Tx_Concepto.Location = new System.Drawing.Point(150, 90);
            this.Tx_Concepto.Name = "Tx_Concepto";
            this.Tx_Concepto.Size = new System.Drawing.Size(274, 20);
            this.Tx_Concepto.TabIndex = 1;
            this.Tx_Concepto.TextChanged += new System.EventHandler(this.Tx_Concepto_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese el Concepto";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Dtg_Resultado);
            this.groupBox2.Location = new System.Drawing.Point(5, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1077, 329);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Items Ingresados";
            // 
            // Dtg_Resultado
            // 
            this.Dtg_Resultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dtg_Resultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dtg_Resultado.Location = new System.Drawing.Point(3, 16);
            this.Dtg_Resultado.Name = "Dtg_Resultado";
            this.Dtg_Resultado.Size = new System.Drawing.Size(1071, 310);
            this.Dtg_Resultado.TabIndex = 0;
            this.Dtg_Resultado.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dtg_Resultado_CellContentDoubleClick);
            // 
            // frmEP_otros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEP_otros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Ingreso de Otros Servicios";
            this.Load += new System.EventHandler(this.frmEP_otros_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_Resultado)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Grabar;
        private System.Windows.Forms.TextBox Tx_Importe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tx_obra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tx_Concepto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView Dtg_Resultado;
        private System.Windows.Forms.Button Btn_eliminar;
        private System.Windows.Forms.TextBox Tx_Id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Tx_total;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Tx_IdEP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Tx_CantTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Tx_cantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Tx_Unidad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Tx_PU;
        private System.Windows.Forms.ComboBox Cmb_Unidades;
        private System.Windows.Forms.ComboBox Cmb_Concepto;
        private System.Windows.Forms.TextBox Tx_saldo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton Rb_Descuento;
        private System.Windows.Forms.RadioButton Rb_AdPuntual;
        private System.Windows.Forms.RadioButton Rb_Adicional;
    }
}