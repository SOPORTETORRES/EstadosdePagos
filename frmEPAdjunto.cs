using System;
using System.Windows.Forms;
using System.Drawing;
using CommonLibrary2;
using System.Data;

namespace EstadosdePagos
{
    public partial class frmEPAdjunto : Form
    {
        public bool changed = false;
        private Forms forms = new Forms();

        #region Getters y Setters

        private int _ep_id = 0;
        private string _ep_obra = "";
        private string _obra = "";
        private string _tecnicoObra = "";
        private int _NroGrabaciones = 0;

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

        #endregion

        public frmEPAdjunto()
        {
            InitializeComponent();
            this.dgvAdjuntos.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvDocumentos_RowPostPaint);
        }

        private void dgvDocumentos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvAdjuntos.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Seleccionar archivo";
            //fdlg.InitialDirectory = @"c:\";
            //fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            //fdlg.FilterIndex = 2;
            //fdlg.RestoreDirectory = true;
            fdlg.Filter = "Todos los archivos (*.*)|*.*";

            //Obtiene el directorio donde se almacenan las imagenes de las guias de despacho
            Result result = new Utils().obtenerParametro("EP_DIRECTORIO", "DIR_ADJUNTO");
            if (result.MensajeError.Equals(""))
            {
                DataRow[] rows = result.DataRows;
                if (rows.Length > 0)
                    fdlg.InitialDirectory = rows[0]["Par_Alf1"].ToString();
            }
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = fdlg.FileName;
                btnAgregarAdjunto.Enabled = true;
            }
        }

        private void btnAgregarAdjunto_Click(object sender, EventArgs e)
        {
            if (!txtArchivo.Text.Trim().Equals(""))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                    estado_Pago = wsOperacion.RegistrarEPAdjunto(_ep_obra, _ep_id, txtArchivo.Text, Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
                        //Agregar el adjunto a la grilla...
                        DataTable dt = ((DataTable)dgvAdjuntos.DataSource);
                        DataRow newRow = dt.NewRow();
                        newRow["FECHA"] = DateTime.Now.ToString();
                        newRow["ARCHIVO"] = txtArchivo.Text;
                        dt.Rows.Add(newRow);

                        btnAgregarAdjunto.Enabled = false;
                        changed = true;
                        _NroGrabaciones = _NroGrabaciones+1;
                    }
                    else
                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);



                    if (_NroGrabaciones > 1)
                        btnCancelar.Enabled = true;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtArchivo.Clear();
                btnExaminar.Focus();
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_NroGrabaciones >1)
                this.Close();
            else
            {
                MessageBox.Show(" Debe adjuntar un Archivo y agregar un  Comentario para continuar. ", "Avisos sistema");
            }
        }

        private void frmEPAdjunto_Load(object sender, EventArgs e)
        {
            lblID.Text = _ep_id.ToString();
            lblObra.Tag = _ep_obra;
            lblObra.Text = _obra;
            lblTecnicoObra.Text = _tecnicoObra;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPAdjunto(_ep_id);
                if (listaDataSet.MensajeError.Equals(""))
                {
                    dgvAdjuntos.DataSource = listaDataSet.DataSet.Tables[0];
                    forms.dataGridViewHideColumns(dgvAdjuntos, new string[] { "ADJ_EP_ID", "ID" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvAdjuntos, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
                else
                    MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);


                CargaComentarios();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;  
        }

        private void dgvAdjuntos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvAdjuntos.SelectedRows.Count > 0)
            {
                string archivo = dgvAdjuntos.SelectedRows[0].Cells["ARCHIVO"].Value.ToString();
                FileSystemUtility fs = new FileSystemUtility();
                if (fs.FileExists(archivo))
                    fs.shell(archivo);
                else
                    MessageBox.Show("El archivo '" + archivo + "' no existe.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaComentarios()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPComentario(_ep_id);
                if (listaDataSet.MensajeError.Equals(""))
                {
                    dgvComentarios.DataSource = listaDataSet.DataSet.Tables[0];
                    forms.dataGridViewHideColumns(dgvComentarios, new string[] { "LOG_EP_ID", "ID" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvComentarios, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
                else
                    MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnAgregarComentario_Click(object sender, EventArgs e)
        {
            if (!txtAgregarComentario.Text.Trim().Equals(""))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                    estado_Pago = wsOperacion.RegistrarEPComentario(_ep_obra, _ep_id, txtAgregarComentario.Text, Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
                        //Agregar el comentario a la grilla...

                        DataTable dt = ((DataTable)dgvComentarios.DataSource);
                        DataRow newRow = dt.NewRow();
                        newRow["FECHA"] = DateTime.Now.ToString();
                        newRow["COMENTARIO"] = txtAgregarComentario.Text;
                        dt.Rows.Add(newRow);

                        changed = true;

                        _NroGrabaciones = _NroGrabaciones + 1;
                    }
                    else
                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);



                    if (_NroGrabaciones >1)
                        btnCancelar.Enabled = true;


                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtAgregarComentario.Clear();
                txtAgregarComentario.Focus();
                Cursor.Current = Cursors.Default;
            }
        }

        private void dgvComentarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvComentarios.SelectedRows.Count > 0)
                txtVerComentario.Text = dgvComentarios.SelectedRows[0].Cells["COMENTARIO"].Value.ToString();
        }
    }
}