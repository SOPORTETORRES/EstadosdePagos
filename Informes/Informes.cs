using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;

namespace EstadosdePagos.Informes
{
    public class Informes
    {

        private string OBtenerValorKilo(string iIdObra)
        {
            string lSql = "";WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();DataTable lTblRes = new DataTable();string lValorKilo = "";
            //Dim lSql As String = "", lPx As New Px_Ws.Ws_ToSoapClient, lTblRes As New DataTable, lValorKilo As String = "0"
            DataSet lDts = new DataSet();
            lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";
            lSql = string.Concat ("Exec SP_CRUD_SERVICIOS_OBRA  0," , iIdObra, ",'',0,0,'','',3");
            //'       @Id int,             '   @IdObra int,	            '@Servicio VARCHAR(50),
            //'   @Importe int,	         '@IdUsuario int,              '@NombreUsuario VARCHAR(50),
            //'@OPCION INT
            lDts = lPx.ObtenerDatos(lSql);
            if (lDts.Tables.Count > 0)
            {
                lValorKilo = lDts.Tables[0].Rows[0][0].ToString();
            }

            return lValorKilo;

        } 
        


        private Dts_PL CargaDatosPortada_ViajeSaldo(string iViaje)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0; ArrayList lIdsPiezas = new ArrayList();string lRes = "";
            string lTmp = "";DataSet lDtsTmp = new DataSet();
            //Dim lPx As New Px_Ws.Ws_ToSoapClient, i As Integer = 0, lIdsPiezas As New ArrayList, lRes As String = ""
            //' Creamos instancia del visualizador
            //Dim frmVisualiza As FrmVisualizador, lTmp As String = ""
            Dts_PL dtsPl = new EstadosdePagos.Informes.Dts_PL();string iIdIt = "";string iIdObra = "";
            //Dim dtsPl As New Dts_PL()
            lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";
            string lSql = string.Concat("select IdIt, IdObra  from viaje v, it where v.IdIt =it.id and Codigo ='", iViaje, "' ");
            lDtsTmp = lPx.ObtenerDatos(lSql);
            if ((lDtsTmp.Tables.Count > 0) && (lDtsTmp.Tables[0].Rows.Count > 0))
            {
                iIdIt = lDtsTmp.Tables[0].Rows[0]["IdIT"].ToString();
                iIdObra = lDtsTmp.Tables[0].Rows[0]["IdObra"].ToString();

                dtsPl.EnforceConstraints = false;   
                dtsPl.Merge(lPx.ObtenerDtsPL_ViajeDesp_ConSaldos("", iIdIt, iViaje,  iIdObra));

                                                                                             //'22/10/2012  ---- cargamos los datos del subreport   
                DataSet lDtsTablas = new DataSet(); Dts_PL.KilosPorDiametroRow lFila = null;
                //Dim lDtsTablas As New DataSet, lFila As Dts_PL.KilosPorDiametroRow // ', lFilaIT As Dts_PL.ResumenDespRow
                lDtsTablas = lPx.ObtenerDiametros_SaldosViaje(iViaje);
                if (lDtsTablas.Tables.Count > 0)
                {
                    for (i = 0; i < lDtsTablas.Tables[0].Rows.Count; i++)
                    {
                        lFila = dtsPl.KilosPorDiametro.NewKilosPorDiametroRow();
                        lFila["Diametro"] = lDtsTablas.Tables[0].Rows[i]["Diametro"].ToString();
                        lFila["Kilos"] = String.Concat(lDtsTablas.Tables[0].Rows[i]["Kilos"].ToString(), " Kilos");
                        lFila["KilosDesp"] = String.Concat(lDtsTablas.Tables[0].Rows[i]["KilosDesp"].ToString(), " Kilos");
                        dtsPl.KilosPorDiametro.Rows.Add(lFila);
                    }
                }

                if (dtsPl.ResumenDesp.Rows.Count > 0)
                {
                    dtsPl.ResumenDesp.Rows[0]["ObsIt"] = ObtenerObsITAprobada(iViaje);
                }
            }
            DataTable lTblTmp = new DataTable(); string lStrTmp = ""; Utils lUtil = new EstadosdePagos.Utils();


            lTblTmp = ObtenerDatosViajeDespachado_Saldos(iViaje);
            if (lTblTmp.Rows.Count > 0)
            {
                //lTmp = Replace(FormatNumber(lTblTmp.Rows(0)("KILOS").ToString, 0), ",", ".")
                //dtsPl.ResumenDesp.Rows(0)("PesoTotalDesp") = lTmp // 'lTblTmp.Rows(0)("KILOS").ToString
                dtsPl.ResumenDesp.Rows[0]["PesoTotalDesp"] = lTblTmp.Rows[0]["KILOS"].ToString();
                dtsPl.ResumenDesp.Rows[0]["NroEtiquetasDesp"] = lTblTmp.Rows[0]["NroEtiquetas"].ToString();

                //lTmp = Replace(FormatNumber(Val(OBtenerValorKilo(IdObra)) * Val(lTblTmp.Rows(0)("KILOS").ToString), 0), ",", ".")
                //    //'dtsPl.ResumenDesp.Rows(0)("ValorTotal_ITDesp") = Val(OBtenerValorKilo(IdObra)) * Val(lTblTmp.Rows(0)("KILOS").ToString)
                //    dtsPl.ResumenDesp.Rows(0)("ValorTotal_ITDesp") = String.Concat("$ ", lTmp)
                dtsPl.ResumenDesp.Rows[0]["ValorTotal_ITDesp"] = lUtil.Val(OBtenerValorKilo(iIdObra)) * lUtil.Val(lTblTmp.Rows[0]["KILOS"].ToString());
          }

            return dtsPl;

        }







