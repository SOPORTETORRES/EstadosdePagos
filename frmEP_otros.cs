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
        private string mEstado_EP = "";
        private string mNombreObra = "";
        private DataTable mTblConcepto = new DataTable();
        public   CurrentUser mUserActivo = new CurrentUser();
        private DataTable mTblDatosObra = new DataTable();
        private DataTable mTblDescuentosObra = new DataTable();

        public frmEP_otros()
        {
            InitializeComponent();
        }

        private void frmEP_otros_Load(object sender, EventArgs e)
        {
       
        }


        public void IniciaForm(string iIdObra, string iNombreObra, string iIdEP, CurrentUser iUSer, string iEstadoEP)
        {
            mIdObra = iIdObra;
            mNombreObra = iNombreObra;
            Tx_obra.Text = mNombreObra;

            mIdEP = iIdEP;
            Tx_IdEP.Text = mIdEP;
            mUserActivo= iUSer;
            mIdEP = iIdEP;
            mEstado_EP = iEstadoEP;

            //private DataTable mTblDescuentosObra = new DataTable();

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();            string lSql = "";  
            //DataTable lTbTmp = new DataTable(); Utils lUti = new Utils();
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",' ',0,0,'','','','',11,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                mTblConcepto = new DataTable();
                mTblConcepto = lDts.Tables[0].Copy();
            }
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0, 0,' ',0,0,'','','','',18,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                mTblDescuentosObra = new DataTable();
                mTblDescuentosObra = lDts.Tables[0].Copy();
            }
            Rb_Adicional_CheckedChanged(null, null);
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
                if (Rb_Descuento.Checked ==true  )
                if (MessageBox.Show("¿Desea adjuntar algún documento?", "Avisos Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    CargaAdj_Comentario();

                MuestraDatos();

            }
        }
        private void CargaAdj_Comentario()
        {
            frmEPAdjunto lFrmAdj = new frmEPAdjunto();
            lFrmAdj.Ep_id = new Utils().Val(this.mIdEP.ToString());
            lFrmAdj.Ep_obra = this.mIdObra.ToString();
            //lFrmAdj.TecnicoObra = this._tecnicoObra;
            lFrmAdj.Obra = this.mNombreObra;
            lFrmAdj.ShowDialog(this);
        }

        private void MuestraDatos()
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; int i = 0; int lTotal = 0;
            DataTable lTbTmp = new DataTable();  Utils lUti = new Utils();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",' ',0,0, '','','','',9,''");
                Dtg_Resultado.DataSource = null;
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    mTblDatosObra = lDts.Tables[0].Copy();
                    Dtg_Resultado.DataSource = mTblDatosObra;
                    lTotal = 0;
                    for (i = 0; i < mTblDatosObra.Rows.Count; i++)
                    {
                        lTotal = lTotal+ int.Parse (mTblDatosObra.Rows [i]["Importe"].ToString ());
                    }
                    Tx_total.Text = lTotal.ToString("#,##0");

                    Dtg_Resultado.Columns[0].Width = 70;
                    Dtg_Resultado.Columns[1].Width = 80;
                    Dtg_Resultado.Columns[2].Width = 50;
                    Dtg_Resultado.Columns[3].Width =50;
                    Dtg_Resultado.Columns[4].Width = 60;
                    Dtg_Resultado.Columns[5].Width = 300;
                    Dtg_Resultado.Columns[6].Width = 80;
                    Dtg_Resultado.Columns[7].Width = 80;
                    Dtg_Resultado.Columns[8].Width = 100;
                    Dtg_Resultado.Columns[9].Width = 70;
                }
               

                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0, 0,' ',0,0,'','','','',15,''");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbTmp = lDts.Tables[0].Copy();
                    Cmb_Unidades.DisplayMember = "Par1";
                    Cmb_Unidades.ValueMember  = "Par1";
                    Cmb_Unidades.DataSource = lTbTmp.Copy ();
                }

                if (Rb_Adicional .Checked ==true )
                {
                    Cmb_Concepto.DisplayMember = "Servicio";
                    Cmb_Concepto.ValueMember = "Cantidad";
                    Cmb_Concepto.DataSource = mTblConcepto.Copy();
                }

                if (Rb_Descuento .Checked == true)
                {
                    Cmb_Concepto.DisplayMember = "Par1";
                    Cmb_Concepto.ValueMember = "Par1";
                    Cmb_Concepto.DataSource = mTblDescuentosObra.Copy();
                }

                //}


                if (this.mEstado_EP.Equals("P45")) //P15-ENVIADO A CLIENTE
                {
                    Btn_Grabar.Enabled = false;
                    Btn_eliminar.Enabled = false;
                }


            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat(" Ha ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos Sistema");
            }
        }

        private void EliminaRegistro(string iId )
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; DataTable lTbl = new DataTable();string lEstadoEP = "";

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // Debemos  controlar el estado del EP, si esta aprobado por el cliente NO SE PUEDE ELIMINAR NADA.
                lSql = string.Concat("  SP_CRUD_EP_OTROS 0, ", Tx_IdEP.Text , ", 0,' ',' ',0,'','','','',19,''");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                    lEstadoEP = lDts.Tables[0].Rows[0]["EP_ESTADO"].ToString();


                if (lEstadoEP.ToUpper().Equals("P45"))
                {
                    MessageBox.Show("NO se puede elimimar ya que el EP esta aprobado", "Avisos Sistema", MessageBoxButtons.OK);
                }
                else
                {

                    lSql = string.Concat("  SP_CRUD_EP_OTROS ", iId, ",0, 0,' ',' ',0,'','','','',8,''");

                    lDts = lPx.ObtenerDatos(lSql);
                    if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                    {
                        MessageBox.Show("Los Datos se ha eliminado Correctamente ", "Avisos sistema ", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show(" Ha Habido un error en la eliminación de los Datos, repita la Operación ", "Avisos sistema ", MessageBoxButtons.OK, MessageBoxIcon.Question);


                }


            }
            catch (Exception iEx)
            {
                MessageBox.Show(" Ha ocurri ", "Avisos sistema ", MessageBoxButtons.OK);
            }
        }

        private void GrabaRegistros()
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();DataSet lDts = new DataSet();  
            string lSql = ""; DataTable lTbl = new DataTable();  Utils lUtil = new Utils();
            string lConcepto = "";string lTipo = ""; string lCantidad = "0";string lImporte = "0";
    
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lConcepto = "";
                if (Rb_Adicional .Checked == true)  
                    lConcepto = Cmb_Concepto.Text .ToString();
                else
                    lConcepto = Tx_Concepto.Text;

                if (Rb_Adicional.Checked == true)
                {
                    lTipo = "C";
                    lImporte = Tx_Importe.Text.Replace(".", "");
                    lCantidad = ( Int64.Parse(Tx_cantidad.Text)).ToString();
                }
                    

                if (Rb_AdPuntual.Checked == true)
                {
                    lTipo = "AP";
                    lImporte = Tx_Importe.Text.Replace(".", "");
                    lCantidad = (Int64.Parse(Tx_cantidad.Text)).ToString();
                }
                  

                if (Rb_Descuento.Checked == true)
                {
                    lTipo = "D";
                    lImporte = ((-1) * Int64.Parse(Tx_Importe.Text.Replace(".", ""))).ToString(); ;
                    lCantidad = (   Int64.Parse(Tx_cantidad.Text)).ToString ();
                }
                    

                //currentUser
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", mIdEP, ", ", mIdObra, ",'", lConcepto, "','", lImporte, "','");
                lSql = string.Concat(lSql,  "0' ,'", lUtil.Val (Tx_PU.Text)  ,"', '");
                lSql = string.Concat(lSql ,  Cmb_Unidades .SelectedValue , "','", lUtil .Val (lCantidad),"','", lTipo,"', 1 ,'",Tx_Concepto .Text ,"'" );

                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbl = lDts.Tables[0].Copy();
                    if (lTbl.Rows.Count > 0)
                    {
                        lSql = string.Concat("   SP_CRUD_EP_OTROS  0,0,0 ,' ',0,0,'','' ,'','',12 ,'' ");

                        lDts = lPx.ObtenerDatos(lSql);
                        if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                        {

                            MessageBox.Show(" Los Datos se han grabdo  Satisfactoriamente ", "Avisos Sistema", MessageBoxButtons.OK);

                        }                   

                        Tx_Concepto.Text = "";
                        Tx_Importe.Text = "";
                        Tx_PU.Text = "";
                        Tx_cantidad.Text = "";
                    }

                }
            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat("Ha ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos Sistema ");

            }
        }


        private void Tx_Concepto_TextChanged(object sender, EventArgs e)
        {

        }
    




    private Boolean ValidaDatos()
        {
            Boolean lRes = true ;string lMsg = ""; Utils lUt = new Utils();

            if ((Rb_Adicional  .Checked ==false ) &&  (this.Tx_Concepto.Text .Trim().Length <3))
            {
                lRes = false;
                lMsg = "  Debe Indicar el Concepto ";
            }

            if (lUt.Val(Tx_Importe .Text)<1 )
            {
                lRes = false;
                lMsg = string .Concat (lMsg , " -   Debe Indicar el Importe ");
            }

            if (lUt.Val (Tx_PU.Text)<0)
            {
                lRes = false;
                lMsg = string.Concat(lMsg, " -   Debe Indicar el Precio Unitario ");
            }
            if (lUt.Val(Tx_cantidad .Text )<1)
            {
                lRes = false;
                lMsg = string.Concat(lMsg, " -   Debe Indicar  la Cantidad ");
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LimpiaDatos()
        {
            Tx_PU.Text = "0";
            Tx_cantidad.Text = "0";
            Tx_Importe.Text = "0";
            Tx_saldo.Text = "0";

        }

        private void CalculaTotal()
        {
            Utils lUti = new Utils();int lTotal = 0;int i = 0; int lCantidadUsada = 0;

            if ((lUti.Val(Tx_PU.Text) > 0) && (lUti.Val(Tx_cantidad.Text) > 0)) 
            {
                lTotal = lUti.Val(Tx_PU.Text) * lUti.Val(Tx_cantidad.Text);
                lCantidadUsada =  lUti.Val(Tx_cantidad.Text);
                for (i=0;i<this .Dtg_Resultado .RowCount-1;i++)
                {
                    if (Dtg_Resultado .Rows[i].Cells ["Descripcion"].Value.ToString ().Equals (Cmb_Concepto.Text  ))
                    {
                        lCantidadUsada = lCantidadUsada + lUti.Val(Dtg_Resultado.Rows[i].Cells["Cantidad"].Value.ToString());

                    }

                }


                Tx_Importe.Text = lTotal.ToString ();
                if (Rb_Adicional.Checked == true)
                {
                    Tx_saldo.Text = (lUti.Val(this.Tx_CantTotal.Text) - lCantidadUsada).ToString("#,##0");
                    if (lUti.Val(Tx_saldo.Text) < 0)
                    {
                        MessageBox.Show("El Saldo NO puede ser menor que cero, NO se puede realizar la Operación, Favor repita el Proceso. ", "Avisos Sistema", MessageBoxButtons.OK);
                        LimpiaDatos();
                        MuestraDatos();
                    }
                }
                //else
                //MuestraDatos();
            }
            else
            {
                Tx_Importe.Text = "0";
                Tx_saldo.Text = "0";
            }
        }

        private void Tx_PU_Validating(object sender, CancelEventArgs e)
        {
            CalculaTotal();


        }

        private void Tx_cantidad_Validating(object sender, CancelEventArgs e)
        {
            CalculaTotal();
        }

        private void Chk_Contrato_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Adicional.Checked == true)
            {
                Tx_CantTotal.Enabled = false;
                Cmb_Concepto.Visible = true;
                Tx_Concepto.Visible = false;
            }
            else
            {
                //Tx_CantTotal.Enabled = false;
                Cmb_Concepto.Visible = false;
                Tx_Concepto.Visible = true;
                Cmb_Unidades.Enabled = true;
                Tx_saldo.Text = "0";
                Tx_CantTotal.Text = "0";
                Tx_PU.Enabled = true;
            }
                
        }

        private void Cmb_Concepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView lVista = null; string lWheres = "";  Utils lUt = new Utils();int  lImporte = 0; int lSaldo = 0;
            if (Cmb_Concepto.SelectedValue != null)
            {
                if (Rb_Adicional.Checked == true)
                {
                    this.Cmb_Unidades.Enabled = true; Tx_saldo.Text = "0";
                    lWheres = string.Concat("Servicio='", Cmb_Concepto.Text.ToString(), "'");
                    lVista = new DataView(mTblConcepto, lWheres, "", DataViewRowState.CurrentRows);
                    if (lVista.Count > 0)
                    {
                        Tx_CantTotal.Text = Cmb_Concepto.SelectedValue.ToString();
                        this.Cmb_Unidades.SelectedValue = lVista[0]["Unidad"].ToString();
                        this.Cmb_Unidades.Enabled = false;
                        Rb_Adicional.Checked = true;
                        this.Tx_PU.Text = lVista[0]["PU"].ToString();
                        this.Tx_PU.Enabled = false;
                    }
                    //Cargamos los datos del servicio seleccionado
                    if (mTblDatosObra.Rows.Count > 0)
                    {
                        lWheres = string.Concat("Descripcion='", Cmb_Concepto.Text.ToString().Trim(), "'");
                        lVista = new DataView(this.mTblDatosObra, lWheres, "", DataViewRowState.CurrentRows);
                        if (lVista.Count > 0)
                        {
                            lImporte = lUt.Val(lVista[0]["Importe"].ToString());
                            lSaldo = lUt.Val(Tx_CantTotal.Text) - lImporte;
                            Tx_saldo.Text = lSaldo.ToString();
                        }
                    }
                }
                if (Rb_Descuento .Checked ==true )
                {
                    if (Cmb_Concepto.SelectedValue.ToString().Trim ().ToUpper().Equals("OTROS"))
                    {
                        Cmb_Concepto.Visible = true;
                        Tx_Concepto.Visible = true;
                        Cmb_Unidades.Enabled = true;
                        Tx_saldo.Text = "0";
                        Tx_CantTotal.Text = "0";
                        Tx_PU.Enabled = true;
                        Cmb_Concepto.Size = new Size(140, 21);
                        Cmb_Concepto.Top = 90;
                        Cmb_Concepto.Left = 7;

                        Tx_Concepto.Top = 90; Tx_Concepto.Left = 150;
                        Tx_Concepto.Size = new Size(274, 20);

                        Tx_Concepto.Visible = true;
                        Tx_Concepto.Focus();
                    }
                }
            }
        }

        private void Rb_Descuento_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Descuento.Checked == true)
            {
                Cmb_Concepto.Visible = true;
                Tx_Concepto.Visible = true;
                Cmb_Unidades.Enabled = true;
                Tx_saldo.Text = "0";
                Tx_CantTotal.Text = "0";
                Tx_PU.Enabled = true;
                Cmb_Concepto.Size = new Size(140, 21);
                Cmb_Concepto.Top = 90;
                Cmb_Concepto.Left = 7;

                Tx_Concepto.Top = 90; Tx_Concepto.Left = 150;
                Tx_Concepto.Size = new Size(274, 20);

                Tx_Concepto .Visible = true;
                //Tx_Concepto.Left  = 150;Cmb_Concepto.Top = 90;
                // Hay que cargar los datos del desplegable

                Cmb_Concepto.DataSource = mTblDescuentosObra.Copy();
                Cmb_Concepto.DisplayMember = "Par1";
                    Cmb_Concepto.ValueMember = "Par1";
    


            }
        }

        private void Rb_AdPuntual_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_AdPuntual.Checked == true)
            {
                Cmb_Concepto.Visible = false;
                Tx_Concepto.Visible = true;
                Cmb_Unidades.Enabled = true;
                Tx_saldo.Text = "0";
                Tx_CantTotal.Text = "0";
                Tx_PU.Enabled = true;
                Tx_Concepto.Top = 90;Tx_Concepto.Left = 7;
                Tx_Concepto.Size = new Size(411, 21);

            }
        }

        private void Rb_Adicional_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Adicional.Checked == true)
            {
                Tx_CantTotal.Enabled = false;
                Cmb_Concepto.Visible = true;
                Tx_Concepto.Visible = false;
                Cmb_Concepto.Size = new Size (411, 21);
                if (mTblConcepto .Rows .Count >0)
                {
                    Cmb_Concepto.DataSource = mTblConcepto.Copy();
                    Cmb_Concepto.DisplayMember = "Servicio";
                    Cmb_Concepto.ValueMember = "Cantidad";

                }
         
                   
          

            }
        }

        private void Dtg_Resultado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string lId = Dtg_Resultado.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            Tx_Id.Text = lId;
        }
    }
}
