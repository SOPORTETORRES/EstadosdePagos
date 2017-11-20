using System;
using System.Windows.Forms;

namespace EstadosdePagos
{
    public partial class frmFactCobrada : Form
    {
        public bool ok = false;

        #region Getters y Setters

        private int _ep_id = 0;
        private string _ep_obra = "";
        private string _obra = "";
        private string _tecnicoObra = "";
        private int _numFactura = 0;

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

        public int NumFactura
        {
            get { return _numFactura; }
            set { _numFactura = value; }
        }

        #endregion

        public frmFactCobrada()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!txtNumFactura.Text.Trim().Equals(""))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                    estado_Pago = wsOperacion.RegistrarEPCobrada(_ep_obra, _ep_id, Program.currentUser.Login, dtpFechaCobro.Value, txtComentario.Text, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
                        new Utils().enviarCorreoNotificacionInterna("EP_PAGADOX_CLIENTE",_ep_obra, _ep_id, "", "", _obra);
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

        private void frmFactCobrada_Load(object sender, EventArgs e)
        {
            lblID.Text = _ep_id.ToString();
            lblObra.Tag = _ep_obra;
            lblObra.Text = _obra;
            lblTecnicoObra.Text = _tecnicoObra;
            txtNumFactura.Text = _numFactura.ToString();
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            txtComentario.Text = new Utils().eliminarCaracteresEspeciales(txtComentario.Text.Trim());
        }
    }
}