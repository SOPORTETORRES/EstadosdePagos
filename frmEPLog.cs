using System;
using System.Windows.Forms;
using CommonLibrary2;
using System.Drawing;

namespace EstadosdePagos
{
    public partial class frmEPLog : Form
    {
        private Forms forms = new Forms();
        private int _id;

        public int Id
        {
            //get { return _id; }
            set { _id = value; }
        }

        public frmEPLog()
        {
            InitializeComponent();
            this.dgvLog.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvLog_RowPostPaint);
        }

        private void dgvLog_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvLog.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmEPLog_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.Text = "Trazabilidad, Estado de Pago ID: " + _id.ToString();
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPTrazabiidad(_id);
                if (listaDataSet.MensajeError.Equals(""))
                {
                    dgvLog.DataSource = listaDataSet.DataSet.Tables[0];
                    forms.dataGridViewHideColumns(dgvLog, new string[] { "LOG_EP_OBRA", "OBRA", "LOG_EP_ID" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvLog, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
                else
                    MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;    
        }
    }
}