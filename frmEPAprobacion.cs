using System;
using System.Windows.Forms;

namespace EstadosdePagos
{
    public partial class frmEPAprobacion : Form
    {
        public bool ok = false;

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

        public frmEPAprobacion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string comentario = txtComentario.Text.Trim();
            if (!comentario.Equals("")) {
                Cursor.Current = Cursors.WaitCursor;
                try { 
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                    estado_Pago = wsOperacion.RegistrarEPAprobacionCliente(_ep_obra, _ep_id, dtpFechaAprobacion.Value, comentario, Program.currentUser.Login, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals("")) {
                        new Utils().enviarCorreoNotificacionInterna("EP_APROBADO_VB_CLIENTE", _ep_obra, _ep_id, "", "", _obra);
                        this.ok = true;
                        this.Hide();
                    }
                    else
                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            txtComentario.Text = new Utils().eliminarCaracteresEspeciales(txtComentario.Text.Trim());
        }

        private void frmEPAprobacion_Load(object sender, EventArgs e)
        {
            lblID.Text = _ep_id.ToString();
            lblObra.Tag = _ep_obra;
            lblObra.Text = _obra;
            lblTecnicoObra.Text = _tecnicoObra;
        }

        private void txtComentario_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = txtComentario.Text.Trim().Length > 0;
        }
    }
}