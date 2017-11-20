namespace EstadosdePagos
{
    partial class frmEPComentario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEPComentario));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvComentarios = new System.Windows.Forms.DataGridView();
            this.lblTecnicoObra = new System.Windows.Forms.Label();
            this.lblObra = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVerComentario = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAgregarComentario = new System.Windows.Forms.TextBox();
            this.btnAgregarComentario = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComentarios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 207);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 226);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvComentarios);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Comentarios ingresados:";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.dgvComentarios.Size = new System.Drawing.Size(346, 194);
            this.dgvComentarios.TabIndex = 0;
            this.dgvComentarios.SelectionChanged += new System.EventHandler(this.dgvComentarios_SelectionChanged);
            // 
            // lblTecnicoObra
            // 
            this.lblTecnicoObra.AutoSize = true;
            this.lblTecnicoObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTecnicoObra.Location = new System.Drawing.Point(111, 69);
            this.lblTecnicoObra.Name = "lblTecnicoObra";
            this.lblTecnicoObra.Size = new System.Drawing.Size(40, 13);
            this.lblTecnicoObra.TabIndex = 25;
            this.lblTecnicoObra.Text = "(dato)";
            // 
            // lblObra
            // 
            this.lblObra.AutoSize = true;
            this.lblObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObra.Location = new System.Drawing.Point(51, 42);
            this.lblObra.Name = "lblObra";
            this.lblObra.Size = new System.Drawing.Size(40, 13);
            this.lblObra.TabIndex = 24;
            this.lblObra.Text = "(dato)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tecnico de Obra:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Obra:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::EstadosdePagos.Properties.Resources.salir;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(281, 525);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(51, 17);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(40, 13);
            this.lblID.TabIndex = 28;
            this.lblID.Text = "(dato)";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Comentario:";
            // 
            // txtVerComentario
            // 
            this.txtVerComentario.Location = new System.Drawing.Point(17, 456);
            this.txtVerComentario.Multiline = true;
            this.txtVerComentario.Name = "txtVerComentario";
            this.txtVerComentario.ReadOnly = true;
            this.txtVerComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVerComentario.Size = new System.Drawing.Size(348, 63);
            this.txtVerComentario.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAgregarComentario);
            this.groupBox1.Controls.Add(this.btnAgregarComentario);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar comentario";
            // 
            // txtAgregarComentario
            // 
            this.txtAgregarComentario.Location = new System.Drawing.Point(6, 19);
            this.txtAgregarComentario.MaxLength = 500;
            this.txtAgregarComentario.Multiline = true;
            this.txtAgregarComentario.Name = "txtAgregarComentario";
            this.txtAgregarComentario.Size = new System.Drawing.Size(348, 63);
            this.txtAgregarComentario.TabIndex = 0;
            this.txtAgregarComentario.TextChanged += new System.EventHandler(this.txtAgregarComentario_TextChanged);
            this.txtAgregarComentario.Leave += new System.EventHandler(this.txtAgregarComentario_Leave);
            // 
            // btnAgregarComentario
            // 
            this.btnAgregarComentario.Enabled = false;
            this.btnAgregarComentario.Image = global::EstadosdePagos.Properties.Resources.agregar;
            this.btnAgregarComentario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarComentario.Location = new System.Drawing.Point(269, 88);
            this.btnAgregarComentario.Name = "btnAgregarComentario";
            this.btnAgregarComentario.Size = new System.Drawing.Size(84, 23);
            this.btnAgregarComentario.TabIndex = 1;
            this.btnAgregarComentario.Text = "Agregar";
            this.btnAgregarComentario.UseVisualStyleBackColor = true;
            this.btnAgregarComentario.Click += new System.EventHandler(this.btnAgregarComentario_Click);
            // 
            // frmEPComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 560);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtVerComentario);
            this.Controls.Add(this.label7);
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
            this.Name = "frmEPComentario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comentarios de seguimiento";
            this.Load += new System.EventHandler(this.frmEPComentario_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComentarios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvComentarios;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVerComentario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAgregarComentario;
        private System.Windows.Forms.Button btnAgregarComentario;
    }
}