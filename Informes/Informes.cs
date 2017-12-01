using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EstadosdePagos.Informes
{
    public class Informes
    {

        private Dts_PL CargaDatosPortada(string iViaje)
        {
            string iIdIt = ""; string iIdObra = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0;
            FrmVisualizador lFrmInf = new FrmVisualizador();
            Dts_PL lDts = new Dts_PL(); DataSet lDtsTmp = new DataSet();
            try
            {
               string lSql = string.Concat("SP_ConsultasGenerales 85,'", iViaje, "','','','',''");
                lDtsTmp = lPx.ObtenerDatos(lSql);
                if ((lDtsTmp.Tables.Count > 0) && (lDtsTmp.Tables[0].Rows.Count > 0))
                {
                    iIdIt = lDtsTmp.Tables[0].Rows[0]["IdIT"].ToString();
                    iIdObra = lDtsTmp.Tables[0].Rows[0]["IdObra"].ToString();

                    lDts.EnforceConstraints = false;
                    //lDts.Merge(lPx.ObtenerDtsPL_ViajeDesp_ConSaldos("", iIdIt, iViaje, iIdObra));
                    lDts.Merge(lPx.ObtenerDtsPL_ViajeDesp("", iIdIt, iViaje, iIdObra));
                    //************************************************************************
                    DataSet lDtsTablas = new System.Data.DataSet(); Dts_PL.KilosPorDiametroRow lFila = null;
                    //Dim lDtsTablas As New DataSet, lFila As Dts_PL.KilosPorDiametroRow 
                    lDtsTablas = lPx.ObtenerDiametros_SaldosViaje(iViaje);

                    if (lDtsTablas.Tables.Count > 0)
                    {
                        for (i = 0; i < lDtsTablas.Tables[0].Rows.Count; i++)
                        {
                            lFila = lDts.KilosPorDiametro.NewKilosPorDiametroRow();
                            lFila["Diametro"] = lDtsTablas.Tables[0].Rows[i]["Diametro"].ToString();
                            lFila["Kilos"] = String.Concat(lDtsTablas.Tables[0].Rows[i]["Kilos"].ToString(), " Kilos");
                            lFila["KilosDesp"] = String.Concat(lDtsTablas.Tables[0].Rows[i]["KilosDesp"].ToString(), " Kilos");
                            lDts.KilosPorDiametro.Rows.Add(lFila);
                        }
                        lDts.ResumenDesp.Rows[0]["ObsIt"] = ObtenerObsITAprobada(iViaje);
                        DataTable lTblTmp = new DataTable(); String lStrTmp = ""; string lTmp = "";
                        lTblTmp = ObtenerDatosViajeDespachado_Saldos(iViaje);
                        if (lTblTmp.Rows.Count > 0)
                        {
                            lTmp = lTblTmp.Rows[0]["KILOS"].ToString();// 0);// ",", "."
                            lDts.ResumenDesp.Rows[0]["PesoTotalDesp"] = lTmp;
                            lDts.ResumenDesp.Rows[0]["NroEtiquetasDesp"] = lTblTmp.Rows[0]["NroEtiquetas"].ToString();
                            //  lTmp = Replace(FormatNumber(Val(OBtenerValorKilo(IdObra)) * Val(lTblTmp.Rows(0)("KILOS").ToString), 0), ",", ".")
                            lDts.ResumenDesp.Rows[0]["ValorTotal_ITDesp"] = String.Concat("$ ", lTmp);
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lDts;
        }

        private Dts_PL CargaDatosDetalle(string iViaje)
        {
            string iIdIt = ""; string iIdObra = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0;
            FrmVisualizador lFrmInf = new FrmVisualizador();
            Dts_PL lDts = new Dts_PL(); DataSet lDtsTmp = new DataSet();

            string lSql = string.Concat("SP_ConsultasGenerales 85,'", iViaje, "','','','',''");
            lDtsTmp = lPx.ObtenerDatos(lSql);
            if ((lDtsTmp.Tables.Count > 0) && (lDtsTmp.Tables[0].Rows.Count > 0))
            {
                iIdIt = lDtsTmp.Tables[0].Rows[0]["IdIT"].ToString();
                iIdObra = lDtsTmp.Tables[0].Rows[0]["IdObra"].ToString();

                lDts.EnforceConstraints = false;

            }

            lDts.Merge(lPx.ObtenerDtsPL_ConDet("", iIdIt, iViaje, "I", iIdObra, "DESP"));

            return lDts;
        }


        public  void ImprimirInforme( string iViaje, Boolean iEliminaArchivo)
        {
            string iIdIt = ""; string iIdObra = ""; Dts_PL lDtsPortada = new Dts_PL() ;
            Dts_PL lDtsDetalle = new Dts_PL();
            FrmVisualizador lFrmInf = new FrmVisualizador();
            try
            { 
                lDtsPortada = CargaDatosPortada(iViaje);
                lFrmInf.InicializaInforme("P", lDtsPortada, iViaje, iEliminaArchivo);
                lFrmInf.GeneraPdf();
                lDtsDetalle = CargaDatosDetalle(iViaje);
                lFrmInf.InicializaInforme("D", lDtsDetalle, iViaje, iEliminaArchivo);
                lFrmInf.GeneraPdf();
                lFrmInf.Close ();
                //****************************************



            }
            catch (Exception exc)   
            {
                //MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //**************************************************************************


        }

        private DataTable ObtenerDatosViajeDespachado_Saldos(String iViaje)
        {
            String lSql   = ""; WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();
            DataSet lDts = new DataSet(); DataTable lRes = new DataTable();
            //'ALTER  PROCEDURE [dbo].[SP_ConsultasGenerales]
            //'@Opcion INT,        '@Par1 Varchar(100),        '@Par2 Varchar(100),
            //'@Par3 Varchar(100),        '@Par4 Varchar(100),  '@Par5 Varchar(100)
            try
            {
                lSql = string.Concat("exec SP_Consultas_WS 24,'", iViaje, "','','','','','',''");
                lDts = lPx.ObtenerDatos(lSql);
                if (lDts.Tables.Count > 0)
                    lRes = lDts.Tables[0].Copy();

            }
            catch (Exception exc)
            { }

            return lRes;
    }

        private string ObtenerObsITAprobada(string iViaje)
        {
            string lSql = ""; WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();
            DataSet lDts = new DataSet(); string lRes = "";


            try
            {
                lSql = string.Concat("exec SP_Consultas_WS 3,'", iViaje, "','','','','','',''");
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lRes = lDts.Tables[0].Rows[0][0].ToString();
                }
            }

            catch (Exception exc)
            {
                lRes = "Error al Obtener la Obs de la IT";
            }
            return lRes;
        }
    }
}
