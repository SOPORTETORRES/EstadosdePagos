namespace EstadosdePagos
{
    partial class frmFacturacion
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturacion));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblUsuarioFacturacion = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cboFiltro = new System.Windows.Forms.ToolStripComboBox();
            this.cboAñoMes = new System.Windows.Forms.ToolStripComboBox();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.btnIngresarINET = new System.Windows.Forms.ToolStripButton();
            this.btnDarxCobrada = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVerReporte = new System.Windows.Forms.ToolStripButton();
            this.btnComentSegEP = new System.Windows.Forms.ToolStripButton();
            this.btnAdjuntoEP = new System.Windows.Forms.ToolStripButton();
            this.btnTrazabilidadEP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlsEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvResumen = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblUsuarioFacturacion,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.cboFiltro,
            this.cboAñoMes,
            this.btnActualizar,
            this.toolStripSeparator1,
            this.toolStripLabel4,
            this.btnIngresarINET,
            this.btnDarxCobrada,
            this.toolStripSeparator4,
            this.btnVerReporte,
            this.btnComentSegEP,
            this.btnAdjuntoEP,
            this.btnTrazabilidadEP,
            this.toolStripSeparator3,
            this.btnExcel,
            this.toolStripSeparator2,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1275, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel1.Text = "Usuario:";
            // 
            // lblUsuarioFacturacion
            // 
            this.lblUsuarioFacturacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioFacturacion.Name = "lblUsuarioFacturacion";
            this.lblUsuarioFacturacion.Size = new System.Drawing.Size(57, 22);
            this.lblUsuarioFacturacion.Text = "(Usuario)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel3.Text = "Filtro(s):";
            // 
            // cboFiltro
            // 
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.Items.AddRange(new object[] {
            "Por ingresar a INET",
            "Facturadas por cobrar",
            "Facturadas vencidas",
            "Pagadas",
            "(Todas)"});
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(130, 25);
            this.cboFiltro.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);
            // 
            // cboAñoMes
            // 
            this.cboAñoMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAñoMes.Name = "cboAñoMes";
            this.cboAñoMes.Size = new System.Drawing.Size(75, 25);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::EstadosdePagos.Properties.Resources.buscar;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(59, 22);
            this.btnActualizar.Text = "Buscar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel4.Text = "Acciones:";
            // 
            // btnIngresarINET
            // 
            this.btnIngresarINET.Image = global::EstadosdePagos.Properties.Resources.agregar;
            this.btnIngresarINET.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIngresarINET.Name = "btnIngresarINET";
            this.btnIngresarINET.Size = new System.Drawing.Size(103, 22);
            this.btnIngresarINET.Text = "Ingresar a INET";
            this.btnIngresarINET.Click += new System.EventHandler(this.btnIngresarINET_Click);
            // 
            // btnDarxCobrada
            // 
            this.btnDarxCobrada.Image = global::EstadosdePagos.Properties.Resources.moneda;
            this.btnDarxCobrada.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDarxCobrada.Name = "btnDarxCobrada";
            this.btnDarxCobrada.Size = new System.Drawing.Size(105, 22);
            this.btnDarxCobrada.Text = "Dar por cobrada";
            this.btnDarxCobrada.Click += new System.EventHandler(this.btnDarxCobrada_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnVerReporte
            // 
            this.btnVerReporte.Image = global::EstadosdePagos.Properties.Resources.reporte;
            this.btnVerReporte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerReporte.Name = "btnVerReporte";
            this.btnVerReporte.Size = new System.Drawing.Size(82, 22);
            this.btnVerReporte.Text = "Ver reporte";
            this.btnVerReporte.Click += new System.EventHandler(this.btnVerReporte_Click);
            // 
            // btnComentSegEP
            // 
            this.btnComentSegEP.Image = global::EstadosdePagos.Properties.Resources.nota;
            this.btnComentSegEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComentSegEP.Name = "btnComentSegEP";
            this.btnComentSegEP.Size = new System.Drawing.Size(87, 22);
            this.btnComentSegEP.Text = "Comentarios";
            this.btnComentSegEP.Click += new System.EventHandler(this.btnComentSegEP_Click);
            // 
            // btnAdjuntoEP
            // 
            this.btnAdjuntoEP.Image = global::EstadosdePagos.Properties.Resources.adjuntar;
            this.btnAdjuntoEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdjuntoEP.Name = "btnAdjuntoEP";
            this.btnAdjuntoEP.Size = new System.Drawing.Size(70, 22);
            this.btnAdjuntoEP.Text = "Adjuntos";
            this.btnAdjuntoEP.Click += new System.EventHandler(this.btnAdjuntoEP_Click);
            // 
            // btnTrazabilidadEP
            // 
            this.btnTrazabilidadEP.Image = global::EstadosdePagos.Properties.Resources.information;
            this.btnTrazabilidadEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrazabilidadEP.Name = "btnTrazabilidadEP";
            this.btnTrazabilidadEP.Size = new System.Drawing.Size(84, 22);
            this.btnTrazabilidadEP.Text = "Trazabilidad";
            this.btnTrazabilidadEP.Click += new System.EventHandler(this.btnTrazabilidadEP_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::EstadosdePagos.Properties.Resources.exportar_excel;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(97, 22);
            this.btnExcel.Text = "Exportar Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::EstadosdePagos.Properties.Resources.salir;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(47, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsEstado});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1275, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlsEstado
            // 
            this.tlsEstado.Name = "tlsEstado";
            this.tlsEstado.Size = new System.Drawing.Size(29, 17);
            this.tlsEstado.Text = "Listo";
            // 
            // dgvResumen
            // 
            this.dgvResumen.AllowUserToAddRows = false;
            this.dgvResumen.AllowUserToDeleteRows = false;
            this.dgvResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResumen.Location = new System.Drawing.Point(0, 25);
            this.dgvResumen.MultiSelect = false;
            this.dgvResumen.Name = "dgvResumen";
            this.dgvResumen.ReadOnly = true;
            this.dgvResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumen.Size = new System.Drawing.Size(1275, 321);
            this.dgvResumen.TabIndex = 3;
            // 
            // frmFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 368);
            this.Controls.Add(this.dgvResumen);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFacturacion";
            this.Text = "Control facturacion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmFacturacion_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlsEstado;
        private System.Windows.Forms.ToolStripLabel lblUsuarioFacturacion;
        private System.Windows.Forms.ToolStripButton btnIngresarINET;
        private System.Windows.Forms.ToolStripButton btnDarxCobrada;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.DataGridView dgvResumen;
        private System.Windows.Forms.ToolStripComboBox cboFiltro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripComboBox cboAñoMes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnTrazabilidadEP;
        private System.Windows.Forms.ToolStripButton btnComentSegEP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnVerReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnAdjuntoEP;
    }
}