        private Dts_PL CargaDatosPortada_ViajeOriginal(string iViaje)
        {
            string iIdIt = ""; string iIdObra = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0;
            FrmVisualizador lFrmInf = new FrmVisualizador();
            Dts_PL lDts = new Dts_PL(); DataSet lDtsTmp = new DataSet();
            try
            {
                lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";
               string lSql = string.Concat("select IdIt, IdObra  from viaje v, it where v.IdIt =it.id and Codigo ='", iViaje, "' ");
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

        private Dts_PL CargaDatosDetalleViajeOriginal(string iViaje)
        {
            string iIdIt = ""; string iIdObra = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0;
            FrmVisualizador lFrmInf = new FrmVisualizador();
            Dts_PL lDts = new Dts_PL(); DataSet lDtsTmp = new DataSet();
            lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";

            string lSql = string.Concat("select IdIt, IdObra  from viaje v, it where v.IdIt =it.id and Codigo ='", iViaje, "' ");
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

        private Dts_PL CargaDatosDetalle_ViajeSaldo(string iViaje)
        {
            string iIdIt = ""; string iIdObra = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); int i = 0;
            FrmVisualizador lFrmInf = new FrmVisualizador();
            Dts_PL lDts = new Dts_PL(); DataSet lDtsTmp = new DataSet();
            lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";
            string lSql = string.Concat("select IdIt, IdObra  from viaje v, it where v.IdIt =it.id and Codigo ='", iViaje, "' ");
            lDtsTmp = lPx.ObtenerDatos(lSql);
            if ((lDtsTmp.Tables.Count > 0) && (lDtsTmp.Tables[0].Rows.Count > 0))
            {
                iIdIt = lDtsTmp.Tables[0].Rows[0]["IdIT"].ToString();
                iIdObra = lDtsTmp.Tables[0].Rows[0]["IdObra"].ToString();

                lDts.EnforceConstraints = false;

            }
            lDts.Merge(lPx.ObtenerDtsPL_ConDet_SaldosViaje("", iIdIt, iViaje, "I", iIdObra, "DESP"));

            return lDts;
        }


        public  void ImprimirInforme( string iViaje, Boolean iEliminaArchivo, string iPathDestino)
        {
            string iIdIt = ""; string iIdObra = ""; Dts_PL lDtsPortada = new Dts_PL() ;
            Dts_PL lDtsDetalle = new Dts_PL(); Char Delimitador = '/';Utils lComun = new Utils();
            FrmVisualizador lFrmInf = new FrmVisualizador();
            try
            {
                String[] lPartes = iViaje.Split(Delimitador);
                if (lPartes.Length > 0)
                {
                    if (lComun.Val(lPartes[1]) ==1)
                    {
                        lDtsPortada = CargaDatosPortada_ViajeOriginal(iViaje);
                        lFrmInf.InicializaInforme("P", lDtsPortada, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf(iPathDestino);
                        lDtsDetalle = CargaDatosDetalleViajeOriginal(iViaje);
                        lFrmInf.InicializaInforme("D", lDtsDetalle, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf(iPathDestino);
                        lFrmInf.Close();
                    }
                    else
                    {
                        //CargaDatosDetalle_ViajeSaldo
                       lDtsPortada = CargaDatosPortada_ViajeSaldo(iViaje);
                             //lDtsPortada = CargaDatosDetalle_ViajeSaldo(iViaje);
                        lFrmInf.InicializaInforme("P", lDtsPortada, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf(iPathDestino);
                        lDtsDetalle = CargaDatosDetalle_ViajeSaldo(iViaje);
                        lFrmInf.InicializaInforme("D", lDtsDetalle, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf(iPathDestino);
                        lFrmInf.Close();
                    }
                }

            }
            catch (Exception exc)   
            {
                //MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw exc;
            }

        }

        public void ImprimirInforme_Automatico(string iViaje, Boolean iEliminaArchivo, string iPathDestino)
        {
            string iIdIt = ""; string iIdObra = ""; Dts_PL lDtsPortada = new Dts_PL();
            Dts_PL lDtsDetalle = new Dts_PL(); Char Delimitador = '/'; Utils lComun = new Utils();
            FrmVisualizador lFrmInf = new FrmVisualizador();
            try
            {
                String[] lPartes = iViaje.Split(Delimitador);
                if (lPartes.Length > 0)
                {
                    if (lComun.Val(lPartes[1]) == 1)
                    {
                        lDtsPortada = CargaDatosPortada_ViajeOriginal(iViaje);
                        lFrmInf.InicializaInforme("P", lDtsPortada, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf_Automatico(iPathDestino);
                        lDtsDetalle = CargaDatosDetalleViajeOriginal(iViaje);
                        lFrmInf.InicializaInforme("D", lDtsDetalle, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf_Automatico(iPathDestino);
                        lFrmInf.Close();
                        lFrmInf.Dispose();
                    }
                    else
                    {
                        //CargaDatosDetalle_ViajeSaldo
                        lDtsPortada = CargaDatosPortada_ViajeSaldo(iViaje);
                        //lDtsPortada = CargaDatosDetalle_ViajeSaldo(iViaje);
                        lFrmInf.InicializaInforme("P", lDtsPortada, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf_Automatico(iPathDestino);
                        lDtsDetalle = CargaDatosDetalle_ViajeSaldo(iViaje);
                        lFrmInf.InicializaInforme("D", lDtsDetalle, iViaje, iEliminaArchivo);
                        lFrmInf.GeneraPdf_Automatico(iPathDestino);
                        lFrmInf.Close();
                        lFrmInf.Dispose();
                    }
                }

            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw exc;
            }

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
                lPx.Url = "http://192.168.1.195/WS/Ws_To.asmx";
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
