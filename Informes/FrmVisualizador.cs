using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EstadosdePagos.Informes
{
    public partial class FrmVisualizador : Form
    {
        DataSet mDtsInforme = new DataSet ();
        string mInforme = "";
        string mViaje = "";
        Boolean mEliminaArchivo = false;

        public FrmVisualizador()
        {
            InitializeComponent();
        }

        private void FrmVisualizador_Load(object sender, EventArgs e)
        {
           

        }

        public void InicializaInforme(String lTipoInf, DataSet iDts, string lVIaje, Boolean iEliminaArchivo)
        {
            mDtsInforme = iDts;
            mInforme = lTipoInf;
            mViaje = lVIaje;
            mEliminaArchivo = iEliminaArchivo;
        }

        private void GrabaGeneracion_PL(string iViaje, string iPath, string iTipo)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
          
                string lSql = string.Concat(" insert into ViajesImpresos (codigo, Tipo, FechaImpresion, Path ) Values ('");
                lSql = string.Concat(lSql, iViaje,"','", iTipo ,"',getdate(),'",iPath ,"') ");
                lDts = lPx.ObtenerDatos(lSql);
                
           
           }
        public void GeneraPdf(string iPathDestino)
        {
            int lRes = 0;
            if (mDtsInforme != null)
            {
                string lPathArchivo = string.Concat(iPathDestino, "\\"); // "C:\\TMP\\Estado de pago\\DOC\\";
                //string lPathArchivo = "//192.168.1.191//Gerencia de Logistica//Guias de Despacho//Guías Santiago//IT//";

                //\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Santiago\IT//string lPathArchivo = "C:\\TMP\\Oficina Tecnica\\EP\\";
                string lArchivo = "";
               // CargarInforme(mDtsInforme, lInforme);
                Cursor = Cursors.WaitCursor;
                try
                {
                       string[] separators = { "-" };
                       string[] lPartes = mViaje.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        if (lPartes.Length > 1)
                        {
                            //lPathArchivo = string.Concat(lPathArchivo, lPartes[0], "\\");
                            if (Directory.Exists(lPathArchivo) == false)
                            {
                                Directory.CreateDirectory(lPathArchivo);
                            }
                    switch (mInforme.ToUpper())
                    {
                        case "P":
                            lArchivo = string.Concat(lPathArchivo, mViaje.Replace("/", "_"), "P.pdf");
                            if (mEliminaArchivo == true)
                            {
                                if (File.Exists(lArchivo) == true)
                                    File.Delete(lArchivo);
                            }
                            Rpt_PortadaDesp mReport = new Rpt_PortadaDesp();
                            mReport.SetDataSource(mDtsInforme);
                            this.crystalReportViewer1.ReportSource = mReport;
                            mReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lArchivo);
                                GrabaGeneracion_PL(mViaje, lPathArchivo, "P");


                            break;

                        case "D":
                                //lArchivo = string.Concat(lArchivo, mViaje.Replace("/", "_"), "D.pdf");
                                lArchivo = string.Concat(lPathArchivo, mViaje.Replace("/", "_"), "D.pdf");
                                if (mEliminaArchivo == true)
                            {
                                if (File.Exists(lArchivo) == true)
                                    File.Delete(lArchivo);
                            }

                            MestroDetalleDesp mReportD = new MestroDetalleDesp();
                               mReportD.SetDataSource(mDtsInforme);
                                this.crystalReportViewer1.ReportSource = mReportD;
                                mReportD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lArchivo);
                                GrabaGeneracion_PL(mViaje, lPathArchivo, "D");
                                break;

                    }
                  }
                    if (lRes == 2)
                    { }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Restart();
                }

            }

        }

    }
}
