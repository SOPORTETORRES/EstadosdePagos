using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EstadosdePagos
{
    public partial class Frm_Incobrable : Form
    {
        private string mIdUser = "0";
        public Frm_Incobrable()
        {
            InitializeComponent();
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Grabar_Click(object sender, EventArgs e)
        {

        }

        private void GrabarDatos()
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0; int lTotal = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat(" Select * from EP_INCOBRABLE where TipoDoc='", Cmb_TipoDoc.SelectedValue .ToString ());
                lSql = string.Concat("'  and NroDoc='",Tx_NroDoc .Text ,"'");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lSql = string.Concat(" Insert into  EP_INCOBRABLE (TipoDoc, NroDoc, FEchaInsert, UsuarioGraba, Motivo) Values (");
                    lSql = string.Concat("'", Cmb_TipoDoc.SelectedValue.ToString(), "','", Tx_NroDoc.Text, "',Getdate(),");
                    lSql = string.Concat("", mIdUser ,",'",  Tx_motivo .Text , "' )   select @@Identity   ");
                    lDts = lPx.ObtenerDatos(lSql);
                    if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                    {
                        if (int.Parse(lDts.Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            MessageBox.Show(" Los datos se han  grabado correctamente ", "Avisos Sistema", MessageBoxButtons.OK);
                        }
                        else
                            MessageBox.Show(" Al parecer hubo un problema en la grabación, favor verifique ", "Avisos Sistema", MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                    }
                }
               

            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat("Ha Ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos sistema", MessageBoxButtons.OK);
            }
        }


        private void CargaDatos ()
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0; int lTotal = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat(" Select * from EP_INCOBRABLE  ");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    Dtg_Datos.DataSource = lDts.Tables[0].Copy();
                }
                else
                    Dtg_Datos.DataSource = null;


            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat("Ha Ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos sistema", MessageBoxButtons.OK);
            }
        }

        private void Frm_Incobrable_Load(object sender, EventArgs e)
        {
            CargaDatos();
        }
    }
}
