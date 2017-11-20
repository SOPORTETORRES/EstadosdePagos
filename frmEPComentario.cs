using System;
using System.Windows.Forms;
using System.Drawing;
using CommonLibrary2;
using System.Data;

namespace EstadosdePagos
{
    public partial class frmEPComentario : Form
    {
        public bool changed = false;
        private Forms forms = new Forms();

        #region Getters y Setters

        private int _ep_id = 0;
        private string _ep_obra = "";
        private string _obra = "";
        private string _tecnicoObra = "";

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

        public frmEPComentario()
        {
            InitializeComponent();
            this.dgvComentarios.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvComentarios_RowPostPaint);
        }

        private void dgvComentarios_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvComentarios.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
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
                    }
                    else
                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmEPComentario_Load(object sender, EventArgs e)
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

        private void dgvComentarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvComentarios.SelectedRows.Count > 0)
                txtVerComentario.Text = dgvComentarios.SelectedRows[0].Cells["COMENTARIO"].Value.ToString();
        }

        private void txtAgregarComentario_TextChanged(object sender, EventArgs e)
        {
            btnAgregarComentario.Enabled = txtAgregarComentario.Text.Trim().Length > 0;
        }

        private void txtAgregarComentario_Leave(object sender, EventArgs e)
        {
            txtAgregarComentario.Text = new Utils().eliminarCaracteresEspeciales(txtAgregarComentario.Text.Trim());
        }
    }
}