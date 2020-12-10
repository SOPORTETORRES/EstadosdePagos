using System;
using System.Windows.Forms;
using System.Drawing;
using CommonLibrary2;
using System.Collections.Generic;
using System.Data;

namespace EstadosdePagos
{
    public partial class frmEPNueva : Form
    {
        public bool changed = false;
        private Forms forms = new Forms();
        private DataTable dtEtiquetasINET = null;
        private Utils utils = new Utils();

        #region Getters y Setters

        private string _empresa = "TO";
        private int _ep_id = 0;
        private string _ep_obra = "";
        private string _obra = "";
        private string _tecnicoObra = "";
        private string _estado = "";
        private string _TieneGuias = "";

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public int Ep_id
        {
            get { return _ep_id; }
            set { _ep_id = value; }
        }

        public string Ep_obra
        {
            get { return _ep_obra; }
            set { _ep_obra = value; }
        }

        public string Obra
        {
            get { return _obra; }
            set { _obra = value; }
        }


        public string TecnicoObra
        {
            get { return _tecnicoObra; }
            set { _tecnicoObra = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string TieneGuias
        {
            get { return _TieneGuias; }
            set { _TieneGuias = value; }
        }

        #endregion

        private const string COLUMNNAME_MARCA = "MARCA";
        private const string COLUMNNAME_MARCA_ORIGINAL = "MARCA_ORIGINAL";
        private const string COLUMNNAME_NROGUIAINET = "NroGuiaINET";
        private const string COLUMNNAME_NROETIQUETAS = "NroEtiquetas";
        private const string COLUMNNAME_ETIQUETASCONEP = "EtiquetasconEP";

        #region "Metodos Publicos"

        public void CargaFormulario(ref ProgressBar Pb, ref Label Lbl_PB, Boolean mContieneGuias )
        {
            cargarInfoObrayGuiaDespacho_INET(ref Pb, ref Lbl_PB, mContieneGuias);

        }

        #endregion

        public frmEPNueva()
        {
            InitializeComponent();
            this.dgvGuiasDespacho.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvGuiasDespacho_RowPostPaint);
            this.dgvEtiquetas.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvEtiquetas_RowPostPaint);
        }

