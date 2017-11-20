using System;
using System.Windows.Forms;
using CommonLibrary2;

namespace EstadosdePagos
{
    public partial class frmFactIngresoINET : Form
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

        public frmFactIngresoINET()
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
                    estado_Pago = wsOperacion.RegistrarEPIngresadaINET(_ep_obra, _ep_id, Program.currentUser.Login, dtpFechaIngresoINET.Value, Convert.ToInt32(txtNumFactura.Text), dtpFechaEnvioClte.Value, dtpFechaVencimiento.Value, txtComentario.Text, Program.currentUser.ComputerName);
                    if (estado_Pago.MensajeError.Equals(""))
                    {
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
            else {
                MessageBox.Show("Debe ingresar al menos el numero de factura.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmFactIngresoINET_Load(object sender, EventArgs e)
        {
            lblID.Text = _ep_id.ToString();
            lblObra.Tag = _ep_obra;
            lblObra.Text = _obra;
            lblTecnicoObra.Text = _tecnicoObra;
        }

        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            txtComentario.Text = new Utils().eliminarCaracteresEspeciales(txtComentario.Text.Trim());
        }
    }
}