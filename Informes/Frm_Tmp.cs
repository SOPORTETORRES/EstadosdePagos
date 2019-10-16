using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EstadosdePagos.Informes
{
    public partial class Frm_Tmp : Form
    {
        private Boolean mEstaProcesando = false;
        public Frm_Tmp()
        {
            InitializeComponent();
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            string lSql = "";DataSet lDts = new DataSet() ; WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            DataTable lTbl = new DataTable();

            lSql = " select id, fechacreacion, v.codigo, nroguiaINET, (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  ) Impreso ";
            lSql = string.Concat(lSql, " from viaje v  where FechaCreacion > getdate()-200 and estado='DES'  ");
            lSql = string.Concat(lSql, " and  (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  )<2 ");
            lSql = string.Concat(lSql, "  order by (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  ) , v.Codigo ");



          lDts = lDal.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0];
                DTG.DataSource = lTbl; 
            }
                        
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTG_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             
            if (DTG.Rows.Count > 0)
            {
                Tx_codigo.Text = DTG.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();

            }
        }

        private void Btn_Imprimir_Click(object sender, EventArgs e)
        {
            int i = 0;string lNroImpresos = "0"; Utils lUtil = new Utils();
            if (DTG.Rows.Count > 0)
            {
                mEstaProcesando = true;
                for (i = 0; i <1; i++)
                {
                    Tx_codigo.Text = DTG.Rows[i].Cells["Codigo"].Value.ToString();
                    lNroImpresos = DTG.Rows[i].Cells["Impreso"].Value.ToString();



                    if (lUtil.Val(lNroImpresos) < 2)
                    {
                        GeneraPDF_PL(Tx_codigo.Text);
                        Lbl_msg.Text = string.Concat("Generando Informe: ", i, " de 40");
                        Application.DoEvents();
                    }

                }
                mEstaProcesando = false ;
            }
            //if (Tx_codigo.Text.Length > 0)
            //{
            //    GeneraPDF_PL(Tx_codigo.Text);
            //}
        }

        private void GeneraPDF_PL(string iCod)
        {

            Utils lInf = new Utils();
            lInf.CreaInforme(iCod, true,"");
            Tx_codigo.Text = "";
            //MessageBox.Show("Generación de PDF Finalizada");
            Buscar();
    }

        private void Frm_Tmp_Load(object sender, EventArgs e)
        {
            Btn_Buscar_Click(null, null);
        }

        private void ImprimeAutomatico()
        {
            
            Btn_Imprimir_Click(null, null);
        }

        private void Frm_Tmp_Activated(object sender, EventArgs e)
        {
            ImprimeAutomatico();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             if (mEstaProcesando == false )
            {
                Btn_Buscar_Click(null, null);
                Btn_Imprimir_Click(null, null);
            }
          
        }
    }
}