        private void dgvGuiasDespacho_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvGuiasDespacho.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvEtiquetas_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvEtiquetas.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private bool comentarioRequerido() {
            bool required = false;
            switch (_estado.ToString() )
            {
                case "P05":
                    if (txtComentario.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("- Debe ingresar un comentario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtComentario.Focus();
                        required = true;
                    }
                    
                    break;
                case "P15":
                    if (txtComentario.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("- Debe ingresar un comentario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtComentario.Focus();
                        required = true;
                    }

                    break;            }


            //if (this._estado.Equals("P15") && txtComentario.Text.Trim().Equals("")) //P15-ENVIADO A CLIENTE
            //{
            //    MessageBox.Show("- Debe ingresar un comentario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtComentario.Focus();
            //    required = true;
            //}
            return required;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!comentarioRequerido()) {
                if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                    utils.enviarCorreoNotificacionInterna("EP_MODIFICADO", _ep_obra,  _ep_id, "", "", _obra);
                if (tabCrecionEP.SelectedIndex == 0)
                {
                    grabarSeleccionxGuiaDespacho();
                    // Debe preguntar si desea agregar alguno otro cobro ( agregar OTROS)
                    if (MessageBox.Show("¿Desea agregar algún otro cobro ?", "Avisos Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Btn_Otros_Click(null, null);

                    //Se debe preguntar si se desea agregar comentario y adjunto  formulario para agregar comentario y Adjunto
                    // Mientras no se ingresese un comentarios y adjunto no se debe dejar continuar
                    if (MessageBox .Show ("¿Desea adjuntar algún documento?","Avisos Sistema",MessageBoxButtons.YesNo ,MessageBoxIcon.Question)==DialogResult.Yes )
                            CargaAdj_Comentario();


                    this.Close();
                }
                   
                else
                    grabarSeleccionxEtiquetas();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!comentarioRequerido())
            {
                btnGuardar_Click(sender, e);
                //this.Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close ();
        }

        private void frmEPNueva_Load(object sender, EventArgs e)
        {
            if (!this._ep_id.Equals(0)) //EP existente
            {
                this.Text = (!this._estado.Equals("P15") ? "Modificacion de un EP" : "Modificacion de un E.P. x Obs. del cliente"); //P15-ENVIADO A CLIENTE

                if (this.dgvGuiasDespacho.Rows.Count ==0)
                        this.btnGuardar.Visible = false; //Desactiva el boton Guardar cuando son modificaciones
                else
                    this.btnGuardar.Visible = true ;

            }
            lblID.Text = _ep_id.ToString();
            lblObra.Text = _obra;
            lblTecnicoObra.Text = _tecnicoObra;
            dgvEtiquetas.Tag = "";  //Se utiliza para detectar el cambio de guia de despacho en la pestaña etiquetas
            cboIT.Tag = "";         //Se utiliza para detectar el cambio de IT en la pestaña etiquetas
                                    //cargarInfoObrayGuiaDespacho_INET();
                                    //RevisaDatosCargados();
            RevisaGrilla();
            CargaDatosObra(this._ep_obra);
            ////dgvGuiasDespacho.TabIndex = 1;

            TabPage lTabTmp = new TabPage (); TabPage lTabTmp2 = new TabPage();
            lTabTmp = tabCrecionEP.TabPages[1];
            lTabTmp2 = tabCrecionEP.TabPages[2];
            tabCrecionEP.TabPages.Remove(lTabTmp);
            tabCrecionEP.TabPages.Remove(lTabTmp2);

            if (lblID.Text.ToUpper().Equals("0"))
                Btn_Otros.Enabled = false;
            else
                Btn_Otros.Enabled = true;
        }


        private void Refrescar(ref ProgressBar Pb, ref Label Lbl_PB)
        {
            Lbl_PB.Refresh();
            Pb.Refresh();
            Application.DoEvents();

        }

        private void RevisaDatosCargados()
        {
            //rowEtiqueta["KgsReales"].ToString())
            DataTable lTbl = new DataTable(); int i = 0;Utils lComun = new Utils();
            lTbl = (DataTable)dgvGuiasDespacho.DataSource ;
            for (i = 0; i < dgvGuiasDespacho.Rows .Count ; i++)
            {
                if (lComun.EsNumero(dgvGuiasDespacho.Rows[i].Cells ["KgsReales"].Value .ToString()) == false)
                {
                    dgvGuiasDespacho.Rows[i].Cells["KgsReales"].Style.ForeColor = Color.Red;
                }
              }

        }

        private void cargarInfoObrayGuiaDespacho_INET(ref ProgressBar Pb, ref Label Lbl_PB,Boolean incluyeGuias )
        {
            //WsTo.Operacion wsTo = null;
            //WsTo.ListaDataSet listaDataSetTo = null;
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            Result result = new Result();
            DataTable dataTable = null, dtGuiasDespachoINET = null;
            Int32 counter = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (incluyeGuias == true)
                    TieneGuias = "S";
                else
                    TieneGuias = "N";


                //wsTo = new WsTo.Operacion();
                //Obtiene el dia de presentacion de los EP, y la fecha de creacion de la Obra
                Lbl_PB.Text = ". . . OBTENIENDO DATOS INICIALES . . . "; Refrescar(ref Pb, ref Lbl_PB);

                listaDataSetOp = wsOperacion.ObtenerObrasActivas_EP(this._empresa);
                if (listaDataSetOp.MensajeError.Equals(""))
                {
                    DataRow[] foundRows = listaDataSetOp.DataSet.Tables[0].Select("IdObra = '" + this._ep_obra + "'");
                    if (foundRows.Length > 0)
                    {
                        txtDiaPresentEP.Text = foundRows[0]["diaPresentacion_EP"].ToString();
                        lblFechaCreacion.Text = foundRows[0]["fechaCreacion"].ToString();
                        //--2017-01-11
                        //Valor kilo suministro
                        string valorKiloSuministro = wsOperacion.ValorKiloSuministro_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                        lblValorKiloSuministro.Text = (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro);

                        //Obtiene los destinatarios de la obra
                        result = utils.obtenerDestinatariosObra(this._ep_obra);
                        if (result.MensajeError.Equals(""))
                            lblDestinatarios.Text = result.StringValue;
                        //else
                        //    error = result.MensajeError;
                        //--2017-01-11
                        if ((_ep_id.Equals(0)) && (txtDiaPresentEP.Text != ""))
                            dtpFechaPresentEP.Value = Convert.ToDateTime(txtDiaPresentEP.Text + DateTime.Now.AddMonths(1).ToString("-MM-yyyy"));
                        else
                        {
                            wsOperacion = new WsOperacion.Operacion();
                            listaDataSetOp = new WsOperacion.ListaDataSet();
                            listaDataSetOp = wsOperacion.ListarEstadoPago(this._ep_id);
                            if (listaDataSetOp.MensajeError.Equals(""))
                            {
                                dataTable = listaDataSetOp.DataSet.Tables[0];
                                if (dataTable.Rows.Count > 0)
                                    dtpFechaPresentEP.Value = Convert.ToDateTime(dataTable.Rows[0]["fecha_prox_present"].ToString());
                            }
                            //else
                            //    result.MensajeError = listaDataSetOp.MensajeError.ToString();
                        }
                    }
                }
                if (incluyeGuias == true)
                {
                    Lbl_PB.Text = ". . . OBTENIENDO GUÍAS DE DESPACHO . . . "; Refrescar(ref Pb, ref Lbl_PB);

                    //Obtiene las guias de despacho, pendientes por asignar a un EP
                    listaDataSetOp = wsOperacion.ObtenerGuiasDesdeInicio_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                    if (listaDataSetOp.MensajeError.Equals(""))
                    {
                        dtGuiasDespachoINET = listaDataSetOp.DataSet.Tables[0];

                        //Inserta la columna MARCA y la columna EtiquetasconEP para mostrar los registros ya asociados a un EP
                        DataColumn marca = new DataColumn(COLUMNNAME_MARCA, typeof(bool));
                        dtGuiasDespachoINET.Columns.Add(marca);
                        marca.SetOrdinal(0);
                        DataColumn etiquetasconEP = new DataColumn(COLUMNNAME_ETIQUETASCONEP, typeof(Int32));
                        dtGuiasDespachoINET.Columns.Add(etiquetasconEP);
                        if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                        {
                            DataColumn marcaOriginal = new DataColumn(COLUMNNAME_MARCA_ORIGINAL, typeof(bool));
                            dtGuiasDespachoINET.Columns.Add(marcaOriginal);
                        }
                        //Recorre el dataTable para contar las etiquetas de la guia de despacho que ya cuentan con EP

                        int lCont = 0;
                        Lbl_PB.Text = ". . . REVISANDO GUIAS DE DESPACHO . . . ";
                        Pb.Maximum = dtGuiasDespachoINET.Rows.Count; Pb.Minimum = 1; Pb.Value = 1;
                        Refrescar(ref Pb, ref Lbl_PB);

                        foreach (DataRow row in dtGuiasDespachoINET.Rows)
                        {
                            lCont++;
                            //Obtiene la cantidad de registros x Guia de despacho que existen en el EP
                            result = cargarInfoEtiquetasxGuiaDespacho_EP(row[COLUMNNAME_NROGUIAINET].ToString());
                            counter = 0;
                            if (result.MensajeError.Equals(""))
                                counter = result.DataTable.Rows.Count;
                            row[COLUMNNAME_ETIQUETASCONEP] = counter;
                            if (counter > 0) //Marca los registros de las guias de despacho que existen en la tabla EP
                                row[COLUMNNAME_MARCA] = true;
                            if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE -> Se utiliza para detectar las modificaciones solicitadas por el cliente
                                row[COLUMNNAME_MARCA_ORIGINAL] = row[COLUMNNAME_MARCA];

                            //Verifica si el registro (Guia despacho) debe mostrarse o no
                            if (this._ep_id.Equals(0))
                            {
                                if (Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) { } //Si qEtiquetasINET > qEtiquetasEP -> Mostrar
                                else
                                    row.Delete();
                            }
                            else
                            {
                                if ((Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) || counter > 0) { } //Si qEtiquetasINET > qEtiquetasEP O gd existe EP -> Mostrar
                                else
                                    row.Delete();
                            }

                            //actualizamos la barra de avance
                            if (Pb.Value < Pb.Maximum)
                                Pb.Value = Pb.Value + 1;
                            //else
                                //Pb.Value = Pb.Value - 1;

                            Refrescar(ref Pb, ref Lbl_PB);
                            //fin

                        }
                      

                        Lbl_PB.Text = ". . . REALIZANDO CALCULOS FINALES . . . "; Refrescar(ref Pb, ref Lbl_PB);
                        dgvGuiasDespacho.DataSource = dtGuiasDespachoINET;
                        utils.bloquearColumnas(dgvGuiasDespacho);
                        forms.dataGridViewHideColumns(dgvGuiasDespacho, new string[] { "Column1", COLUMNNAME_MARCA_ORIGINAL });
                        new Utils().estiloMillaresDataGridViewColumn(dgvGuiasDespacho, new string[] { "Kgsguia" });
                        forms.dataGridViewAutoSizeColumnsMode(dgvGuiasDespacho, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        dgvGuiasDespacho.Columns["fechaDespacho"].DisplayIndex = dgvGuiasDespacho.Columns.Count - 1;
                        lblRegistrosGDespacho.Text = "Registro(s): " + dgvGuiasDespacho.Rows.Count;

                        Btn_ObtenerKgsSel_Click(null, null);
                    }
                    else
                        MessageBox.Show(listaDataSetOp.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {


                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;    
        }


        public void CargarDatos_Ep(ref ProgressBar Pb, ref Label Lbl_PB, string lNro_EP )
        {
            //WsTo.Operacion wsTo = null;
            //WsTo.ListaDataSet listaDataSetTo = null;
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            Result result = new Result();
            DataTable dataTable = null, dtGuiasDespachoINET = null;
            Int32 counter = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //wsTo = new WsTo.Operacion();
                //Obtiene el dia de presentacion de los EP, y la fecha de creacion de la Obra
                Lbl_PB.Text = ". . . OBTENIENDO DATOS INICIALES . . . "; Refrescar(ref Pb, ref Lbl_PB);

                listaDataSetOp = wsOperacion.ObtenerObrasActivas_EP(this._empresa);
                if (listaDataSetOp.MensajeError.Equals(""))
                {
                    DataRow[] foundRows = listaDataSetOp.DataSet.Tables[0].Select("IdObra = '" + this._ep_obra + "'");
                    if (foundRows.Length > 0)
                    {
                        txtDiaPresentEP.Text = foundRows[0]["diaPresentacion_EP"].ToString();
                        lblFechaCreacion.Text = foundRows[0]["fechaCreacion"].ToString();
                        //--2017-01-11
                        //Valor kilo suministro
                        string valorKiloSuministro = wsOperacion.ValorKiloSuministro_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                        lblValorKiloSuministro.Text = (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro);

                        //Obtiene los destinatarios de la obra
                        result = utils.obtenerDestinatariosObra(this._ep_obra);
                        if (result.MensajeError.Equals(""))
                            lblDestinatarios.Text = result.StringValue;
                        //else
                        //    error = result.MensajeError;
                        //--2017-01-11
                        if ((_ep_id.Equals(0)) && (txtDiaPresentEP.Text != ""))
                            dtpFechaPresentEP.Value = Convert.ToDateTime(txtDiaPresentEP.Text + DateTime.Now.AddMonths(1).ToString("-MM-yyyy"));
                        else
                        {
                            wsOperacion = new WsOperacion.Operacion();
                            listaDataSetOp = new WsOperacion.ListaDataSet();
                            listaDataSetOp = wsOperacion.ListarEstadoPago(this._ep_id);
                            if (listaDataSetOp.MensajeError.Equals(""))
                            {
                                dataTable = listaDataSetOp.DataSet.Tables[0];
                                if (dataTable.Rows.Count > 0)
                                    dtpFechaPresentEP.Value = Convert.ToDateTime(dataTable.Rows[0]["fecha_prox_present"].ToString());
                            }
                            //else
                            //    result.MensajeError = listaDataSetOp.MensajeError.ToString();
                        }
                    }
                }
                //if (incluyeGuias == true)
                //{
                    Lbl_PB.Text = ". . . OBTENIENDO GUÍAS DE DESPACHO . . . "; Refrescar(ref Pb, ref Lbl_PB);

                    //Obtiene las guias de despacho, pendientes por asignar a un EP
                    listaDataSetOp = wsOperacion.ObtenerDatos_EP(this._ep_obra, this._ep_id.ToString () );
                    if (listaDataSetOp.MensajeError.Equals(""))
                    {
                    // hay 3 tablas en el Dataset EP - Otros - Guias 
                        dtGuiasDespachoINET = listaDataSetOp.DataSet.Tables["Guias"];

                        //Inserta la columna MARCA y la columna EtiquetasconEP para mostrar los registros ya asociados a un EP
                        DataColumn marca = new DataColumn(COLUMNNAME_MARCA, typeof(bool));
                        dtGuiasDespachoINET.Columns.Add(marca);
                        marca.SetOrdinal(0);
                        DataColumn etiquetasconEP = new DataColumn(COLUMNNAME_ETIQUETASCONEP, typeof(Int32));
                        dtGuiasDespachoINET.Columns.Add(etiquetasconEP);
                        if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                        {
                            DataColumn marcaOriginal = new DataColumn(COLUMNNAME_MARCA_ORIGINAL, typeof(bool));
                            dtGuiasDespachoINET.Columns.Add(marcaOriginal);
                        }
                        //Recorre el dataTable para contar las etiquetas de la guia de despacho que ya cuentan con EP

                        int lCont = 0;
                        Lbl_PB.Text = ". . . REVISANDO GUIAS DE DESPACHO . . . ";
                    if (dtGuiasDespachoINET.Rows.Count > 0)
                    {
                        Pb.Maximum = dtGuiasDespachoINET.Rows.Count; Pb.Minimum = 1; Pb.Value = 1;
                    }

                        
                        Refrescar(ref Pb, ref Lbl_PB);

                        foreach (DataRow row in dtGuiasDespachoINET.Rows)
                        {
                            lCont++;
                            //Obtiene la cantidad de registros x Guia de despacho que existen en el EP
                            result = cargarInfoEtiquetasxGuiaDespacho_EP(row[COLUMNNAME_NROGUIAINET].ToString());
                            counter = 0;
                            if (result.MensajeError.Equals(""))
                                counter = result.DataTable.Rows.Count;
                            row[COLUMNNAME_ETIQUETASCONEP] = counter;
                            if (counter > 0) //Marca los registros de las guias de despacho que existen en la tabla EP
                                row[COLUMNNAME_MARCA] = true;
                            if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE -> Se utiliza para detectar las modificaciones solicitadas por el cliente
                                row[COLUMNNAME_MARCA_ORIGINAL] = row[COLUMNNAME_MARCA];

                            //Verifica si el registro (Guia despacho) debe mostrarse o no
                            if (this._ep_id.Equals(0))
                            {
                                if (Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) { } //Si qEtiquetasINET > qEtiquetasEP -> Mostrar
                                else
                                    row.Delete();
                            }
                            else
                            {
                                if ((Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) || counter > 0) { } //Si qEtiquetasINET > qEtiquetasEP O gd existe EP -> Mostrar
                                else
                                    row.Delete();
                            }

                            //actualizamos la barra de avance
                            if (Pb.Value < Pb.Maximum)
                                Pb.Value = Pb.Value + 1;
                            //else
                            //Pb.Value = Pb.Value - 1;

                            Refrescar(ref Pb, ref Lbl_PB);
                            //fin

                        }

                    if (this._estado.Equals("P45")) //P15-ENVIADO A CLIENTE
                    {
                        btnGuardar.Enabled = false;
                        btnAceptar.Enabled = false;
                    } 
                    else
                    {
                        btnGuardar.Enabled = true;
                        btnAceptar.Enabled = true ;

                    }



                    Lbl_PB.Text = ". . . REALIZANDO CALCULOS FINALES . . . "; Refrescar(ref Pb, ref Lbl_PB);
                        dgvGuiasDespacho.DataSource = dtGuiasDespachoINET;
                        utils.bloquearColumnas(dgvGuiasDespacho);
                        forms.dataGridViewHideColumns(dgvGuiasDespacho, new string[] { "Column1", COLUMNNAME_MARCA_ORIGINAL });
                        new Utils().estiloMillaresDataGridViewColumn(dgvGuiasDespacho, new string[] { "Kgsguia" });
                        forms.dataGridViewAutoSizeColumnsMode(dgvGuiasDespacho, DataGridViewAutoSizeColumnsMode.DisplayedCells);

                    if (dtGuiasDespachoINET.Rows.Count > 0)
                    {
                        dgvGuiasDespacho.Columns["fechaDespacho"].DisplayIndex = dgvGuiasDespacho.Columns.Count - 1;
                    }
                    
                        lblRegistrosGDespacho.Text = "Registro(s): " + dgvGuiasDespacho.Rows.Count;

                        Btn_ObtenerKgsSel_Click(null, null);
                    }
                    else
                        MessageBox.Show(listaDataSetOp.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        #region Modificacion EP

        private void CargaCabecera()
        {
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            Result result = new Result();  DataTable dataTable = null;

            listaDataSetOp = wsOperacion.ObtenerObrasActivas_EP(this._empresa);
            if (listaDataSetOp.MensajeError.Equals(""))
            {
                DataRow[] foundRows = listaDataSetOp.DataSet.Tables[0].Select("IdObra = '" + this._ep_obra + "'");
                if (foundRows.Length > 0)
                {
                    txtDiaPresentEP.Text = foundRows[0]["diaPresentacion_EP"].ToString();
                    lblFechaCreacion.Text = foundRows[0]["fechaCreacion"].ToString();
                    //--2017-01-11
                    //Valor kilo suministro
                    string valorKiloSuministro = wsOperacion.ValorKiloSuministro_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                    lblValorKiloSuministro.Text = (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro);

                    //Obtiene los destinatarios de la obra
                    result = utils.obtenerDestinatariosObra(this._ep_obra);
                    if (result.MensajeError.Equals(""))
                        lblDestinatarios.Text = result.StringValue;
                    //else
                    //    error = result.MensajeError;
                    //--2017-01-11
                    if (_ep_id.Equals(0))
                        dtpFechaPresentEP.Value = Convert.ToDateTime(txtDiaPresentEP.Text + DateTime.Now.AddMonths(1).ToString("-MM-yyyy"));
                    else
                    {
                        wsOperacion = new WsOperacion.Operacion();
                        listaDataSetOp = new WsOperacion.ListaDataSet();
                        listaDataSetOp = wsOperacion.ListarEstadoPago(this._ep_id);
                        if (listaDataSetOp.MensajeError.Equals(""))
                        {
                            dataTable = listaDataSetOp.DataSet.Tables[0];
                            if (dataTable.Rows.Count > 0)
                                dtpFechaPresentEP.Value = Convert.ToDateTime(dataTable.Rows[0]["fecha_prox_present"].ToString());
                        }
                        //else
                        //    result.MensajeError = listaDataSetOp.MensajeError.ToString();
                    }
                }
            }
        }


        private void cargarInfoObrayGuiaDespacho_INET_V2(ref ProgressBar Pb, ref Label Lbl_PB)
        {
            //WsTo.Operacion wsTo = null;
            //WsTo.ListaDataSet listaDataSetTo = null;
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            Result result = new Result();
            DataTable dataTable = null, dtGuiasDespachoINET = null;
            Int32 counter = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //wsTo = new WsTo.Operacion();
                //Obtiene el dia de presentacion de los EP, y la fecha de creacion de la Obra
                Lbl_PB.Text = ". . . OBTENIENDO DATOS INICIALES . . . "; Refrescar(ref Pb, ref Lbl_PB);
                CargaCabecera();
                
                Lbl_PB.Text = ". . . OBTENIENDO GUÍAS DE DESPACHO . . . "; Refrescar(ref Pb, ref Lbl_PB);

                //Obtiene las guias de despacho, pendientes por asignar a un EP
                listaDataSetOp = wsOperacion.ObtenerGuiasDesdeInicio_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                if (listaDataSetOp.MensajeError.Equals(""))
                {
                    dtGuiasDespachoINET = listaDataSetOp.DataSet.Tables[0];

                    //Inserta la columna MARCA y la columna EtiquetasconEP para mostrar los registros ya asociados a un EP
                    DataColumn marca = new DataColumn(COLUMNNAME_MARCA, typeof(bool));
                    dtGuiasDespachoINET.Columns.Add(marca);
                    marca.SetOrdinal(0);
                    DataColumn etiquetasconEP = new DataColumn(COLUMNNAME_ETIQUETASCONEP, typeof(Int32));
                    dtGuiasDespachoINET.Columns.Add(etiquetasconEP);
                    if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                    {
                        DataColumn marcaOriginal = new DataColumn(COLUMNNAME_MARCA_ORIGINAL, typeof(bool));
                        dtGuiasDespachoINET.Columns.Add(marcaOriginal);
                    }
                    //Recorre el dataTable para contar las etiquetas de la guia de despacho que ya cuentan con EP

                    int lCont = 0;
                    Lbl_PB.Text = ". . . REVISANDO GUIAS DE DESPACHO . . . ";
                    Pb.Maximum = dtGuiasDespachoINET.Rows.Count; Pb.Minimum = 1; Pb.Value = 1;
                    //Refrescar(ref Pb, ref Lbl_PB);

                    foreach (DataRow row in dtGuiasDespachoINET.Rows)
                    {
                        lCont++;
                        //Obtiene la cantidad de registros x Guia de despacho que existen en el EP
                        result = cargarInfoEtiquetasxGuiaDespacho_EP(row[COLUMNNAME_NROGUIAINET].ToString());
                        counter = 0;
                        if (result.MensajeError.Equals(""))
                            counter = result.DataTable.Rows.Count;
                        row[COLUMNNAME_ETIQUETASCONEP] = counter;
                        if (counter > 0) //Marca los registros de las guias de despacho que existen en la tabla EP
                            row[COLUMNNAME_MARCA] = true;
                        if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE -> Se utiliza para detectar las modificaciones solicitadas por el cliente
                            row[COLUMNNAME_MARCA_ORIGINAL] = row[COLUMNNAME_MARCA];

                        //Verifica si el registro (Guia despacho) debe mostrarse o no
                        if (this._ep_id.Equals(0))
                        {
                            if (Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) { } //Si qEtiquetasINET > qEtiquetasEP -> Mostrar
                            else
                                row.Delete();
                        }
                        else
                        {
                            if ((Convert.ToInt32(row[COLUMNNAME_NROETIQUETAS].ToString()) > Convert.ToInt32(row[COLUMNNAME_ETIQUETASCONEP].ToString())) || counter > 0) { } //Si qEtiquetasINET > qEtiquetasEP O gd existe EP -> Mostrar
                            else
                                row.Delete();
                        }

                        //actualizamos la barra de avance
                        if (Pb.Value < Pb.Maximum)
                            Pb.Value = Pb.Value + 1;
                        else
                            Pb.Value = Pb.Value - 1;

                        Refrescar(ref Pb, ref Lbl_PB);
                        //fin

                    }

                    Lbl_PB.Text = ". . . REALIZANDO CALCULOS FINALES . . . "; Refrescar(ref Pb, ref Lbl_PB);
                    dgvGuiasDespacho.DataSource = dtGuiasDespachoINET;
                    utils.bloquearColumnas(dgvGuiasDespacho);
                    forms.dataGridViewHideColumns(dgvGuiasDespacho, new string[] { "Column1", COLUMNNAME_MARCA_ORIGINAL });
                    new Utils().estiloMillaresDataGridViewColumn(dgvGuiasDespacho, new string[] { "Kgsguia" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvGuiasDespacho, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    dgvGuiasDespacho.Columns["fechaDespacho"].DisplayIndex = dgvGuiasDespacho.Columns.Count - 1;
                    lblRegistrosGDespacho.Text = "Registro(s): " + dgvGuiasDespacho.Rows.Count;

                }
                else
                    MessageBox.Show(listaDataSetOp.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        private void RevisaGrilla()
        {
            int i = 0;Utils lCom = new Utils();int j = 0;
            for (i = 0; i < dgvGuiasDespacho.RowCount; i++)
            {
                // if (lCom.EsNumero(dgvGuiasDespacho.Rows[i].Cells["Kgsguia"].Value.ToString()) == false)
                if ( dgvGuiasDespacho.Rows[i].Cells["Kgsguia"].Value.ToString().Trim ().Length ==0)
                {
                    for (j=1;j< dgvGuiasDespacho.ColumnCount;j++)
                           dgvGuiasDespacho.Rows[i].Cells[j].Style.BackColor = Color.LightSalmon;

                }

            }
        }

        private void CargaDatosObra(string idObra)
        {
            ////string valorKiloSuministro = wsOperacion.ValorKiloSuministro_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;  

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat(" Select * from Obras Where id=",this ._ep_obra);
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    txtDiaPresentEP.Text = lTbl.Rows[0]["DiaPresentacion_EP"].ToString();
                    lblFechaCreacion.Text = lTbl.Rows[0]["FechaCrea"].ToString();
                    string valorKiloSuministro = wsOperacion.ValorKiloSuministro_EP(this._ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                    lblValorKiloSuministro.Text = valorKiloSuministro;

                    if (this.Ep_id > 0)
                    {
                        lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", this.Ep_id, ",0,' ', 0,0,'','','','',7,''");
                        lDts = lPx.ObtenerDatos(lSql);
                        if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                        {
                            Lbl_totalOtros.Text = (Convert.ToInt32(lDts.Tables[0].Rows[0][0].ToString())).ToString("#,##0");
                            //btnGuardar.Enabled = true;
                            //btnAceptar.Enabled = true;
                        }
                    }
                    else
                    {
                        lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0,", this._ep_obra, ", ' ',0,0,'','','','',12,''");
                        lDts = lPx.ObtenerDatos(lSql);
                        if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                        {
                            Lbl_totalOtros.Text = (Convert.ToInt32(lDts.Tables[0].Rows[0][0].ToString())).ToString("#,##0");
                            if (Convert.ToInt32(lDts.Tables[0].Rows[0][0].ToString())>0)
                            { 
                                btnGuardar.Enabled = true;
                                btnAceptar.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception iEx)
            {

            }
        }
           

        private void cargarInfoEtiquetasxGuiaDespacho_INET(string guiaDespacho)
        {
            DataTable dtIT = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!dgvEtiquetas.Tag.Equals(guiaDespacho)) {
                    WsOperacion.Operacion ws = new WsOperacion.Operacion();
                    WsOperacion .ListaDataSet listaDataSet = ws.ObtenerITPorGuiaDetalle_EP(this._ep_obra, guiaDespacho);
                    if (listaDataSet.MensajeError.Equals(""))
                    {
                        dtEtiquetasINET = listaDataSet.DataSet.Tables[0];

                        //Inserta la columna MARCA para mostrar los registros ya agregados al EP
                        DataColumn marca = new DataColumn(COLUMNNAME_MARCA, typeof(bool));
                        dtEtiquetasINET.Columns.Add(marca);
                        marca.SetOrdinal(0);
                        if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                        {
                            DataColumn marcaOriginal = new DataColumn(COLUMNNAME_MARCA_ORIGINAL, typeof(bool));
                            dtEtiquetasINET.Columns.Add(marcaOriginal);
                        }
                        //Carga el combobox con las IT
                        DataView view = new DataView(dtEtiquetasINET);
                        dtIT = view.ToTable(true, "Codigo");

                        cboIT.Items.Clear();
                        cboIT.Tag = "";
                        foreach (DataRow row in dtIT.Rows)
                        {
                            cboIT.Items.Add(row[0].ToString());
                        }
                        if (cboIT.Items.Count > 0) {
                            cboIT.Enabled = true;
                            cboIT.SelectedIndex = 0;
                        }
                        dgvEtiquetas.DataSource = dtEtiquetasINET;
                        dgvEtiquetas.Tag = guiaDespacho;
                    }
                    else
                        MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private Result cargarInfoEtiquetasxGuiaDespacho_EP(string guiaDespacho)
        {
            Result result = new Result();
            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPDetalleEtiquetasxGuiaDespacho(Convert.ToInt32(guiaDespacho));
                if (listaDataSet.MensajeError.Equals(""))
                    result.DataTable = listaDataSet.DataSet.Tables[0];
                else
                    result.MensajeError = listaDataSet.MensajeError.ToString();
            }
            catch (Exception exc)
            {
                result.MensajeError = exc.Message;
            }
            return result;
        }

        private bool grabarSeleccionxGuiaDespacho()
        {
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
            int counter = 0;Utils lComun = new Utils();
            string error = "", guia_Despacho = "";

            this.dgvGuiasDespacho.EndEdit();
            counter = utils.dataGridViewCountRowsChecked(dgvGuiasDespacho, COLUMNNAME_MARCA);
            if (counter > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    //Graba el EP para generar el ID
                    //if (_ep_id.Equals(0))
                    //{
                    //estado_Pago = wsOperacion.CrearEP(_ep_obra, dtpFechaPresentEP.Value, Program.currentUser.Login, Program.currentUser.ComputerName);
                    estado_Pago = wsOperacion.RegistrarEP(_ep_obra, _ep_id, dtpFechaPresentEP.Value, txtComentario.Text.Trim(), Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
                        _ep_id = estado_Pago.Id;
                        lblID.Text = _ep_id.ToString();
                    }
                    else
                        error = estado_Pago.MensajeError.ToString();
                    //}
                    //else
                    if (error.Equals(""))
                    {
                        //Elimina la informacion completa del EP_Detalle, para actualizarla
                        estado_Pago = wsOperacion.EliminarEPCreacionEtiquetaxEP(_ep_obra, _ep_id, Program.currentUser.Login, Program.currentUser.ComputerName);
                        if (!estado_Pago.MensajeError.Equals(""))
                            error = estado_Pago.MensajeError.ToString();
                    }

                    if (error.Equals(""))
                    {
                        WsOperacion.Estado_Pago_Detalle etiqueta = null;
                        List<WsOperacion.Estado_Pago_Detalle> listaEtiquetas = new List<WsOperacion.Estado_Pago_Detalle>();

                        foreach (DataGridViewRow rowGuiaDespacho in dgvGuiasDespacho.Rows)
                        {
                            if (rowGuiaDespacho.Cells[COLUMNNAME_MARCA].Value != null)
                            {
                                if (!String.IsNullOrEmpty(rowGuiaDespacho.Cells[COLUMNNAME_MARCA].Value.ToString()))
                                {
                                    if (Convert.ToBoolean(rowGuiaDespacho.Cells[COLUMNNAME_MARCA].Value) == true)
                                    {
                                        //Obtiene las etiquetas de la guia de despacho
                                        //ObtenerEtiquetasPorViajeYGuia_EP (3) etiquetas
                                        //Parámetros de entrada: IdObra,  nroGuiaInet , codigo del viaje ( todos son string)
                                        guia_Despacho = rowGuiaDespacho.Cells["NroGuiaInet"].Value.ToString();

                                        WsOperacion.Operacion ws = new WsOperacion.Operacion();
                                        WsOperacion.ListaDataSet listaDataSet = ws.ObtenerITPorGuiaDetalle_EP(this._ep_obra, guia_Despacho);
                                        if (listaDataSet.MensajeError.Equals(""))
                                        {
                                            //Codigo -> it
                                            //NroGuiaInet -> gdespacho
                                            //IdPieza -> etiqueta
                                            foreach (DataRow rowEtiqueta in listaDataSet.DataSet.Tables[0].Rows)
                                            {
                                                etiqueta = new WsOperacion.Estado_Pago_Detalle();
                                                etiqueta.Obra = _ep_obra;
                                                etiqueta.Id = _ep_id; //EP
                                                etiqueta.Guia_despacho = guia_Despacho; //rowEtiqueta["NroGuiaInet"].ToString();
                                                etiqueta.It = rowEtiqueta["Codigo"].ToString();
                                                etiqueta.Etiqueta = rowEtiqueta["IdPaq"].ToString();
                                                if (lComun.EsNumero(rowEtiqueta["KgsPaquete"].ToString()) == true)
                                                    etiqueta.Kgs = Convert.ToInt32(rowEtiqueta["KgsPaquete"].ToString());
                                                else
                                                    etiqueta.Kgs = 0;


                                                listaEtiquetas.Add(etiqueta);
                                            }
                                        }
                                        else
                                        {
                                            error = listaDataSet.MensajeError.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (error.Equals("") && listaEtiquetas.Count > 0)
                        {
                            //Graba las etiquetas asociadas al EP
                            estado_Pago = wsOperacion.RegistrarEPCreacionEtiqueta(listaEtiquetas.ToArray(), Program.currentUser.Login, Program.currentUser.ComputerName);
                            if (!estado_Pago.MensajeError.Equals(""))
                                error = estado_Pago.MensajeError.ToString();

                            //Determina cuales fueron los registros modificados una vez enviado al cliente para registrar en el LOG
                            if (estado_Pago.MensajeError.Equals("") && this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                                registrarCambiosEPEnviadaaClte();
                        }
                    }
                    if (error.Equals(""))
                    {
                        MessageBox.Show("Proceso finalizado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.changed = true;
                    }
                    else
                        MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if ((lComun.Val(Lbl_totalOtros.Text) > 0) ) // || (TieneGuias=="N"))
                {
                    //1.- debemos guardar el EP 
                    estado_Pago = wsOperacion.RegistrarEP(_ep_obra, _ep_id, dtpFechaPresentEP.Value, txtComentario.Text.Trim(), Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
                        _ep_id = estado_Pago.Id;
                        lblID.Text = _ep_id.ToString();
                    }
                    else
                        error = estado_Pago.MensajeError.ToString();

                    //2.- Vinculamos el EP a los EP_Otros 
                    if (error.Equals(""))
                    {
                        WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
                        string lSql = "";
                        lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", _ep_id, ", ", _ep_obra, ",' ',0,0, '','','','',13,''");
                        lDts = lPx.ObtenerDatos(lSql);
                        if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                        {
                            if (lComun.Val(lDts.Tables[0].Rows[0][0].ToString())>0)
                            {
                                MessageBox.Show("Los Datos se han Grado Correctamente ", "Avisos sistema", MessageBoxButtons.OK);
                            }
                            else
                                MessageBox.Show("Hubo un problema en la grabación de los datos. ", "Avisos sistema", MessageBoxButtons.OK);
                        }                      
                     }
                }
                else
                {
                    if (TieneGuias == "N")
                    {
                        //1.- debemos guardar el EP 
                        estado_Pago = wsOperacion.RegistrarEP(_ep_obra, _ep_id, dtpFechaPresentEP.Value, txtComentario.Text.Trim(), Program.currentUser.Login, Program.currentUser.ComputerName);
                        if (estado_Pago.MensajeError.Equals(""))
                        {
                            _ep_id = estado_Pago.Id;
                            lblID.Text = _ep_id.ToString();
                        }
                        else
                            error = estado_Pago.MensajeError.ToString();

                        //2.- Vinculamos el EP a los EP_Otros 
                        if (error.Equals(""))
                        {
                            //WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
                            //string lSql = "";
                            //lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", _ep_id, ", ", _ep_obra, ",' ',0,0, '','','','',13");
                            //lDts = lPx.ObtenerDatos(lSql);
                            //if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                            //{
                            //    if (lComun.Val(lDts.Tables[0].Rows[0][0].ToString()) > 0)
                            //    {
                            //        MessageBox.Show("Los Datos se han Grado Correctamente ", "Avisos sistema", MessageBoxButtons.OK);
                            //    }
                            //    else
                            //        MessageBox.Show("Hubo un problema en la grabación de los datos. ", "Avisos sistema", MessageBoxButtons.OK);
                            //}
                        }

                    } 
                    else
                        MessageBox.Show("No existen registros seleccionados.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

            }

            return error.Equals("");
        }

        private void registrarCambiosEPEnviadaaClte()
        {
            List<String> listAgregadas = new List<String>();
            List<String> listEliminadas = new List<String>();
            string guia_Despacho = "", etiqueta = "";
            //P35 - MODIFICADO POR OBSERVACIONES DEL CLIENTE
            //P40 - MODIFICADO POR USUARIO ADMINISTRADOR
            string estadoModificadaPor = (!Program.currentUser.PerfilUsuario.Equals("ADMIN") ? "P35" : "P40");

            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();

                if (tabCrecionEP.SelectedIndex == 0)
                {
                    foreach (DataGridViewRow rowGuiaDespacho in dgvGuiasDespacho.Rows)
                    {
                        if (!rowGuiaDespacho.Cells[COLUMNNAME_MARCA].Value.Equals(rowGuiaDespacho.Cells[COLUMNNAME_MARCA_ORIGINAL].Value))
                        {
                            guia_Despacho = rowGuiaDespacho.Cells["NroGuiaInet"].Value.ToString();
                            if (Convert.ToBoolean(rowGuiaDespacho.Cells[COLUMNNAME_MARCA].Value) == true)
                                listAgregadas.Add(guia_Despacho);
                            else
                                listEliminadas.Add(guia_Despacho);
                        }
                    }
                    if (listAgregadas.Count > 0)  //P25 - GUIA DESPACHO AGREGADA AL EP
                        estado_Pago = wsOperacion.RegistrarEPCambiosGdEnviadaaClte(_ep_obra, _ep_id, estadoModificadaPor, listAgregadas.ToArray(), "P25", Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (listEliminadas.Count > 0) //P26 - GUIA DESPACHO ELIMINADA DEL EP
                    {
                        if (listAgregadas.Count > 0)
                            estadoModificadaPor = ""; //Evita que se registre 2 veces la marca de modificacion si ya se realizo al momento de agregar
                        estado_Pago = wsOperacion.RegistrarEPCambiosGdEnviadaaClte(_ep_obra, _ep_id, estadoModificadaPor, listEliminadas.ToArray(), "P26", Program.currentUser.Login, Program.currentUser.ComputerName);
                    }
                }
                else 
                {
                    foreach (DataGridViewRow rowEtiqueta in dgvEtiquetas.Rows)
                    {
                        if (!rowEtiqueta.Cells[COLUMNNAME_MARCA].Value.Equals(rowEtiqueta.Cells[COLUMNNAME_MARCA_ORIGINAL].Value))
                        {
                            etiqueta = rowEtiqueta.Cells["Etiqueta"].Value.ToString();
                            if (Convert.ToBoolean(rowEtiqueta.Cells[COLUMNNAME_MARCA].Value) == true)
                                listAgregadas.Add(etiqueta);
                            else
                                listEliminadas.Add(etiqueta);
                        }
                    }
                    if (listAgregadas.Count > 0)  //P30 - ETIQUETA AGREGADA AL EP
                        estado_Pago = wsOperacion.RegistrarEPCambiosGdEnviadaaClte(_ep_obra, _ep_id, estadoModificadaPor, listAgregadas.ToArray(), "P30", Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (listEliminadas.Count > 0) //P31 - ETIQUETA ELIMINADA DEL EP
                    {
                        if (listAgregadas.Count > 0)
                            estadoModificadaPor = ""; //Evita que se registre 2 veces la marca de modificacion si ya se realizo al momento de agregar
                        estado_Pago = wsOperacion.RegistrarEPCambiosGdEnviadaaClte(_ep_obra, _ep_id, estadoModificadaPor, listEliminadas.ToArray(), "P31", Program.currentUser.Login, Program.currentUser.ComputerName);
                    }
                }
                if (!estado_Pago.MensajeError.Equals(""))
                    MessageBox.Show(estado_Pago.MensajeError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool grabarSeleccionxEtiquetas()
        {
            int counter = 0;
            string error = "";

            this.dgvEtiquetas.EndEdit();
            counter = utils.dataGridViewCountRowsChecked(dgvEtiquetas, COLUMNNAME_MARCA);
            if (counter > 0 && !lblGuiaDespacho.Equals("") && !cboIT.Text.Equals(""))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();

                    //Graba el EP para generar el ID
                    //if (_ep_id.Equals(0))
                    //{
                        //estado_Pago = wsOperacion.CrearEP(_ep_obra, dtpFechaPresentEP.Value, Program.currentUser.Login, Program.currentUser.ComputerName);
                        estado_Pago = wsOperacion.RegistrarEP(_ep_obra, _ep_id, dtpFechaPresentEP.Value, txtComentario.Text.Trim(), Program.currentUser.Login, Program.currentUser.ComputerName);
                        if (estado_Pago.MensajeError.Equals(""))
                        {
                            _ep_id = estado_Pago.Id;
                            lblID.Text = _ep_id.ToString();
                        }
                        else
                            error = estado_Pago.MensajeError.ToString();
                    //}
                    //else
                    if (error.Equals(""))
                    {
                        //Elimina la informacion de una IT del EP_Detalle, para actualizarla
                        estado_Pago = wsOperacion.EliminarEPCreacionEtiquetaxIT(_ep_obra, _ep_id, Convert.ToInt32(lblGuiaDespacho.Text), cboIT.Text, Program.currentUser.Login, Program.currentUser.ComputerName);
                        if (!estado_Pago.MensajeError.Equals(""))
                            error = estado_Pago.MensajeError.ToString();
                    }

                    if (error.Equals(""))
                    {
                        WsOperacion.Estado_Pago_Detalle etiqueta = null;
                        List<WsOperacion.Estado_Pago_Detalle> listaEtiquetas = new List<WsOperacion.Estado_Pago_Detalle>();

                        foreach (DataGridViewRow rowEtiqueta in dgvEtiquetas.Rows)
                        {
                            if (rowEtiqueta.Cells[COLUMNNAME_MARCA].Value != null)
                            {
                                if (!String.IsNullOrEmpty(rowEtiqueta.Cells[COLUMNNAME_MARCA].Value.ToString()))
                                {
                                    if (Convert.ToBoolean(rowEtiqueta.Cells[COLUMNNAME_MARCA].Value) == true)
                                    {
                                        //Codigo -> it
                                        //NroGuiaInet -> gdespacho
                                        //IdPieza -> etiqueta
                                        etiqueta = new WsOperacion.Estado_Pago_Detalle();
                                        etiqueta.Obra = _ep_obra;
                                        etiqueta.Id = _ep_id; //EP
                                        etiqueta.Guia_despacho = rowEtiqueta.Cells["NroGuiaInet"].Value.ToString();
                                        etiqueta.It = rowEtiqueta.Cells["Codigo"].Value.ToString();
                                        etiqueta.Etiqueta = rowEtiqueta.Cells["IdPaq"].Value.ToString();
                                        etiqueta.Kgs = Convert.ToInt32(rowEtiqueta.Cells["KgsReales"].Value.ToString());
                                        listaEtiquetas.Add(etiqueta);
                                    }
                                }
                            }
                        }
                        if (error.Equals("") && listaEtiquetas.Count > 0)
                        {
                            //Graba las etiquetas asociadas al EP
                            estado_Pago = wsOperacion.RegistrarEPCreacionEtiqueta(listaEtiquetas.ToArray(), Program.currentUser.Login, Program.currentUser.ComputerName);
                            if (!estado_Pago.MensajeError.Equals(""))
                                error = estado_Pago.MensajeError.ToString();

                            //Determina cuales fueron los registros modificados una vez enviado al cliente para registrar en el LOG
                            if (estado_Pago.MensajeError.Equals("") && this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE
                                registrarCambiosEPEnviadaaClte();
                        }
                    }
                    if (error.Equals(""))
                    {
                        MessageBox.Show("Proceso finalizado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.changed = true;
                    }
                    else
                        MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor.Current = Cursors.Default;
            }
            else
                MessageBox.Show("No existen registros seleccionados.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return error.Equals("");

        }

        private void dgvGuiasDespacho_DoubleClick(object sender, EventArgs e)
        {
            //DataGridViewRow currentRow = dgvGuiasDespacho.CurrentRow;
            //if (currentRow != null) 
            //{
            //    lblGuiaDespacho.Text = (currentRow.Cells[1].Value.ToString().Equals("") ? "0" : currentRow.Cells[1].Value.ToString());
            //    if (!lblGuiaDespacho.Text.Equals("0")) { 
            //        cargarInfoEtiquetasxGuiaDespacho_INET(lblGuiaDespacho.Text);
            //        tabCrecionEP.SelectedIndex = 1;
            //    }
            //}
        }

        private void cboIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string it = "", guiaDespacho = "", etiqueta = "";
            DataTable dtEtiquetasEP = null;

            if (!cboIT.Tag.Equals(cboIT.Text))
            {
                it = cboIT.Text;
                dgvEtiquetas.DataSource = null;

                DataTable dtResultINET = dtEtiquetasINET.Clone();
                DataRow[] resultINET = dtEtiquetasINET.Select("Codigo = '" + it + "'");
                if (resultINET.Length > 0) //Verifica si existen etiquetas con la IT especificada
                {
                    guiaDespacho = resultINET[0]["NroGuiaInet"].ToString();
                    //Obtiene la informacion de la guia de despacho, tabla: EP_DETALLE
                    dtEtiquetasEP = cargarInfoEtiquetasxGuiaDespacho_EP(guiaDespacho).DataTable;

                    foreach (DataRow row in resultINET)
                    {
                        etiqueta = row["IdPaq"].ToString();
                        DataRow[] resultEP = dtEtiquetasEP.Select("det_guia_despacho = " + guiaDespacho + " AND det_etiqueta = '" + etiqueta + "'");
                        row[COLUMNNAME_MARCA] = false;
                        if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE -> Se utiliza para detectar las modificaciones solicitadas por el cliente
                            row[COLUMNNAME_MARCA_ORIGINAL] = row[COLUMNNAME_MARCA];

                        if (this._ep_id.Equals(0))
                        {
                            if (resultEP.Length == 0) //Si la etiqueta no existe en la tabla: EP_Detalle -> Mostrar
                                dtResultINET.ImportRow(row);
                        }
                        else
                        {
                            //Si la etiqueta no existe en la tabla: EP_Detalle o existe, pero con el mismo numero de EP -> Mostrar
                            if (resultEP.Length == 0)
                            {
                                dtResultINET.ImportRow(row);
                            }
                            else if (resultEP[0]["det_ep_id"].ToString().Equals(this._ep_id.ToString()))
                            {
                                row[COLUMNNAME_MARCA] = true;
                                if (this._estado.Equals("P15")) //P15-ENVIADO A CLIENTE -> Se utiliza para detectar las modificaciones solicitadas por el cliente
                                    row[COLUMNNAME_MARCA_ORIGINAL] = row[COLUMNNAME_MARCA];
                                dtResultINET.ImportRow(row);
                            }
                        }
                    }
                    dgvEtiquetas.DataSource = dtResultINET;
                    utils.bloquearColumnas(dgvEtiquetas);
                    forms.dataGridViewHideColumns(dgvEtiquetas, new string[] { "Codigo", "NroGuiaInet", "IdDespachoCamion", "patente", "codigo1", COLUMNNAME_MARCA_ORIGINAL });
                    forms.dataGridViewAutoSizeColumnsMode(dgvEtiquetas, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    dgvEtiquetas.Columns["Etiqueta"].DisplayIndex = 1;
                    cboIT.Tag = it;
                }
                lblRegistrosEtiquetas.Text = "Registro(s): " + dgvEtiquetas.Rows.Count;
            }
        }

        private void btnMarcarTodo_Click(object sender, EventArgs e)
        {
            forms.dataGridViewRowsChecked(dgvEtiquetas, COLUMNNAME_MARCA, true);
        }

        private void btnDesmarcarTodo_Click(object sender, EventArgs e)
        {
            forms.dataGridViewRowsChecked(dgvEtiquetas, COLUMNNAME_MARCA, false);
        }

        private void dgvGuiasDespacho_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //btnGuardar.Enabled = true;
            //btnAceptar.Enabled = true;
        }

        private void dgvEtiquetas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnGuardar.Enabled = true;
            btnAceptar.Enabled = true;
        }

        private void dgvGuiasDespacho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////int lKgsSel = 0; Utils lCnn = new EstadosdePagos.Utils();
            ////try {
            ////    if (dgvGuiasDespacho.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
            ////    {
            ////        if (dgvGuiasDespacho.CurrentCell.IsInEditMode)
            ////        {
            ////            if (dgvGuiasDespacho.IsCurrentCellDirty)
            ////            {
            ////                dgvGuiasDespacho.EndEdit();
            ////                string fechaDespINET = dgvGuiasDespacho.Rows[dgvGuiasDespacho.CurrentCell.RowIndex].Cells["FechaDespINET"].Value.ToString();
            ////                if (dgvGuiasDespacho.CurrentCell.Value.Equals(true) && !fechaDespINET.Equals(""))
            ////                {
            ////                    if (Convert.ToDateTime(fechaDespINET) > dtpFechaPresentEP.Value)
            ////                    {
            ////                        if (MessageBox.Show("Esta seleccionando una guia con fecha de despacho INET posterior a la próxima presentación.\n\n¿Desea continuar?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            ////                            dgvGuiasDespacho.CurrentCell.Value = false;
            ////                    }
            ////                    //if (lCnn.EsNumeroDesdeINET(dgvGuiasDespacho.Rows[dgvGuiasDespacho.CurrentCell.RowIndex].Cells["KgsGuia"].Value.ToString()) == true)
            ////                    //{
            ////                    //    lKgsSel = lKgsSel + lCnn.ValDesdeINET(dgvGuiasDespacho.Rows[dgvGuiasDespacho.CurrentCell.RowIndex].Cells["KgsGuia"].Value.ToString());
            ////                    //    tx_KgsSeleccionado.Text = lKgsSel.ToString();
            ////                    //}
            ////                }
            ////            }
            ////        }
            ////    }   
            ////    //TotalKilosSeleccionados();
            ////}
            ////catch (Exception exc)
            ////{
            ////    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
        }

        private string TotalKilosSeleccionados()
        {
            int i = 0;Utils lCnn = new Utils();int lTotal = 0;/* bool lCheck = false;*/
            try
            {
                for (i = 0; i < dgvGuiasDespacho.RowCount; i++)
                {
                    DataGridViewRow row = dgvGuiasDespacho.Rows[i];
                    DataGridViewCheckBoxCell lCheck = row.Cells["MARCA"] as DataGridViewCheckBoxCell;

                    //lCheck = (bool)dgvGuiasDespacho.Rows[i].Cells[0].Value;
                    if  (( !DBNull .Value .Equals(lCheck.Value )) &&    (Convert.ToBoolean(lCheck.Value)))
                    {
                        if (lCnn.EsNumeroDesdeINET(dgvGuiasDespacho.Rows[i].Cells["KgsGuia"].Value.ToString()))
                        {
                            lTotal = lTotal + lCnn.ValDesdeINET(dgvGuiasDespacho.Rows[i].Cells["KgsGuia"].Value.ToString());
                        }
                    }

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lTotal.ToString();

        }



        private void dgvEtiquetas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEtiquetas.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
                if (dgvEtiquetas.CurrentCell.IsInEditMode)
                    if (dgvEtiquetas.IsCurrentCellDirty)
                        dgvEtiquetas.EndEdit();
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            txtComentario.Text = new Utils().eliminarCaracteresEspeciales(txtComentario.Text.Trim());
        }

        private void Btn_ObtenerKgsSel_Click(object sender, EventArgs e)
        {
           tx_KgsSeleccionado .Text = TotalKilosSeleccionados();
        }

        private void Btn_Otros_Click(object sender, EventArgs e)
        {
            frmEP_otros lFrm = new frmEP_otros();
            lFrm.IniciaForm(this .Ep_obra ,this .lblObra.Text , this .Ep_id.ToString (), Program.currentUser, this._estado );
            lFrm.ShowDialog();

            CargaDatosObra(Ep_obra);
        }

        private void Lbl_totalOtros_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Adjunto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea adjuntar algún documento?", "Avisos Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                CargaAdj_Comentario();

        }

        private void CargaAdj_Comentario()
        {
            frmEPAdjunto lFrmAdj = new frmEPAdjunto();
            lFrmAdj.Ep_id = new Utils().Val(this._ep_id.ToString());
            lFrmAdj.Ep_obra = this._ep_obra.ToString();
            lFrmAdj.TecnicoObra = this._tecnicoObra;
            lFrmAdj.Obra = this._obra;
            lFrmAdj.ShowDialog(this);
        }


    }
}