namespace EstadosdePagos
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dgvResumen = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblUsuario = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCrearEP = new System.Windows.Forms.ToolStripButton();
            this.btnReporteEP = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnVerReporteEP = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGenerarReporteEP = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEnviarEP = new System.Windows.Forms.ToolStripButton();
            this.btnModEPxObsClte = new System.Windows.Forms.ToolStripButton();
            this.btnComentSegEP = new System.Windows.Forms.ToolStripButton();
            this.btnAprobarEP = new System.Windows.Forms.ToolStripButton();
            this.btnAdjuntoEP = new System.Windows.Forms.ToolStripButton();
            this.btnTrazabilidadEP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlsEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalGuiasDespacho = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalEtiquetas = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalKilos = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Gb_Estado = new System.Windows.Forms.GroupBox();
            this.Lbl_PB = new System.Windows.Forms.Label();
            this.Pb = new System.Windows.Forms.ProgressBar();
            this.Btn_imprimePL = new System.Windows.Forms.Button();
            this.btnDestinatariosObra = new System.Windows.Forms.Button();
            this.chkActualizar = new System.Windows.Forms.CheckBox();
            this.cboEmpresa = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblObras = new System.Windows.Forms.Label();
            this.cboFiltro = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboObra = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTecnicoObra = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.tabGuiasDespacho = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Gb_Estado.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.tabGuiasDespacho.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResumen
            // 
            this.dgvResumen.AllowUserToAddRows = false;
            this.dgvResumen.AllowUserToDeleteRows = false;
            this.dgvResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumen.Location = new System.Drawing.Point(0, 153);
            this.dgvResumen.MultiSelect = false;
            this.dgvResumen.Name = "dgvResumen";
            this.dgvResumen.ReadOnly = true;
            this.dgvResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumen.Size = new System.Drawing.Size(1174, 222);
            this.dgvResumen.TabIndex = 6;
            this.dgvResumen.SelectionChanged += new System.EventHandler(this.dgvResumen_SelectionChanged);
            this.dgvResumen.DoubleClick += new System.EventHandler(this.dgvResumen_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblUsuario,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.btnActualizar,
            this.toolStripSeparator3,
            this.btnCrearEP,
            this.btnReporteEP,
            this.btnEnviarEP,
            this.btnModEPxObsClte,
            this.btnComentSegEP,
            this.btnAprobarEP,
            this.btnAdjuntoEP,
            this.btnTrazabilidadEP,
            this.toolStripSeparator2,
            this.btnExcel,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1179, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabel1.Text = "Usuario:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(57, 22);
            this.lblUsuario.Text = "(Usuario)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel4.Text = "Acciones:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::EstadosdePagos.Properties.Resources.buscar;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(62, 22);
            this.btnActualizar.Text = "Buscar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCrearEP
            // 
            this.btnCrearEP.Image = global::EstadosdePagos.Properties.Resources.agregar;
            this.btnCrearEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCrearEP.Name = "btnCrearEP";
            this.btnCrearEP.Size = new System.Drawing.Size(55, 22);
            this.btnCrearEP.Text = "Crear";
            this.btnCrearEP.Click += new System.EventHandler(this.btnCrearEP_Click);
            // 
            // btnReporteEP
            // 
            this.btnReporteEP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnVerReporteEP,
            this.btnGenerarReporteEP});
            this.btnReporteEP.Image = global::EstadosdePagos.Properties.Resources.reporte;
            this.btnReporteEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReporteEP.Name = "btnReporteEP";
            this.btnReporteEP.Size = new System.Drawing.Size(77, 22);
            this.btnReporteEP.Text = "Reporte";
            // 
            // btnVerReporteEP
            // 
            this.btnVerReporteEP.Name = "btnVerReporteEP";
            this.btnVerReporteEP.Size = new System.Drawing.Size(115, 22);
            this.btnVerReporteEP.Text = "Ver";
            this.btnVerReporteEP.Click += new System.EventHandler(this.btnVerReporteEP_Click);
            // 
            // btnGenerarReporteEP
            // 
            this.btnGenerarReporteEP.Name = "btnGenerarReporteEP";
            this.btnGenerarReporteEP.Size = new System.Drawing.Size(115, 22);
            this.btnGenerarReporteEP.Text = "Generar";
            this.btnGenerarReporteEP.Click += new System.EventHandler(this.btnGenerarReporteEP_Click);
            // 
            // btnEnviarEP
            // 
            this.btnEnviarEP.Image = global::EstadosdePagos.Properties.Resources.email_adjuntar;
            this.btnEnviarEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarEP.Name = "btnEnviarEP";
            this.btnEnviarEP.Size = new System.Drawing.Size(106, 22);
            this.btnEnviarEP.Text = "Enviar a cliente";
            this.btnEnviarEP.Click += new System.EventHandler(this.btnEnviarEP_Click);
            // 
            // btnModEPxObsClte
            // 
            this.btnModEPxObsClte.Image = global::EstadosdePagos.Properties.Resources.formulario_editar;
            this.btnModEPxObsClte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModEPxObsClte.Name = "btnModEPxObsClte";
            this.btnModEPxObsClte.Size = new System.Drawing.Size(153, 22);
            this.btnModEPxObsClte.Text = "Modificar x Obs. Cliente";
            this.btnModEPxObsClte.Click += new System.EventHandler(this.btnModEPxObsClte_Click);
            // 
            // btnComentSegEP
            // 
            this.btnComentSegEP.Image = global::EstadosdePagos.Properties.Resources.nota;
            this.btnComentSegEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComentSegEP.Name = "btnComentSegEP";
            this.btnComentSegEP.Size = new System.Drawing.Size(143, 22);
            this.btnComentSegEP.Text = "Coment. Seguimiento";
            this.btnComentSegEP.Click += new System.EventHandler(this.btnComentSegEP_Click);
            // 
            // btnAprobarEP
            // 
            this.btnAprobarEP.Image = global::EstadosdePagos.Properties.Resources.aceptar;
            this.btnAprobarEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAprobarEP.Name = "btnAprobarEP";
            this.btnAprobarEP.Size = new System.Drawing.Size(70, 22);
            this.btnAprobarEP.Text = "Aprobar";
            this.btnAprobarEP.ToolTipText = "Aprobar";
            this.btnAprobarEP.Click += new System.EventHandler(this.btnAprobarEP_Click);
            // 
            // btnAdjuntoEP
            // 
            this.btnAdjuntoEP.Image = global::EstadosdePagos.Properties.Resources.adjuntar;
            this.btnAdjuntoEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdjuntoEP.Name = "btnAdjuntoEP";
            this.btnAdjuntoEP.Size = new System.Drawing.Size(75, 22);
            this.btnAdjuntoEP.Text = "Adjuntos";
            this.btnAdjuntoEP.Click += new System.EventHandler(this.btnAdjuntoEP_Click);
            // 
            // btnTrazabilidadEP
            // 
            this.btnTrazabilidadEP.Image = global::EstadosdePagos.Properties.Resources.information;
            this.btnTrazabilidadEP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrazabilidadEP.Name = "btnTrazabilidadEP";
            this.btnTrazabilidadEP.Size = new System.Drawing.Size(90, 22);
            this.btnTrazabilidadEP.Text = "Trazabilidad";
            this.btnTrazabilidadEP.Click += new System.EventHandler(this.btnTrazabilidadEP_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::EstadosdePagos.Properties.Resources.exportar_excel;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(99, 22);
            this.btnExcel.Text = "Exportar Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::EstadosdePagos.Properties.Resources.salir;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsEstado,
            this.toolStripStatusLabel1,
            this.lblTotalGuiasDespacho,
            this.toolStripStatusLabel3,
            this.lblTotalEtiquetas,
            this.toolStripStatusLabel2,
            this.lblTotalKilos});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1179, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlsEstado
            // 
            this.tlsEstado.AutoSize = false;
            this.tlsEstado.Name = "tlsEstado";
            this.tlsEstado.Size = new System.Drawing.Size(180, 17);
            this.tlsEstado.Text = "Listo";
            this.tlsEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(142, 17);
            this.toolStripStatusLabel1.Text = "Total guia(s) despacho(s):";
            // 
            // lblTotalGuiasDespacho
            // 
            this.lblTotalGuiasDespacho.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalGuiasDespacho.Name = "lblTotalGuiasDespacho";
            this.lblTotalGuiasDespacho.Size = new System.Drawing.Size(14, 17);
            this.lblTotalGuiasDespacho.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabel3.Text = "Total etiqueta(s):";
            // 
            // lblTotalEtiquetas
            // 
            this.lblTotalEtiquetas.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalEtiquetas.Name = "lblTotalEtiquetas";
            this.lblTotalEtiquetas.Size = new System.Drawing.Size(14, 17);
            this.lblTotalEtiquetas.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel2.Text = "Total kilo(s):";
            // 
            // lblTotalKilos
            // 
            this.lblTotalKilos.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalKilos.Name = "lblTotalKilos";
            this.lblTotalKilos.Size = new System.Drawing.Size(14, 17);
            this.lblTotalKilos.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Gb_Estado);
            this.panel1.Controls.Add(this.Btn_imprimePL);
            this.panel1.Controls.Add(this.btnDestinatariosObra);
            this.panel1.Controls.Add(this.chkActualizar);
            this.panel1.Controls.Add(this.cboEmpresa);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblObras);
            this.panel1.Controls.Add(this.cboFiltro);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cboObra);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cboTecnicoObra);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 128);
            this.panel1.TabIndex = 7;
            // 
            // Gb_Estado
            // 
            this.Gb_Estado.Controls.Add(this.Lbl_PB);
            this.Gb_Estado.Controls.Add(this.Pb);
            this.Gb_Estado.Location = new System.Drawing.Point(601, 18);
            this.Gb_Estado.Name = "Gb_Estado";
            this.Gb_Estado.Size = new System.Drawing.Size(461, 63);
            this.Gb_Estado.TabIndex = 15;
            this.Gb_Estado.TabStop = false;
            this.Gb_Estado.Text = "Estado de la Operación";
            // 
            // Lbl_PB
            // 
            this.Lbl_PB.Location = new System.Drawing.Point(29, 42);
            this.Lbl_PB.Name = "Lbl_PB";
            this.Lbl_PB.Size = new System.Drawing.Size(406, 18);
            this.Lbl_PB.TabIndex = 16;
            this.Lbl_PB.Text = "label3";
            // 
            // Pb
            // 
            this.Pb.Location = new System.Drawing.Point(26, 20);
            this.Pb.Name = "Pb";
            this.Pb.Size = new System.Drawing.Size(409, 15);
            this.Pb.TabIndex = 15;
            // 
            // Btn_imprimePL
            // 
            this.Btn_imprimePL.Location = new System.Drawing.Point(357, 13);
            this.Btn_imprimePL.Name = "Btn_imprimePL";
            this.Btn_imprimePL.Size = new System.Drawing.Size(75, 23);
            this.Btn_imprimePL.TabIndex = 13;
            this.Btn_imprimePL.Text = "Imprime PL";
            this.Btn_imprimePL.UseVisualStyleBackColor = true;
            this.Btn_imprimePL.Click += new System.EventHandler(this.Btn_imprimePL_Click);
            // 
            // btnDestinatariosObra
            // 
            this.btnDestinatariosObra.Location = new System.Drawing.Point(819, 95);
            this.btnDestinatariosObra.Name = "btnDestinatariosObra";
            this.btnDestinatariosObra.Size = new System.Drawing.Size(127, 23);
            this.btnDestinatariosObra.TabIndex = 12;
            this.btnDestinatariosObra.Text = "Destinatarios Obra";
            this.btnDestinatariosObra.UseVisualStyleBackColor = true;
            this.btnDestinatariosObra.Visible = false;
            this.btnDestinatariosObra.Click += new System.EventHandler(this.btnDestinatariosObra_Click);
            // 
            // chkActualizar
            // 
            this.chkActualizar.AutoSize = true;
            this.chkActualizar.Checked = true;
            this.chkActualizar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActualizar.Location = new System.Drawing.Point(439, 96);
            this.chkActualizar.Name = "chkActualizar";
            this.chkActualizar.Size = new System.Drawing.Size(336, 17);
            this.chkActualizar.TabIndex = 11;
            this.chkActualizar.Text = "(Buscar automaticamente cuando haya un cambio en los criterios)";
            this.chkActualizar.UseVisualStyleBackColor = true;
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresa.FormattingEnabled = true;
            this.cboEmpresa.Items.AddRange(new object[] {
            "TO",
            "TOSOL"});
            this.cboEmpresa.Location = new System.Drawing.Point(153, 10);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(68, 21);
            this.cboEmpresa.TabIndex = 10;
            this.cboEmpresa.SelectedIndexChanged += new System.EventHandler(this.cboEmpresa_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Empresa:";
            // 
            // lblObras
            // 
            this.lblObras.AutoSize = true;
            this.lblObras.Location = new System.Drawing.Point(516, 71);
            this.lblObras.Name = "lblObras";
            this.lblObras.Size = new System.Drawing.Size(27, 13);
            this.lblObras.TabIndex = 8;
            this.lblObras.Text = "Q=0";
            this.lblObras.Visible = false;
            // 
            // cboFiltro
            // 
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.Items.AddRange(new object[] {
            "E.P por ingresar",
            "Creado sin reporte",
            "Con reporte pero no enviado a cliente",
            "Enviado a cliente pero sin aprobacion"});
            this.cboFiltro.Location = new System.Drawing.Point(153, 92);
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(228, 21);
            this.cboFiltro.TabIndex = 7;
            this.cboFiltro.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Filtro(s):";
            // 
            // cboObra
            // 
            this.cboObra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObra.FormattingEnabled = true;
            this.cboObra.Location = new System.Drawing.Point(153, 65);
            this.cboObra.Name = "cboObra";
            this.cboObra.Size = new System.Drawing.Size(354, 21);
            this.cboObra.TabIndex = 5;
            this.cboObra.SelectedIndexChanged += new System.EventHandler(this.cboObra_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Obra:";
            // 
            // cboTecnicoObra
            // 
            this.cboTecnicoObra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTecnicoObra.FormattingEnabled = true;
            this.cboTecnicoObra.Location = new System.Drawing.Point(153, 37);
            this.cboTecnicoObra.Name = "cboTecnicoObra";
            this.cboTecnicoObra.Size = new System.Drawing.Size(185, 21);
            this.cboTecnicoObra.TabIndex = 3;
            this.cboTecnicoObra.SelectedIndexChanged += new System.EventHandler(this.cboTecnicoObra_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario/Tecnico de Obra:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvDetalle);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1171, 176);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Guías de despacho";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Location = new System.Drawing.Point(3, 3);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(1165, 170);
            this.dgvDetalle.TabIndex = 7;
            // 
            // tabGuiasDespacho
            // 
            this.tabGuiasDespacho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabGuiasDespacho.Controls.Add(this.tabPage1);
            this.tabGuiasDespacho.Location = new System.Drawing.Point(0, 378);
            this.tabGuiasDespacho.Name = "tabGuiasDespacho";
            this.tabGuiasDespacho.SelectedIndex = 0;
            this.tabGuiasDespacho.Size = new System.Drawing.Size(1179, 202);
            this.tabGuiasDespacho.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 602);
            this.Controls.Add(this.dgvResumen);
            this.Controls.Add(this.tabGuiasDespacho);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control estados de pago";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Gb_Estado.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.tabGuiasDespacho.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResumen;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlsEstado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboObra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTecnicoObra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFiltro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TabControl tabGuiasDespacho;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalKilos;
        private System.Windows.Forms.ToolStripButton btnCrearEP;
        private System.Windows.Forms.ToolStripButton btnEnviarEP;
        private System.Windows.Forms.ToolStripButton btnModEPxObsClte;
        private System.Windows.Forms.ToolStripButton btnAprobarEP;
        private System.Windows.Forms.ToolStripDropDownButton btnReporteEP;
        private System.Windows.Forms.ToolStripMenuItem btnVerReporteEP;
        private System.Windows.Forms.ToolStripMenuItem btnGenerarReporteEP;
        private System.Windows.Forms.ToolStripButton btnTrazabilidadEP;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnComentSegEP;
        private System.Windows.Forms.ToolStripButton btnAdjuntoEP;
        private System.Windows.Forms.Label lblObras;
        private System.Windows.Forms.ComboBox cboEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkActualizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalGuiasDespacho;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalEtiquetas;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblUsuario;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnDestinatariosObra;
        private System.Windows.Forms.Button Btn_imprimePL;
        private System.Windows.Forms.GroupBox Gb_Estado;
        private System.Windows.Forms.Label Lbl_PB;
        private System.Windows.Forms.ProgressBar Pb;
    }
}