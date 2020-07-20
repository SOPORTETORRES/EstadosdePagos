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
    public partial class Frm_ContratosObra : Form
    {
        private string mIdObra = "";
        public Frm_ContratosObra()
        {
            InitializeComponent();
        }


        public void IniciaForm(string iIdObra, string lNombreObra)
        {
            mIdObra = iIdObra;
            Tx_Obra.Text = lNombreObra;

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmb_Unidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Grabar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos() == true)
            {
                GrabaRegistros();
                MuestraDatos();

            }
        }

        private void MuestraDatos()
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0; int lTotal = 0;
            DataTable lTbTmp = new DataTable(); Utils lUti = new Utils();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0, ", mIdObra, ",' ', 0,0,  '','','',11");

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    Dtg_Datos.DataSource = lTbl;
                    lTotal = 0;
                    for (i = 0; i < lTbl.Rows.Count; i++)
                    {
                        lTotal = lTotal + int.Parse(lTbl.Rows[i]["Par2"].ToString());
                    }
                    //Tx_total.Text = lTotal.ToString("#,##0");

                    Dtg_Datos.Columns[0].Width = 60;
                    Dtg_Datos.Columns[1].Width = 60;
                    Dtg_Datos.Columns[2].Width = 70;
                    Dtg_Datos.Columns[3].Width = 400;
                }


                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0, ", mIdObra, ",' ' ,0,0, '','','',12");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbTmp = lDts.Tables[0].Copy();
                    Cmb_Unidades.DisplayMember = "Par1";
                    Cmb_Unidades.ValueMember = "Par1";
                    Cmb_Unidades.DataSource = lTbTmp.Copy();
                }

                //lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",' ',' ',0,0,'',0,'','','',11");
                //lDts = lPx.ObtenerDatos(lSql);
                //if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                //{
                //    lTbTmp = new DataTable();
                //    lTbTmp = lDts.Tables[0].Copy();
                //    Cmb_Concepto.DisplayMember = "Par1";
                //    Cmb_Concepto.ValueMember = "Par1";
                //    Cmb_Concepto.DataSource = lTbTmp.Copy(); ;
                //}


                //lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",' ',' ',0,0,'',0,'','','',13");
                //lDts = lPx.ObtenerDatos(lSql);
                //if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                //{
                //    lTbTmp = new DataTable();
                //    lTbTmp = lDts.Tables[0].Copy();
                //    Tx_CantTotal.Text = lTbTmp.Rows[0]["Par2"].ToString();
                //}
                //int lSaldo = 0;

                //lSaldo = lUti.Val(Tx_CantTotal.Text) - lUti.Val(Tx_total.Text.Replace(".", ""));
                //Tx_saldo.Text = lSaldo.ToString();

            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat(" Ha ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos Sistema");
            }
        }

        private void GrabaRegistros()
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable(); Utils lUtil = new Utils();
            string lConcepto = "";

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lConcepto = Tx_Servicio.Text;

                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0, ", mIdObra, ",'", lConcepto, "','", lUtil.Val(Tx_CantidadTotal.Text.Replace(".", "")), "',0,'", Cmb_Unidades .SelectedValue , "','");
                lSql = string.Concat(lSql, "' ,'',10 ");

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    if (lTbl.Rows.Count > 0)
                    {
                        MessageBox.Show(" Los Datos se han grabdo  Satisfactoriamente ", "Avisos Sistema", MessageBoxButtons.OK);
                        Tx_CantidadTotal.Text = "";
                        Tx_Servicio.Text = "";
                    }
                }
            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat("Ha ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos Sistema ");
            }
        }

        private Boolean ValidaDatos()
        {
            Boolean lRes = true; string lMsg = ""; Utils lUt = new Utils();

            if (Tx_Servicio.Text.Trim().Length < 2)
            {
                lRes = false;
                lMsg = string.Concat(lMsg, " -   Debe Indicar el Servicio ");
            }

            if (lUt.Val(Tx_CantidadTotal.Text) < 0)
            {
                lRes = false;
                lMsg = string.Concat(lMsg, " -   Debe Indicar  la Cantidad Total ");
            }

            if (lMsg.Trim().Length > 0)
            {
                MessageBox.Show(lMsg, "Mensajes del sistema", MessageBoxButtons.OK);
            }

            return lRes;
        }

        private void Frm_ContratosObra_Load(object sender, EventArgs e)
        {
            MuestraDatos();
        }
    }


     
}
