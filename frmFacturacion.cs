using System;
using System.Windows.Forms;
using CommonLibrary2;
using System.Drawing;

namespace EstadosdePagos
{
    public partial class frmFacturacion : Form
    {
        Forms forms = new Forms();

        public frmFacturacion()
        {
            InitializeComponent();
            this.dgvResumen.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvResumen_RowPostPaint);
        }

        private void dgvResumen_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvResumen.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void ejecutarAccion(string accion)
        {
            if (dgvResumen.SelectedRows.Count > 0)
            {
                DataGridViewRow currentRow = dgvResumen.SelectedRows[0];
                String ep_obra = currentRow.Cells["EP_OBRA"].Value.ToString();
                String obra = currentRow.Cells["OBRA"].Value.ToString();
                Int32 ep_id = Convert.ToInt32(currentRow.Cells["ID"].Value.ToString());
                String tecnicoObra = currentRow.Cells["USUARIO"].Value.ToString();
                Int32 numFactura = (String.IsNullOrEmpty(currentRow.Cells["NUMERO_FACT_INET"].Value.ToString()) ? 0 : Convert.ToInt32(currentRow.Cells["NUMERO_FACT_INET"].Value.ToString()));

                switch (accion)
                {
                    case "btnIngresarINET":
                        frmFactIngresoINET frm0 = new frmFactIngresoINET();
                        frm0.Ep_id = ep_id;
                        frm0.Ep_obra = ep_obra;
                        frm0.Obra = obra;
                        frm0.TecnicoObra = tecnicoObra;
                        frm0.ShowDialog(this);
                        if (frm0.ok)
                            btnActualizar.PerformClick();
                        frm0.Dispose();
                        break;

                    case "btnDarxCobrada":
                        frmFactCobrada frm1 = new frmFactCobrada();
                        frm1.Ep_id = ep_id;
                        frm1.Ep_obra = ep_obra;
                        frm1.Obra = obra;
                        frm1.TecnicoObra = tecnicoObra;
                        frm1.NumFactura = numFactura;
                        frm1.ShowDialog(this);
                        if (frm1.ok)
                            btnActualizar.PerformClick();
                        frm1.Dispose();
                        break;

                    case "btnVerReporte":
                        Cursor.Current = Cursors.WaitCursor;
                        new Utils().generarEP(ep_obra, obra, ep_id, 1); //1-Vista preliminar
                        Cursor.Current = Cursors.Default;
                        break;

                    case "btnComentSegEP":
                        frmEPComentario frm5 = new frmEPComentario();
                        frm5.Ep_id = ep_id;
                        frm5.Ep_obra = ep_obra;
                        frm5.Obra = obra;
                        frm5.TecnicoObra = tecnicoObra;
                        frm5.ShowDialog(this);
                        frm5.Dispose();
                        break;

                    case "btnAdjuntoEP":
                        frmEPAdjunto frm9 = new frmEPAdjunto();
                        frm9.Ep_id = ep_id;
                        frm9.Ep_obra = ep_obra;
                        frm9.Obra = obra;
                        frm9.TecnicoObra = tecnicoObra;
                        frm9.ShowDialog(this);
                        if (frm9.changed)
                            btnActualizar.PerformClick();
                        frm9.Dispose();
                        break;

                    case "btnTrazabilidadEP":
                        frmEPLog frm8 = new frmEPLog();
                        frm8.Id = Convert.ToInt32(currentRow.Cells["ID"].Value.ToString());
                        frm8.ShowDialog(this);
                        frm8.Dispose();
                        break;
                }
            }
            else
                MessageBox.Show("No existen registros seleccionados.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnIngresarINET_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnIngresarINET");
        }
        
        private void btnDarxCobrada_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnDarxCobrada");
        }

        private void actualizar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPFacturacion(obtenerEstadoCboFiltro(), (cboAñoMes.Visible ? cboAñoMes.Text : "%"));
                if (listaDataSet.MensajeError.Equals(""))
                {
                    dgvResumen.DataSource = listaDataSet.DataSet.Tables[0];
                    forms.dataGridViewHideColumns(dgvResumen, new string[] { "FECHA_PROX_PRESENT", "FECHA_REPORTE", "USUARIO_MOD_ADMIN" }); //, "FECHA_MOD_ADMIN" });
                    new Utils().estiloMillaresDataGridViewColumn(dgvResumen, new string[] { "TOTAL_KGS", "TOTAL_KGS_REPUESTOS", "TOTAL_KGS_FACTURAR", "VALOR_KILO", "TOTAL_$$$" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvResumen, DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    tlsEstado.Text = "Registro(s): " + dgvResumen.Rows.Count;
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvResumen.Rows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                new Excel().exportar(dgvResumen);
                Cursor.Current = Cursors.Default;
            }
            else
                MessageBox.Show("No existen registros a exportar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private string obtenerEstadoCboFiltro()
        {
            //P45-Por ingresar a INET
            //P50-Ingresadas a INET
            //P55-Cobradas

            //2016-12-23
            //P45-Por ingresar a INET
            //P50-Facturadas por cobrar
                //P50.1-Facturadas por cobrar
                //P50.2-Facturadas vencidas
            //P55-Pagadas
            //%-(Todas)
            //return (cboFiltro.SelectedIndex == 0 ? "P45" : (cboFiltro.SelectedIndex == 1 ? "P50" : (cboFiltro.SelectedIndex == 2 ? "P55" : "%")));
            return (cboFiltro.SelectedIndex == 0 ? "P45" : (cboFiltro.SelectedIndex == 1 ? "P50.1" : (cboFiltro.SelectedIndex == 2 ? "P50.2" : (cboFiltro.SelectedIndex == 3 ? "P55" : "%"))));
        }

        private void cargarCboAñoMes()
        {
            for (int i = 0; i < 12; i++)
            {
                cboAñoMes.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy-MM"));
            }
            if (cboAñoMes.Items.Count > 0)
                cboAñoMes.SelectedIndex = 0;
        }

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            lblUsuarioFacturacion.Text = Program.currentUser.Login;
            cboFiltro.SelectedIndex = 0;
            cargarCboAñoMes();
            btnActualizar.PerformClick();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnIngresarINET.Enabled = cboFiltro.SelectedIndex == 0;
            btnDarxCobrada.Enabled = cboFiltro.SelectedIndex == 1 || cboFiltro.SelectedIndex == 2;
            cboAñoMes.Visible = cboFiltro.SelectedIndex > 2;
            dgvResumen.DataSource = null;
        }

        private void btnVerReporte_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnVerReporte");
        }

        private void btnComentSegEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnComentSegEP");
        }

        private void btnAdjuntoEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnAdjuntoEP");
        }

        private void btnTrazabilidadEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnTrazabilidadEP");
        }
    }
}