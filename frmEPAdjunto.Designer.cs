namespace EstadosdePagos
{
    partial class frmEPAdjunto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEPAdjunto));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvAdjuntos = new System.Windows.Forms.DataGridView();
            this.lblTecnicoObra = new System.Windows.Forms.Label();
            this.lblObra = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAgregarAdjunto = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAgregarComentario = new System.Windows.Forms.TextBox();
            this.btnAgregarComentario = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvComentarios = new System.Windows.Forms.DataGridView();
            this.txtVerComentario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjuntos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComentarios)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(18, 136);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(381, 326);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvAdjuntos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(373, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Documentos adjuntos:";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvAdjuntos
            // 
            this.dgvAdjuntos.AllowUserToAddRows = false;
            this.dgvAdjuntos.AllowUserToDeleteRows = false;
            this.dgvAdjuntos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdjuntos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdjuntos.Location = new System.Drawing.Point(3, 3);
            this.dgvAdjuntos.MultiSelect = false;
            this.dgvAdjuntos.Name = "dgvAdjuntos";
            this.dgvAdjuntos.ReadOnly = true;
            this.dgvAdjuntos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdjuntos.Size = new System.Drawing.Size(367, 294);
            this.dgvAdjuntos.TabIndex = 8;
            this.dgvAdjuntos.DoubleClick += new System.EventHandler(this.dgvAdjuntos_DoubleClick);
            // 
            // lblTecnicoObra
            // 
            this.lblTecnicoObra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTecnicoObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTecnicoObra.Location = new System.Drawing.Point(227, 14);
            this.lblTecnicoObra.Name = "lblTecnicoObra";
            this.lblTecnicoObra.Size = new System.Drawing.Size(164, 23);
            this.lblTecnicoObra.TabIndex = 28;
            this.lblTecnicoObra.Text = "(dato)";
            this.lblTecnicoObra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblObra
            // 
            this.lblObra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObra.Location = new System.Drawing.Point(450, 14);
            this.lblObra.Name = "lblObra";
            this.lblObra.Size = new System.Drawing.Size(395, 23);
            this.lblObra.TabIndex = 24;
            this.lblObra.Text = "(dato)";
            this.lblObra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tecnico de Obra:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Obra:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = global::EstadosdePagos.Properties.Resources.salir;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(880, 96);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 34);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblID
            // 
            this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(46, 14);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(70, 23);
            this.lblID.TabIndex = 28;
            this.lblID.Text = "(dato)";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "E.P:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExaminar);
            this.groupBox1.Controls.Add(this.txtArchivo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnAgregarAdjunto);
            this.groupBox1.Location = new System.Drawing.Point(18, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 69);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar documento";
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(345, 33);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(27, 23);
            this.btnExaminar.TabIndex = 34;
            this.btnExaminar.Text = "...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(13, 35);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.ReadOnly = true;
            this.txtArchivo.Size = new System.Drawing.Size(326, 20);
            this.txtArchivo.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Archivo:";
            // 
            // btnAgregarAdjunto
            // 
            this.btnAgregarAdjunto.Enabled = false;
            this.btnAgregarAdjunto.Image = global::EstadosdePagos.Properties.Resources.agregar;
            this.btnAgregarAdjunto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarAdjunto.Location = new System.Drawing.Point(255, 9);
            this.btnAgregarAdjunto.Name = "btnAgregarAdjunto";
            this.btnAgregarAdjunto.Size = new System.Drawing.Size(84, 23);
            this.btnAgregarAdjunto.TabIndex = 31;
            this.btnAgregarAdjunto.Text = "Agregar";
            this.btnAgregarAdjunto.UseVisualStyleBackColor = true;
            this.btnAgregarAdjunto.Click += new System.EventHandler(this.btnAgregarAdjunto_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 465);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(281, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Nota: Puede hacer doble clic sobre el archivo para abrirlo.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAgregarComentario);
            this.groupBox2.Controls.Add(this.btnAgregarComentario);
            this.groupBox2.Location = new System.Drawing.Point(419, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 82);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agregar comentario";
            // 
            // txtAgregarComentario
            // 
            this.txtAgregarComentario.Location = new System.Drawing.Point(6, 19);
            this.txtAgregarComentario.MaxLength = 500;
            this.txtAgregarComentario.Multiline = true;
            this.txtAgregarComentario.Name = "txtAgregarComentario";
            this.txtAgregarComentario.Size = new System.Drawing.Size(337, 56);
            this.txtAgregarComentario.TabIndex = 0;
            // 
            // btnAgregarComentario
            // 
            this.btnAgregarComentario.Image = global::EstadosdePagos.Properties.Resources.agregar;
            this.btnAgregarComentario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarComentario.Location = new System.Drawing.Point(344, 43);
            this.btnAgregarComentario.Name = "btnAgregarComentario";
            this.btnAgregarComentario.Size = new System.Drawing.Size(77, 32);
            this.btnAgregarComentario.TabIndex = 1;
            this.btnAgregarComentario.Text = "Agregar";
            this.btnAgregarComentario.UseVisualStyleBackColor = true;
            this.btnAgregarComentario.Click += new System.EventHandler(this.btnAgregarComentario_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(419, 140);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(431, 322);
            this.tabControl2.TabIndex = 34;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvComentarios);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(423, 296);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Comentarios ingresados:";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvComentarios
            // 
            this.dgvComentarios.AllowUserToAddRows = false;
            this.dgvComentarios.AllowUserToDeleteRows = false;
            this.dgvComentarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComentarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComentarios.Location = new System.Drawing.Point(3, 3);
            this.dgvComentarios.MultiSelect = false;
            this.dgvComentarios.Name = "dgvComentarios";
            this.dgvComentarios.ReadOnly = true;
            this.dgvComentarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComentarios.Size = new System.Drawing.Size(417, 290);
            this.dgvComentarios.TabIndex = 0;
            this.dgvComentarios.SelectionChanged += new System.EventHandler(this.dgvComentarios_SelectionChanged);
            // 
            // txtVerComentario
            // 
            this.txtVerComentario.Location = new System.Drawing.Point(482, 465);
            this.txtVerComentario.Multiline = true;
            this.txtVerComentario.Name = "txtVerComentario";
            this.txtVerComentario.ReadOnly = true;
            this.txtVerComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVerComentario.Size = new System.Drawing.Size(358, 49);
            this.txtVerComentario.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 483);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Comentario:";
            // 
            // frmEPAdjunto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 526);
            this.ControlBox = false;
            this.Controls.Add(this.txtVerComentario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTecnicoObra);
            this.Controls.Add(this.lblObra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEPAdjunto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario para agregar Comentarios y Archivos Adjuntos";
            this.Load += new System.EventHandler(this.frmEPAdjunto_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjuntos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComentarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTecnicoObra;
        private System.Windows.Forms.Label lblObra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dgvAdjuntos;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgregarAdjunto;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAgregarComentario;
        private System.Windows.Forms.Button btnAgregarComentario;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvComentarios;
        private System.Windows.Forms.TextBox txtVerComentario;
        private System.Windows.Forms.Label label4;
    }
}