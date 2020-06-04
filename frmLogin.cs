using System;
using System.Windows.Forms;
using CommonLibrary2;
using System.Data;

namespace EstadosdePagos
{
    public partial class frmLogin : Form
    {
        public bool logon = false;

        public frmLogin()
        {
            InitializeComponent();
            this.Text += " - versión: " + Application.ProductVersion;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                WsSesion.WS_Sesion wsSesion = new WsSesion.WS_Sesion();
                WsSesion.ListaDataSet  listaDataSet = new WsSesion.ListaDataSet();
                listaDataSet = wsSesion.ObtenerUsuario(txtUsuario.Text, txtClave.Text);
                Cursor.Current = Cursors.Default;

                if (listaDataSet.MensajeError.Equals(""))
                {
                    DataTable dt = listaDataSet.DataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Vigente"].ToString().Equals("S"))
                        {
                            Registry registry = new Registry();
                            if (chkRecordarUsuario.Checked)
                                registry.SetValue(Program.regeditKeyName, "Usuario", txtUsuario.Text);
                            registry.SetValue(Program.regeditKeyName, "Recordar", (chkRecordarUsuario.Checked ? "1" : "0"));

                            Program.currentUser.Login = txtUsuario.Text.ToUpper();
                            Program.currentUser.Name = dt.Rows[0]["Nombre"].ToString() + " " + dt.Rows[0]["Apellidos"].ToString();
                            Program.currentUser.PerfilUsuario = (dt.Rows[0]["Perfil"].ToString().Equals("1") ? "ADMIN" : "");
                            logon = true;
                            this.Hide();
                        }
                        else
                            MessageBox.Show("El usuario " + txtUsuario.Text + " esta inactivo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                    else
                        MessageBox.Show("El usuario " + txtUsuario.Text + " no existe o la contraseña es incorrecta.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = (txtUsuario.Text.Trim().Length > 0 && txtClave.Text.Trim().Length > 0);
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            txtUsuario_TextChanged(sender, e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    //e.SuppressKeyPress = true;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            Registry registry = new Registry();
            if (registry.GetValue(Program.regeditKeyName, "Recordar", "0").Equals("1"))
            {
                txtUsuario.Text = (string)registry.GetValue(Program.regeditKeyName, "Usuario", "");
                SendKeys.Send("{ENTER}");
            }
            else
                chkRecordarUsuario.Checked = false;

            btnAceptar.Enabled = !txtUsuario.Text.Trim().Equals("") && !txtClave.Text.Trim().Equals("");
        }

        private string eliminarCaracteresEspeciales(string entrada)
        {
            StringUtility stringUtility = new StringUtility();
            string salida = entrada;
            if (!salida.Trim().Equals(""))
                salida = stringUtility.removeInvalidCharacters(salida, stringUtility.RegexPattern_Address);
            return salida;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.Text = eliminarCaracteresEspeciales(txtUsuario.Text.Trim());
        }

        private void txtClave_Leave(object sender, EventArgs e)
        {
            txtClave.Text = eliminarCaracteresEspeciales(txtClave.Text.Trim());
        }

        private void Btn_imprimePL_Click(object sender, EventArgs e)
        {
            Informes.Frm_Tmp lFrm = new Informes.Frm_Tmp();
            lFrm.ShowDialog(this);





        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            //Btn_imprimePL_Click(null, null);
        }
    }
}