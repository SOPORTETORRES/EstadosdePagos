using System.Windows.Forms;
using CommonLibrary2;
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.IO;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace EstadosdePagos
{
    public class Utils
    {
        //Constantes
        private const string COLUMNNAME_GUIA_DESPACHO = "GUIA_DESPACHO";
        private const string COLUMNNAME_IT = "IT";
        private const string COLUMNNAME_N_ETIQUETAS = "N_ETIQUETAS";
        private const string COLUMNNAME_TOTAL_KGS = "TOTAL_KGS";
        //Variables
        private Forms forms = new Forms();
        private StringUtility su = new StringUtility();
        private FileSystemUtility fs = new FileSystemUtility();

        /// <summary>
        /// Aplica formato millares a una grilla e indices de columnas especificados
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="columnsIndex">Arreglo con los indices de columnas a formatear</param>
        public void estiloMillaresDataGridViewColumn(DataGridView dgv, int[] columnsIndex)
        {
            foreach (int columnIndex in columnsIndex)
            {
                if (columnIndex <= dgv.Columns.Count)
                {
                    dgv.Columns[columnIndex].DefaultCellStyle.Format = "N0";
                    dgv.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        /// <summary>
        /// Aplica formato millares a una grilla y nombres de columnas especificados
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="columnsName">Arreglo con los nombres de columnas a formatear</param>
        public void estiloMillaresDataGridViewColumn(DataGridView dgv, string[] columnsName)
        {
            foreach (string columnName in columnsName)
            {
                if (forms.dataGridViewExistsColumn(dgv, columnName))
                {
                    dgv.Columns[columnName].DefaultCellStyle.Format = "N0";
                    dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        /// <summary>
        /// Aplica formato millares a un valor
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>string, Texto formateado</returns>
        public string estiloMillares(int valor)
        {
            return valor.ToString("NO");
        }

        /// <summary>
        /// Cuenta las filas marcadas con check en un DataGridView
        /// </summary>
        /// <param name="dataGridView">DataGridView</param>
        /// <param name="columnName">Nombre de la columna (CheckBox)</param>
        /// <returns>int, cantidad de filas marcadas</returns>
        public int dataGridViewCountRowsChecked(DataGridView dataGridView, string columnName)
        {
            int count = -1;

            if (dataGridViewExistsColumn(dataGridView, columnName))
            {
                count = 0;

                if (dataGridViewExistsColumn(dataGridView, columnName))
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Cells[columnName].Value != null)
                        {
                            if (!String.IsNullOrEmpty(row.Cells[columnName].Value.ToString()))
                            {
                                if (Convert.ToBoolean(row.Cells[columnName].Value) == true)
                                    count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Verifica si existe una columna dentro de un DataGridView
        /// </summary>
        /// <param name="dataGridView">DataGridView</param>
        /// <param name="columnName">Nombre de la columna</param>
        /// <returns>bool</returns>
        public bool dataGridViewExistsColumn(DataGridView dataGridView, string columnName)
        {
            bool ok = false;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Name.ToUpper().Equals(columnName.ToUpper()))
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        /// <summary>
        /// Bloquea las columnas de un DataGridView (ReadOnly)
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        public void bloquearColumnas(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Index > 0)
                    column.ReadOnly = true;
            }
        }

        /// <summary>
        /// Elimina los caracteres especiales de una cadena de texto
        /// </summary>
        /// <param name="entrada">Cadena de texto</param>
        /// <returns>string, Texto sin caracteres especiales</returns>
        public string eliminarCaracteresEspeciales(string entrada)
        {
            StringUtility stringUtility = new StringUtility();
            string salida = entrada;
            if (!salida.Trim().Equals(""))
                salida = stringUtility.removeInvalidCharacters(salida, stringUtility.RegexPattern_Address);
            return salida;
        }

        /// <summary>
        /// Permite obtener la(s) fila(s) de la tabla parametro (tabla,codint)
        /// </summary>
        /// <param name="tabla">Tabla</param>
        /// <param name="codint">CodInt</param>
        /// <returns>Result (DataRows,MensajeError)</returns>
        public Result obtenerParametro(string tabla, string codint)
        {
            Result result = new Result();

            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet();

            listaDataSet = wsOperacion.ObtenerParametro(tabla);
            if (listaDataSet.MensajeError.Equals(""))
            {
                if (!codint.Trim().Equals(""))
                    result.DataRows = listaDataSet.DataSet.Tables[0].Select("Par_codint = '" + codint + "'");
                else
                    result.DataRows = listaDataSet.DataSet.Tables[0].Select();
            }
            else
                result.MensajeError = listaDataSet.MensajeError;

            return result;
        }

        /// <summary>
        /// Permite obtener los destinatarios (correos del cliente) de una obra
        /// </summary>
        /// <param name="ep_obra">Id de la obra</param>
        /// <returns>Result (StringValue,MensajeError)</returns>
        public Result obtenerDestinatariosObra(string ep_obra)
        {
            Result result = new Result();

            //Obtiene los destinatarios de la obra
            WsOperacion .Operacion ws = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSet = new WsOperacion.ListaDataSet(); //ws.ObtenerTablaConDestinatariosMAIL_EP(ep_obra);

            listaDataSet = ws.ObtenerTablaConDestinatariosMAIL_EP(ep_obra);
            if (listaDataSet.MensajeError.Equals(""))
            {
                foreach (DataRow row in listaDataSet.DataSet.Tables[0].Rows)
                {
                    result.StringValue += (result.StringValue.Equals("") ? "" : ";");
                    result.StringValue += row["maildest"].ToString().Trim().ToLower();
                }
            }
            else
                result.MensajeError = listaDataSet.MensajeError;

            return result;
        }

        /// <summary>
        /// Rutina para el envio de un correo a una obra especifica
        /// </summary>
        /// <param name="origen">Especifica el tipo de correo a enviar (INTERNO,CLIENTE)</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="obra">Id de la obra</param>
        /// <param name="cuerpo">Cuerpo del correo</param>
        /// <param name="ep">Id del EP</param>
        /// <returns></returns>
        public string enviarCorreoNotificacionaObra(string origen, string asunto, int obra, string cuerpo, int ep)
        {
            //WsMensajeria.Ws_To wsMensajeria = new WsMensajeria.Ws_To();
            //return wsMensajeria.EnviaNotificacionesEnviaMsgDeNotificacion("", asunto, obra, cuerpo); //"Cierre de solicitudes", -500
            MessageBox.Show("Origen: " + origen + "\nEP: " + ep + "\nObra: " + obra + "\nAsunto: " + asunto  + "\nCuerpo: " + cuerpo, "EnviarCorreo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return "";
        }

        //2017-08-25
        //public string enviarCorreo(string destinatarios, string asunto, string cuerpo, string[] adjuntos)
        //{
        //    MessageBox.Show("Destinatarios: " + destinatarios + ", Asunto: " + asunto + ", Cuerpo: " + cuerpo + ", NºAdjuntos: " + adjuntos.Length);
        //    return "";
        //}

        /// <summary>
        /// Permite el envio de una notificacion interna (correo)
        /// </summary>
        /// <param name="tipoNotificacionInterna">Par_codint en la tabla parametro: EP_NOTIFICACION</param>
        /// <param name="obra_id">Id de la obra</param>
        /// <param name="ep_id">Id del EP</param>
        /// <param name="asunto">Si este valor viene vacio, se obtendra de la tabla parametro, Par_alf1. Nota: Permite el uso de comodines: @obra_id, @ep_id, @obra</param>
        /// <param name="cuerpo">Si este valor viene vacio, se obtendra de la tabla parametro, Par_alf2. Nota: Permite el uso de comodines: @obra_id, @ep_id, @obra</param>
        /// <param name="obra">Nombre de la obra</param>
        /// <returns></returns>
        public string enviarCorreoNotificacionInterna(string tipoNotificacionInterna, string obra_id, int ep_id, string asunto, string cuerpo, string obra)
        {
            string destinatarios = "";
            Result result = obtenerParametro("EP_NOTIFICACION", tipoNotificacionInterna);
            if (result.MensajeError.Equals(""))
            {
                DataRow[] dataRows = result.DataRows;
                if (dataRows.Length > 0)
                {
                    destinatarios = dataRows[0]["PAR_DESCRIPCION"].ToString();
                    if (asunto.Trim().Equals(""))
                        asunto = dataRows[0]["PAR_ALF1"].ToString();
                    if (cuerpo.Trim().Equals(""))
                        cuerpo = dataRows[0]["PAR_ALF2"].ToString();
                    //if (!destinatarios.Trim().Equals(""))
                    //    MessageBox.Show("NOTIFICACION INTERNA/(" + tipoNotificacionInterna + ") -> Destinatarios: " + destinatarios + ", Asunto: " + asunto + ", Cuerpo: " + cuerpo, "NOTIFICACIONES (NO IMPLEMENTADA)");

                    //Reemplazo de comodines
                    //Asunto
                    asunto = asunto.Replace("@obra_id", obra_id).Replace("@OBRA_ID", obra_id);
                    asunto = asunto.Replace("@ep_id", ep_id.ToString()).Replace("@EP_ID", ep_id.ToString());
                    asunto = asunto.Replace("@obra", obra).Replace("@OBRA", obra);
                    //Cuerpo
                    cuerpo = cuerpo.Replace("@obra_id", obra_id).Replace("@OBRA_ID", obra_id);
                    cuerpo = cuerpo.Replace("@ep_id", ep_id.ToString()).Replace("@EP_ID", ep_id.ToString());
                    cuerpo = cuerpo.Replace("@obra", obra).Replace("@OBRA", obra);
                    //
                    enviarCorreoNotificacionaObra("INTERNA", asunto, Convert.ToInt32(obra_id), cuerpo, ep_id);
                }
            }
            return result.MensajeError;
        }

        /// <summary>
        /// Muestra un cuadro de dialogo para la seleccion de una carpeta
        /// </summary>
        /// <returns>string, ruta de la carpeta seleccionada</returns>
        private string seleccionarCarpeta()
        {
            string selectedPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Seleccione la carpeta donde guardará el reporte:";
            folderBrowserDialog.ShowNewFolderButton = true;
            //folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //folderBrowserDialog.SelectedPath =  Path.GetTempPath();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                selectedPath = folderBrowserDialog.SelectedPath;
            return selectedPath;
        }

        /// <summary>
        /// Copia una lista de archivos
        /// </summary>
        /// <param name="sourcePath">Carpeta de origen</param>
        /// <param name="destinationPath">Carpeta de destino</param>
        /// <param name="listaArchivos">Lista de archivos a copiar</param>
        private void copiarArchivosLista(string sourcePath, string destinationPath, List<string> listaArchivos)
        {
            sourcePath += (!su.Right(sourcePath, 1).Equals("\\") ? "\\" : "");
            destinationPath += (!su.Right(destinationPath, 1).Equals("\\") ? "\\" : "");
            if (fs.DirectoryExists(sourcePath) && fs.DirectoryExists(destinationPath))
            {
                foreach (string archivo in listaArchivos)
                {
                    if (fs.FileExists(sourcePath + archivo))
                        fs.copyFile(sourcePath + archivo, destinationPath + archivo);
                }
            }
        }

        /// <summary>
        /// Crea el archivo Excel del EP
        /// </summary>
        /// <param name="destinationPath">Carpeta de destino</param>
        /// <param name="text">Texto a insertar en la celda A1 del archivo</param>
        /// <returns>bool</returns>
        private bool generarArchivoEP(string destinationPath, string text)
        {
            destinationPath += (!su.Right(destinationPath, 1).Equals("\\") ? "\\" : "");
            string filename = destinationPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

            try
            {
                object paramMissing = Type.Missing;
                ExcelApp.Application excelApplication = new ExcelApp.Application();
                //excelApplication.Visible = true;
                ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Add(paramMissing);
                ExcelApp.Worksheet excelSheet = null;
                //int processId;
                // Find the Process Id
                //GetWindowThreadProcessId(excelApplication.Hwnd, out processId);

                if (excelWorkBook != null)
                {
                    excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1];
                    excelSheet.Cells[1, 1] = text;
                    excelSheet.Cells.Select();
                    excelSheet.Cells.EntireColumn.AutoFit();
                    excelSheet.Cells.EntireRow.AutoFit();
                    excelSheet.Range["A1"].Select();

                    excelWorkBook.SaveAs(filename);
                }
                excelWorkBook.Close(false);
                excelApplication.Quit();

                if (excelSheet != null)
                {
                    while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet) != 0) { }
                    excelSheet = null;
                }
                if (excelWorkBook != null)
                {
                    while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkBook) != 0) { }
                    excelWorkBook = null;
                }
                if (excelApplication != null)
                {
                    while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication) != 0) { }
                    excelApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //Process excelProcess = Process.GetProcessById(processId);
                //if (excelProcess != null)
                //    excelProcess.Kill();

                //Copia la plantilla al directorio de destino
                string plantillaEP = Application.StartupPath + (!su.Right(Application.StartupPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
                if (fs.FileExists(plantillaEP))
                    fs.copyFile(plantillaEP, destinationPath + "PlantillaEP.xls");
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Utils", MessageBoxButtons.OK, MessageBoxIcon.Error); //this.Text
                return false;
            }
        }

        /// <summary>
        /// Genera una EP con sus archivos asociados
        /// </summary>
        /// <param name="ep_obra">Id de la obra</param>
        /// <param name="obra">Nombre de la obra</param>
        /// <param name="ep_id">Id del EP</param>
        /// <param name="accion">Acciona realizar: 1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente</param>
        /// <returns>string, Vacio si no hubo errores o errores encontrados dentro de la generacion</returns>
        public string generarEP(string ep_obra, string obra, Int32 ep_id, int accion) //1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente
        {
            string error = "", valorKiloSuministro = "", destinatarios = "", dir_guiaDespacho = "", dir_it = "", archivo = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder validacion = new StringBuilder();
            StringBuilder archivoFaltantesGD = new StringBuilder();
            StringBuilder archivoFaltantesIT = new StringBuilder();
            List<string> listArchivosGuiasDespacho = new List<string>();
            List<string> listArchivosIT = new List<string>();
            string selectedPath = "";
            WsOperacion.Operacion ws = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetTo = new WsOperacion.ListaDataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            string validarFiles_GuiaDespacho = "0";
            Result result = null;
            DataRow[] rows = null;string lEmpresa = "";
            int totalGuiasDespacho = 0, totalITs = 0, totalEtiquetas = 0, totalKilos = 0;
            DataView view = null;

            //Verifica si existe la informacion necesaria para generar el informe
            try
            {
                //Encabezado
                sb.Append("Obra: " + obra + "\n");
                sb.Append("EP: " + ep_id.ToString() + "\n\n");

                //Actualiza la informacion del detalle EP: dgvDetalle
                DataTable dtResumenxGuiaDespacho = null;
                listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id); //GD /IT /Etiquetas /Kilos
                if (listaDataSetOp.MensajeError.Equals(""))
                    dtResumenxGuiaDespacho = listaDataSetOp.DataSet.Tables[0];
                else
                    error = listaDataSetOp.MensajeError.ToString();

                //Valor kilo suministro
                valorKiloSuministro = ws.ValorKiloSuministro_EP(ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                if (valorKiloSuministro.Equals("") && Program.ENVIRONMENT.Equals("DEBUG"))
                    valorKiloSuministro = "100";
                sb.Append("Valor kilo suministro de la obra $: " + (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro) + "\n\n");

                //Obtiene los destinatarios de la obra
                result = obtenerDestinatariosObra(ep_obra); //utils
                if (result.MensajeError.Equals(""))
                {
                    destinatarios = result.StringValue;
                    if (destinatarios.Equals("") && Program.ENVIRONMENT.Equals("DEBUG"))
                        destinatarios = "gerardo.jorquera@hotmail.com";
                    sb.Append("Destinatarios del envio del EP (Cliente): " + destinatarios + "\n\n");
                }
                else
                    error = result.MensajeError;

                // Debemos Saber a que empresa estamos haciendo el EP, ya que el  directorio esta por Empresa.
                lEmpresa = ws.ObtenerEmpresaPor_EP(ep_obra);
               
                ///---GUIAS DE DESPACHO---///
                //Obtiene el directorio donde se almacenan las imagenes de las guias de despacho
                result = obtenerParametro("EP_DIRECTORIO", "DIR_GUIADESPACHO"); //utils
                if (result.MensajeError.Equals(""))
                {
                    rows = result.DataRows;
                    if (rows.Length > 0)
                    {
                        dir_guiaDespacho = rows[0]["Par_Alf1"].ToString();
                        dir_guiaDespacho += (!su.Right(dir_guiaDespacho, 1).Equals("\\") ? "\\" : "");
                        dir_guiaDespacho = string.Concat(dir_guiaDespacho, lEmpresa, "\\");
                    }
                }
                else
                    error = result.MensajeError;
                sb.Append("Directorio con las imagenes de guias de despacho: " + (dir_guiaDespacho.Trim().Equals("") ? "(No definido)" : dir_guiaDespacho) + "\n\n");

                //Obtiene el parametro que indica si las imagenes digitalizadas de las guias de despacho son requeridas o no (0-No, 1-Si)
                if (!Program.ENVIRONMENT.Equals("DEBUG"))
                {
                    result = obtenerParametro("EP_REQUIRED", "FILES_GUIADESPACHO"); //utils
                    if (result.MensajeError.Equals(""))
                    {
                        rows = result.DataRows;
                        if (rows.Length > 0)
                            validarFiles_GuiaDespacho = rows[0]["par_num1"].ToString();
                    }
                    else
                        error = result.MensajeError;
                }

                //Obtiene la cantidad de GDs y verifica si el reporte cuenta con las imagenes de las guias de despacho
                view = new DataView(dtResumenxGuiaDespacho);
                totalGuiasDespacho = view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows.Count;

                if (!dir_guiaDespacho.Equals(""))
                {
                    foreach (DataRow row in view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows)
                    {
                        archivo = row[COLUMNNAME_GUIA_DESPACHO].ToString() + ".pdf";
                        if (fs.FileExists(dir_guiaDespacho + archivo))
                            listArchivosGuiasDespacho.Add(archivo);
                        else
                            archivoFaltantesGD.Append(" -> " + archivo + "\n");
                    }
                    if (archivoFaltantesGD.ToString().Length > 0)
                        sb.Append("Faltan los siguientes archivos gds :\n");
                    sb.Append(archivoFaltantesGD);
                }

                ///---ITs---///
                //Obtiene el directorio donde se almacenan los PDF de las its
                result = obtenerParametro("EP_DIRECTORIO", "DIR_IT"); //utils
                if (result.MensajeError.Equals(""))
                {
                    rows = result.DataRows;
                    if (rows.Length > 0)
                    { 
                        dir_it = rows[0]["Par_Alf1"].ToString();
                        dir_it += (!su.Right(dir_it, 1).Equals("\\") ? "\\" : "");
                    }
                }
                else
                    error = result.MensajeError;
                sb.Append("Directorio con las ITs: " + (dir_it.Trim().Equals("") ? "(No definido)" : dir_it) + "\n\n");

                //Obtiene la cantidad de ITs y verifica si el reporte cuenta con los archivos PDFs de las ITs
                view = new DataView(dtResumenxGuiaDespacho);
                totalITs = view.ToTable(true, COLUMNNAME_IT).Rows.Count;

                if (!dir_it.Equals(""))
                {
                    foreach (DataRow row in view.ToTable(true, COLUMNNAME_IT).Rows)
                    {
                        archivo = row[COLUMNNAME_IT].ToString() + ".PDF";
                        //Parche para los nombres de las ITS 
                        archivo = archivo.Replace("/", "_"); //ECT-1/1.PDF -> ECT-1_1.PDF
                        if (fs.FileExists(dir_it + archivo))
                            listArchivosIT.Add(archivo);
                        else
                        {
                            CreaInforme(row[COLUMNNAME_IT].ToString());
                            archivoFaltantesIT.Append(" -> " + archivo + "\n");
                        }
                            
                    }
                    if (archivoFaltantesIT.ToString().Length > 0)
                        sb.Append("Faltan los siguientes archivos its :\n");
                    sb.Append(archivoFaltantesIT);
                }

                ///--------///
                //Resumen
                //Obtiene la cantidad de etiquetas y kilos totales x EP
                foreach (DataRow row in dtResumenxGuiaDespacho.Rows)
                {
                    totalEtiquetas += Convert.ToInt32(row[COLUMNNAME_N_ETIQUETAS].ToString());
                    totalKilos += Convert.ToInt32(row[COLUMNNAME_TOTAL_KGS].ToString());
                }
                sb.Append("\nTotal guia(s) despacho(s): " + totalGuiasDespacho.ToString("N0") + "\n");
                sb.Append("\nTotal it(s): " + totalITs.ToString("N0") + "\n");
                sb.Append("Total etiqueta(s): " + totalEtiquetas.ToString("N0") + "\n");
                sb.Append("Total kilo(s): " + totalKilos.ToString("N0") + "\n\n");
                sb.Append("Total $$$ a cobrar: " + (totalKilos * (valorKiloSuministro.Equals("") ? 0 : Convert.ToInt32(valorKiloSuministro))).ToString("N0"));
                //MessageBox.Show(sb.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }

            if (error.Equals(""))
            {
                if (valorKiloSuministro.Equals("") || valorKiloSuministro.Equals("0"))
                    validacion.Append(" - Valor kilo suministro de la obra\n");
                if (destinatarios.Equals(""))
                    validacion.Append(" - Destinatarios de correo del reporte (cliente)\n");
                if (dir_guiaDespacho.Equals(""))
                    validacion.Append(" - Directorio de las guias de despacho digitalizadas\n");

                //ESPECIFICA SI LAS GDESPACHO SON REQUERIDAS AL MOMENTO DE GENERAR LA EP
                if (validarFiles_GuiaDespacho.Equals("1"))
                {
                    //2-Generar el reporte
                    //3-Envio al cliente
                    if (accion.Equals(2) || accion.Equals(3))
                    {
                        if (!dir_guiaDespacho.Equals("") && (listArchivosGuiasDespacho.Count < totalGuiasDespacho))
                            if (!archivoFaltantesGD.ToString().Equals(""))
                                validacion.Append(" - Archivos digitalizados de guias de despacho:\n\n" + archivoFaltantesGD.ToString());
                    }
                }

                if (validacion.Length == 0)
                {
                    //1-Vista preliminar -> Genera una carpeta temporal para "mostrar" el reporte y las imagenes de las guias de despacho
                    //3-Envio al cliente -> Genera una carpeta temporal para "enviar" el reporte y las imagenes de las guias de despacho por correo
                    if (accion.Equals(1) || accion.Equals(3))
                    {
                        selectedPath = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        if (!fs.DirectoryExists(selectedPath))
                        {
                            fs.createDirectory(selectedPath);
                            if (fs.error != null)
                            {
                                validacion.Append(fs.error.Message);
                                selectedPath = "";
                            }
                        }
                    }
                    else
                        selectedPath = seleccionarCarpeta();

                    if (!selectedPath.Equals(""))
                    {
                        if (generarArchivoEP(selectedPath, sb.ToString()))
                        {
                            //1-Vista preliminar
                            //2-Generar el reporte
                            if (accion.Equals(1) || accion.Equals(2))
                            {
                                if (listArchivosGuiasDespacho.Count > 0)
                                    copiarArchivosLista(dir_guiaDespacho, selectedPath, listArchivosGuiasDespacho);
                                if (listArchivosIT.Count > 0)
                                    copiarArchivosLista(dir_it, selectedPath, listArchivosIT);
                                fs.shell(selectedPath); //Abre la carpeta de destino
                            }
                            else if (accion.Equals(3))
                            { //3-Envio al cliente
                                //enviarCorreo(destinatarios, selectedPath);
                                enviarCorreoNotificacionInterna("EP_ENVIADOA_CLIENTE", ep_obra, ep_id, "", "", obra); //utils.
                                //Envio a cliente -> Se comenta porque no esta claro si el asunto y cuerpo se generará desde aqui o en la funcion de envio.
                                //enviarCorreoNotificacionaObra("CLIENTE", "asunto", Convert.ToInt32(ep_obra), "cuerpo", ep_id);
                            }
                        }
                    }
                    else
                        validacion.Insert(0, "El usuario seleciono el botón Cancelar");
                }
                else
                    validacion.Insert(0, "Faltan los siguientes datos requeridos:\n\n");
            }
            else
                validacion.Append(error);

            if (validacion.Length != 0)
                MessageBox.Show(validacion.ToString(), "Utils", MessageBoxButtons.OK, MessageBoxIcon.Error); //this.Text

            return validacion.ToString();
        }

        private void CreaInforme(string iViaje)
        {
            Informes.Informes lInf = new Informes.Informes();
            lInf.ImprimirInforme(iViaje);
        }

        public Boolean EsNumero(string iDato)
        {
            Boolean lRes = true;
            try
            {
                Convert.ToInt64 (iDato);

            }
            catch (Exception exc)
            {
                lRes = false;
            }
            return lRes;
        }

    }
}