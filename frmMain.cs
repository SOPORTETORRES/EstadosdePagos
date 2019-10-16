using System;
using System.Windows.Forms;
using CommonLibrary2;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.IO;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace EstadosdePagos
{
    public partial class frmMain : Form
    {
        //[DllImport("user32.dll")]
        //static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        private Forms forms = new Forms();
        private DataTable dtObra = null;
        private DataTable dtTecnicoObra = null;
        private bool load = false;
        private FileSystemUtility fs = new FileSystemUtility();
        private StringUtility su = new StringUtility();
        private const string COLUMNNAME_ID = "ID";
        private const string COLUMNNAME_GUIA_DESPACHO = "GUIA_DESPACHO";
        private const string COLUMNNAME_IT = "IT";
        private const string COLUMNNAME_N_ETIQUETAS = "N_ETIQUETAS";
        private const string COLUMNNAME_TOTAL_KGS = "TOTAL_KGS";
        private Utils utils = new Utils();

        public frmMain()
        {
            InitializeComponent();
            this.dgvResumen.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvResumen_RowPostPaint);
            this.dgvDetalle.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvDetalle_RowPostPaint);
        }

        private void dgvResumen_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvResumen.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvDetalle_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDetalle.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        //private void copiarArchivosLista(string sourcePath, string destinationPath, List<string> listaArchivos) 
        //{
        //    sourcePath += (!su.Right(sourcePath, 1).Equals("\\") ? "\\" : "");
        //    destinationPath += (!su.Right(destinationPath, 1).Equals("\\") ? "\\" : "");
        //    if (fs.DirectoryExists(sourcePath) && fs.DirectoryExists(destinationPath)) 
        //    {
        //        foreach (string archivo in listaArchivos) 
        //        {
        //            if (fs.FileExists(sourcePath + archivo))
        //                fs.copyFile(sourcePath + archivo, destinationPath + archivo);
        //        }
        //    }
        //}

        //private bool generarArchivoEP(string destinationPath, string text) 
        //{
        //    destinationPath += (!su.Right(destinationPath, 1).Equals("\\") ? "\\" : "");
        //    string filename = destinationPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        //    try
        //    {
        //        object paramMissing = Type.Missing;
        //        ExcelApp.Application excelApplication = new ExcelApp.Application();
        //        //excelApplication.Visible = true;
        //        ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Add(paramMissing);
        //        ExcelApp.Worksheet excelSheet = null;
        //        //int processId;
        //        // Find the Process Id
        //        //GetWindowThreadProcessId(excelApplication.Hwnd, out processId);

        //        if (excelWorkBook != null)
        //        {
        //            excelSheet = (ExcelApp.Worksheet) excelWorkBook.Worksheets[1];
        //            excelSheet.Cells[1, 1] = text;
        //            excelSheet.Cells.Select();
        //            excelSheet.Cells.EntireColumn.AutoFit();
        //            excelSheet.Cells.EntireRow.AutoFit();
        //            excelSheet.Range["A1"].Select();

        //            excelWorkBook.SaveAs(filename);
        //        }
        //        excelWorkBook.Close(false);
        //        excelApplication.Quit();

        //        if (excelSheet != null)
        //        {
        //            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet) != 0) { }
        //            excelSheet = null;
        //        }
        //        if (excelWorkBook != null)
        //        {
        //            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkBook) != 0) { }
        //            excelWorkBook = null;
        //        }
        //        if (excelApplication != null)
        //        {
        //            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication) != 0) { }
        //            excelApplication = null;
        //        }
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();

        //        //Process excelProcess = Process.GetProcessById(processId);
        //        //if (excelProcess != null)
        //        //    excelProcess.Kill();

        //        //Copia la plantilla al directorio de destino
        //        string plantillaEP = Application.StartupPath + (!su.Right(Application.StartupPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
        //        if (fs.FileExists(plantillaEP))
        //            fs.copyFile(plantillaEP, destinationPath + "PlantillaEP.xls");
        //        return true;
        //    }
        //    catch (Exception exc)
        //    {
        //        MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        //private string seleccionarCarpeta()
        //{
        //    string selectedPath = "";
        //    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        //    folderBrowserDialog.Description = "Seleccione la carpeta donde guardará el reporte:";
        //    folderBrowserDialog.ShowNewFolderButton = true;
        //    //folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
        //    //folderBrowserDialog.SelectedPath =  Path.GetTempPath();
        //    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        //        selectedPath = folderBrowserDialog.SelectedPath;
        //    return selectedPath;
        //}

        //private string generarEP(string ep_obra, Int32 ep_id, int accion) //1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente
        //{
        //    string error = "", valorKiloSuministro = "", destinatarios = "", dir_guiaDespacho = "", dir_it = "", archivo = "";
        //    StringBuilder sb = new StringBuilder();
        //    StringBuilder validacion = new StringBuilder();
        //    StringBuilder archivoFaltantesGD = new StringBuilder();
        //    StringBuilder archivoFaltantesIT = new StringBuilder();
        //    List<string> listArchivosGuiasDespacho = new List<string>();
        //    List<string> listArchivosIT = new List<string>();
        //    string selectedPath = "";
        //    WsTo.Operacion ws = new WsTo.Operacion();
        //    WsTo.ListaDataSet listaDataSetTo = new WsTo.ListaDataSet();
        //    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
        //    WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
        //    string validarFiles_GuiaDespacho = "0";
        //    Result result = null;
        //    DataRow[] rows = null;

        //    //Verifica si existe la informacion necesaria para generar el informe
        //    try
        //    {
        //        //Encabezado
        //        DataGridViewRow currentRow = dgvResumen.CurrentRow;
        //        if (currentRow != null) {
        //            sb.Append("Obra: " + currentRow.Cells["Obra"].Value.ToString() + "\n");
        //            sb.Append("EP: " + ep_id.ToString() + "\n\n");
        //        }

        //        //Actualiza la informacion del detalle EP: dgvDetalle
        //        DataTable dtResumenxGuiaDespacho = actualizarInfoDetalle(ep_id); //GD + IT

        //        //Valor kilo suministro
        //        valorKiloSuministro = ws.ValorKiloSuministro_EP(ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
        //        sb.Append("Valor kilo suministro de la obra $: " + (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro) + "\n\n");

        //        //Obtiene los destinatarios de la obra
        //        result = utils.obtenerDestinatariosObra(ep_obra);
        //        if (result.MensajeError.Equals("")) {
        //            destinatarios = result.StringValue;
        //            sb.Append("Destinatarios del envio del EP (Cliente): " + destinatarios + "\n\n");
        //        }
        //        else
        //            error = result.MensajeError;

        //        ///---GUIAS DE DESPACHO---///
        //        //Obtiene el directorio donde se almacenan las imagenes de las guias de despacho
        //        result = utils.obtenerParametro("EP_DIRECTORIO", "DIR_GUIADESPACHO");
        //        if (result.MensajeError.Equals(""))
        //        {
        //            rows = result.DataRows;
        //            if (rows.Length > 0)
        //                dir_guiaDespacho = rows[0]["Par_Alf1"].ToString();
        //        }
        //        else
        //            error = result.MensajeError;
        //        sb.Append("Directorio con las imagenes de guias de despacho: " + (dir_guiaDespacho.Trim().Equals("") ? "(No definido)" : dir_guiaDespacho) + "\n\n");

        //        //Obtiene el parametro que indica si las imagenes digitalizadas de las guias de despacho son requeridas o no (0-No, 1-Si)
        //        result = utils.obtenerParametro("EP_REQUIRED", "FILES_GUIADESPACHO");
        //        if (result.MensajeError.Equals(""))
        //        {
        //            rows = result.DataRows;
        //            if (rows.Length > 0)
        //                validarFiles_GuiaDespacho = rows[0]["par_num1"].ToString();
        //        }
        //        else
        //            error = result.MensajeError;

        //        //Verifica si el reporte cuenta con las imagenes de las guias de despacho
        //        if (!dir_guiaDespacho.Equals("")) { 
        //            dir_guiaDespacho += (!su.Right(dir_guiaDespacho,1).Equals("\\") ? "\\" : "");
        //            DataView view = new DataView(dtResumenxGuiaDespacho);
        //            foreach (DataRow row in view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows)
        //            {
        //                archivo = row[COLUMNNAME_GUIA_DESPACHO].ToString() + ".JPG";
        //                if (fs.FileExists(dir_guiaDespacho + archivo))
        //                    listArchivosGuiasDespacho.Add(archivo);
        //                else
        //                    archivoFaltantesGD.Append(" -> " + archivo + "\n");
        //            }
        //            if (archivoFaltantesGD.ToString().Length > 0)
        //                sb.Append("Faltan los siguientes archivos gds :\n");
        //            sb.Append(archivoFaltantesGD);
        //        }
        //        ///---IT---///666
        //        //Obtiene el directorio donde se almacenan los PDF de las its
        //        result = utils.obtenerParametro("EP_DIRECTORIO", "DIR_IT");
        //        if (result.MensajeError.Equals(""))
        //        {
        //            rows = result.DataRows;
        //            if (rows.Length > 0)
        //                dir_it = rows[0]["Par_Alf1"].ToString();
        //        }
        //        else
        //            error = result.MensajeError;
        //        sb.Append("Directorio con las its: " + (dir_it.Trim().Equals("") ? "(No definido)" : dir_it) + "\n\n");

        //        //Verifica si el reporte cuenta con las imagenes de las guias de despacho
        //        if (!dir_it.Equals(""))
        //        {
        //            dir_it += (!su.Right(dir_it, 1).Equals("\\") ? "\\" : "");
        //            DataView view = new DataView(dtResumenxGuiaDespacho);
        //            foreach (DataRow row in view.ToTable(true, COLUMNNAME_IT).Rows)
        //            {
        //                archivo = row[COLUMNNAME_IT].ToString() + ".PDF";
        //                //Parche para los nombres de las ITS 
        //                archivo = archivo.Replace("/", "_"); //ECT-1/1.PDF -> ECT-1_1.PDF
        //                if (fs.FileExists(dir_it + archivo))
        //                    listArchivosIT.Add(archivo);
        //                else
        //                    archivoFaltantesIT.Append(" -> " + archivo + "\n");
        //            }
        //            if (archivoFaltantesIT.ToString().Length > 0)
        //                sb.Append("Faltan los siguientes archivos its :\n");
        //            sb.Append(archivoFaltantesIT);
        //        }
        //        ///--------///
        //        //Resumen
        //        sb.Append("\nTotal guia(s) despacho(s): " + lblTotalGuiasDespacho.Text + "\n");
        //        sb.Append("Total etiqueta(s): " + lblTotalEtiquetas.Text + "\n");
        //        sb.Append("Total kilo(s): " + lblTotalKilos.Text + "\n\n");
        //        sb.Append("Total $$$ a cobrar: " + (Convert.ToInt32(lblTotalKilos.Text.Replace(".", "")) * (valorKiloSuministro.Equals("") ? 0 : Convert.ToInt32(valorKiloSuministro))).ToString("N0"));

        //        //MessageBox.Show(sb.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception exc)
        //    {
        //        error = exc.Message;
        //    }

        //    if (error.Equals(""))
        //    {
        //        if (valorKiloSuministro.Equals("") || valorKiloSuministro.Equals("0"))
        //            validacion.Append(" - Valor kilo suministro de la obra\n");
        //        if (destinatarios.Equals(""))
        //            validacion.Append(" - Destinatarios de correo del reporte (cliente)\n");
        //        if (dir_guiaDespacho.Equals(""))
        //            validacion.Append(" - Directorio de las guias de despacho digitalizadas\n");

        //        //ESPECIFICA SI LAS GDESPACHO SON REQUERIDAS AL MOMENTO DE GENERAR LA EP
        //        if (validarFiles_GuiaDespacho.Equals("1")) {
        //            //2-Generar el reporte
        //            //3-Envio al cliente
        //            if (accion.Equals(2) || accion.Equals(3))
        //            {
        //                 if (!dir_guiaDespacho.Equals("") && (listArchivosGuiasDespacho.Count < Convert.ToInt32(lblTotalGuiasDespacho.Text)))
        //                     if (!archivoFaltantesGD.ToString().Equals(""))
        //                         validacion.Append(" - Archivos digitalizados de guias de despacho:\n\n" + archivoFaltantesGD.ToString());
        //            }
        //        }

        //        if (validacion.Length == 0)
        //        {
        //            //1-Vista preliminar -> Genera una carpeta temporal para "mostrar" el reporte y las imagenes de las guias de despacho
        //            //3-Envio al cliente -> Genera una carpeta temporal para "enviar" el reporte y las imagenes de las guias de despacho por correo
        //            if (accion.Equals(1) || accion.Equals(3))
        //            {
        //                selectedPath = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //                if (!fs.DirectoryExists(selectedPath))
        //                {
        //                    fs.createDirectory(selectedPath);
        //                    if (fs.error != null)
        //                    {
        //                        validacion.Append(fs.error.Message);
        //                        selectedPath = "";
        //                    }
        //                }
        //            }
        //            else
        //                selectedPath = seleccionarCarpeta();

        //            if (!selectedPath.Equals(""))
        //            {
        //                if (generarArchivoEP(selectedPath, sb.ToString()))
        //                {
        //                    //1-Vista preliminar
        //                    //2-Generar el reporte
        //                    if (accion.Equals(1) || accion.Equals(2))
        //                    { 
        //                        if (listArchivosGuiasDespacho.Count > 0)
        //                            copiarArchivosLista(dir_guiaDespacho, selectedPath, listArchivosGuiasDespacho);
        //                        if (listArchivosIT.Count > 0)
        //                            copiarArchivosLista(dir_it, selectedPath, listArchivosIT);
        //                        fs.shell(selectedPath); //Abre la carpeta de destino
        //                    }
        //                    else if (accion.Equals(3)) { //3-Envio al cliente
        //                        //enviarCorreo(destinatarios, selectedPath);
        //                        //utils.enviarCorreo(destinatarios, "Envio de EP al cliente" + ep_id.ToString(), "Cuerpo del documento", null);
        //                        utils.enviarCorreoNotificacionInterna("EP_ENVIADOA_CLIENTE", "", "");
        //                    }
        //                }
        //            } 
        //            else
        //                validacion.Insert(0, "El usuario seleciono el botón Cancelar");
        //        }
        //        else 
        //            validacion.Insert(0, "Faltan los siguientes datos requeridos:\n\n");
        //    }
        //    else
        //        validacion.Append(error);

        //    if (validacion.Length != 0)
        //        MessageBox.Show(validacion.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    return validacion.ToString();
        //}

        private void ejecutarAccion(string accion)
        {
            if (dgvResumen.SelectedRows.Count > 0)
            {
                DataGridViewRow currentRow = dgvResumen.SelectedRows[0];
                String ep_obra = currentRow.Cells["EP_OBRA"].Value.ToString();
                String obra = currentRow.Cells["OBRA"].Value.ToString();
                Int32 ep_id = Convert.ToInt32(currentRow.Cells[COLUMNNAME_ID].Value.ToString());
                String tecnicoObra = currentRow.Cells["USUARIO"].Value.ToString();
                Int32 correlativo = (String.IsNullOrEmpty(currentRow.Cells["CORRELATIVO"].Value.ToString()) ? 0 : Convert.ToInt32(currentRow.Cells["CORRELATIVO"].Value.ToString()));
                String carpeta = currentRow.Cells["CARPETA"].Value.ToString();

               
                //Validaciones iniciales
                if (!accion.Equals("btnCrearEP") && ep_id.Equals(0))
                {
                    MessageBox.Show("Opcion no disponible porque NO existe ningun EP generado para esta obra", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (accion.Equals("btnCrearEP") && !ep_id.Equals(0)) //Valida si existen EP sin fecha de aprob.clte en una obra, no se puede generar uno nuevo
                {
                    try
                    {
                        WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                        WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                        listaDataSet = wsOperacion.ListarObraEPNoAprobado(ep_obra);
                        if (listaDataSet.MensajeError.Equals("")) {
                            if (listaDataSet.DataSet.Tables[0].Rows.Count > 0)
                            {
                                MessageBox.Show("No se puede crear un nuevo EP para esta obra, porque aun existen EP pendientes por aprobar (" + listaDataSet.DataSet.Tables[0].Rows.Count + ")", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                            MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
 
                //Evalua la accion a realizar
                switch (accion)
                {
                    case "btnCrearEP":
                        frmEPNueva frm0 = new frmEPNueva();
                        frm0.Empresa = cboEmpresa.Text;
                        frm0.Ep_id = ep_id;
                        frm0.Ep_obra = ep_obra;
                        frm0.Obra = obra;
                        frm0.TecnicoObra = tecnicoObra;
                        frm0.CargaFormulario(ref Pb, ref  Lbl_PB);
                        frm0.ShowDialog(this);
                        if (frm0.changed)
                            btnActualizar.PerformClick();
                        frm0.Dispose();
                        break;

                    case "btnVerReporteEP":
                        Cursor.Current = Cursors.WaitCursor;
                        //generarEP_V2
                        utils.generarEP_V2(ep_obra, obra, ep_id, 1, ref Pb, ref Lbl_PB, carpeta, correlativo);
                      //  utils.generarEP(ep_obra, obra, ep_id, 1, ref Pb , ref Lbl_PB, carpeta, correlativo); //1-Vista preliminar
                        Cursor.Current = Cursors.Default;  
                        break;

                    case "btnGenerarReporteEP":
                        
                        if (MessageBox.Show("¿Esta seguro que desea generar el reporte?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                if (utils.generarEP(ep_obra, obra, ep_id, 2, ref Pb  , ref Lbl_PB , carpeta, correlativo).Equals("")) //2-Generar el reporte
                                {
                                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                                    estado_Pago = wsOperacion.RegistrarEPGeneracionReporte(ep_obra, ep_id, Program.currentUser.Login, Program.currentUser.ComputerName);
                                    if (estado_Pago.MensajeError.Equals(""))
                                        btnActualizar.PerformClick();
                                    else
                                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            Cursor.Current = Cursors.Default;  
                        }
                        break;

                    case "btnEnviarEP":
                        if (MessageBox.Show("¿Esta seguro que desea enviar este EP al cliente?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                if (utils.generarEP(ep_obra, obra, ep_id, 3,ref Pb , ref Lbl_PB,carpeta ,correlativo ).Equals("")) //3-Envio al cliente
                                {
                                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                                    WsOperacion.Estado_Pago estado_Pago = new WsOperacion.Estado_Pago();
                                    estado_Pago = wsOperacion.RegistrarEPEnvioaCliente(ep_obra, ep_id, Program.currentUser.Login, Program.currentUser.ComputerName);
                                    if (estado_Pago.MensajeError.Equals(""))
                                        btnActualizar.PerformClick();
                                    else
                                        MessageBox.Show(estado_Pago.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            Cursor.Current = Cursors.Default;  
                        }
                        break;

                    case "btnModEPxObsClte":
                        if (dgvResumen.CurrentRow != null)
                            dgvResumen_DoubleClick(null, null);
                        break;

                    case "btnComentSegEP":
                        frmEPComentario frm5 = new frmEPComentario();
                        frm5.Ep_id = ep_id;
                        frm5.Ep_obra = ep_obra;
                        frm5.Obra = obra;
                        frm5.TecnicoObra = tecnicoObra;
                        frm5.ShowDialog(this);
                        if (frm5.changed)
                            btnActualizar.PerformClick();
                        frm5.Dispose();
                        break;

                    case "btnAprobarEP":
                        if (MessageBox.Show("¿Esta seguro que desea aprobar este EP?\n\nNota: Primero debe adjuntar el documento digitalizado con la firma o VºBº del cliente", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                frmEPAdjunto frm7 = new frmEPAdjunto();
                                frm7.Ep_id = ep_id;
                                frm7.Ep_obra = ep_obra;
                                frm7.Obra = obra;
                                frm7.TecnicoObra = tecnicoObra;
                                frm7.ShowDialog(this);
                                if (frm7.changed) //Se adjunto el documento
                                {
                                    frmEPAprobacion frm8 = new frmEPAprobacion();
                                    frm8.Ep_id = ep_id;
                                    frm8.Ep_obra = ep_obra;
                                    frm8.Obra = obra;
                                    frm8.TecnicoObra = tecnicoObra;
                                    frm8.ShowDialog(this);
                                    if (frm8.ok)
                                        btnActualizar.PerformClick();
                                    frm8.Dispose();
                                }
                                frm7.Dispose();
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
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
                        frmEPLog frm10 = new frmEPLog();
                        frm10.Id = ep_id;
                        frm10.ShowDialog(this);
                        frm10.Dispose();
                        break;
                }
            }
            else
                MessageBox.Show("No existen registros seleccionados.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCrearEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnCrearEP");
        }

        private void btnVerReporteEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnVerReporteEP");
        }

        private void btnGenerarReporteEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnGenerarReporteEP");
        }

        private void btnEnviarEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnEnviarEP");
        }

        private void btnModEPxObsClte_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnModEPxObsClte");
        }

        private void btnComentSegEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnComentSegEP");
        }

        private void btnAprobarEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnAprobarEP");
        }

        private void btnAdjuntoEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnAdjuntoEP");
        }

        private void btnTrazabilidadEP_Click(object sender, EventArgs e)
        {
            ejecutarAccion("btnTrazabilidadEP");
        }

        private void actualizar()
        {
            if (cboTecnicoObra.SelectedValue != null && cboObra.SelectedValue != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    dgvResumen.DataSource = null;
                    dgvDetalle.DataSource = null;
                    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                    WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();

                    string obra = (cboObra.SelectedValue == null ? "-1" : cboObra.SelectedValue.ToString());
                    listaDataSet = wsOperacion.ListarEstadoPagosPendientes(obra, cboTecnicoObra.SelectedValue.ToString(), cboFiltro.SelectedIndex);
                    if (listaDataSet.MensajeError.Equals(""))
                    {
                        dgvResumen.DataSource = listaDataSet.DataSet.Tables[0];
                        forms.dataGridViewHideColumns(dgvResumen, new string[] { "USUARIO_MOD_ADMIN", "FECHA_ENVIO_FACT", "USUARIO_CREACION_FACT_INET", "FECHA_CREACION_FACT_INET", "NUMERO_FACT_INET", "FECHA_ENVIO_FACT_CLTE", "FECHA_VENC_FACT_CLTE", "USUARIO_DAXCOBRADO", "FECHA_COBRO" }); //"FECHA_MOD_ADMIN", 
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
                Excel excel = new Excel();
                if (tabGuiasDespacho.Visible && dgvDetalle.Rows.Count > 0) //Detalle
                    excel.exportar(dgvDetalle);
                excel.exportar(dgvResumen); //Resumen
                Cursor.Current = Cursors.Default;
            }
            else
                MessageBox.Show("No existen registros a exportar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTecnicoObra.DataSource = null;
            cboObra.DataSource = null;
            //Vacia las grillas
            dgvResumen.DataSource = null;
            dgvDetalle.DataSource = null;

            cargarComboBoxTecnicos(cboEmpresa.Text);

            if (!Program.currentUser.PerfilUsuario.Equals("ADMIN"))
            {
                if (dtTecnicoObra != null)
                {
                    string login = Program.currentUser.Login;
                    DataRow[] rows = dtTecnicoObra.Select("Usuario = '" + login + "'");
                    if (rows.Length > 0)
                    {
                        cboTecnicoObra.SelectedValue = login;
                        cboTecnicoObra.Enabled = false;
                        btnActualizar.PerformClick();
                    }
                    else
                    {
                        //MessageBox.Show("El login de '" + login + "' no corresponde a un tecnico de obras o administrador del sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("El login de '" + login + "' no existe como tecnico dentro de esta empresa.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Dispose();
                    }
                }
            }
            else //ADMIN
            {
                if (cboTecnicoObra.Items.Count > 0)
                    cboTecnicoObra_SelectedIndexChanged(sender, e);
                btnActualizar.PerformClick();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            load = true;
            lblUsuario.Text = Program.currentUser.Login;
            string error = cargarComboBoxEmpresa();
            if (error.Equals(""))
            {
                cboEmpresa.SelectedIndex = 0;
                cboFiltro.SelectedIndex = 0;
                if (chkActualizar.Checked)
                    actualizar();
                btnDestinatariosObra.Visible = Program.ENVIRONMENT.Equals("DEBUG");
            }
            else
            {
                MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
            load = false;
        }

        private string cargarComboBoxEmpresa()
        {
            string error = "";
            try
            {
                //Obtiene las empresas del grupo
                cboEmpresa.Items.Clear();
                WsOperacion.Operacion ws = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet lista = ws.ObtenerParametroTO("EmpresasGrupo");
                if (lista.MensajeError.Equals(""))
                {
                    if (lista.DataSet.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = lista.DataSet.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            cboEmpresa.Items.Add(row["Par1"].ToString());
                        }
                    }
                }
                else
                    error = lista.MensajeError;
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        private string cargarComboBoxTecnicos(string empresa)
        {
            string error = "";
            try
            {
                dtObra = null;
                dtTecnicoObra = null;
                //Obtiene las obras activas de la empresa
                WsOperacion.Operacion ws = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet lista = ws.ObtenerObrasActivas_EP(empresa);
                if (lista.MensajeError.Equals(""))
                {
                    dtObra = lista.DataSet.Tables[0];
                    if (dtObra.Rows.Count > 0)
                    {
                        //Obtiene los tecnicos de las obras activas EP -> cboTecnicoObra
                        DataView view = new DataView(dtObra);
                        dtTecnicoObra = view.ToTable(true, "Usuario", "nombreUsuario");
                        forms.comboBoxFill(cboTecnicoObra, dtTecnicoObra, "Usuario", "nombreUsuario", 0);
                    }
                    cboTecnicoObra.Enabled = dtObra.Rows.Count > 0;
                }
                else
                    error = lista.MensajeError;
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        private string cargarComboBoxObrasxTecnico(string empresa, string tecnico)
        {
            string error = "";
            try
            {
                dtObra = null;
                dtTecnicoObra = null;
                //Obtiene las obras activas de la empresa
                WsOperacion.Operacion ws = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet lista = ws.ObtenerObrasActivas_EP(empresa);
                if (lista.MensajeError.Equals(""))
                {
                    dtObra = lista.DataSet.Tables[0];
                    if (dtObra.Rows.Count > 0)
                    {
                        //Obtiene los tecnicos de las obras activas EP -> cboTecnicoObra
                        DataView view = new DataView(dtObra);
                        dtTecnicoObra = view.ToTable(true, "Usuario", "nombreUsuario");
                        forms.comboBoxFill(cboTecnicoObra, dtTecnicoObra, "Usuario", "nombreUsuario", 0);
                    }
                }
                else
                    error = lista.MensajeError;
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }


        private void cboTecnicoObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboObra.DataSource = null;
            //Vacia las grillas
            dgvResumen.DataSource = null;
            dgvDetalle.DataSource = null;

            //if (!load) 
            if (cboTecnicoObra.SelectedValue != null)
            {
                DataTable dtTemporal = null;
                if (dtObra != null)
                {
                    DataRow[] rows = dtObra.Select("Usuario = '" + cboTecnicoObra.SelectedValue.ToString() + "'");
                    if (rows.Length > 0)
                    {
                        dtTemporal = dtObra.Clone();
                        foreach (DataRow dr in rows)
                        {
                            dtTemporal.ImportRow(dr);
                        }
                        DataRow newRow1 = dtTemporal.NewRow();
                        newRow1["IdObra"] = "-1";
                        newRow1["NombreObra"] = "(Todas)";
                        dtTemporal.Rows.InsertAt(newRow1, 0);

                        forms.comboBoxFill(cboObra, dtTemporal, "IdObra", "NombreObra", 0);
                    }
                    cboObra.Enabled = rows.Length > 0;
                }
                lblObras.Text = "Q=" + (cboObra.Items.Count - 1).ToString();
            }
        }

        private void cboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Vacia las grillas
            dgvResumen.DataSource = null;
            dgvDetalle.DataSource = null;

            if (!load && chkActualizar.Checked)
                btnActualizar.PerformClick();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerarReporteEP.Enabled = true;

            switch (cboFiltro.SelectedIndex)
            {
                case 0: //E.P por ingresar
                    btnCrearEP.Enabled = (Program.currentUser.PerfilUsuario.Equals("") ? true : false); //El usuario administrador no puede crear EP (Relacion: Obra vs Tecnico)
                    btnReporteEP.Enabled = false;
                    btnEnviarEP.Enabled = false;
                    btnModEPxObsClte.Enabled = false;
                    btnAprobarEP.Enabled = false;
                    break;
                case 1: //Creado sin reporte
                    btnCrearEP.Enabled = false;
                    btnReporteEP.Enabled = true;
                    btnEnviarEP.Enabled = false;
                    btnModEPxObsClte.Enabled = false;
                    btnAprobarEP.Enabled = false;
                    break;
                case 2: //Con reporte pero no enviado a cliente
                    btnCrearEP.Enabled = false;
                    btnReporteEP.Enabled = true;
                    //btnGenerarReporteEP.Enabled = false;
                    btnEnviarEP.Enabled = true;
                    btnModEPxObsClte.Enabled = false;
                    btnAprobarEP.Enabled = false;
                    break;
                case 3: //Enviado a cliente pero sin aprobacion
                    btnCrearEP.Enabled = false;
                    btnReporteEP.Enabled = true;
                    btnGenerarReporteEP.Enabled = false;
                    btnEnviarEP.Enabled = false;
                    btnModEPxObsClte.Enabled = true;
                    btnAprobarEP.Enabled = true;
                    break;
            }
            //Vacia las grillas
            dgvResumen.DataSource = null;
            dgvDetalle.DataSource = null;

            //tabGuiasDespacho.Visible = cboFiltro.SelectedIndex != 0;
            if (!load && chkActualizar.Checked)
                btnActualizar.PerformClick();
        }

        private void actualizarInfoDetalle(int ep_id) //ResumenGuiaDespachoxEp: agrupado x Gd/IT 
        {
            int totalEtiquetas = 0, totalKilos = 0;
            DataTable dtResumenxGuiaDespacho = null;

            lblTotalGuiasDespacho.Text = "0";
            lblTotalEtiquetas.Text = "0";
            lblTotalKilos.Text = "0";

            try
            {
                WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
                WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();
                listaDataSet = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id);
                if (listaDataSet.MensajeError.Equals(""))
                {
                    dtResumenxGuiaDespacho = listaDataSet.DataSet.Tables[0];
                    dgvDetalle.DataSource = dtResumenxGuiaDespacho;
                    //forms.dataGridViewHideColumns(dgvResumen, new string[] { "ID", "USUARIO", "FECHA", "TOTEM", "COMPLETA", "USUARIO_RECEP", "FECHA_RECEP", "USUARIO_CIERRE", "FECHA_CIERRE", "INET_MSG", "INET_FECHA" });
                    new Utils().estiloMillaresDataGridViewColumn(dgvDetalle, new string[] { "N_ETIQUETAS", "TOTAL_KGS" });
                    forms.dataGridViewAutoSizeColumnsMode(dgvDetalle, DataGridViewAutoSizeColumnsMode.AllCells);
                    //Obtiene la cantidad de etiquetas y kilos totales x EP
                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        totalEtiquetas += Convert.ToInt32(row.Cells[COLUMNNAME_N_ETIQUETAS].Value.ToString());
                        totalKilos += Convert.ToInt32(row.Cells[COLUMNNAME_TOTAL_KGS].Value.ToString());
                    }
                }
                else
                    MessageBox.Show(listaDataSet.MensajeError.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataView view = new DataView(dtResumenxGuiaDespacho);
            lblTotalGuiasDespacho.Text = view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows.Count.ToString("N0");
            lblTotalEtiquetas.Text = totalEtiquetas.ToString("N0");
            lblTotalKilos.Text = totalKilos.ToString("N0");
        }

        private void dgvResumen_SelectionChanged(object sender, EventArgs e)
        {
            tabGuiasDespacho.Visible = false;
            DataGridViewRow currentRow = dgvResumen.CurrentRow;
            if (currentRow != null) 
            {
                if (!currentRow.Cells[COLUMNNAME_ID].Value.ToString().Equals("0")) //Actualiza la informacion del detalle de la EP: dgvDetalle
                { 
                    Cursor.Current = Cursors.WaitCursor;
                    actualizarInfoDetalle(Convert.ToInt32(currentRow.Cells[COLUMNNAME_ID].Value.ToString()));
                    tabGuiasDespacho.Visible = true;
                    Cursor.Current = Cursors.Default; 
                }
            }
        }

        private void dgvResumen_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = dgvResumen.CurrentRow;
            if (currentRow != null) 
            {
                if (cboFiltro.SelectedIndex == 0 && !Program.currentUser.PerfilUsuario.Equals("")) //El usuario administrador no puede crear EP (Relacion: Obra vs Tecnico)
                    return;

                frmEPNueva frm0 = new frmEPNueva();
                frm0.Ep_id = Convert.ToInt32(currentRow.Cells[COLUMNNAME_ID].Value.ToString());
                frm0.Ep_obra = currentRow.Cells["EP_OBRA"].Value.ToString();
                frm0.Obra = currentRow.Cells["OBRA"].Value.ToString();
                frm0.TecnicoObra = currentRow.Cells["USUARIO"].Value.ToString();
                frm0.Estado = currentRow.Cells["EP_ESTADO"].Value.ToString();
                frm0.CargaFormulario(ref Pb, ref Lbl_PB);

                frm0.ShowDialog(this);
                if (frm0.changed)
                    btnActualizar.PerformClick();
            }
        }

        private void btnDestinatariosObra_Click(object sender, EventArgs e)
        {
            if (dgvResumen.CurrentRow != null)
            {
                MessageBox.Show(utils.obtenerDestinatariosObra(dgvResumen.CurrentRow.Cells[0].Value.ToString()).StringValue);
            }   
        }

        private void Btn_imprimePL_Click(object sender, EventArgs e)
        {
            Informes.Frm_Tmp lFrm = new Informes.Frm_Tmp();
            lFrm.ShowDialog(this);
            //ImprimirInforme("","","");

        }

        
      

       
    }
}