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
        private string mTipoejecucion = "";
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

            lSql = " select id, fechacreacion, v.codigo, nroguiaINET, (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  ) Impreso , fechaGuiaINET";
            lSql = string.Concat(lSql, " from viaje v  where FechaCreacion > getdate()-200 and nroguiaINET>0  ");
            lSql = string.Concat(lSql, " and  (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  )<2 ");
            lSql = string.Concat(lSql, "  order by  fechacreacion desc , (Select count(1) from ViajesIMpresos vi where vi.codigo=v.codigo  ) , v.Codigo ");



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
                if (mTipoejecucion == "REPARA")
                {
                    Lbl_NroAtencion.Text = DTG.Rows[e.RowIndex].Cells["NroAtencion"].Value.ToString();
                    lbl_NombreObra .Text = DTG.Rows[e.RowIndex].Cells["Obra"].Value.ToString();
                }
                else
                    Tx_codigo.Text = DTG.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
            }
        }

        private void Btn_Imprimir_Click(object sender, EventArgs e)
        {
            int i = 0;string lNroImpresos = "0"; Utils lUtil = new Utils();
            if (DTG.Rows.Count > 0)
            {
                mEstaProcesando = true;
                for (i = 0; i <1 ; i++)
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
                AppDomain.CurrentDomain.SetData("Tipo", "Autom");
                this.Close();
            }
            //if (Tx_codigo.Text.Length > 0)
            //{
            //    GeneraPDF_PL(Tx_codigo.Text);
            //}
        }

        private void GeneraPDF_PL(string iCod)
        {
            try
            {
                Utils lInf = new Utils();
                //lInf.CreaInforme_Autom(iCod, true, @"S:\Guías Santiago\IT\");

                lInf.CreaInforme_Autom(iCod, true, @"C:\Cubigest\PDF");

                Tx_codigo.Text = "";
                MessageBox.Show("Generación de PDF Finalizada"); //C:\TMP\IT
                Buscar();
            }
            catch (Exception iex)
            {
            }
    }

        private void Frm_Tmp_Load(object sender, EventArgs e)
        {
            Btn_Buscar_Click(null, null);

            string lTipo = "";  //AppDomain.CurrentDomain.GetData("Tipo").ToString(); //, "Autom");

            if (lTipo.ToString().ToUpper().Equals("Autom".ToUpper()))
            {
                this.Btn_Imprimir_Click(null, null);
            }
        }

        private void ImprimeAutomatico()
        {
            
            Btn_Imprimir_Click(null, null);
        }

        private void Frm_Tmp_Activated(object sender, EventArgs e)
        {
            //ImprimeAutomatico();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             if (mEstaProcesando == false )
            {
                Btn_Buscar_Click(null, null);
                Btn_Imprimir_Click(null, null);
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lSql = ""; DataSet lDts = new DataSet(); WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            DataTable lTbl = new DataTable(); int i = 0; 

            lSql = "select 'SP_EliminaTodosLosRegistros_porViaje ' + convert(varchar,v.id) ";
            lSql = string.Concat(lSql, " from viaje v, it   ");
            lSql = string.Concat(lSql, " where v.idit=it.id  and  ");
            lSql = string.Concat(lSql, "    fechadespacho between '30/12/2090 00:00:01'  and  '30/12/2090 23:59:59' ");



            lDts = lDal.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0];
                for (i = 0; i < lTbl.Rows.Count; i++)
                {
                    lSql = lTbl.Rows[i][0].ToString();
                    lDal.ObtenerDatos(lSql);
                }
            }
        }

        private void ReparaDiferenciaKgs()
        {
            string lSql = "";  DataSet lDts = new DataSet(); WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            DataTable lTbl = new DataTable(); int i = 0; int lDif = 0; DataTable lTbl2 = new DataTable();

            lSql = " select p.TotalKgs TotalKgsOK  , p1.TotalKgs , p.TotalKgs - p1.TotalKgs Dif , p1.idmov , p.id IdPTB, p1.Id IdPieza";
            lSql = string.Concat(lSql, "  from  piezastipoB p , hojadespiece hd , piezas P1   ");
            lSql = string.Concat(lSql, " where p.id_hd=hd.id and  p.id=P1.idpiezatipoB and   year(p.fecha)=2020 ");
            lSql = string.Concat(lSql, "   and   idobra=682 and   p.TotalKgs <> p1.TotalKgs   and p1.Estado <>'00'  "); //and p.id in (1948818,1948816, 1948810, 1948811, 1948812 ) ");
            lSql = string.Concat(lSql, " and p.Fecha > getdate()-400   ");

            lDts = lDal.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0];
                for (i = 0; i < lTbl.Rows.Count; i++)
                {
                    lSql = string.Concat(" update piezas set totalKgs=", lTbl.Rows[i]["TotalKgsOK"].ToString());
                    lSql = string.Concat(lSql, "    where  idpiezatipob=", lTbl.Rows[i]["IdPTB"].ToString() );
                    lDal.ObtenerDatos(lSql);

                    lSql = string.Concat(" Update movimientos set PesoAsignado=", lTbl.Rows[i]["TotalKgsOK"].ToString());
                    lSql = string.Concat(lSql, "    where  Id=", lTbl.Rows[i]["idmov"].ToString());
                    lDal.ObtenerDatos(lSql);


                    lSql = string.Concat(" Select max(id)  from DetallePaquetesPieza where idpieza=", lTbl.Rows[i]["IdPieza"].ToString() );
                    lDts = lDal.ObtenerDatos(lSql);
                    if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                    {
                        lTbl2 = lDts.Tables[0];
                        lDif = int.Parse (lTbl.Rows[i]["Dif"].ToString());
                        lSql = string.Concat(" Update DetallePaquetesPieza set KgsPaquete=KgsPaquete+", lDif, " where id=", lTbl2.Rows[0][0].ToString());
                        lDts = lDal.ObtenerDatos(lSql);
                    }
                }
            }



        }

        private void RevisaObrasVarias_porCliente()
        {
            string lSql = ""; DataSet lDts = new DataSet(); WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            DataTable lTbl = new DataTable(); int i = 0; int lDif = 0; DataTable lTbl2 = new DataTable();

            lSql = " select   AteNum NroFactura, a.AteNumRea NroAtencion,AteGlo , a.AteFchDoc,a.CliCod ,a.DocCod, ";
            lSql = string.Concat(lSql, " convert(varchar(10),AteFchDoc,103) FechaDoc , p.BarCod     ");
        //    lSql = string.Concat(lSql, "    convert(varchar,convert(int,AtePrcBar)) AtePrcBar ,convert(varchar,convert(int,AteQxP)) AteQxP    ");
            lSql = string.Concat(lSql, "  from torresocaranza.dbo.ATECLIEN a, torresocaranza.dbo.ATECLIE2 a3, torresocaranza.dbo.ATECLI2 a2,   ");
            lSql = string.Concat(lSql, "   torresocaranza.dbo.PRODVENT p   where    a2.BarCod in ('SUM.OBRASVARIAS','PREP.OBRASVARIAS')  ");
            lSql = string.Concat(lSql, "   and Year(a.AteFchDoc)>2018  ");
            lSql = string.Concat(lSql, "    and a.AteNumRea =a3.AteNumRea and AteTotNom ='Neto Afecto' and a.DocCod =a2.DocCod and a.AteNumRea =a2.AteNumRea ");
            lSql = string.Concat(lSql, "    and a.DocCod  not in   ( select DocCod from torresocaranza.dbo.TIPDOC  where DOCCODSII =61)    ");
            lSql = string.Concat(lSql, "    and  p.BarCod=a2.BarCod  order by AteNum,AteGlo  ");
 
            lDts = lDal.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0];
                lTbl.Columns.Add("Obra", Type.GetType("System.String"));
                for (i = 0; i < lTbl.Rows.Count; i++)
                {
                    lTbl.Rows[i]["Obra"] = obtenerNombre(lTbl.Rows[i]["CliCod"].ToString (), lTbl.Rows[i]["AteGlo"].ToString());                   
                }
                DTG.DataSource = lTbl;


                for (i = 0; i < DTG.Rows.Count-1; i++)
                {
                    if ((DTG.Rows[i].Cells[8].Value.ToString().Trim().Length  > 1))
                    {
                        if (DTG.Rows[i].Cells["AteGlo"].Value.ToString().IndexOf(DTG.Rows[i].Cells[8].Value.ToString()) > -1)
                        {
                            DTG.Rows[i].Cells["AteGlo"].Style.BackColor = Color.LightGreen;
                        }               
                    }
                    else
                        DTG.Rows[i].Cells["AteGlo"].Style.BackColor = Color.LightSalmon;

                }

            }
        }

        private string obtenerNombre(string irut, string iAteGlosa )
        {
            string lRes = "";
            string lSql = ""; DataSet lDts = new DataSet(); WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            DataTable lTbl = new DataTable();

            string[] separators = { " " };
            string[] lPartes = iAteGlosa.Split(separators, StringSplitOptions.RemoveEmptyEntries);


            lSql = string .Concat (" select nombre from obras where rutcliente like '%",irut , "%'"  );
            lSql = string.Concat(lSql , " ", "and codigo_inet = '63VARIOS' and nombre like '%", lPartes [1].ToString (),"%'");


            lDts = lDal.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0];

                lRes = lTbl.Rows[0][0].ToString();


            }

            return lRes;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mTipoejecucion = "REPARA";
            RevisaObrasVarias_porCliente();
          //  ReparaDiferenciaKgs();

        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            string lSql = ""; DataSet lDts = new DataSet(); WsMensajeria.Ws_To lDal = new WsMensajeria.Ws_To();
            if (Lbl_NroAtencion.Text.Length > 0)
            {
                lSql = string.Concat ("  update torresocaranza.dbo.ATECLIEN set  ateglo='Obra ",lbl_NombreObra .Text );
                lSql = string.Concat(lSql , "'  where    AteNumRea=", Lbl_NroAtencion.Text);
                lDts = lDal.ObtenerDatos(lSql);

                MessageBox.Show("Registros actualizados ");
            }
        }
    }
}
