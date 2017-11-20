using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EstadosdePagos.Informes
{
    public partial class FrmVisualizador : Form
    {
        DataSet mDtsInforme = new DataSet ();
        string lInforme = "";
        public FrmVisualizador()
        {
            InitializeComponent();
        }

        private void FrmVisualizador_Load(object sender, EventArgs e)
        {
            if (mDtsInforme != null)
            {
                //CargarInforme(mDtsInforme, lInforme);
                Cursor = Cursors.WaitCursor;
                try
                {
                    Rpt_PortadaDesp mReport = new Rpt_PortadaDesp();
                    mReport.SetDataSource(mDtsInforme);
                    this.crystalReportViewer1.ReportSource = mReport;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        public void InicializaInforme(String lTipoInf, DataSet iDts)
        {
            mDtsInforme = iDts;
            lInforme = lTipoInf;

        }

        public void GeneraPdf(String iPath)
        {
           

        }

    }
}
