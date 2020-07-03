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
    public partial class frmEP_otros : Form
    {
        private string mIdObra = "";
        private string mIdEP = "";
        private string mNombreObra = "";

        public frmEP_otros()
        {
            InitializeComponent();
        }

        private void frmEP_otros_Load(object sender, EventArgs e)
        {

        }


        public void IniciaForm(string iIdObra, string iNombreObra, string iIdEP)
        {
            mIdObra = iIdObra;
            mNombreObra = iNombreObra;
            Tx_obra.Text = mNombreObra;

            mIdEP = iIdEP;
            Tx_IdEP.Text = mIdEP;

            MuestraDatos();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
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
            string lSql = ""; DataTable lTbl = new DataTable();int i = 0; int lTotal = 0;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",' ',' ',0,'','','',9");

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    Dtg_Resultado.DataSource = lTbl;
                    lTotal = 0;
                    for (i = 0; i < lTbl.Rows.Count; i++)
                    {
                        lTotal = lTotal+ int.Parse (lTbl .Rows [i]["Importe"].ToString ());
                    }
                }
                Tx_total.Text = lTotal.ToString("#,##0");

                Dtg_Resultado.Columns[0].Width = 60;
                Dtg_Resultado.Columns[1].Width = 60;
                Dtg_Resultado.Columns[2].Width = 70;
                Dtg_Resultado.Columns[3].Width = 400;
                Dtg_Resultado.Columns[4].Width = 70;
                Dtg_Resultado.Columns[5].Width = 70;
                Dtg_Resultado.Columns[6].Width = 100;




            }
            catch (Exception iEx)
            {

            }
        }

        private void EliminaRegistro(string iId )
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS ", iId ,",0, 0,' ',' ',0,'','','',8");

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();

                }
            }
            catch (Exception iEx)
            {

            }
        }

        private void GrabaRegistros()
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();DataSet lDts = new DataSet();  
            string lSql = ""; DataTable lTbl = new DataTable();  
    
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0," , mIdEP, ", ",mIdObra,",'" ,Tx_Concepto .Text ,"','",Tx_Importe .Text ,"',0,'','','',1");

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    if (lTbl.Rows.Count > 0)
                    {
                        MessageBox.Show(" Los Datos se han grabdo  Satisfactoriamente ", "Avisos Sistema", MessageBoxButtons.OK);

                        Tx_Concepto.Text = "";
                        Tx_Importe.Text = "";
                    }

                }
            }
            catch (Exception iEx)
            {

            }
        }


        private void Tx_Concepto_TextChanged(object sender, EventArgs e)
        {

        }
    




    private Boolean ValidaDatos()
        {
            Boolean lRes = true ;string lMsg = "";

            if (this.Tx_Concepto.Text .Trim().Length <3)
            {
                lRes = false;
                lMsg = "  Debe Indicar el Concepto ";
            }

            if (Tx_Importe .Text.Trim().Length < 3)
            {
                lRes = false;
                lMsg = string .Concat (lMsg , " -   Debe Indicar el Importe ");
            }

            if (lMsg.Trim().Length > 0)
            {
                MessageBox.Show(lMsg, "Mensajes del sistema", MessageBoxButtons.OK);
            }

            return lRes;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Utils lUtil = new Utils();

            if (MessageBox.Show("¿Esta Seguro que desea eliminar el Registro? ", "Avisos Sistema", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (lUtil.Val(Tx_Id.Text) > 0)
                {
                    EliminaRegistro(lUtil.Val(Tx_Id.Text).ToString());
                    MuestraDatos();
                }
            }  

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Dtg_Resultado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string lId = Dtg_Resultado.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            Tx_Id.Text = lId;
        }
    }
}
