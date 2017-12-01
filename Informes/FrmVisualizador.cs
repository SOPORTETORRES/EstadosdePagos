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

        public void GeneraPdf( )
        {
            if (mDtsInforme != null)
            {
                string lArchivo = "P:\\Escaneados\\IT\\";
                //CargarInforme(mDtsInforme, lInforme);
                Cursor = Cursors.WaitCursor;
                try
                {
                    switch (mInforme.ToUpper())
                    {
                        case "P":
                            lArchivo = string.Concat(lArchivo, mViaje.Replace("/", "_"), "P.pdf");
                            if (mEliminaArchivo == true)
                            {
                                if (File.Exists(lArchivo) == true)
                                    File.Delete(lArchivo);

                                Rpt_PortadaDesp mReport = new Rpt_PortadaDesp();
                                mReport.SetDataSource(mDtsInforme);
                                this.crystalReportViewer1.ReportSource = mReport;
                                mReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lArchivo);
                            }
                 
                            break;
                        case "D":
                            lArchivo = string.Concat(lArchivo, mViaje.Replace("/", "_"), "D.pdf");
                            if (mEliminaArchivo == true)
                            {
                                if (File.Exists(lArchivo) == true)
                                    File.Delete(lArchivo);


                                MestroDetalleDesp mReport = new MestroDetalleDesp();
                               mReport.SetDataSource(mDtsInforme);
                                this.crystalReportViewer1.ReportSource = mReport;
                                mReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lArchivo);
                            }
                            break;

                    }
                  }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

    }
}
