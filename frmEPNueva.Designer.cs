namespace EstadosdePagos
{
    partial class frmEPNueva
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEPNueva));
            this.tabCrecionEP = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvGuiasDespacho = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRegistrosGDespacho = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvEtiquetas = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblRegistrosEtiquetas = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDesmarcarTodo = new System.Windows.Forms.Button();
            this.btnMarcarTodo = new System.Windows.Forms.Button();
            this.cboIT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGuiaDespacho = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Dtg_OtrasGuias = new System.Windows.Forms.DataGridView();
            this.lblTecnicoObra = new System.Windows.Forms.Label();
            this.lblObra = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiaPresentEP = new System.Windows.Forms.TextBox();
            this.dtpFechaPresentEP = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFechaCreacion = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblValorKiloSuministro = new System.Windows.Forms.Label();
            this.lblDestinatarios = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tx_KgsSeleccionado = new System.Windows.Forms.TextBox();
            this.Btn_ObtenerKgsSel = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.Btn_Otros = new System.Windows.Forms.Button();
            this.Lbl_totalOtros = new System.Windows.Forms.Label();
            this.Btn_Adjunto = new System.Windows.Forms.Button();
            this.tabCrecionEP.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasDespacho)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtiquetas)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_OtrasGuias)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCrecionEP
            // 
            this.tabCrecionEP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCrecionEP.Controls.Add(this.tabPage1);
            this.tabCrecionEP.Controls.Add(this.tabPage2);
            this.tabCrecionEP.Controls.Add(this.tabPage3);
            this.tabCrecionEP.Location = new System.Drawing.Point(7, 175);
            this.tabCrecionEP.Name = "tabCrecionEP";
            this.tabCrecionEP.SelectedIndex = 0;
            this.tabCrecionEP.Size = new System.Drawing.Size(788, 399);
            this.tabCrecionEP.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvGuiasDespacho);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 373);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Seleccion de las Guias de despacho";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvGuiasDespacho
            // 
            this.dgvGuiasDespacho.AllowUserToAddRows = false;
            this.dgvGuiasDespacho.AllowUserToDeleteRows = false;
            this.dgvGuiasDespacho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGuiasDespacho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGuiasDespacho.Location = new System.Drawing.Point(3, 3);
            this.dgvGuiasDespacho.MultiSelect = false;
            this.dgvGuiasDespacho.Name = "dgvGuiasDespacho";
            this.dgvGuiasDespacho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGuiasDespacho.Size = new System.Drawing.Size(774, 367);
            this.dgvGuiasDespacho.TabIndex = 8;
            this.dgvGuiasDespacho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGuiasDespacho_CellContentClick);
            this.dgvGuiasDespacho.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGuiasDespacho_CellValueChanged);
            this.dgvGuiasDespacho.DoubleClick += new System.EventHandler(this.dgvGuiasDespacho_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lblRegistrosGDespacho);
            this.panel2.Location = new System.Drawing.Point(3, 230);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(774, 232);
            this.panel2.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(124, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(534, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Nota: Si Ud. guarda o acepta en esta pestaña estara seleccionando todas las etiqu" +
    "etas de la guia de despacho";
            // 
            // lblRegistrosGDespacho
            // 
            this.lblRegistrosGDespacho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistrosGDespacho.AutoSize = true;
            this.lblRegistrosGDespacho.Location = new System.Drawing.Point(12, 118);
            this.lblRegistrosGDespacho.Name = "lblRegistrosGDespacho";
            this.lblRegistrosGDespacho.Size = new System.Drawing.Size(69, 13);
            this.lblRegistrosGDespacho.TabIndex = 0;
            this.lblRegistrosGDespacho.Text = "Registro(s): 0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvEtiquetas);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 373);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Etiquetas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvEtiquetas
            // 
            this.dgvEtiquetas.AllowUserToAddRows = false;
            this.dgvEtiquetas.AllowUserToDeleteRows = false;
            this.dgvEtiquetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEtiquetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEtiquetas.Location = new System.Drawing.Point(3, 34);
            this.dgvEtiquetas.MultiSelect = false;
            this.dgvEtiquetas.Name = "dgvEtiquetas";
            this.dgvEtiquetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEtiquetas.Size = new System.Drawing.Size(774, 307);
            this.dgvEtiquetas.TabIndex = 9;
            this.dgvEtiquetas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEtiquetas_CellContentClick);
            this.dgvEtiquetas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEtiquetas_CellValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblRegistrosEtiquetas);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 341);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(774, 29);
            this.panel3.TabIndex = 10;
            // 
            // lblRegistrosEtiquetas
            // 
            this.lblRegistrosEtiquetas.AutoSize = true;
            this.lblRegistrosEtiquetas.Location = new System.Drawing.Point(12, 9);
            this.lblRegistrosEtiquetas.Name = "lblRegistrosEtiquetas";
            this.lblRegistrosEtiquetas.Size = new System.Drawing.Size(69, 13);
            this.lblRegistrosEtiquetas.TabIndex = 0;
            this.lblRegistrosEtiquetas.Text = "Registro(s): 0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDesmarcarTodo);
            this.panel1.Controls.Add(this.btnMarcarTodo);
            this.panel1.Controls.Add(this.cboIT);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblGuiaDespacho);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnDesmarcarTodo
            // 
            this.btnDesmarcarTodo.Location = new System.Drawing.Point(531, 6);
            this.btnDesmarcarTodo.Name = "btnDesmarcarTodo";
            this.btnDesmarcarTodo.Size = new System.Drawing.Size(75, 23);
            this.btnDesmarcarTodo.TabIndex = 21;
            this.btnDesmarcarTodo.Text = "Desmarcar";
            this.btnDesmarcarTodo.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodo.Click += new System.EventHandler(this.btnDesmarcarTodo_Click);
            // 
            // btnMarcarTodo
            // 
            this.btnMarcarTodo.Location = new System.Drawing.Point(450, 6);
            this.btnMarcarTodo.Name = "btnMarcarTodo";
            this.btnMarcarTodo.Size = new System.Drawing.Size(75, 23);
            this.btnMarcarTodo.TabIndex = 20;
            this.btnMarcarTodo.Text = "Marcar todo";
            this.btnMarcarTodo.UseVisualStyleBackColor = true;
            this.btnMarcarTodo.Click += new System.EventHandler(this.btnMarcarTodo_Click);
            // 
            // cboIT
            // 
            this.cboIT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIT.Enabled = false;
            this.cboIT.FormattingEnabled = true;
            this.cboIT.Location = new System.Drawing.Point(210, 6);
            this.cboIT.Name = "cboIT";
            this.cboIT.Size = new System.Drawing.Size(121, 21);
            this.cboIT.TabIndex = 3;
            this.cboIT.SelectedIndexChanged += new System.EventHandler(this.cboIT_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "IT:";
            // 
            // lblGuiaDespacho
            // 
            this.lblGuiaDespacho.AutoSize = true;
            this.lblGuiaDespacho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuiaDespacho.Location = new System.Drawing.Point(92, 9);
            this.lblGuiaDespacho.Name = "lblGuiaDespacho";
            this.lblGuiaDespacho.Size = new System.Drawing.Size(14, 13);
            this.lblGuiaDespacho.TabIndex = 1;
            this.lblGuiaDespacho.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Guia despacho:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Dtg_OtrasGuias);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(780, 373);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Otro tipo de Guías";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Dtg_OtrasGuias
            // 
            this.Dtg_OtrasGuias.AllowUserToAddRows = false;
            this.Dtg_OtrasGuias.AllowUserToDeleteRows = false;
            this.Dtg_OtrasGuias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dtg_OtrasGuias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dtg_OtrasGuias.Location = new System.Drawing.Point(3, 3);
            this.Dtg_OtrasGuias.MultiSelect = false;
            this.Dtg_OtrasGuias.Name = "Dtg_OtrasGuias";
            this.Dtg_OtrasGuias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dtg_OtrasGuias.Size = new System.Drawing.Size(774, 367);
            this.Dtg_OtrasGuias.TabIndex = 9;
            // 
            // lblTecnicoObra
            // 
            this.lblTecnicoObra.AutoSize = true;
            this.lblTecnicoObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTecnicoObra.Location = new System.Drawing.Point(110, 32);
            this.lblTecnicoObra.Name = "lblTecnicoObra";
            this.lblTecnicoObra.Size = new System.Drawing.Size(40, 13);
            this.lblTecnicoObra.TabIndex = 25;
            this.lblTecnicoObra.Text = "(dato)";
            // 
            // lblObra
            // 
            this.lblObra.AutoSize = true;
            this.lblObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObra.Location = new System.Drawing.Point(50, 5);
            this.lblObra.Name = "lblObra";
            this.lblObra.Size = new System.Drawing.Size(40, 13);
            this.lblObra.TabIndex = 24;
            this.lblObra.Text = "(dato)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tecnico de Obra:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Obra:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Dia de presentacion de los EP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Proxima fecha de presentación:";
            // 
            // txtDiaPresentEP
            // 
            this.txtDiaPresentEP.Location = new System.Drawing.Point(175, 56);
            this.txtDiaPresentEP.Name = "txtDiaPresentEP";
            this.txtDiaPresentEP.ReadOnly = true;
            this.txtDiaPresentEP.Size = new System.Drawing.Size(32, 20);
            this.txtDiaPresentEP.TabIndex = 30;
            // 
            // dtpFechaPresentEP
            // 
            this.dtpFechaPresentEP.Location = new System.Drawing.Point(175, 84);
            this.dtpFechaPresentEP.Name = "dtpFechaPresentEP";
            this.dtpFechaPresentEP.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaPresentEP.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(416, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Fecha creacion obra:";
            // 
            // lblFechaCreacion
            // 
            this.lblFechaCreacion.AutoSize = true;
            this.lblFechaCreacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaCreacion.Location = new System.Drawing.Point(530, 5);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(40, 13);
            this.lblFechaCreacion.TabIndex = 33;
            this.lblFechaCreacion.Text = "(dato)";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(530, 32);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(40, 13);
            this.lblID.TabIndex = 35;
            this.lblID.Text = "(dato)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(497, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "E.P:";
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(175, 108);
            this.txtComentario.MaxLength = 500;
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(574, 38);
            this.txtComentario.TabIndex = 0;
            this.txtComentario.Leave += new System.EventHandler(this.txtComentario_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Comentario:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(422, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Valor kilo suministro:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(453, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Destinatarios:";
            // 
            // lblValorKiloSuministro
            // 
            this.lblValorKiloSuministro.AutoSize = true;
            this.lblValorKiloSuministro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorKiloSuministro.Location = new System.Drawing.Point(530, 59);
            this.lblValorKiloSuministro.Name = "lblValorKiloSuministro";
            this.lblValorKiloSuministro.Size = new System.Drawing.Size(40, 13);
            this.lblValorKiloSuministro.TabIndex = 39;
            this.lblValorKiloSuministro.Text = "(dato)";
            // 
            // lblDestinatarios
            // 
            this.lblDestinatarios.AutoSize = true;
            this.lblDestinatarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinatarios.Location = new System.Drawing.Point(530, 87);
            this.lblDestinatarios.Name = "lblDestinatarios";
            this.lblDestinatarios.Size = new System.Drawing.Size(40, 13);
            this.lblDestinatarios.TabIndex = 40;
            this.lblDestinatarios.Text = "(dato)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Total de Kilos seleccionados";
            // 
            // tx_KgsSeleccionado
            // 
            this.tx_KgsSeleccionado.Location = new System.Drawing.Point(177, 151);
            this.tx_KgsSeleccionado.Name = "tx_KgsSeleccionado";
            this.tx_KgsSeleccionado.ReadOnly = true;
            this.tx_KgsSeleccionado.Size = new System.Drawing.Size(198, 20);
            this.tx_KgsSeleccionado.TabIndex = 42;
            // 
            // Btn_ObtenerKgsSel
            // 
            this.Btn_ObtenerKgsSel.Location = new System.Drawing.Point(393, 152);
            this.Btn_ObtenerKgsSel.Name = "Btn_ObtenerKgsSel";
            this.Btn_ObtenerKgsSel.Size = new System.Drawing.Size(114, 25);
            this.Btn_ObtenerKgsSel.TabIndex = 43;
            this.Btn_ObtenerKgsSel.Text = "Kilos Seleccionados";
            this.Btn_ObtenerKgsSel.UseVisualStyleBackColor = true;
            this.Btn_ObtenerKgsSel.Click += new System.EventHandler(this.Btn_ObtenerKgsSel_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Image = global::EstadosdePagos.Properties.Resources.guardar;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(349, 587);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 23);
            this.btnGuardar.TabIndex = 27;
            this.btnGuardar.Text = "Grabar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Image = global::EstadosdePagos.Properties.Resources.cancelar;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(572, 587);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(87, 23);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Image = global::EstadosdePagos.Properties.Resources.aceptar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(479, 587);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(87, 23);
            this.btnAceptar.TabIndex = 18;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Visible = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // Btn_Otros
            // 
            this.Btn_Otros.Location = new System.Drawing.Point(533, 153);
            this.Btn_Otros.Name = "Btn_Otros";
            this.Btn_Otros.Size = new System.Drawing.Size(114, 25);
            this.Btn_Otros.TabIndex = 44;
            this.Btn_Otros.Text = "Otros Conceptos";
            this.Btn_Otros.UseVisualStyleBackColor = true;
            this.Btn_Otros.Click += new System.EventHandler(this.Btn_Otros_Click);
            // 
            // Lbl_totalOtros
            // 
            this.Lbl_totalOtros.AutoSize = true;
            this.Lbl_totalOtros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_totalOtros.Location = new System.Drawing.Point(663, 159);
            this.Lbl_totalOtros.Name = "Lbl_totalOtros";
            this.Lbl_totalOtros.Size = new System.Drawing.Size(40, 13);
            this.Lbl_totalOtros.TabIndex = 45;
            this.Lbl_totalOtros.Text = "(dato)";
            this.Lbl_totalOtros.Click += new System.EventHandler(this.Lbl_totalOtros_Click);
            // 
            // Btn_Adjunto
            // 
            this.Btn_Adjunto.Location = new System.Drawing.Point(220, 51);
            this.Btn_Adjunto.Name = "Btn_Adjunto";
            this.Btn_Adjunto.Size = new System.Drawing.Size(96, 25);
            this.Btn_Adjunto.TabIndex = 46;
            this.Btn_Adjunto.Text = "Agregar Adjunto";
            this.Btn_Adjunto.UseVisualStyleBackColor = true;
            this.Btn_Adjunto.Click += new System.EventHandler(this.Btn_Adjunto_Click);
            // 
            // frmEPNueva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 615);
            this.Controls.Add(this.Btn_Adjunto);
            this.Controls.Add(this.Lbl_totalOtros);
            this.Controls.Add(this.Btn_Otros);
            this.Controls.Add(this.Btn_ObtenerKgsSel);
            this.Controls.Add(this.tx_KgsSeleccionado);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblDestinatarios);
            this.Controls.Add(this.lblValorKiloSuministro);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblFechaCreacion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFechaPresentEP);
            this.Controls.Add(this.txtDiaPresentEP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.tabCrecionEP);
            this.Controls.Add(this.lblTecnicoObra);
            this.Controls.Add(this.lblObra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEPNueva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Creacion de un EP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEPNueva_Load);
            this.tabCrecionEP.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasDespacho)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtiquetas)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_OtrasGuias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCrecionEP;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTecnicoObra;
        private System.Windows.Forms.Label lblObra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.DataGridView dgvGuiasDespacho;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvEtiquetas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGuiaDespacho;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboIT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRegistrosGDespacho;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRegistrosEtiquetas;
        private System.Windows.Forms.Button btnDesmarcarTodo;
        private System.Windows.Forms.Button btnMarcarTodo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiaPresentEP;
        private System.Windows.Forms.DateTimePicker dtpFechaPresentEP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFechaCreacion;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblValorKiloSuministro;
        private System.Windows.Forms.Label lblDestinatarios;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tx_KgsSeleccionado;
        private System.Windows.Forms.Button Btn_ObtenerKgsSel;
        private System.Windows.Forms.Button Btn_Otros;
        private System.Windows.Forms.Label Lbl_totalOtros;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView Dtg_OtrasGuias;
        private System.Windows.Forms.Button Btn_Adjunto;
    }
}