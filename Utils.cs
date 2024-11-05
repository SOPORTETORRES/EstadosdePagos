using System.Windows.Forms;
using CommonLibrary2;
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.IO;
using ExcelApp = Microsoft.Office.Interop.Excel;
using System.Configuration;

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
        private void copiarArchivosLista(string sourcePath, string destinationPath, List<string> listaArchivos, string iTipo)
        {
              string lArchivo = "";
            sourcePath += (!su.Right(sourcePath, 1).Equals("\\") ? "\\" : "");
            destinationPath += (!su.Right(destinationPath, 1).Equals("\\") ? "\\" : "");

            if (iTipo == "G")
            {
                if (fs.DirectoryExists(sourcePath) && fs.DirectoryExists(destinationPath))
                {
                    foreach (string archivo in listaArchivos)
                    {
                        if (fs.FileExists(sourcePath + archivo))
                            fs.copyFile(sourcePath + archivo, destinationPath + archivo);
                    }
                }
            }
            if (iTipo == "I")
            {
                Char Delimitador = '.';
         
                if (fs.DirectoryExists(sourcePath) && fs.DirectoryExists(destinationPath))
                {
                    foreach (string archivo in listaArchivos)
                    {
                        String[] lPartes = archivo.Split(Delimitador);
                        lArchivo = string.Concat (lPartes[0].Replace("/", "_"), "P.pdf");

                        if (fs.FileExists(sourcePath + lArchivo))
                            fs.copyFile(sourcePath + lArchivo, destinationPath + lArchivo);

                         lPartes = archivo.Split(Delimitador);
                        lArchivo = string.Concat(lPartes[0].Replace("/", "_"), "D.pdf");

                        if (fs.FileExists(sourcePath + lArchivo))
                            fs.copyFile(sourcePath + lArchivo, destinationPath + lArchivo);


                    }
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
        //public string generarEP(string ep_obra, string obra, Int32 ep_id, int accion, ref ProgressBar Pb, ref Label Lbl_PB, string carpeta, Int32 correlativo) //1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente
        //{
        //    string error = "", valorKiloSuministro = "", destinatarios = "", dir_guiaDespacho = "", dir_it = "", archivo = "";
        //    StringBuilder sb = new StringBuilder();
        //    StringBuilder validacion = new StringBuilder();
        //    StringBuilder archivoFaltantesGD = new StringBuilder();
        //    StringBuilder archivoFaltantesIT = new StringBuilder();
        //    List<string> listArchivosGuiasDespacho = new List<string>();
        //    List<string> listArchivosIT = new List<string>();
        //    string selectedPath = "";
        //    WsOperacion.Operacion ws = new WsOperacion.Operacion();
        //    WsOperacion.ListaDataSet listaDataSetTo = new WsOperacion.ListaDataSet();
        //    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
        //    WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
        //    WsOperacion.ListaDataSet listaDataTMP = new WsOperacion.ListaDataSet();
        //    string validarFiles_GuiaDespacho = "0";
        //    Result result = null;
        //    DataRow[] rows = null;string lEmpresa = "";
        //    int totalGuiasDespacho = 0, totalITs = 0, totalEtiquetas = 0, totalKilos = 0 ;
        //    DataView view = null;
        //    DataTable dtResumenxGuiaDespacho = null;
        //    WsOperacion.EP_Generado lEP = new WsOperacion.EP_Generado();

        //    //Verifica si existe la informacion necesaria para generar el informe
        //    try
        //    {
        //        lEP.Id_EP = correlativo;
        //        Lbl_PB.Text = "Inicializando Variables . . . "; Lbl_PB.Refresh();

        //        //Encabezado
        //        sb.Append("Obra: " + obra + "\n");
        //        sb.Append("EP: " + ep_id.ToString() + "\n\n");

        //        //Actualiza la informacion del detalle EP: dgvDetalle
               
        //        listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id); //GD /IT /Etiquetas /Kilos
        //        if (listaDataSetOp.MensajeError.Equals(""))
        //            dtResumenxGuiaDespacho = listaDataSetOp.DataSet.Tables[0];
        //        else
        //            error = listaDataSetOp.MensajeError.ToString();


        //        //Valor kilo suministro
        //        valorKiloSuministro = ws.ValorKiloSuministro_EP(ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
        //        if (valorKiloSuministro.Equals("") && Program.ENVIRONMENT.Equals("DEBUG"))
        //            valorKiloSuministro = "100";
        //        sb.Append("Valor kilo suministro de la obra $: " + (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro) + "\n\n");

        //        //Obtiene los destinatarios de la obra
        //        result = obtenerDestinatariosObra(ep_obra); //utils
        //        if (result.MensajeError.Equals(""))
        //        {
        //            destinatarios = result.StringValue;
        //            if (destinatarios.Equals("") && Program.ENVIRONMENT.Equals("DEBUG"))
        //                destinatarios = "rbecerra@torresocaranza.cl";
        //            sb.Append("Destinatarios del envio del EP (Cliente): " + destinatarios + "\n\n");
        //        }
        //        else
        //            error = result.MensajeError;

        //        // Debemos Saber a que empresa estamos haciendo el EP, ya que el  directorio esta por Empresa.
        //        lEmpresa = ws.ObtenerEmpresaPor_EP(ep_obra);

        //        Lbl_PB.Text = " Obteniendo Parametros Generales  . . . "; Lbl_PB.Refresh();

        //        ///---GUIAS DE DESPACHO---///
        //        //Obtiene el directorio donde se almacenan las imagenes de las guias de despacho
        //        result = obtenerParametro("EP_DIRECTORIO", "DIR_GUIADESPACHO"); //utils
        //        if (result.MensajeError.Equals(""))
        //        {
        //            rows = result.DataRows;
        //            if (rows.Length > 0)
        //            {
        //                dir_guiaDespacho = rows[0]["Par_Alf1"].ToString();
        //                dir_guiaDespacho += (!su.Right(dir_guiaDespacho, 1).Equals("\\") ? "\\" : "");
        //                //dir_guiaDespacho = string.Concat(dir_guiaDespacho, lEmpresa, "\\");
        //            }
        //        }
        //        else
        //            error = result.MensajeError;
        //        sb.Append("Directorio con las imagenes de guias de despacho: " + (dir_guiaDespacho.Trim().Equals("") ? "(No definido)" : dir_guiaDespacho) + "\n\n");

        //        //Obtiene el parametro que indica si las imagenes digitalizadas de las guias de despacho son requeridas o no (0-No, 1-Si)
        //        if (!Program.ENVIRONMENT.Equals("DEBUG"))
        //        {
        //            result = obtenerParametro("EP_REQUIRED", "FILES_GUIADESPACHO"); //utils
        //            if (result.MensajeError.Equals(""))
        //            {
        //                rows = result.DataRows;
        //                if (rows.Length > 0)
        //                    validarFiles_GuiaDespacho = rows[0]["par_num1"].ToString();
        //            }
        //            else
        //                error = result.MensajeError;
        //        }

        //        //Obtiene la cantidad de GDs y verifica si el reporte cuenta con las imagenes de las guias de despacho
        //        view = new DataView(dtResumenxGuiaDespacho);
        //        totalGuiasDespacho = view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows.Count;

        //        Lbl_PB.Text = " Revisando Guías de Despacho   . . . "; Lbl_PB.Refresh();
        //        //Pb.Maximum  = totalGuiasDespacho; Pb.Minimum = 1; Pb.Value  = 1;

        //        if (!dir_guiaDespacho.Equals(""))
        //        {
        //            foreach (DataRow row in view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows)
        //            {
        //                //    if (Pb.Value < Pb.Maximum)
        //                //      Pb.Value = Pb.Value+1;
        //                //    else
        //                //        Pb.Value = Pb.Value-1;


        //                archivo = row[COLUMNNAME_GUIA_DESPACHO].ToString() + ".pdf";
        //                if (fs.FileExists(dir_guiaDespacho + archivo))
        //                    listArchivosGuiasDespacho.Add(archivo);
        //                else
        //                    archivoFaltantesGD.Append(" -> " + archivo + "\n");
        //            }

        //            if (archivoFaltantesGD.ToString().Length > 0)
        //                sb.Append("Faltan los siguientes archivos gds :\n");
        //            sb.Append(archivoFaltantesGD);
        //        }

        //        ///---ITs---///
        //        //Obtiene el directorio donde se almacenan los PDF de las its
        //        result = obtenerParametro("EP_DIRECTORIO", "DIR_IT"); //utils
        //        if (result.MensajeError.Equals(""))
        //        {
        //            rows = result.DataRows;
        //            if (rows.Length > 0)
        //            { 
        //                dir_it = rows[0]["Par_Alf1"].ToString();
        //                dir_it += (!su.Right(dir_it, 1).Equals("\\") ? "\\" : "");
        //            }
        //        }
        //        else
        //            error = result.MensajeError;
        //        sb.Append("Directorio con las ITs: " + (dir_it.Trim().Equals("") ? "(No definido)" : dir_it) + "\n\n");


        //        // Recorremos la tabla con las IT, si no estan las creamos
        //        Lbl_PB.Text = " Revisando las IT    . . . "; Lbl_PB.Refresh();
        //        Pb.Maximum = dtResumenxGuiaDespacho.Rows.Count; Pb.Minimum = 1; Pb.Value = 1;

        //        int i = 0; string lPathArchivo = dir_it;
        //        for (i = 0; i < dtResumenxGuiaDespacho.Rows.Count; i++)
        //        {
        //            if (Pb.Value < Pb.Maximum)
        //                Pb.Value = Pb.Value+1;
        //            else
        //                if (Pb.Value > Pb.Minimum )
        //                Pb.Value = Pb.Value-1;

        //            Pb.Refresh();
        //            //por cada IT, debe haber una portada y un detalle 

        //        //  Hay que descaomentar estas lineas y establecer una PathDestino 
        //            //if (ExisteArchivo(dtResumenxGuiaDespacho.Rows[i]["It"].ToString(), lPathArchivo) ==false)
        //            //        CreaInforme(dtResumenxGuiaDespacho.Rows [i]["It"].ToString(), true );
        //        }

        //        //Obtiene la cantidad de ITs y verifica si el reporte cuenta con los archivos PDFs de las ITs
        //        view = new DataView(dtResumenxGuiaDespacho);
        //        totalITs = view.ToTable(true, COLUMNNAME_IT).Rows.Count;

        //        if (!dir_it.Equals(""))
        //        {
        //            foreach (DataRow row in view.ToTable(true, COLUMNNAME_IT).Rows)
        //            {
        //                archivo = row[COLUMNNAME_IT].ToString() + ".PDF";
        //                //Parche para los nombres de las ITS 
        //                archivo = archivo.Replace("/", "_"); //ECT-1/1.PDF -> ECT-1_1.PDF
        //                //debemos revisar 2 archivos por IT
        //                lPathArchivo = string.Concat(dir_it, archivo);
        //                if (ExisteArchivo(archivo.ToString(), lPathArchivo) == false)
        //                    //if (fs.FileExists(dir_it + archivo))
        //                    listArchivosIT.Add(archivo);
        //                else
        //                {   
        //                    archivoFaltantesIT.Append(" -> " + archivo + "\n");
        //                }
                            
        //            }
        //            if (archivoFaltantesIT.ToString().Length > 0)
        //                sb.Append("Faltan los siguientes archivos its :\n");
        //            sb.Append(archivoFaltantesIT);
        //        }

        //        ///--------///
        //        //Resumen
        //        //Obtiene la cantidad de etiquetas y kilos totales x EP
        //        foreach (DataRow row in dtResumenxGuiaDespacho.Rows)
        //        {
        //            totalEtiquetas += Convert.ToInt32(row[COLUMNNAME_N_ETIQUETAS].ToString());
        //            totalKilos += Convert.ToInt32(row[COLUMNNAME_TOTAL_KGS].ToString());
        //        }
        //        sb.Append("\nTotal guia(s) despacho(s): " + totalGuiasDespacho.ToString("N0") + "\n");
        //        sb.Append("\nTotal it(s): " + totalITs.ToString("N0") + "\n");
        //        sb.Append("Total etiqueta(s): " + totalEtiquetas.ToString("N0") + "\n");
        //        sb.Append("Total kilo(s): " + totalKilos.ToString("N0") + "\n\n");
        //        sb.Append("Total $$$ a cobrar: " + (totalKilos * (valorKiloSuministro.Equals("") ? 0 : Convert.ToInt32(valorKiloSuministro))).ToString("N0"));
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
        //        if (validarFiles_GuiaDespacho.Equals("1"))
        //        {
        //            //2-Generar el reporte
        //            //3-Envio al cliente
        //            if (accion.Equals(2) || accion.Equals(3))
        //            {
        //                if (!dir_guiaDespacho.Equals("") && (listArchivosGuiasDespacho.Count < totalGuiasDespacho))
        //                    if (!archivoFaltantesGD.ToString().Equals(""))
        //                        validacion.Append(" - Archivos digitalizados de guias de despacho:\n\n" + archivoFaltantesGD.ToString());
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

        //            //*****************************************************************ZOna de Cambio  **************************************//
        //            if (!selectedPath.Equals(""))
        //            {
        //                try
        //                {
        //                    //Anexa el nombre de la carpeta a generar
        //                    selectedPath = selectedPath + (!su.Right(Application.StartupPath, 1).Equals("\\") ? "\\" : "") + carpeta;
        //                    if (!fs.DirectoryExists(selectedPath))
        //                        fs.createDirectory(selectedPath);

        //                    //Obtenemos el Objeto Obra para poder cargar la planilla excel
        //                    listaDataTMP = ws.ObtenerDatosObraParaEP(ep_obra);
                            

        //                    //Copia la plantilla al directorio de destino
        //                    string plantillaEP = Application.StartupPath + (!su.Right(Application.StartupPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
        //                    string outputEP = selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
        //                    string outputEPPdf = selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.pdf";
        //                    if (fs.FileExists(plantillaEP))
        //                        fs.copyFile(plantillaEP, outputEP);

        //                    if (fs.error == null)
        //                    {
        //                        //Inserta los datos en el excel de salida
        //                        object paramMissing = Type.Missing;
        //                        ExcelApp.Application excelApplication = new ExcelApp.Application();
        //                        excelApplication.DisplayAlerts = false;
        //                        excelApplication.Visible = true;
        //                        ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Open(outputEP); //.Add(paramMissing);
        //                        ExcelApp.Worksheet excelSheet = null;
        //                        DataTable ltblServicios = new DataTable ();
        //                        if (listaDataTMP.DataSet.Tables.Count  > 1)
        //                            ltblServicios = listaDataTMP.DataSet.Tables["ServiciosObra"].Copy();


        //                        if (excelWorkBook != null)
        //                        {
        //                            //Hoja: Resumen
        //                            (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
        //                            listaDataSetOp = wsOperacion.ListarEPExcel_ResumenObra(ep_obra);
        //                            if (listaDataSetOp.MensajeError.Equals(""))
        //                            {
        //                                foreach (DataRow row in listaDataSetOp.DataSet.Tables[0].Rows)
        //                                {
        //                                    excelSheet.Range["D6"].Value = "ORDEN DE COMPRA N° " + row["ORDEN_COMPRA"].ToString();
        //                                    excelSheet.Range["F11"].Value = row["UNIDAD"].ToString();
        //                                    excelSheet.Range["F12"].Value = row["UNIDAD"].ToString();
        //                                    excelSheet.Range["F13"].Value = row["UNIDAD"].ToString();
        //                                    excelSheet.Range["BH29"].Value = Program.currentUser.Name;  //row["NOMBREUSUARIO"].ToString(); //Program.currentUser.Name;
        //                                    excelSheet.Range["E25"].Value = row["CLIENTE"].ToString();
        //                                    excelSheet.Range["E27"].Value = row["RUTCliente"].ToString();
        //                                    excelSheet.Range["E29"].Value = row["CentroCOSTO"].ToString();

        //                                    if ((listaDataTMP.DataSet.Tables.Count > 0) && (listaDataTMP.DataSet.Tables[0].Rows.Count > 0))
        //                                    {
        //                                        excelSheet.Range["E11"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
        //                                        excelSheet.Range["E12"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
        //                                        excelSheet.Range["E13"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
                                   
        //                                        if (ltblServicios != null)
        //                                        {
        //                                            excelSheet.Range["G11"].Value = ObtenerPrecioUnitarioPorServicio(ltblServicios, "Suministro"); //Sumunistro
        //                                            excelSheet.Range["G12"].Value = ObtenerPrecioUnitarioPorServicio(ltblServicios, "Preparacion"); //Preparacion
        //                                        }
        //                                        else
        //                                        {
        //                                            excelSheet.Range["G11"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["ValorUnitario"].ToString(); //Sumunistro
        //                                            excelSheet.Range["G12"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["ValorUnitario"].ToString(); //Preparacion
        //                                        }


        //                                        //
        //                                        excelSheet.Range["BE11"].Value = totalKilos;  //Sumunistro
                                                
        //                                         excelSheet.Range["BE12"].Value = RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho);  // si la IT es tipo TP 
        //                                        //excelSheet.Range["BE12"].Value = totalKilos;  // si la IT es tipo TP 
        //                                        //No se incluye en la linea de cobro de preparación==> TotalKilos - Kilos de la Guia 
        //                                       // excelSheet.Range["BE11"].Value = totalKilos*int.Parse (listaDataTMP.DataSet.Tables[0].Rows[0]["ValorUnitario"].ToString());
        //                                    }
        //                                }
        //                            }
        //                            else
        //                                error = listaDataSetOp.MensajeError.ToString();

        //                            excelSheet.Range["BC5"].Value = "Obra " + obra;
        //                            excelSheet.Range["BE6"].Value = "ESTADO DE PAGO ACTUAL N° " + correlativo;

        //                            //EP generados (Todos)
        //                            int fila = 11;
        //                            int columna = 9;
        //                            Int64 kilos = 0, total = 0, correl = 0;
        //                            Int64 totalKilosCobrados = 0, totalCobrado = 0;
        //                            listaDataSetOp = wsOperacion.ListarEPExcel_ResumenEpGeneradosxObra(ep_obra);
        //                            if (listaDataSetOp.MensajeError.Equals(""))
        //                            {
        //                                foreach (DataRow row in listaDataSetOp.DataSet.Tables[0].Rows)
        //                                {
        //                                    //row["CORRELATIVO"].ToString();
        //                                    kilos = !EsNumero(row["KILOS"].ToString()) ? 0 : Convert.ToInt32(row["KILOS"].ToString());
        //                                    total = !EsNumero(row["TOTAL_$$$"].ToString()) ? 0 : Convert.ToInt32(row["TOTAL_$$$"].ToString());
        //                                    correl = !EsNumero(row["CORRELATIVO"].ToString()) ? 0 : Convert.ToInt32(row["CORRELATIVO"].ToString());

        //                                    excelSheet.Cells[11, columna] = kilos;
        //                                    excelSheet.Cells[11, columna + 1] = total;

        //                                    if (correlativo != correl) //Acumula los EP cobrados, sin incluir el EP actual
        //                                    {
        //                                        totalKilosCobrados += kilos;
        //                                        totalCobrado += total;
        //                                    }
        //                                    columna = columna + 2;
        //                                }
        //                            }
        //                            else
        //                                error = listaDataSetOp.MensajeError.ToString();

        //                            //LLenamos el Objeto con los totales del EP **********************************************+
        //                            int lTmp = 0;
        //                            lTmp = !EsNumero(excelSheet.Range["BE14"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["BE14"].Value.ToString());
        //                            lEP.TotalKgs_EP = lTmp;
        //                            lTmp = !EsNumero(excelSheet.Range["BF14"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["BF14"].Value.ToString());
        //                            lEP.TotalValor_EP  = lTmp;
        //                            lTmp = !EsNumero(ep_obra.ToString()) ? 0 : Convert.ToInt32(ep_obra.ToString());
        //                            lEP.IdObra = lTmp;

        //                            //List<WsOperacion.EP_GeneradoDetalle> lList_DetEP = new List<WsOperacion.EP_GeneradoDetalle>() ;
        //                            WsOperacion.EP_GeneradoDetalle[] lList_DetEP = new WsOperacion.EP_GeneradoDetalle[3];

        //                            WsOperacion.EP_GeneradoDetalle lDetEP = new WsOperacion.EP_GeneradoDetalle();
        //                            //Suministro
        //                            lDetEP.Id_EP = lEP.Id_EP; lDetEP.Servicio = "Suministro";
        //                            lTmp = !EsNumero(excelSheet.Range["G11"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["G11"].Value.ToString());
        //                            lDetEP.ValorKgs = lTmp;
        //                            lTmp = !EsNumero(excelSheet.Range["BE11"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["BE11"].Value.ToString());
        //                            lDetEP.TotalKgs   = lTmp;
        //                            lList_DetEP[0]=lDetEP;
        //                            //Preparacion
        //                            lDetEP = new WsOperacion.EP_GeneradoDetalle();
        //                            lDetEP.Id_EP = lEP.Id_EP; lDetEP.Servicio = "Preparacion";
        //                            lTmp = !EsNumero(excelSheet.Range["G12"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["G12"].Value.ToString());
        //                            lDetEP.ValorKgs = lTmp;
        //                            lTmp = !EsNumero(excelSheet.Range["BE12"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["BE12"].Value.ToString());
        //                            lDetEP.TotalKgs = lTmp;
        //                            lList_DetEP[1]=(lDetEP);
        //                            //Otros 
        //                            lDetEP = new WsOperacion.EP_GeneradoDetalle();
        //                            lDetEP.Id_EP = lEP.Id_EP; lDetEP.Servicio = "Otros";
        //                            lTmp = !EsNumero(excelSheet.Range["G13"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["G13"].Value.ToString());
        //                            lDetEP.ValorKgs = lTmp;
        //                            lTmp = !EsNumero(excelSheet.Range["BE13"].Value.ToString()) ? 0 : Convert.ToInt32(excelSheet.Range["BE13"].Value.ToString());
        //                            lDetEP.TotalKgs = lTmp;
        //                            lList_DetEP[2]=(lDetEP);
        //                            //Persistimos los regitros
        //                            //Falta la llamada a  la Ws por la persistencia de datos, con try cath para evitar la visualizacion de Excel
        //                            try
        //                            {
        //                                lEP.Detalle = lList_DetEP;
        //                                lEP = ws.GrabaEP_Generado(lEP);

        //                            }
        //                            catch (Exception exc)
        //                            {
        //                                MessageBox.Show(string.Concat("Ha Ocurrido el siguiente error: ", exc.Message.ToString()), "Avisos Sistema");
        //                            }

        //                            // Falta de MModificacion
        //                            //Fin  carga Obj EP

        //                            //EP anteriores
        //                            excelSheet.Range["BC11"].Value = totalKilosCobrados;
                                    
        //                            excelSheet.Range["BD11"].Value = totalCobrado;
        //                            //EP actual
        //                            // se comenta por modificaciones 06-12-2017
        //                            //excelSheet.Range["BD11"].Value = totalKilos;
        //                            //excelSheet.Range["BE11"].Value = totalaCobrar;

        //                            //Hoja: Detalle Guias
        //                            (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[3]).Select();
        //                            ExcelApp.Range rng = excelSheet.get_Range("B8", "A1000");
        //                            rng.EntireRow.Delete(ExcelApp.XlDirection.xlUp);

        //                            fila = 8;
        //                            listaDataSetOp = wsOperacion.ListarEPExcel_ResumenGuiasxObra(ep_obra);
        //                            if (listaDataSetOp.MensajeError.Equals(""))
        //                            {
        //                                foreach (DataRow row in listaDataSetOp.DataSet.Tables[0].Rows)
        //                                {
        //                                    excelSheet.Cells[fila, 1] = row["FECHA"].ToString();
        //                                    excelSheet.Cells[fila, 2] = row["DESCRIPCION"].ToString();
        //                                    excelSheet.Cells[fila, 3] = row["GUIA_DESPACHO"].ToString();
        //                                    excelSheet.Cells[fila, 4] = row["KILOS"].ToString();
        //                                    fila++;
        //                                }
        //                            }
        //                            else
        //                                error = listaDataSetOp.MensajeError.ToString();

        //                            (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
        //                            excelWorkBook.Save();
        //                            excelWorkBook.ExportAsFixedFormat(ExcelApp.XlFixedFormatType.xlTypePDF, outputEPPdf);
        //                        }
        //                        excelWorkBook.Close(false);
        //                        excelApplication.Quit();

        //                        if (excelSheet != null)
        //                        {
        //                            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet) != 0) { }
        //                            excelSheet = null;
        //                        }
        //                        if (excelWorkBook != null)
        //                        {
        //                            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkBook) != 0) { }
        //                            excelWorkBook = null;
        //                        }
        //                        if (excelApplication != null)
        //                        {
        //                            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication) != 0) { }
        //                            excelApplication = null;
        //                        }
        //                        GC.Collect();
        //                        GC.WaitForPendingFinalizers();

        //                        //if (generarArchivoEP(selectedPath, sb.ToString()))
        //                        //{
        //                        //1-Vista preliminar
        //                        //2-Generar el reporte
        //                        if (accion.Equals(1) || accion.Equals(2))
        //                        {
        //                            if (listArchivosGuiasDespacho.Count > 0)
        //                                copiarArchivosLista(dir_guiaDespacho, selectedPath, listArchivosGuiasDespacho,"G");
        //                            if (listArchivosIT.Count > 0)
        //                                copiarArchivosLista(dir_it, selectedPath, listArchivosIT,"I");
        //                            fs.shell(selectedPath); //Abre la carpeta de destino
        //                        }
        //                        else if (accion.Equals(3))
        //                        { //3-Envio al cliente
        //                            //enviarCorreo(destinatarios, selectedPath);
        //                            enviarCorreoNotificacionInterna("EP_ENVIADOA_CLIENTE", ep_obra, ep_id, "", "", obra); //utils.
        //                            //Envio a cliente -> Se comenta porque no esta claro si el asunto y cuerpo se generará desde aqui o en la funcion de envio.
        //                            //enviarCorreoNotificacionaObra("CLIENTE", "asunto", Convert.ToInt32(ep_obra), "cuerpo", ep_id);
        //                        }
        //                        //}
        //                    }
        //                    else
        //                        validacion.Append(fs.error.Message);
        //                }
        //                catch (Exception exc)
        //                {
        //                    validacion.Append(exc.Message);
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
        //        MessageBox.Show(validacion.ToString(), "Utils", MessageBoxButtons.OK, MessageBoxIcon.Error); //this.Text

        //    return validacion.ToString();
        //}


        private void CopiarArchivo(string iOrigen, string iDestino)
        {
            if (File.Exists(iDestino) == true)
                File.Delete(iDestino);

            File.Copy(iOrigen, iDestino);
        }


        private string  NombreArchivoGuia(string iNroGuia,string iPathBuscar)
        {
            DirectoryInfo di = null;string lArchivo = ""; string[] separators2 = { " " };
            string[] lPartes = null;int i = 0;string lTx = ""; string lRes = "";

            if (Directory.Exists (iPathBuscar ))
            {
                di = new DirectoryInfo(iPathBuscar);
                foreach (var fi in di.GetFiles())
                {
                    lArchivo=fi.Name;
                    lTx = string.Concat(iNroGuia, ".pdf");
                     lPartes = lArchivo.Split(separators2, StringSplitOptions.RemoveEmptyEntries);
                    for (i=0;i<lPartes .Length; i++ )
                    {
                        if (lPartes[i].ToString().Equals(lTx))
                        {
                            lRes = lArchivo;
                        }

                    }
                }
            }
            return lRes;

        }

        private string OBtenerPath_EGD(string iDato)
        {
            string lRes = "";string lCodSuc = ""; string lFecha = "";
            string[] separators = { "|" }; string[] separators2 = { "/" };
            string[] lPartes = iDato.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            lCodSuc = lPartes[0]; string lMesPath = ""; string lTmpPath = "";
            lFecha = lPartes[1];

            lRes = Path.Combine(@"S:\Guias Torres Ocaranza\", lPartes[2].ToString());

            //switch (lPartes[0])
            //{
            //    case "1":  // Santiago
            //        lPartes = lFecha.Split(separators2, StringSplitOptions.RemoveEmptyEntries);
            //        lRes = Path.Combine(@"S:\Guias Torres Ocaranza\",  lPartes[2].ToString());

            //        //lRes = @"U:\Guías TO (Stgo)\2020\";
            //        break;
            //    case "2":  //Calama 
            //        lPartes = lFecha.Split(separators2, StringSplitOptions.RemoveEmptyEntries);

            //        //if (lPartes[1].ToString().Equals ("01"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\01 ENERO\"));

            //        //if (lPartes[1].ToString().Equals("02"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\02 FEBRERO\"));

            //        //if (lPartes[1].ToString().Equals("03"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString()), @"\03 MARZO\");

            //        //if (lPartes[1].ToString().Equals("04"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\04 ABRIL\"));

            //        //if (lPartes[1].ToString().Equals("05"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\05 MAYO\"));

            //        //if (lPartes[1].ToString().Equals("06"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\06 JUNIO\"));

            //        //if (lPartes[1].ToString().Equals("07"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\07 JULIO\"));

            //        //if (lPartes[1].ToString().Equals("08"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\08 AGOSTO\"));

            //        //if (lPartes[1].ToString().Equals("09"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\09 SEPTIEMBRE\"));

            //        //if (lPartes[1].ToString().Equals("10"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\10 OCTUBRE\"));

            //        //if (lPartes[1].ToString().Equals("11"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\11 NOVIEMBRE\"));

            //        //if (lPartes[1].ToString().Equals("12"))
            //        //    lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías TO (Calama)\", string.Concat(lPartes[2].ToString(), @"\12 DICIEMBRE\"));


            //        break;
              
            //    case "3": //Coronel
            //        lPartes = lFecha.Split(separators2, StringSplitOptions.RemoveEmptyEntries);
            //        lMesPath = Path .Combine (@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat ("Coronel ", lPartes[2].ToString()));


            //        if (lPartes[1].ToString().Equals("01"))
            //        {
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\ENERO 2020\"));

            //        }
                       


            //        if (lPartes[1].ToString().Equals("02"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\FEBRERO 2020\"));

            //        if (lPartes[1].ToString().Equals("03"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\MARZO 2020\"));  

            //        if (lPartes[1].ToString().Equals("04"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\ABRIL 2020\"));

            //        if (lPartes[1].ToString().Equals("05"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\MARZO 2020\"));

            //        if (lPartes[1].ToString().Equals("06"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\JUNIO 2020\"));

            //        if (lPartes[1].ToString().Equals("07"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\JULIO 2020\"));

            //        if (lPartes[1].ToString().Equals("08"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\AGOSTO 2020\"));

            //        if (lPartes[1].ToString().Equals("09"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\SEPTIEMBRE 2020\"));

            //        if (lPartes[1].ToString().Equals("10"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\OCTUBRE 2020\"));

            //        if (lPartes[1].ToString().Equals("11"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\2020\NOVIEMBRE 2020\"));

            //        if (lPartes[1].ToString().Equals("12"))
            //            lRes = Path.Combine(@"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho\Guías Coronel\", string.Concat("Coronel ", lPartes[2].ToString(), @"\DICIEMBRE 2020\"));
                   
            //        break;
                         
            //}
            return lRes;
        }

        public string generarEP_V2(string ep_obra, string obra, Int32 ep_id, int accion, ref ProgressBar Pb, ref Label Lbl_PB, string carpeta, Int32 correlativo) //1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente
        {
            string error = "", valorKiloSuministro = "", destinatarios = "", dir_guiaDespacho = "", dir_it = "", archivo = "";
            StringBuilder sb = new StringBuilder();            StringBuilder validacion = new StringBuilder();
            StringBuilder archivoFaltantesGD = new StringBuilder();            StringBuilder archivoFaltantesIT = new StringBuilder();
            List<string> listArchivosGuiasDespacho = new List<string>();            List<string> listArchivosIT = new List<string>();
            string selectedPath = "";            WsOperacion.Operacion ws = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetTo = new WsOperacion.ListaDataSet();            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();            WsOperacion.ListaDataSet listaDataTMP = new WsOperacion.ListaDataSet();
            string validarFiles_GuiaDespacho = "0";            Result result = null;            DataRow[] rows = null; string lEmpresa = "";
            int totalGuiasDespacho = 0, totalITs = 0, totalEtiquetas = 0, totalKilos = 0;            DataView view = null;
            DataTable dtResumenxGuiaDespacho = null;            WsOperacion.EP_Generado lEP = new WsOperacion.EP_Generado();
            string lPathDestino = ""; string lPathDestinoLocal = "";string lPathServer = "";string lSiglaOBra = "";
            string lPathGuia = "";string lTmp = "";
            //Verifica si existe la informacion necesaria para generar el informe
            try
            {
                lEP.Id_EP = correlativo;
                Lbl_PB.Text = "Inicializando Variables . . . "; Lbl_PB.Refresh();

                //Encabezado
                sb.Append("Obra: " + obra + "\n");
                sb.Append("EP: " + ep_id.ToString() + "\n\n");

                selectedPath = @"C:\TMP\Estado de pago\DOC\";



                //Actualiza la informacion del detalle EP: dgvDetalle

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
                        destinatarios = "rbecerra@torresocaranza.cl";
                    sb.Append("Destinatarios del envio del EP (Cliente): " + destinatarios + "\n\n");
                }
                else
                    error = result.MensajeError;

                // Debemos Saber a que empresa estamos haciendo el EP, ya que el  directorio esta por Empresa.
                lEmpresa = ws.ObtenerEmpresaPor_EP(ep_obra);

                Lbl_PB.Text = " Obteniendo Parametros Generales  . . . "; Lbl_PB.Refresh();

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
                        //dir_guiaDespacho = string.Concat(dir_guiaDespacho, lEmpresa, "\\");
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
                if (totalGuiasDespacho>0)
                    lSiglaOBra = view[0]["IT"].ToString().Substring(0, 3);


                lPathDestino = Path.Combine(selectedPath, lSiglaOBra, correlativo.ToString ());
                lPathDestinoLocal = Path.Combine(selectedPath);
                if (Directory.Exists(lPathDestino) == false)
                    Directory.CreateDirectory(lPathDestino);

                Lbl_PB.Text = " Revisando Guías de Despacho   . . . "; Lbl_PB.Refresh();
                //Pb.Maximum  = totalGuiasDespacho; Pb.Minimum = 1; Pb.Value  = 1;


                string lOBtenerPdfGuias = ConfigurationManager.AppSettings["IncluyeGuiasPdf"].ToString();
                if (MessageBox.Show("¿Esta seguro que desea Descargar los Respaldos ?", "Avisos Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)          
                   // if (lOBtenerPdfGuias.ToUpper().Equals("S"))
                { 
                    if ((!dir_guiaDespacho.Equals(""))) // &&())
                    {
                        foreach (DataRow row in view.ToTable(true, COLUMNNAME_GUIA_DESPACHO).Rows)
                        {
                            // lCodSucINET
                            //lTmp = ws.ObtenerSucursal_PorNroGuiaDespacho(row[COLUMNNAME_GUIA_DESPACHO].ToString());

                            dir_guiaDespacho = Path.Combine(@"S:\Guias Torres Ocaranza\"); //, string.Concat (row[COLUMNNAME_GUIA_DESPACHO].ToString()));  //OBtenerPath_EGD(lTmp);

                            //archivo = string.Concat ( "GTE GUIA ",  row[COLUMNNAME_GUIA_DESPACHO].ToString() , ".pdf");
                            archivo = string.Concat("", row[COLUMNNAME_GUIA_DESPACHO].ToString(), ".pdf");

                            if (fs.FileExists(Path.Combine(dir_guiaDespacho, archivo)))
                            {
                                listArchivosGuiasDespacho.Add(archivo);
                                CopiarArchivo(Path.Combine(dir_guiaDespacho, archivo), Path.Combine(lPathDestino, archivo));
                            } 
                            else
                                {
                                    lPathGuia= NombreArchivoGuia(row[COLUMNNAME_GUIA_DESPACHO].ToString(), dir_guiaDespacho);
                                    if (File.Exists(Path.Combine(dir_guiaDespacho, lPathGuia)) == false)
                                        archivoFaltantesGD.Append(" -> " + archivo + "\n");
                                    else
                                    {
                                        listArchivosGuiasDespacho.Add(lPathGuia);
                                        CopiarArchivo(Path.Combine(dir_guiaDespacho, lPathGuia), Path.Combine(lPathDestino, lPathGuia));
                                    }
                                }
                          
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


                // Recorremos la tabla con las IT, si no estan las creamos
                Lbl_PB.Text = " Revisando las IT    . . . "; Lbl_PB.Refresh();
                Pb.Maximum = dtResumenxGuiaDespacho.Rows.Count; Pb.Minimum = 1; Pb.Value = 1;

                int i = 0; string lPathArchivo = ""; // @"\\192.168.1.191\Gerencia de Logistica\Guias de Despacho"; // dir_it;
                for (i = 0; i < dtResumenxGuiaDespacho.Rows.Count; i++)
                {
                    if (Pb.Value < Pb.Maximum)
                        Pb.Value = Pb.Value + 1;
                    else
                        if (Pb.Value > Pb.Minimum)
                        Pb.Value = Pb.Value - 1;

                    Pb.Refresh();
                        //lPathServer = Path.Combine(dir_it, dtResumenxGuiaDespacho.Rows[i]["It"].ToString().Substring(0, 3));
                        lPathServer = Path.Combine(@"S:\Guías Santiago\IT", ObtenerCodigoViaje(dtResumenxGuiaDespacho.Rows[i]["It"].ToString()));
                        //por cada IT, debe haber una portada y un detalle 
                        if (ExisteArchivo(dtResumenxGuiaDespacho.Rows[i]["It"].ToString(), lPathServer) == false)
                        CreaInforme(dtResumenxGuiaDespacho.Rows[i]["It"].ToString(), true, lPathServer);
                }

                //Obtiene la cantidad de ITs y verifica si el reporte cuenta con los archivos PDFs de las ITs
                view = new DataView(dtResumenxGuiaDespacho);
                if (view .Count >0)
                      totalITs = view.ToTable(true, COLUMNNAME_IT).Rows.Count;

                if (!dir_it.Equals(""))
                {
                    foreach (DataRow row in view.ToTable(true, COLUMNNAME_IT).Rows)
                    {
                        
                        lPathDestinoLocal = Path.Combine(lPathDestinoLocal);
                        //debemos revisar 2 archivos por IT
                        // lPathArchivo = Path.Combine(lPathDestinoLocal);

                        // if (ExisteArchivo(archivo.ToString(), lPathArchivo) == false)
                        //Primero P
                        archivo = row[COLUMNNAME_IT].ToString().Replace("/", "_")  + "P.PDF";
                  //      archivo = archivo.Replace("/", "_"); //ECT-1/1.PDF -> ECT-1_1.PDF
                        if (fs.FileExists(Path.Combine(lPathServer,  archivo)))
                        {
                            listArchivosIT.Add(archivo);
                            CopiarArchivo(Path.Combine(lPathServer, archivo), Path.Combine(lPathDestino, archivo));
                        }
                        else
                        {
                            archivoFaltantesIT.Append(" -> " + archivo + "\n");
                        }
                        //Primero D
                        archivo = row[COLUMNNAME_IT].ToString().Replace("/", "_") + "D.PDF";
                        //archivo = archivo.Replace("/", "_"); //ECT-1/1.PDF -> ECT-1_1.PDF
                        if (fs.FileExists(Path.Combine(lPathServer, archivo)))
                        {
                            listArchivosIT.Add(archivo);
                            CopiarArchivo(Path.Combine(lPathServer, archivo), Path.Combine(lPathDestino, archivo));
                        }
                        else
                        {
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

                    //lSiglaOBra = view[0]["IT"].ToString().Substring(0, 3);
                    //*****************************************************************ZOna de Cambio  **************************************//
                   
                    GeneraExcel_EP(accion, ep_obra, correlativo, ep_id, lPathDestino, lSiglaOBra);
                    GrabarArchivoLog(lPathDestino, sb  );


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


        private string ObtenerCodigoViaje(string lCodigo)
        {
            string lRes = "";
            string[] separators = { "-" }; 
            string[] lPartes = lCodigo.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (lPartes.Length == 2)
                lRes = lPartes[0].ToString();

            return lRes;
        }

        public string generarEP_Inicial_Ver(string ep_obra, string obra, Int32 ep_id, int accion, ref ProgressBar Pb, ref Label Lbl_PB, string carpeta, Int32 correlativo) //1-Vista preliminar, 2-Generar el reporte, 3-Envio al cliente
        {
            string error = "", valorKiloSuministro = "";
            StringBuilder sb = new StringBuilder(); StringBuilder validacion = new StringBuilder();
            StringBuilder archivoFaltantesGD = new StringBuilder(); StringBuilder archivoFaltantesIT = new StringBuilder();
            List<string> listArchivosGuiasDespacho = new List<string>(); List<string> listArchivosIT = new List<string>();
            string selectedPath = ""; WsOperacion.Operacion ws = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetTo = new WsOperacion.ListaDataSet(); WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet(); WsOperacion.ListaDataSet listaDataTMP = new WsOperacion.ListaDataSet();
            string validarFiles_GuiaDespacho = "0"; Result result = null; DataRow[] rows = null; string lEmpresa = "";
            int totalGuiasDespacho = 0, totalITs = 0, totalEtiquetas = 0, totalKilos = 0; DataView view = null;
            DataTable dtResumenxGuiaDespacho = null; WsOperacion.EP_Generado lEP = new WsOperacion.EP_Generado();
            string lPathDestino = ""; string lPathDestinoLocal = ""; string lPathServer = ""; string lSiglaOBra = "";
            string lPathGuia = ""; string lTmp = "";
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDatas = new DataSet();

            //Verifica si existe la informacion necesaria para generar el informe
            try
            {
                lEP.Id_EP = correlativo;
                Lbl_PB.Text = "Inicializando Variables . . . "; Lbl_PB.Refresh();
                selectedPath = @"C:\TMP\Estado de pago\DOC\";

                //Valor kilo suministro
                valorKiloSuministro = ws.ValorKiloSuministro_EP(ep_obra, DateTime.Now.ToString("dd-MM-yyyy"));
                if (valorKiloSuministro.Equals("") && Program.ENVIRONMENT.Equals("DEBUG"))
                    valorKiloSuministro = "100";
                sb.Append("Valor kilo suministro de la obra $: " + (valorKiloSuministro.Trim().Equals("") ? "(No definido)" : valorKiloSuministro) + "\n\n");

                lDatas = lPx.ObtenerDatos(string.Concat("Select * from obras where id=", ep_obra));
                if ((lDatas.Tables.Count > 0) && (lDatas.Tables[0].Rows.Count > 0))
                {
                    lSiglaOBra = (lDatas.Tables[0].Rows[0]["SiglaObra"].ToString());
                    lEmpresa = (lDatas.Tables[0].Rows[0]["Empresa"].ToString());

                }
               
            }
            catch (Exception exc)
            {
                error = exc.Message;

            }


            if (error.Equals(""))
            {
                if (validacion.Length == 0)
                {
          
         
                    WsOperacion.ListaDataSet lDatos = new WsOperacion.ListaDataSet();int lCont = 0;          string lRangoEx = "";
               int i = 0; DataTable lTbl = new DataTable();
                  DataTable ltblServicios = new DataTable();
                    string lHora = ""; int lTotalCD = 0; int lFE = 0; string lSubTitulo = "";
                    string lValorSum = "0"; string lValorPrep = "0"; string lStrTmp = "";
                    long totalKgSum = 0; long totalKgPrep = 0; int lIndex = -1; long totalKgRep = 0;
                    string lCodigo_INET = ""; string lEncargado = ""; string lNombreObra = ""; string lNombreCliente = "";

                    try
                    {
                        lPathDestino = selectedPath;

                        //Anexa el nombre de la carpeta a generar
                        if (!fs.DirectoryExists(lPathDestino))
                            fs.createDirectory(lPathDestino);

                        //Obtenemos el Objeto Obra para poder cargar la planilla excel
                        listaDataTMP = ws.ObtenerDatosObraParaEP(ep_obra);

                        listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id); //GD /IT /Etiquetas /Kilos

                        lHora = string.Concat(DateTime.Now.ToShortDateString().Replace("/", "_"), "_", DateTime.Now.ToShortTimeString().Replace(":", "_"));
                        //Copia la plantilla al directorio de destino 
                        //string plantillaEP = Path.Combine (Application.StartupPath ,  "PlantillaEP.xlsx");
                        string plantillaEP = Path.Combine(Application.StartupPath, "PLANTILL_EDP.xlsx"); bool lCeldaEncontrada = false;
                        string lNombreArchivo = string.Concat("EP_NRO_", correlativo, "_Estado_"); int lIndice = 9;
                        //string outputEP = Path.Combine(lPathDestino,string.Concat ("PlantillaEP_", lHora,".xls"));  //selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
                        string outputEP = Path.Combine(lPathDestino, string.Concat("EPNro_", correlativo, "_", lSiglaOBra, "_", lHora, ".xlsx"));

                        string outputEPPdf = Path.Combine(lPathDestino, "PlantillaEP.Pdf");
                        if (fs.FileExists(plantillaEP))
                            fs.copyFile(plantillaEP, outputEP);

                        if (Directory.Exists(lPathDestino) == false)
                            Directory.CreateDirectory(lPathDestino);


                        if (fs.error == null)
                        {
                            //Inserta los datos en el excel de salida
                            object paramMissing = Type.Missing;
                            //MessageBox.Show(" 1 ");
                            ExcelApp.Application excelApplication = new ExcelApp.Application();
                            //MessageBox.Show(" 2 ");
                            excelApplication.DisplayAlerts = false;
                            //MessageBox.Show(" 3 ");
                            excelApplication.Visible = true;
                            //MessageBox.Show(" 4 ");
                            ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Open(outputEP); //.Add(paramMissing);
                                                                                                         //MessageBox.Show(" 5 ");
                            ExcelApp.Worksheet excelSheet = null;
                            //MessageBox.Show(" 6 ");
                            DataTable ltblDatos = new DataTable(); DataTable ltblDatosPor_IT = new DataTable();
                            //MessageBox.Show(" 7 ");
                            ltblDatos = listaDataSetOp.DataSet.Tables[0].Copy();


                            if (excelWorkBook != null)
                            {
                                //LLenaremos primero el detalle de la pestaña de  Resumen GD, de la plantilla
                                //
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                                listaDataSetOp = wsOperacion.ListarEPExcel_ResumenObra(ep_obra);
                                if (listaDataSetOp.MensajeError.Equals(""))
                                {
                                    lValorSum = listaDataSetOp.DataSet.Tables[0].Rows[0]["VALOR_S"].ToString();
                                    lValorPrep = listaDataSetOp.DataSet.Tables[0].Rows[0]["VALOR_P"].ToString();
                                    foreach (DataRow row in listaDataSetOp.DataSet.Tables[0].Rows)
                                    {

                                        //Suministro
                                        excelSheet.Range["E9"].Value = lValorSum;
                                        //Preparacion
                                        excelSheet.Range["E10"].Value = lValorPrep;
                                        //Centro de Costo
                                       lNombreObra = row["Nombre"].ToString();
                                        excelSheet.Range["M1"].Value = lNombreObra; // row["Nombre"].ToString();
                                                                                    //Nombre Cliente
                                        lNombreCliente = row["CLIENTE"].ToString();
                                        excelSheet.Range["M2"].Value = lNombreCliente; // row["CLIENTE"].ToString();
                                                                                       //CODIGO DE DESPACHO: lCodigo_INET = ""; string lEncargado
                                        lCodigo_INET = row["Codigo_INET"].ToString();
                                        excelSheet.Range["M3"].Value = lCodigo_INET; // row["Codigo_INET"].ToString();
                                                                                     //ADM. CONTRATO:
                                        lEncargado = row["Encargado"].ToString();
                                        excelSheet.Range["M4"].Value = lEncargado; // row["Encargado"].ToString();
                                                                                   //Rut Cliente
                                                                                   //excelSheet.Range["D28"].Value = row["RUTCliente"].ToString();

                                        if ((listaDataTMP.DataSet.Tables.Count > 0) && (listaDataTMP.DataSet.Tables[0].Rows.Count > 0))
                                        {
                                            excelSheet.Range["C9"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
                                            excelSheet.Range["C10"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();

                                            listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id); //GD /IT /Etiquetas /Kilos
                                            if (listaDataSetOp.MensajeError.Equals(""))
                                                dtResumenxGuiaDespacho = listaDataSetOp.DataSet.Tables[0];

                                            totalKgSum = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "S"));
                                            totalKgPrep = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "P"));
                                            totalKgRep = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "R"));

                                            excelSheet.Range["I9"].Value = totalKgSum;  // si la IT es tipo TP 
                                            excelSheet.Range["I10"].Value = totalKgPrep; // RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho);  //totalKilos;  //Sumunistro
                                                                                         //excelSheet.Range["BE12"].Value = totalKilos;  // si la IT es tipo TP                                                                                                                      //No se incluye en la linea de cobro de preparación==> TotalKilos - Kilos de la Guia 
                                                                                         // excelSheet.Range["BE11"].Value = totalKilos*int.Parse (listaDataTMP.DataSet.Tables[0].Rows[0]["ValorUnitario"].ToString());

                                        }
                                    }
                                }
                                else
                                    error = listaDataSetOp.MensajeError.ToString();


                                Int64 kilos = 0, total = 0, correl = 0;

                                //LLenamos el Objeto con los totales del EP **********************************************+
   
                                String lRango = ""; string lTotalAcumAnt = "0";

                                //List<WsOperacion.EP_GeneradoDetalle> lList_DetEP = new List<WsOperacion.EP_GeneradoDetalle>() ;
                                WsOperacion.EP_GeneradoDetalle[] lList_DetEP = new WsOperacion.EP_GeneradoDetalle[3];

                                WsOperacion.EP_GeneradoDetalle lDetEP = new WsOperacion.EP_GeneradoDetalle();

                                //*****************************    EP ANTERIORES    ******************************************
                                DataView lVistaTmp = null; string lWheres = "";
                                WsOperacion.ListaDataSet lDts = new WsOperacion.ListaDataSet(); DataTable lTmps = new DataTable();
                                lDts = wsOperacion.ListarEPExcel_AcumuladoAnteriorxObra(ep_obra, ep_id);
                                lTmps = lDts.DataSet.Tables["Otros"].Copy();
                                lWheres = string.Concat("");

                                //*****************************   Otros   ******************************************
                                DataTable lTblOtros = ObtenerTablaTotalOtros(ep_id.ToString());
                                DataTable lTblOtrosResumen = ObtenerTablaOtrosResumen(ep_obra);
                                DataTable lTblOtrosTodos = ObtenerTablaOtrosHIstorico(ep_obra);

                                //******************************INICIO ****CAMBIOS 

                                lCeldaEncontrada = false; lIndice = 9;
                                while (lCeldaEncontrada == false)
                                {
                                    lRango = string.Concat("B", (lIndice).ToString());
                                    if ((excelSheet.Range[lRango].Value) == "ADICIONAL POR CONTRATO")
                                        lCeldaEncontrada = true;
                                    else
                                        lIndice = lIndice + 1;
                                }
                                //Debemos visualizar todos los adicionales por contrato
                                lRango = "   Select par3 servicio, par2 ValorUnitario, par5 CantidadTotal, Par4 Unidad from  to_parametros where ";
                                lRango = string.Concat(lRango, "  SubTabla = 'ObrasContrato' and Par1 =", ep_obra);
                                lDatas = lPx.ObtenerDatos(lRango);
                                if ((lDatas.Tables.Count > 0) && (lDatas.Tables[0].Rows.Count > 0))
                                {
                                    lTbl = lDatas.Tables[0].Copy();
                                    for (i = 0; i < lTbl.Rows.Count; i++)
                                    {

                                        excelSheet.Rows[(lIndice + 1 + i).ToString()].Insert();
                                        lRango = string.Concat("A", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("3.", (i + 1).ToString());
                                        lRango = string.Concat("B", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["servicio"].ToString();
                                        lRango = string.Concat("C", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["CantidadTotal"].ToString();
                                        lRango = string.Concat("D", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["Unidad"].ToString();
                                        lRango = string.Concat("E", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["ValorUnitario"].ToString();
                                        lRango = string.Concat("F", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=+E", (lIndice + 1 + i).ToString(), "*C", (lIndice + 1 + i).ToString());
                                        lRango = string.Concat("M", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice + 1 + i).ToString(), "-K", (lIndice + 1 + i).ToString());
                                        lRango = string.Concat("N", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice + 1 + i).ToString(), "-L", (lIndice + 1 + i).ToString());

                                    }

                                }

                                //***************************FIN CAMBIOS ****************************


                                if (lTblOtrosResumen.Rows.Count > 0)
                                {
                                   


                                    lIndex = -1;
                                    lWheres = " Tipo='C'  ";
                                    // Seleccionamos la hoja del excel 
                                    (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                                    lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                                    //lVistaTmp = new DataView(lTblOtros, lWheres, "", DataViewRowState.CurrentRows);
                                    for (i = 0; i < lVistaTmp.Count; i++)
                                    {
                                        //INICIO cAMNIOS
                                        //Buscamos el concepto del adicional
                                        lCeldaEncontrada = false; lIndice = 9;
                                        while ((lCeldaEncontrada == false)  || (lIndice >30))
                                        {
                                            lRango = string.Concat("B", (lIndice).ToString());
                                            if ((excelSheet.Range[lRango].Value) == lVistaTmp[i]["Descripcion"].ToString())
                                                lCeldaEncontrada = true;
                                            else
                                                lIndice = lIndice + 1;
                                        }
                                        if (lCeldaEncontrada == true)  // Actualizamos los datos de la fila
                                        {
                                            //if (i == 0)
                                            //    excelSheet.Rows[(i + 12).ToString()].Insert();

                                            //lRango = string.Concat("A", (i + 12).ToString()); excelSheet.Range[lRango].Value = string.Concat("3.", (i + 1).ToString());
                                            //lRango = string.Concat("B", (i + 12).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Descripcion"].ToString();
                                            //lRango = string.Concat("C", (i + 12).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["TotalContrato"].ToString();
                                            //lRango = string.Concat("D", (i + 12).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Unidad"].ToString();
                                            //lRango = string.Concat("E", (i + 12).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["PU"].ToString();
                                            //lRango = string.Concat("F", (i + 12).ToString());
                                            //excelSheet.Range[lRango].Value = string.Concat("=E", (i + 12).ToString(), "*C", (i + 12).ToString()); //"=G10-M10";

                                            //Acumulado Anterior
                                            lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "SAA", lTblOtrosTodos, ep_id.ToString());
                                            lRango = string.Concat("G", (lIndice).ToString());
                                            excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                            lRango = string.Concat("H", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndice).ToString(), "*G", (lIndice).ToString()); //"=G10-M10";

                                            //Estado de Pago Actual
                                            lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "SPA", lTblOtrosTodos, ep_id.ToString());
                                            lRango = string.Concat("I", (lIndice).ToString());
                                            excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                            lRango = string.Concat("J", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndice).ToString(), "*E", (lIndice).ToString()); //"=G10-M10";

                                            //Total Acumulado
                                            lRango = string.Concat("K", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=G", (lIndice).ToString(), "+I", (lIndice).ToString()); //"J10+H10";
                                            lRango = string.Concat("L", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=J", (lIndice).ToString(), "+H", (lIndice).ToString());
                                            //=I10 +K10";

                                            //Saldo Pendiente
                                            lRango = string.Concat("M", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice).ToString(), "-K", (lIndice).ToString()); //"=D10-L10";
                                            lRango = string.Concat("N", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "-L", (lIndice).ToString()); //"=G10-M10";



                                            //excelSheet.Rows["9"].Insert();                        
                                            lIndex = i + 1;
                                            //if (i < lVistaTmp.Count - 1)
                                            //    excelSheet.Rows[(i + 12 + 1).ToString()].Insert();

                                        }

                                        // fin cambios  lIndice


                                    }

                                    // Buscamos ADICIONAL PUNTUAL 
                                    lCeldaEncontrada = false; lIndice = 9;
                                    while ((lCeldaEncontrada == false) || (lIndice > 30))
                                    {
                                        lRango = string.Concat("B", (lIndice).ToString());
                                        if ((excelSheet.Range[lRango].Value) == "ADICIONAL PUNTUAL")
                                            lCeldaEncontrada = true;
                                        else
                                            lIndice = lIndice + 1;
                                    }
                                    if (lCeldaEncontrada == true)  // Actualizamos los datos de la fila
                                    {


                                        lIndex = lIndice + 1 ;
                                    lWheres = " Tipo='AP'   ";
                                    lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                                        //lIndex = lIndex + 10;
                                        //// PAra el caso que el EP no tengan Adicional
                                        //if (lIndex == 10)
                                        //    lIndex = 13;
                                        //**************************+
                                        //for (i = 0; i < lVistaTmp.Count; i++)
                                        //{
                                        //    if (i == 0)
                                        //        excelSheet.Rows[(i + lIndex).ToString()].Insert();

                                        //    lRango = string.Concat("A", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("4.", (i + 1).ToString());
                                        //    lRango = string.Concat("B", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Descripcion"].ToString();
                                        //    lRango = string.Concat("C", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["cant"].ToString();
                                        //    lRango = string.Concat("D", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Unidad_Otros"].ToString();
                                        //    lRango = string.Concat("E", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["PU"].ToString();
                                        //    lRango = string.Concat("F", (lIndex).ToString());
                                        //    excelSheet.Range[lRango].Value = string.Concat("=c", (lIndex).ToString(), "*E", (lIndex).ToString()); //"=G10-M10";

                                        //    //Acumulado Anterior
                                        //    lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "NAA", lTblOtrosTodos, ep_id.ToString());
                                        //    lRango = string.Concat("G", (lIndex).ToString());
                                        //    excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        //    lRango = string.Concat("H", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndex).ToString(), "*G", (lIndex).ToString()); //"=G10-M10";

                                        //    //Estado de Pago Actual
                                        //    lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "NPA", lTblOtrosTodos, ep_id.ToString());
                                        //    lRango = string.Concat("I", (lIndex).ToString());
                                        //    excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        //    lRango = string.Concat("J", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndex).ToString(), "*E", (lIndex).ToString()); //"=G10-M10";

                                        //    //Total Acumulado
                                        //    lRango = string.Concat("K", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndex).ToString(), "+G", (lIndex).ToString()); //"J10+H10";
                                        //    lRango = string.Concat("L", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=H", (lIndex).ToString(), "+J", (lIndex).ToString());

                                        //    //Saldo Pendiente
                                        //    lRango = string.Concat("M", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndex).ToString(), "-K", (lIndex).ToString()); //"=D10-L10";
                                        //    lRango = string.Concat("N", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndex).ToString(), "-L", (lIndex).ToString()); //"=G10-M10";

                                        //    lIndex = lIndex + 1;
                                        //    if (i < lVistaTmp.Count - 1)
                                        //    {
                                        //        excelSheet.Rows[(lIndex).ToString()].Insert();
                                        //    }
                                        //}
                                        //// Descuentos 
                                        //string lEsDevAnticipo = "N";
                                        //lWheres = " Tipo='D'   ";
                                        //lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                                        //lIndex = lIndex + 1;
                                        //for (i = 0; i < lVistaTmp.Count; i++)
                                        //{
                                        //    if (lVistaTmp[i]["Descripcion"].ToString().IndexOf("Dev. Anticipo") > -1)
                                        //        lEsDevAnticipo = "S";
                                        //    else
                                        //        lEsDevAnticipo = "N";

                                        //    if (i == 0)
                                        //    {
                                        //        if (lEsDevAnticipo == "N")
                                        //            excelSheet.Rows[(i + lIndex).ToString()].Insert();

                                        //    }

                                        //    if (lEsDevAnticipo == "N")
                                        //    {
                                        //        lRango = string.Concat("A", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("5.", (i + 1).ToString());
                                        //        lRango = string.Concat("B", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Descripcion"].ToString();
                                        //        lRango = string.Concat("C", (lIndex).ToString()); excelSheet.Range[lRango].Value = (long.Parse(lVistaTmp[i]["cant"].ToString()) * (-1));
                                        //        lRango = string.Concat("D", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Unidad_Otros"].ToString();
                                        //        lRango = string.Concat("E", (lIndex).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["PU"].ToString();
                                        //        lRango = string.Concat("F", (lIndex).ToString());
                                        //        excelSheet.Range[lRango].Value = string.Concat("=C", (lIndex).ToString(), "*E", (lIndex).ToString()); //"=G10-M10";

                                        //        //Acumulado Anterior
                                        //        lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "DAA", lTblOtrosTodos, ep_id.ToString());
                                        //        lRango = string.Concat("G", (lIndex).ToString());
                                        //        excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        //        // lRango = string.Concat("I", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=(F", (lIndex).ToString(), "*H", (lIndex).ToString(),")*(-1))"); //"=G10-M10";
                                        //        lRango = string.Concat("H", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndex).ToString(), "*G", (lIndex).ToString(), "*(-1)"); //"=G10-M10";

                                        //        //Estado de Pago Actual
                                        //        lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "DPA", lTblOtrosTodos, ep_id.ToString());
                                        //        lRango = string.Concat("I", (lIndex).ToString());
                                        //        excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        //        lRango = string.Concat("J", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndex).ToString(), "*E", (lIndex).ToString()); //"=G10-M10";

                                        //        //Total Acumulado
                                        //        lRango = string.Concat("K", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=G", (lIndex).ToString(), "+I", (lIndex).ToString()); //"J10+H10";
                                        //        lRango = string.Concat("L", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=J", (lIndex).ToString(), "+H", (lIndex).ToString());

                                        //        //Saldo Pendiente
                                        //        lRango = string.Concat("M", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndex).ToString(), "-K", (lIndex).ToString()); //"=D10-L10";
                                        //        lRango = string.Concat("N", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndex).ToString(), "-L", (lIndex).ToString()); //"=G10-M10";

                                        //        lIndex = lIndex + 1;
                                        //        if (i < lVistaTmp.Count - 1)
                                        //        {
                                        //            excelSheet.Rows[(lIndex).ToString()].Insert();
                                        //        }
                                        //    }

                                    }
                                    string lRangoDV = "";string lTotalCobroAnticipo = "0";
                                        //// Descuento de  Anticipo
                                        lTblOtros = ObtenerTablaDescuentoAnticipo(ep_obra);
                                    // lWheres = string.Concat(" select *   from to_parametros where subtabla='AnticiposObra' and  Par1='", ep_obra,"'");
                                    DataView lVistaTMPORAL = null; int lContTmp = 0; 
                                    lVistaTMPORAL = new DataView(lTblOtros, "", "", DataViewRowState.CurrentRows);
                                    if (lVistaTMPORAL.Count > 0)
                                    {
                                        while (lCeldaEncontrada == false)
                                        {
                                            lRango = string.Concat("A", (lIndex + lContTmp).ToString());
                                            if ((excelSheet.Range[lRango].Value) == "TOTAL NETO")
                                                lCeldaEncontrada = true;
                                            else
                                                lContTmp = lContTmp + 1;
                                        }
                                        lTotalCobroAnticipo= double.Parse(lVistaTMPORAL[0]["Par2"].ToString()).ToString ();
                                        if (lVistaTMPORAL[0]["Par3"].ToString().ToUpper().Equals("Porcentaje (%)".ToUpper()))
                                        {
                                            lRango = string.Concat("F", (lIndex + lContTmp - 2).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                                            lRango = string.Concat("E", (lIndex + lContTmp - 2).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par4"].ToString()) / 100;
                                        }
                                        else
                                           if (lVistaTMPORAL[0]["Par3"].ToString().ToUpper().Equals("Importe Fijo".ToUpper()))
                                        {
                                            lRango = string.Concat("I", (lIndex + lContTmp - 2).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par4"].ToString());
                                            lRangoDV = lRango;
                                            lRango = string.Concat("F", (lIndex + lContTmp - 2).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                                          
                                        }

                                        //Buscamos COBRO ANTICIPO
                                        lCeldaEncontrada = false;  lIndice = 9;
                                        while (lCeldaEncontrada == false)
                                        {
                                            lRango = string.Concat("B", (lIndice).ToString());
                                            if ((excelSheet.Range[lRango].Value) == "COBRO DE ANTICIPO")
                                                lCeldaEncontrada = true;
                                            else
                                                lIndice = lIndice + 1;
                                        }
                                        lRango = string.Concat("F", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = double.Parse(lTotalCobroAnticipo.ToString ());
                                        lRango = string.Concat("J", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                                        // Total Acumulado  =J17+H17
                                        lRango = string.Concat("L", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = string.Concat("=L", (lIndex).ToString(), "+H", (lIndex).ToString());
                                        // Saldo pendiente   =F13-L13
                                        lRango = string.Concat("N", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "+L", (lIndice).ToString());
                                        
                                    }
                                }
                               

                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                                excelWorkBook.Save();
                                //excelWorkBook.ExportAsFixedFormat(ExcelApp.XlFixedFormatType.xlTypePDF, outputEPPdf);
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


                            fs.shell(lPathDestino);

                        }
                        else
                            error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                        //validacion.Append(fs.error.Message);
                    }
                    catch (Exception exc)
                    {
                        error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                        MessageBox.Show("Error en UTIL.GeneraExcel_EP", exc.Message.ToString());
                        //validacion.Append(exc.Message);
                    }

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

        private void GrabarArchivoLog(string lPathDestino, StringBuilder lDatos)
        {
            string path = Path .Combine (lPathDestino ,"Log.txt")  ; // @"c:\temp\MyTest.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write (lDatos.ToString());
                }
            }
        }

        private string ObtenerNombreMes(int iMes)
        {
            string lNombreMes = "";
            switch (iMes)
            {
                case 1:
                    lNombreMes = "Enero";
                    break;
                case 2:
                    lNombreMes = "Febrero";
                    break;
                case 3:
                    lNombreMes = "Marzo";
                    break;
                case 4:
                    lNombreMes = "Abril";
                    break;
                case 5:
                    lNombreMes = "Mayo";
                    break;
                case 6:
                    lNombreMes = "Junio";
                    break;
                case 7:
                    lNombreMes = "Julio";
                    break;
                case 8:
                    lNombreMes = "Agosto";
                    break;
                case 9:
                    lNombreMes = "Septiembre";
                    break;
                case 10:
                    lNombreMes = "Octubre";
                    break;
                case 11:
                    lNombreMes = "Noviembre";
                    break;
                case 12:
                    lNombreMes = "Diciembre";
                    break;

            }
            return lNombreMes;
        }

        private string ObtenerTotalOtros(string iEP)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;


            string lRes = "0";
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", iEP, ", 0 ,' ',' ',0,'','','','',7,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lRes = (Convert.ToInt32(lDts.Tables[0].Rows[0][0].ToString())).ToString("#,##0");
                //(Convert.ToInt32(lMontoExc)).ToString("#,##0");
            }

            return lRes;
        }

        private DataTable  ObtenerTablaTotalOtros(string iEP)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;


            string lRes = "0";
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", iEP, ", 0 ,' ',' ',0,'','','','',14,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                //lRes = (Convert.ToInt32(lDts.Tables[0].Rows[0][0].ToString())).ToString("#,##0");
                //(Convert.ToInt32(lMontoExc)).ToString("#,##0");
                lTbl= lDts.Tables[0].Copy ();
            }

            return lTbl;
        }

        private DataTable ObtenerTablaOtrosResumen(string idObra)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;


            string lRes = "0";
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0,", idObra, " ,' ',' ',0,'','','','',16,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0].Copy();
            }

            return lTbl;
        }

        private DataTable ObtenerTablaDescuentoAnticipo(string idObra)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;


            string lRes = "0";
            lSql = string.Concat(" select *   from to_parametros where subtabla='AnticiposObra' and  Par1='", idObra, "'");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0].Copy();
            }

            return lTbl;
        }

        //private string ObtenerACumuladoAnterior_DevAnticipo(string idObra, int Id_EP_Actual)
        //{
        //    WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
        //    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
        //    string lSql = ""; DataTable lTbl = new DataTable(); 


        //    string lRes = "0";
        //    lSql = string.Concat(" select sum(importe) *(-1)  AnticipoPagado  from EP_OTROS where id_Obra =", idObra, "  and Obs ='DEVOLUCION ANTICIPO' ");
        //     lSql = string.Concat( lSql , "       and  id_EP<", Id_EP_Actual);
        //    lDts = lPx.ObtenerDatos(lSql);
        //    if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
        //    {
        //        lRes = lDts.Tables[0].Rows[0][0].ToString();
        //    }

        //    return lRes;
        //}

        //private string  ObtenerImporteTotal_Anticipo(string idObra)
        //{
        //    WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
        //    WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
        //    string lSql = ""; DataTable lTbl = new DataTable();


        //    string lRes = "0";
        //    lSql = string.Concat("select Par2 Importe , par3 tipo  from  to_parametros  where subtabla='AnticiposObra' and Par1='", idObra, "'"  );
        //    lDts = lPx.ObtenerDatos(lSql);
        //    if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
        //    {
        //        lRes = lDts.Tables[0].Rows[0][0].ToString();
        //    }

        //    return lRes;
        //}
        private string ObtenerTipo_Anticipo(string idObra)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable();


            string lRes = "0";
            lSql = string.Concat("select par3 tipo  from  to_parametros  where subtabla='AnticiposObra' and Par1='", idObra, "'");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lRes = lDts.Tables[0].Rows[0][0].ToString();
            }

            return lRes;
        }

        private DataTable ObtenerTablaOtrosHIstorico(string idObra)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable(); int i = 0;


            string lRes = "0";
            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0,", idObra, " ,' ',' ',0,'','','','',17,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbl = lDts.Tables[0].Copy();
            }

            return lTbl;
        }

        private string ObtenerAcumulado_DevolucionAnticipo(int IdEP_Actual,string  IdObra)
        {
            WsOperacion.Operacion lPx = new WsOperacion.Operacion();
            string lRes = "";

            lRes = lPx.ObtenerAcumuladoAnterior_DevAnt(IdEP_Actual, IdObra);

            return lRes;
        }

        private string Obtener_DevolucionAnticipo_EPActual(int IdEP_Actual, string IdObra)
        {
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            string lSql = ""; DataTable lTbl = new DataTable();


            string lRes = "0";
            lSql = string.Concat("select *   from Ep_otros  where id_obra =", IdObra, " and Id_EP =", IdEP_Actual, " ");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lRes = lDts.Tables[0].Rows[0][0].ToString();
            }

            return lRes;
        }


        private string ObtenerAcumulados(string iNombre, string iTipo, DataTable lTbl, string iId_EpActual)
        {
            int i = 0; int lTotal = 0;
            string lRes = "";DataView lVtm = null;

            try
            {
                switch (iTipo)
                {
                    case "N":
                    case "S":   // Con Contrato
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Contrato='", iTipo, "'"), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + int.Parse(lVtm[i]["cant"].ToString());
                            }
                        }

                        break;
                    case "SAA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Contrato='S' and id_ep<", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + int.Parse(lVtm[i]["cantidad"].ToString());
                            }
                        }
                        break;
                    case "NAA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Contrato='N' and id_ep<", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + int.Parse(lVtm[i]["cantidad"].ToString());
                            }
                        }
                        break;
                    case "DAA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Tipo='D' and id_ep<", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + (int.Parse(lVtm[i]["cantidad"].ToString())  );
                            }
                            lTotal = lTotal * (-1);
                        }
                        break;
                    case "SPA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Contrato='S' and id_ep=", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + int.Parse(lVtm[i]["cantidad"].ToString());
                            }
                        }
                        break;
                    case "NPA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Contrato='N' and id_ep=", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + int.Parse(lVtm[i]["cantidad"].ToString());
                            }
                        }
                        break;
                    case "DPA":
                        lVtm = new DataView(lTbl, string.Concat("Descripcion='", iNombre, "' and Tipo='D' and id_ep=", iId_EpActual), "", DataViewRowState.CurrentRows);
                        if (lVtm.Count > 0)
                        {
                            for (i = 0; i < lVtm.Count; i++)
                            {
                                lTotal = lTotal + (int.Parse(lVtm[i]["cantidad"].ToString())  );
                            }
                            lTotal = lTotal * (-1);
                        }
                        break;

                }
            } catch (Exception iEx)
            {


            }

            


            return lTotal.ToString ();
        }

        private DataTable ObtenerTablaPrecios(DataTable iTbl)
        {
            DataTable lTblRes = new DataTable(); int i = 0; string lTxAux = "";DataView lVista = null;
            int lKgsSum = 0;int lCont = 0;DataRow lfila = null; int lKgsPrep = 0;string lFecha = "";

            lTblRes.Columns.Add("Precio");
            lTblRes.Columns.Add("KgsSUM");
            lTblRes.Columns.Add("KgsPREP");
            lTblRes.Columns.Add("Fecha");

            for (i = 0; i < iTbl.Rows.Count ; i++)
            {
                if (lTxAux.IndexOf(iTbl.Rows[i]["Importe"].ToString()) == -1)
                {
                    lVista = new DataView(iTbl, string.Concat("Importe=", iTbl.Rows[i]["Importe"].ToString()), "", DataViewRowState.CurrentRows);
                    for (lCont = 0; lCont < lVista.Count; lCont++)
                    {
                        if (lVista[lCont]["TipoGuia_INET"].ToString().ToUpper().Equals("F"))
                        {
                            lKgsSum = lKgsSum + int.Parse(lVista[lCont]["Total_Kgs"].ToString());
                            lKgsPrep = lKgsSum + int.Parse(lVista[lCont]["Total_Kgs"].ToString());
                        }
                        else
                            lKgsSum = lKgsSum + int.Parse(lVista[lCont]["Total_Kgs"].ToString());

                    }
                    lfila = lTblRes.NewRow();
                    lfila["Precio"] = iTbl.Rows[i]["Importe"].ToString();
                    lfila["KgsSUM"] = lKgsSum;
                    lfila["KgsPREP"] = lKgsPrep;
                    lTblRes.Rows.Add(lfila);
                    lTxAux = string.Concat(lTxAux, "-", iTbl.Rows[i]["Importe"].ToString());
                    lKgsSum = 0;
                    lKgsPrep = 0;
                }
            }
            //Obtenemos las Fechas de los cambios de precio
            for (i = 0; i < lTblRes.Rows.Count; i++)
            {
                lVista = new DataView(iTbl, string.Concat("Importe=", lTblRes.Rows[i]["Precio"].ToString()), "FechaDespacho asc", DataViewRowState.CurrentRows);
                if (lVista.Count > 0)
                {
                    lFecha = string.Concat(lVista[0]["FechaDespacho"].ToString(), " - ", lVista[lVista.Count-1]["FechaDespacho"].ToString());
                }
                lTblRes.Rows[i]["Fecha"] = lFecha;
            }


                return lTblRes;
        }
        private void GeneraExcel_EP(int accion, string ep_obra, Int32 correlativo,int  ep_id , string lPathDest, string iSiglaObra)
        {
            //string selectedPath = ""; string obra = "";
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            WsOperacion.ListaDataSet listaDataTMP = new WsOperacion.ListaDataSet();
            WsOperacion.ListaDataSet lDatos = new WsOperacion.ListaDataSet();
            WsOperacion.EP_Generado lEP = new WsOperacion.EP_Generado();
            List<string> listArchivosGuiasDespacho = new List<string>();int lCont = 0;
            WsOperacion.Operacion ws = new WsOperacion.Operacion();string lRangoEx = "";
            DataTable dtResumenxGuiaDespacho = null;int i = 0;DataTable lTbl = new DataTable();
            string error = ""; string lPathDestino = ""; DataTable ltblServicios = new DataTable();
            string lHora = ""; int lTotalCD = 0; int lFE = 0; string lSubTitulo = "";
            string lValorSum = "0"; string lValorPrep = "0"; string lStrTmp = "";DataTable lTBlTmp = new DataTable();
            long totalKgSum = 0; long totalKgPrep = 0;  int lIndex = -1; long totalKgRep = 0;
            string lCodigo_INET = ""; string lEncargado = ""; string lNombreObra = ""; string lNombreCliente = "";

            try
            {
                lPathDestino = lPathDest;

                //Anexa el nombre de la carpeta a generar
                if (!fs.DirectoryExists(lPathDestino))
                    fs.createDirectory(lPathDestino);

                //Obtenemos el Objeto Obra para poder cargar la planilla excel
                listaDataTMP = ws.ObtenerDatosObraParaEP(ep_obra);

                listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachoxEp(ep_id); //GD /IT /Etiquetas /Kilos

                lHora =string .Concat (DateTime.Now.ToShortDateString().Replace("/", "_"),"_", DateTime.Now.ToShortTimeString().Replace (":","_"));
                //Copia la plantilla al directorio de destino 
                //string plantillaEP = Path.Combine (Application.StartupPath ,  "PlantillaEP.xlsx");
                string plantillaEP = Path.Combine(Application.StartupPath, "PLANTILL_EDP.xlsx");
                string lNombreArchivo = string.Concat("EP_NRO_", correlativo, "_Estado_");
                //string outputEP = Path.Combine(lPathDestino,string.Concat ("PlantillaEP_", lHora,".xls"));  //selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
                string outputEP = Path.Combine(lPathDestino, string.Concat("EPNro_", correlativo, "_", iSiglaObra,"_", lHora,".xlsx"));

                string outputEPPdf = Path.Combine(lPathDestino, "PlantillaEP.Pdf");
                if (fs.FileExists(plantillaEP))
                    fs.copyFile(plantillaEP, outputEP);

                if (Directory.Exists(lPathDestino) == false)
                    Directory.CreateDirectory(lPathDestino);


                if (fs.error == null)
                {
                    //Inserta los datos en el excel de salida
                    object paramMissing = Type.Missing;
                    //MessageBox.Show(" 1 ");
                    ExcelApp.Application excelApplication = new ExcelApp.Application();
                    //MessageBox.Show(" 2 ");
                    excelApplication.DisplayAlerts = false;
                    //MessageBox.Show(" 3 ");
                    excelApplication.Visible = true;
                    //MessageBox.Show(" 4 ");
                    ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Open(outputEP); //.Add(paramMissing);
                    //MessageBox.Show(" 5 ");
                    ExcelApp.Worksheet excelSheet = null;
                    //MessageBox.Show(" 6 ");
                    DataTable ltblDatos = new DataTable(); DataTable ltblDatosPor_IT = new DataTable();
                    //MessageBox.Show(" 7 ");
                    ltblDatos = listaDataSetOp.DataSet.Tables[0].Copy();
                   

                    if (excelWorkBook != null)
                    {
                        //LLenaremos primero el detalle de la pestaña de  Resumen GD, de la plantilla
                        //Hoja: Resumen
                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[2]).Select();
                     
                        lSubTitulo = string.Concat("EP N°", correlativo, "- ", ObtenerNombreMes(DateTime.Now.Month) , " ", DateTime.Now.Year.ToString());
                        excelSheet.Range["A5"].Value = lSubTitulo.ToString();
                   
                        lTotalCD = 0; lFE = 0;
                        lCont = 7;
                        for (i = 0; i < ltblDatos.Rows.Count; i++)
                        {
                            lDatos = wsOperacion.ObtenerResumenGD_ParaExcel(ltblDatos.Rows[i]["IT"].ToString());
                            if ((lDatos.MensajeError.Equals("")) &&(lDatos.DataSet.Tables.Count > 0))
                            {
                        
                                lTbl = lDatos.DataSet.Tables[0].Copy();
                               
                                foreach (DataRow row in lTbl.Rows)
                                {
                                    lRangoEx = string.Concat ("A",lCont .ToString ()); excelSheet.Range[lRangoEx].Value = row["Codigo"].ToString();
                                    lRangoEx = string.Concat("B", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["NroGuiaInet"].ToString();
                                    lRangoEx = string.Concat("C", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["FechaGuiaInet"].ToString();
                                    lRangoEx = string.Concat("D", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["TipoGuia_Inet"].ToString();

                                    switch (row["TipoGuia_Inet"].ToString())
                                    {
                                        // int lTotalCD = 0; int lFE = 0;
                                        case "Sumin C&D":
                                            lTotalCD = lTotalCD + Val(row["Kgs_Diam"].ToString());
                                            break;
                                        case "Sumin":
                                            lFE = lFE + Val(row["Kgs_Diam"].ToString());
                                            break;
                                    }
                               
                                    switch (row["Diametro"].ToString())
                                    {
                                        case "8":
                                            lRangoEx = string.Concat("E", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "10":
                                            lRangoEx = string.Concat("F", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "12":
                                            lRangoEx = string.Concat("G", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "16":
                                            lRangoEx = string.Concat("H", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;

                                        case "18":
                                            lRangoEx = string.Concat("I", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "22":
                                            lRangoEx = string.Concat("J", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "25":
                                            lRangoEx = string.Concat("K", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "28":
                                            lRangoEx = string.Concat("L", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "32":
                                            lRangoEx = string.Concat("M", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                        case "36":
                                            lRangoEx = string.Concat("N", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Kgs_Diam"].ToString();
                                            break;
                                    }
                                   
                                }
                                lCont++;
                            }
                        }
                      
                        // actualizamos los Totales.
                        excelSheet.Range["P2"].Value = lFE.ToString();
                        excelSheet.Range["P3"].Value = lTotalCD.ToString();
                        // Debemos sumar los totales por viaje
                        int lTotalFila = 0;
                        for (i = 0; i < ltblDatos.Rows.Count; i++)
                        {
                            lTotalFila = 0;
                            lRangoEx = string.Concat("E", (7 + i).ToString());

                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());

                            lRangoEx = string.Concat("F", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("G", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("H", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("I", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("J", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("K", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("L", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("M", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());
                            lRangoEx = string.Concat("N", (7 + i).ToString());
                            if (excelSheet.Range[lRangoEx].Value != null)
                                lTotalFila = lTotalFila + Val(excelSheet.Range[lRangoEx].Value.ToString());

                            lRangoEx = string.Concat("O", (7 + i).ToString());
                            //if (excelSheet.Range[lRangoEx].Value != null)
                              excelSheet.Range[lRangoEx].Value = lTotalFila.ToString(); //string.Concat("=SUMA(E", (7+i).ToString(), ":N", (7 + i).ToString(),")"); //"=G10-M10";
                        }

                       
                        //Sigue todo como estaba 
                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                        listaDataSetOp = wsOperacion.ListarEPExcel_ResumenObra(ep_obra);
                        if (listaDataSetOp.MensajeError.Equals(""))
                        {
                            lValorSum = listaDataSetOp.DataSet.Tables[0].Rows[0]["VALOR_S"].ToString();
                            lValorPrep = listaDataSetOp.DataSet.Tables[0].Rows[0]["VALOR_P"].ToString();
                            foreach (DataRow row in listaDataSetOp.DataSet.Tables[0].Rows)
                            {
                                
                                //Suministro
                                //excelSheet.Range["E9"].Value = lValorSum;
                                ////Preparacion
                                //excelSheet.Range["E10"].Value = lValorPrep;
                                //Centro de Costo
                                //excelSheet.Range["D30"].Value = row["CentroCOSTO"].ToString();
                                //Nombre Obra  lNombreObra = ""; string lNombreCliente
                                lNombreObra = row["Nombre"].ToString();
                                excelSheet.Range["M1"].Value = lNombreObra; // row["Nombre"].ToString();
                                //Nombre Cliente
                                lNombreCliente = row["CLIENTE"].ToString();
                                excelSheet.Range["M2"].Value = lNombreCliente; // row["CLIENTE"].ToString();
                                //CODIGO DE DESPACHO: lCodigo_INET = ""; string lEncargado
                                lCodigo_INET = row["Codigo_INET"].ToString();
                                excelSheet.Range["M3"].Value = lCodigo_INET; // row["Codigo_INET"].ToString();
                                //ADM. CONTRATO:
                                lEncargado = row["Encargado"].ToString();
                                excelSheet.Range["M4"].Value = lEncargado; // row["Encargado"].ToString();
                                //Rut Cliente
                                //excelSheet.Range["D28"].Value = row["RUTCliente"].ToString();

                                if ((listaDataTMP.DataSet.Tables.Count > 0) && (listaDataTMP.DataSet.Tables[0].Rows.Count > 0))
                                {
                                    // obtenemos el histocio de datos
                                    listaDataSetOp = wsOperacion.ListarEPResumenGuiaDespachox_IdPObra(ep_id); //GD /IT /Etiquetas /Kilos
                                    // Con los datos historicos creamos la tabla y los totales.
                                    lTBlTmp = ObtenerTablaPrecios(listaDataSetOp.DataSet.Tables[0].Copy());
                                    //dependiendo de la cantidad de precios que tenga  lValorSum
                                    if (lTBlTmp.Rows.Count == 1)
                                    {
                                        excelSheet.Range["C9"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
                                        excelSheet.Range["E9"].Value = lValorSum.ToString();
                                        excelSheet.Range["C10"].Value = listaDataTMP.DataSet.Tables[0].Rows[0]["CANTIDAD"].ToString();
                                        excelSheet.Range["E10"].Value = lValorPrep.ToString();

                                        if (listaDataSetOp.MensajeError.Equals(""))
                                            dtResumenxGuiaDespacho = listaDataSetOp.DataSet.Tables[0];

                                        totalKgSum = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "S"));
                                        totalKgPrep = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "P"));
                                        totalKgRep = long.Parse(RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho, "R"));
                                                                               
                                        excelSheet.Range["I9"].Value = totalKgSum;  // si la IT es tipo TP 
                                        excelSheet.Range["I10"].Value = totalKgPrep; // RevisaTiposDeGuiaINET(dtResumenxGuiaDespacho);  //totalKilos;  //Sumunistro                                                                                     
                                    }
                                    // tiene mas de un precio hay que iterar
                                    if (lTBlTmp.Rows.Count > 1)
                                    {
                                        lCont = 9;
                                        for (i = 0; i < lTBlTmp.Rows.Count; i++)
                                        {
                                            if (i == 0)
                                            {
                                                excelSheet.Range[string.Concat("C", lCont + i)].Value = lTBlTmp.Rows[i]["KgsSum"].ToString();
                                                excelSheet.Range[string.Concat("E", lCont + i)].Value = lTBlTmp.Rows[i]["Precio"].ToString();
                                            }
                                            else
                                            {
                                                excelSheet.Rows[(lCont + i).ToString()].Insert();
                                                excelSheet.Range[string.Concat("B", lCont + i)].Value = string.Concat ("Suministro: ",lTBlTmp.Rows[i]["Fecha"].ToString());
                                                excelSheet.Range[string.Concat("C", lCont + i)].Value = lTBlTmp.Rows[i]["KgsSum"].ToString();
                                                excelSheet.Range[string.Concat("E", lCont + i)].Value = lTBlTmp.Rows[i]["Precio"].ToString();
                                                excelSheet.Range[string.Concat("F", lCont + i)].Value = string.Concat("=+", string.Concat("E", lCont + i),"*", string.Concat("C", lCont + i));// E9*C9";
                                            }
                                          totalKgPrep =  totalKgPrep + long.Parse (lTBlTmp.Rows[i]["KgsPrep"].ToString());


                                        }
                                        //ahora Preparacion
                                        excelSheet.Range[string.Concat("C", lCont + i)].Value = totalKgPrep;

                                    }


                                }
                            }
                        }
                        else
                            error = listaDataSetOp.MensajeError.ToString();


                        Int64 kilos = 0, total = 0, correl = 0;
       
                        //LLenamos el Objeto con los totales del EP **********************************************+
                        int lTmp = 0;
                        String lRango = ""; string lTotalAcumAnt = "0";

                        //List<WsOperacion.EP_GeneradoDetalle> lList_DetEP = new List<WsOperacion.EP_GeneradoDetalle>() ;
                        WsOperacion.EP_GeneradoDetalle[] lList_DetEP = new WsOperacion.EP_GeneradoDetalle[3];

                        WsOperacion.EP_GeneradoDetalle lDetEP = new WsOperacion.EP_GeneradoDetalle();

                        //*****************************    EP ANTERIORES    ******************************************
                        DataView lVistaTmp = null; string lWheres = "";
                        WsOperacion.ListaDataSet lDts = new WsOperacion.ListaDataSet(); DataTable lTmps = new DataTable();
                        Int64 lTotal_F = 0; Int64 lTotal_R = 0; Int64 lTotal_FE = 0; Int64 lTotalSum = 0; Int64 lTotaPrep = 0;
                        Int64 lTotalRep = 0;
                        lDts = wsOperacion.ListarEPExcel_AcumuladoAnteriorxObra(ep_obra, ep_id);
                        lTmps = lDts.DataSet.Tables["Otros"].Copy();
                        lWheres = string.Concat("");
     


                        //*****************************   Otros   ******************************************
                        DataTable  lTblOtros = ObtenerTablaTotalOtros(ep_id.ToString ());
                        DataTable lTblOtrosResumen = ObtenerTablaOtrosResumen(ep_obra);
                        DataTable lTblOtrosTodos= ObtenerTablaOtrosHIstorico(ep_obra);

                        //******************************INICIO ****CAMBIOS 

                        bool lCeldaEncontrada = false; int  lIndice = 9; DataSet lDatas = new DataSet(); WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To();
                        while (lCeldaEncontrada == false)
                        {
                            lRango = string.Concat("B", (lIndice).ToString());
                            if ((excelSheet.Range[lRango].Value) == "ADICIONAL POR CONTRATO")
                                lCeldaEncontrada = true;
                            else
                                lIndice = lIndice + 1;
                        }
                        //Debemos visualizar todos los adicionales por contrato
                        lRango = "   Select par3 servicio, par2 CantidadTotal, par5  ValorUnitario , Par4 Unidad from  to_parametros where ";
                        lRango = string.Concat(lRango, "  SubTabla = 'ObrasContrato' and Par1 =", ep_obra);
                        lDatas = lPx.ObtenerDatos(lRango);
                        if ((lDatas.Tables.Count > 0) && (lDatas.Tables[0].Rows.Count > 0))
                        {
                            lTbl = lDatas.Tables[0].Copy();
                            for (i = 0; i < lTbl.Rows.Count; i++)
                            {

                                excelSheet.Rows[(lIndice + 1 + i).ToString()].Insert();
                                lRango = string.Concat("A", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("3.", (i + 1).ToString());
                                lRango = string.Concat("B", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["servicio"].ToString();
                                lRango = string.Concat("C", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["CantidadTotal"].ToString();
                                lRango = string.Concat("D", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["Unidad"].ToString();
                                lRango = string.Concat("E", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = lTbl.Rows[i]["ValorUnitario"].ToString();
                                lRango = string.Concat("F", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=+E", (lIndice + 1 + i).ToString(), "*C", (lIndice + 1 + i).ToString());
                                lRango = string.Concat("M", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice + 1 + i).ToString(), "-K", (lIndice + 1 + i).ToString());
                                lRango = string.Concat("N", (lIndice + 1 + i).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice + 1 + i).ToString(), "-L", (lIndice + 1 + i).ToString());
                            }
                        }

                        //***************************FIN CAMBIOS ****************************
                        if (lTblOtrosResumen.Rows.Count > 0)
                        {
                         
                            lIndex = -1;
                            lWheres = " Tipo='C'  ";
                            // Seleccionamos la hoja del excel 
                            (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                            lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                            //lVistaTmp = new DataView(lTblOtros, lWheres, "", DataViewRowState.CurrentRows);
                            for (i = 0; i < lVistaTmp.Count; i++)
                            {
                                //INICIO cAMNIOS
                                //Buscamos el concepto del adicional
                                lCeldaEncontrada = false; lIndice = 9;
                                while ((lCeldaEncontrada == false) || (lIndice > 30))
                                {
                                    lRango = string.Concat("B", (lIndice).ToString());
                                    if ((excelSheet.Range[lRango].Value) == lVistaTmp[i]["Descripcion"].ToString())
                                        lCeldaEncontrada = true;
                                    else
                                        lIndice = lIndice + 1;
                                }
                                if (lCeldaEncontrada == true)  // Actualizamos los datos de la fila
                                {
                                    //lIndice = lIndice + 1;

                                    //Acumulado Anterior
                                    lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "SAA", lTblOtrosTodos, ep_id.ToString());
                                    lRango = string.Concat("G", (lIndice).ToString());
                                    excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                    lRango = string.Concat("H", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndice).ToString(), "*G", (lIndice).ToString()); //"=G10-M10";

                                    //Estado de Pago Actual
                                    lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "SPA", lTblOtrosTodos, ep_id.ToString());
                                    lRango = string.Concat("I", (lIndice).ToString());
                                    excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                    lRango = string.Concat("J", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndice).ToString(), "*E", (lIndice).ToString()); //"=G10-M10";

                                    //Total Acumulado
                                    lRango = string.Concat("K", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=G", (lIndice).ToString(), "+I", (lIndice).ToString()); //"J10+H10";
                                    lRango = string.Concat("L", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=J", (lIndice).ToString(), "+H", (lIndice).ToString());
                                    //=I10 +K10";

                                    //Saldo Pendiente
                                    lRango = string.Concat("M", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice).ToString(), "-K", (lIndice).ToString()); //"=D10-L10";
                                    lRango = string.Concat("N", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "-L", (lIndice).ToString()); //"=G10-M10";
                                    //excelSheet.Rows["9"].Insert();                        
                                    lIndex = i + 1;
                                    //if (i < lVistaTmp.Count - 1)
                                    //    excelSheet.Rows[(i + 12 + 1).ToString()].Insert();
                                }
                            }
                            // Buscamos ADICIONAL PUNTUAL 
                            lCeldaEncontrada = false; lIndice = 9;
                                while ((lCeldaEncontrada == false) || (lIndice > 30))
                                {
                                    lRango = string.Concat("B", (lIndice).ToString());
                                    if ((excelSheet.Range[lRango].Value) == "ADICIONAL PUNTUAL")
                                        lCeldaEncontrada = true;
                                    else
                                        lIndice = lIndice + 1;
                                }
                            //if (lCeldaEncontrada == true)  // Actualizamos los datos de la fila
                            //{

                            lIndice = lIndice + 1;
                            lWheres = " Tipo='AP'   ";
                            lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                            //lIndex = lIndex + 10;
                            // PAra el caso que el EP no tengan Adicional
                            //if (lIndex == 10)
                            //    lIndex = 13;
                            //**************************+
                            for (i = 0; i < lVistaTmp.Count; i++)
                            {
                                if (i == 0)
                                    excelSheet.Rows[(i + lIndice).ToString()].Insert();

                                lRango = string.Concat("A", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("4.", (i + 1).ToString());
                                lRango = string.Concat("B", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Descripcion"].ToString();
                                lRango = string.Concat("C", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["cant"].ToString();
                                lRango = string.Concat("D", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Unidad_Otros"].ToString();
                                lRango = string.Concat("E", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["PU"].ToString();
                                lRango = string.Concat("F", (lIndice).ToString());
                                excelSheet.Range[lRango].Value = string.Concat("=+E", (lIndice).ToString(), "*C", (lIndice).ToString()); //"=G10-M10";

                                //Acumulado Anterior
                                lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "NAA", lTblOtrosTodos, ep_id.ToString());
                                lRango = string.Concat("G", (  lIndice).ToString());
                                excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                lRango = string.Concat("H", (  lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndice).ToString(), "*G", (lIndice).ToString()); //"=G10-M10";

                                //Estado de Pago Actual
                                lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "NPA", lTblOtrosTodos, ep_id.ToString());
                                lRango = string.Concat("I", (  lIndice).ToString());
                                excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                lRango = string.Concat("J", ( lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndice).ToString(), "*E", (lIndice).ToString()); //"=G10-M10";

                                //Total Acumulado
                                lRango = string.Concat("K", (  lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndice).ToString(), "+G", (lIndice).ToString()); //"J10+H10";
                                lRango = string.Concat("L", (  lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=H", (lIndice).ToString(), "+J", (lIndice).ToString());

                                //Saldo Pendiente
                                lRango = string.Concat("M", (  lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice).ToString(), "-K", (lIndice).ToString()); //"=D10-L10";
                                lRango = string.Concat("N", (  lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "-L", (lIndice).ToString()); //"=G10-M10";

                                lIndice = lIndice + 1;
                                if (i < lVistaTmp.Count - 1)
                                {
                                    excelSheet.Rows[(lIndice).ToString()].Insert();
                                }
                            }
                            // Descuentos 
                            // Buscamos ADICIONAL PUNTUAL 
                            lCeldaEncontrada = false; lIndice = 9;
                            while ((lCeldaEncontrada == false) || (lIndice > 30))
                            {
                                lRango = string.Concat("B", (lIndice).ToString());
                                if ((excelSheet.Range[lRango].Value) == "DESCUENTO")
                                    lCeldaEncontrada = true;
                                else
                                    lIndice = lIndice + 1;
                            }

                            string lEsDevAnticipo = "N";
                            lWheres = " Tipo='D'   ";
                            lVistaTmp = new DataView(lTblOtrosResumen, lWheres, "", DataViewRowState.CurrentRows);
                            lIndice = lIndice + 1;
                                for (i = 0; i < lVistaTmp.Count; i++)
                                {
                                    if (lVistaTmp[i]["Descripcion"].ToString().IndexOf("Dev. Anticipo") > -1)
                                        lEsDevAnticipo = "S";
                                    else
                                        lEsDevAnticipo = "N";

                                //if (i == 0)
                                //{
                                if (lEsDevAnticipo == "N")
                                    excelSheet.Rows[(  lIndice).ToString()].Insert();

                                //}

                                if (lEsDevAnticipo == "N")
                                    {
                                        lRango = string.Concat("A", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("5.", (i  ).ToString());
                                        lRango = string.Concat("B", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Descripcion"].ToString();
                                        lRango = string.Concat("C", (lIndice).ToString()); excelSheet.Range[lRango].Value = (long.Parse(lVistaTmp[i]["cant"].ToString()) * (-1));
                                        lRango = string.Concat("D", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["Unidad_Otros"].ToString();
                                        lRango = string.Concat("E", (lIndice).ToString()); excelSheet.Range[lRango].Value = lVistaTmp[i]["PU"].ToString();
                                        lRango = string.Concat("F", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice).ToString(), "*E", (lIndice).ToString()); //"=G10-M10";

                                        //Acumulado Anterior
                                        lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "DAA", lTblOtrosTodos, ep_id.ToString());
                                        lRango = string.Concat("G", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        // lRango = string.Concat("I", (lIndex).ToString()); excelSheet.Range[lRango].Value = string.Concat("=(F", (lIndex).ToString(), "*H", (lIndex).ToString(),")*(-1))"); //"=G10-M10";
                                        lRango = string.Concat("H", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=E", (lIndice).ToString(), "*G", (lIndice).ToString(), "*(-1)"); //"=G10-M10";

                                        //Estado de Pago Actual
                                        lTotalAcumAnt = ObtenerAcumulados(lVistaTmp[i]["Descripcion"].ToString(), "DPA", lTblOtrosTodos, ep_id.ToString());
                                        lRango = string.Concat("I", (lIndice).ToString());
                                        excelSheet.Range[lRango].Value = lTotalAcumAnt;
                                        lRango = string.Concat("J", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=I", (lIndice).ToString(), "*E", (lIndice).ToString()); //"=G10-M10";

                                        //Total Acumulado
                                        lRango = string.Concat("K", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=G", (lIndice).ToString(), "+I", (lIndice).ToString()); //"J10+H10";
                                        lRango = string.Concat("L", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=J", (lIndice).ToString(), "+H", (lIndice).ToString());

                                        //Saldo Pendiente
                                        lRango = string.Concat("M", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=C", (lIndice).ToString(), "-K", (lIndice).ToString()); //"=D10-L10";
                                        lRango = string.Concat("N", (lIndice).ToString()); excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "-L", (lIndice).ToString()); //"=G10-M10";

                                    lIndice = lIndice + 1;
                                        if (i < lVistaTmp.Count - 1)
                                        {
                                            excelSheet.Rows[(lIndice).ToString()].Insert();
                                        }
                                    }
                            
                            }
                            string lRangoDV = ""; string  lTotalCobroAnticipo = "";
                            // Descuento de  Anticipo
                            lTblOtros = ObtenerTablaDescuentoAnticipo(ep_obra);
                           // lWheres = string.Concat(" select *   from to_parametros where subtabla='AnticiposObra' and  Par1='", ep_obra,"'");
                                DataView lVistaTMPORAL = null;  lIndice = 9; lCeldaEncontrada = false;
                             lVistaTMPORAL = new DataView(lTblOtros, "", "", DataViewRowState.CurrentRows);
                                if (lVistaTMPORAL.Count > 0)
                                {
                                lTotalCobroAnticipo = double.Parse(lVistaTMPORAL[0]["Par2"].ToString()).ToString();
                                while (lCeldaEncontrada==false)
                                    {
                                        lRango = string.Concat("B", (lIndice).ToString());
                                    if ((excelSheet.Range[lRango].Value) == "COBRO DE ANTICIPO")
                                        lCeldaEncontrada = true;
                                    else
                                        lIndice = lIndice + 1;
                                    }
                                    if (lVistaTMPORAL[0]["Par3"].ToString().ToUpper().Equals("Porcentaje (%)".ToUpper()))
                                    {
                                        lRango = string.Concat("F", (lIndice +5).ToString());
                                        excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                                        lRango = string.Concat("E", (lIndice +5).ToString());
                                        excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par4"].ToString()) / 100;
                                    }
                                    else
                                       if (lVistaTMPORAL[0]["Par3"].ToString().ToUpper().Equals("Importe Fijo".ToUpper()))
                                        {
                                            lRango = string.Concat("I", (lIndice +5).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par4"].ToString()) ;
                                            lRangoDV = lRango;
                                            lRango = string.Concat("F", (lIndice +5).ToString());
                                            excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                                        }
                                // Si en % se debe  poner en la celda 
                               // lRango = string.Concat("F", (lIndex + lContTmp-1).ToString());
                                // Si es por Importe  se debe Poner en la celda                                   
                                }
                            // Debemos llenar el campo  devolucion anticipo Acumula anterior.
                            //ObtenerAcumulado_DevolucionAnticipo
                            string lDevAnt = ObtenerAcumulado_DevolucionAnticipo(correlativo, ep_obra);
                            lRango = string.Concat("G", (lIndice +5).ToString());
                            excelSheet.Range[lRango].Value = lDevAnt;
                            // debemos Ver si  se realizo un  registro en tabla Otros de descuento de anticipo
                            // en caso de NO ==> se debe eliminar el EP I21
                            // Obtenemos el registro de devolucion anticipo del EP actual
                            //Obtener_DevolucionAnticipo_EPActual
                            string lDevAntActual = Obtener_DevolucionAnticipo_EPActual(ep_id, ep_obra);

                            if (lDevAntActual.ToString().Equals("0"))
                            {
                                lRango = string.Concat("I", (lIndice +5).ToString());
                                excelSheet.Range[lRango].Value = "0";
                            }

                            //string lvarTmp = ObtenerACumuladoAnterior_DevAnticipo(ep_obra, ep_id);
                            //// Devolucion Anticipo EP Anterior
                            //lRango = string.Concat("G", (lIndex + lContTmp - 2).ToString());
                            //excelSheet.Range[lRango].Value = lvarTmp.ToString() ;
                            //// Obtenemos el Tipo de Anticipo
                            //lvarTmp = ObtenerTipo_Anticipo(ep_obra);
                            //if (lvarTmp.Equals ("AnticiposObra") )
                            //    lRango = string.Concat("F", (lIndex + lContTmp - 2).ToString());
                            //else
                            //    lRango = string.Concat("E", (lIndex + lContTmp - 2).ToString());

                            //// Obtenemos el  importe del  Anticipo
                            //lvarTmp = ObtenerImporteTotal_Anticipo   (ep_obra);
                            //excelSheet.Range[lRango].Value = lvarTmp.ToString();

                            // visualizamos el Descuento d anticipo
                            lCeldaEncontrada = false;  lIndice = 9;
                            while (lCeldaEncontrada == false)
                            {
                                lRango = string.Concat("B", (lIndice).ToString());
                                if ((excelSheet.Range[lRango].Value) == "COBRO DE ANTICIPO")
                                    lCeldaEncontrada = true;
                                else
                                    lIndice = lIndice + 1;
                            }
                            //lRango = string.Concat("F", (lIndice).ToString());
                            //excelSheet.Range[lRango].Value = double.Parse(lTotalCobroAnticipo.ToString());
                            //lRango = string.Concat("J", (lIndice).ToString());
                            //excelSheet.Range[lRango].Value = double.Parse(lVistaTMPORAL[0]["Par2"].ToString());
                            //// Total Acumulado  =J17+H17
                            //lRango = string.Concat("L", (lIndice).ToString());
                            //excelSheet.Range[lRango].Value = string.Concat("=L", (lIndex).ToString(), "+H", (lIndex).ToString());
                            //// Saldo pendiente   =F13-L13
                            //lRango = string.Concat("N", (lIndice).ToString());
                            //excelSheet.Range[lRango].Value = string.Concat("=F", (lIndice).ToString(), "+L", (lIndice).ToString());



                        }

                        //*****************************  Repocisiones  ******************************************
                        lTmps = lDts.DataSet.Tables["Guias"].Copy();
                        total = 0;
                        for (i = 0; i < lTmps.Rows .Count; i++)
                        {
                            if (lTmps.Rows [i]["TipoGuia_INET"].ToString().ToUpper ().Equals ("R"))
                            {
                                lTotal_R= lTotal_R+ Int64.Parse(lTmps.Rows[i]["KILOS"].ToString());
                            }
                            if (lTmps.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("F"))
                            {
                                lTotal_F = lTotal_F + Int64.Parse(lTmps.Rows[i]["KILOS"].ToString());
                            }
                            if ((lTmps.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("FE")) || (lTmps.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("FC")) || (lTmps.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("LC"))) 
                            {
                                lTotal_FE = lTotal_FE + Int64.Parse(lTmps.Rows[i]["KILOS"].ToString());
                            }
                            //total = total+Int64.Parse(lVistaTmps[i]["KILOS"].ToString());
                        }
                        lTotalSum = lTotal_F + lTotal_FE;
                        lTotaPrep = lTotal_F  ;
                        lTotalRep = lTotal_R;

                        // Suministro y preparacion EP Anterior
                        excelSheet.Range["G9"].Value = lTotalSum;
                        excelSheet.Range["G10"].Value = lTotaPrep;
                                            
                        if (lTotalRep > 0)
                        {
                            if (lIndex == -1)
                                lIndex = 12;

                            lRango = string.Concat("G", (lIndex).ToString());
                            excelSheet.Range[lRango].Value = lTotalRep;
                            //lRango = string.Concat("D", (lIndex).ToString());
                            //excelSheet.Range[lRango].Value = string.Concat("=E", (lIndex).ToString(), "+G", (lIndex).ToString());
                        }

                        if (totalKgRep > 0)
                        {
                            if (lIndex == -1)
                                lIndex = 15;

                            lRango = string.Concat("J", (lIndex).ToString());
                            excelSheet.Range[lRango].Value = totalKgRep;
                            //lRango = string.Concat("D", (lIndex).ToString());
                            //excelSheet.Range[lRango].Value = string.Concat("=E", (lIndex).ToString(), "+G", (lIndex).ToString());
                        }



                        // debemos  llenar la Informacion de la Nueva Pestaña
                        //•Se agrega hoja llamada “BD”, la cual debe contener las siguientes columnas desde sistema, acotadas para el EP
                        //actual: Plano, Nivel, Elemento, Figura, Referencia, Marca, id pieza, Cant, Diam, 
                        //Largo Total(y todos los largos parciales), Kg, Nro Etiqueta, IT, Viaje, Nro Guía, Fecha Guía.
                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[3]).Select();
                        lCont = 7;
                        for (i = 0; i < ltblDatos.Rows.Count; i++)
                        {
                            lDatos = wsOperacion.ObtenerBD_ParaExcel(ltblDatos.Rows[i]["IT"].ToString());
                            if ((lDatos.MensajeError.Equals("")) && (lDatos.DataSet.Tables.Count > 0))
                            {

                                lTbl = lDatos.DataSet.Tables[0].Copy();
                               
                                foreach (DataRow row in lTbl.Rows)
                                {
                                    lRangoEx = string.Concat("A", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["IdPieza"].ToString();
                                    lRangoEx = string.Concat("B", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Plano"].ToString();
                                    lRangoEx = string.Concat("C", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Nivel"].ToString();
                                    lRangoEx = string.Concat("D", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Elemento"].ToString();
                                    lRangoEx = string.Concat("E", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Figura"].ToString();
                                    lRangoEx = string.Concat("F", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Referencia"].ToString();
                                    lRangoEx = string.Concat("G", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Marca"].ToString();
                                    lRangoEx = string.Concat("H", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["IdForma"].ToString();
                                    lRangoEx = string.Concat("I", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["NroPiezas"].ToString();

                                    lRangoEx = string.Concat("J", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Diametro"].ToString();
                                    lRangoEx = string.Concat("K", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Largo"].ToString();
                                    lRangoEx = string.Concat("L", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["A"].ToString();
                                    lRangoEx = string.Concat("M", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["B"].ToString();
                                    lRangoEx = string.Concat("N", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["C"].ToString();
                                    lRangoEx = string.Concat("O", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["D"].ToString();
                                    lRangoEx = string.Concat("P", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["E"].ToString();
                                    lRangoEx = string.Concat("Q", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["F"].ToString();
                                    lRangoEx = string.Concat("R", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["G"].ToString();
                                    lRangoEx = string.Concat("S", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["H"].ToString();
                                    lRangoEx = string.Concat("T", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["I"].ToString();
                                    lRangoEx = string.Concat("U", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["J"].ToString();
                                    lRangoEx = string.Concat("V", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["K"].ToString();
                                    lRangoEx = string.Concat("W", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["L"].ToString();
                                    lRangoEx = string.Concat("X", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["N"].ToString();

                                    lRangoEx = string.Concat("Y", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["TOTAL_kGS"].ToString();
                                    lRangoEx = string.Concat("Z", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Etiqueta"].ToString();
                                    lRangoEx = string.Concat("AA", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["IT"].ToString();
                                    lRangoEx = string.Concat("AB", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["viaje"].ToString(); 
                                    lRangoEx = string.Concat("AC", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["NroGuiaINET"].ToString();
                                    lRangoEx = string.Concat("AD", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["Fecha_GD_INET"].ToString();
                                    lRangoEx = string.Concat("AE", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["TipoGuia_INET"].ToString();
                                    lRangoEx = string.Concat("AF", lCont.ToString()); excelSheet.Range[lRangoEx].Value = row["importeKgs"].ToString();
                                    lCont++;
                                }
                             
                            }
                        }



                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                        excelWorkBook.Save();
                        //excelWorkBook.ExportAsFixedFormat(ExcelApp.XlFixedFormatType.xlTypePDF, outputEPPdf);
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


                    fs.shell(lPathDestino);

                }
                else
                    error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                    //validacion.Append(fs.error.Message);
            }
            catch (Exception exc)
            {
                error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                MessageBox.Show("Error en UTIL.GeneraExcel_EP", exc.Message.ToString());
                //validacion.Append(exc.Message);
            }




        }

        public  void GeneraExcel_Resumen_Eps()
        {
            //string selectedPath = ""; string obra = "";
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.Operacion ws = new WsOperacion.Operacion(); string lRangoEx = "";
             int i = 0; DataTable lTbl = new DataTable();
            string error = ""; string lPathDestino = ""; DataTable ltblServicios = new DataTable();
            string lHora = "";   int lIndex = -1;

  
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataView lVista = null;DataSet lDts = new DataSet();
            int lDias = 0;
            try
            {
                lDts = wsOperacion.ObtenerInforme_EP();      //lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    //Anexa el nombre de la carpeta a generar
                    //if (!fs.DirectoryExists(lPathDestino))
                    //    fs.createDirectory(lPathDestino);

                    lTbl = lDts.Tables[0].Copy();

                    lHora = string.Concat(DateTime.Now.ToShortDateString().Replace("/", "_"), "_", DateTime.Now.ToShortTimeString().Replace(":", "_"));
                    //Copia la plantilla al directorio de destino
                    string plantillaEP = Path.Combine(Application.StartupPath, "PlantillaEP_Generados.xlsx");
                    //string lNombreArchivo = string.Concat("EP_NRO_", correlativo, "_Estado_");
                    lPathDestino = @"C:\TMP\Estado de pago\";
                    string outputEP = Path.Combine(lPathDestino, string.Concat("Listado_EP_", lHora, ".xlsx"));  //selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
                                                                                                                 //string outputEP = Path.Combine(lPathDestino, string.Concat("EPNro_", correlativo, "_", iSiglaObra, "_", lHora, ".xls"));
                                                                                                                 //string outputEPPdf = Path.Combine(lPathDestino, "PlantillaEP.Pdf");
                    if (fs.FileExists(plantillaEP))
                        fs.copyFile(plantillaEP, outputEP);


                    if (fs.error == null)
                    {
                        //Inserta los datos en el excel de salida
                        object paramMissing = Type.Missing;
                        ExcelApp.Application excelApplication = new ExcelApp.Application();
                        excelApplication.DisplayAlerts = false;
                        excelApplication.Visible = true;
                        ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Open(outputEP); //.Add(paramMissing);
                        ExcelApp.Worksheet excelSheet = null;
                        DataTable ltblDatos = new DataTable(); DataTable ltblDatosPor_IT = new DataTable();
                        //ltblDatos = listaDataSetOp.DataSet.Tables[0].Copy();


                        if (excelWorkBook != null)
                        {
                            //LLenaremos primero el detalle de la pestaña de  Resumen GD, de la plantilla
                            //Hoja: EP_Creado
                            lVista = new DataView(lTbl, "Ep_Estado='P05'", "", DataViewRowState.CurrentRows);
                            if (lVista.Count >0)
                            {
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();
                                for (i=0; i<lVista .Count; i++)
                                {
                                    lIndex = 3 + i;
                                    lRangoEx = string.Concat("B", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Empresa"].ToString();
                                    lRangoEx = string.Concat("C", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Año"].ToString();
                                    lRangoEx = string.Concat("D", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["mes"].ToString();
                                    lRangoEx = string.Concat("E", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["ep_fecha_creacion"].ToString();
                                    lRangoEx = string.Concat("F", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Cliente"].ToString();
                                    lRangoEx = string.Concat("G", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Nombre"].ToString();
                                    lRangoEx = string.Concat("H", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Correlativo"].ToString();
                                    lRangoEx = string.Concat("I", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Estado"].ToString();
                                    lRangoEx = string.Concat("J", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["MontoTotal"].ToString();
                                    lRangoEx = string.Concat("K", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Usuario"].ToString();
                                    lRangoEx = string.Concat("L", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Dias_del_envio"].ToString();
                                }
                            }

                            lVista = new DataView(lTbl, "Ep_Estado='P10'", "", DataViewRowState.CurrentRows);
                            if (lVista.Count > 0)
                            {
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[2]).Select();
                                for (i = 0; i < lVista.Count; i++)
                                {
                                    lIndex = 3 + i;
                                    lRangoEx = string.Concat("B", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Empresa"].ToString();
                                    lRangoEx = string.Concat("C", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Año"].ToString();
                                    lRangoEx = string.Concat("D", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["mes"].ToString();
                                    lRangoEx = string.Concat("E", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["ep_fecha_creacion"].ToString();
                                    lRangoEx = string.Concat("F", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Cliente"].ToString();
                                    lRangoEx = string.Concat("G", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Nombre"].ToString();
                                    lRangoEx = string.Concat("H", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Correlativo"].ToString();
                                    lRangoEx = string.Concat("I", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Estado"].ToString();
                                    lRangoEx = string.Concat("J", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["MontoTotal"].ToString();
                                    lRangoEx = string.Concat("K", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Usuario"].ToString();
                                    lRangoEx = string.Concat("L", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Dias_del_envio"].ToString();
                                }
                            }

                            lVista = new DataView(lTbl, "Ep_Estado='P15'", "", DataViewRowState.CurrentRows);
                            if (lVista.Count > 0)
                            {
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[3]).Select();
                                for (i = 0; i < lVista.Count; i++)
                                {
                                    lIndex = 3 + i;
                                    lRangoEx = string.Concat("B", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Empresa"].ToString();
                                    lRangoEx = string.Concat("C", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Año"].ToString();
                                    lRangoEx = string.Concat("D", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["mes"].ToString();
                                    lRangoEx = string.Concat("E", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["ep_fecha_creacion"].ToString();
                                    lRangoEx = string.Concat("F", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Cliente"].ToString();
                                    lRangoEx = string.Concat("G", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Nombre"].ToString();
                                    lRangoEx = string.Concat("H", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Correlativo"].ToString();
                                    lRangoEx = string.Concat("I", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Estado"].ToString();
                                    lRangoEx = string.Concat("J", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["MontoTotal"].ToString();
                                    lRangoEx = string.Concat("K", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Usuario"].ToString();
                                    lRangoEx = string.Concat("L", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Dias_del_envio"].ToString();
                                }
                            }

                            lVista = new DataView(lTbl, "Ep_Estado='P45'", "", DataViewRowState.CurrentRows);
                            if (lVista.Count > 0)
                            {
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[4]).Select();
                                for (i = 0; i < lVista.Count; i++)
                                {
                                    lIndex = 3 + i;
                                    lRangoEx = string.Concat("B", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Empresa"].ToString();
                                    lRangoEx = string.Concat("C", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Año"].ToString();
                                    lRangoEx = string.Concat("D", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["mes"].ToString();
                                    lRangoEx = string.Concat("E", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["ep_fecha_creacion"].ToString();
                                    lRangoEx = string.Concat("F", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Cliente"].ToString();
                                    lRangoEx = string.Concat("G", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Nombre"].ToString();
                                    lRangoEx = string.Concat("H", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Correlativo"].ToString();
                                    lRangoEx = string.Concat("I", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Estado"].ToString();
                                    lRangoEx = string.Concat("J", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["MontoTotal"].ToString();
                                    lRangoEx = string.Concat("K", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Usuario"].ToString();
                                    lRangoEx = string.Concat("L", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Dias_del_envio"].ToString();
                                }
                            }

                            lVista = new DataView(lTbl, "Ep_Estado='P60'", "", DataViewRowState.CurrentRows);
                            if (lVista.Count > 0)
                            {
                                (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[5]).Select();
                                for (i = 0; i < lVista.Count; i++)
                                {
                                    lIndex = 3 + i;
                                    lRangoEx = string.Concat("B", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Empresa"].ToString();
                                    lRangoEx = string.Concat("C", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Año"].ToString();
                                    lRangoEx = string.Concat("D", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["mes"].ToString();
                                    lRangoEx = string.Concat("E", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["ep_fecha_creacion"].ToString();
                                    lRangoEx = string.Concat("F", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Cliente"].ToString();
                                    lRangoEx = string.Concat("G", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Nombre"].ToString();
                                    lRangoEx = string.Concat("H", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Correlativo"].ToString();
                                    lRangoEx = string.Concat("I", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Estado"].ToString();
                                    lRangoEx = string.Concat("J", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["MontoTotal"].ToString();
                                    lRangoEx = string.Concat("K", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["EP_Usuario"].ToString();
                                    lRangoEx = string.Concat("L", lIndex.ToString()); excelSheet.Range[lRangoEx].Value = lVista[i]["Dias_del_envio"].ToString();
                                }
                            }



                            excelWorkBook.Save();
                            //excelWorkBook.ExportAsFixedFormat(ExcelApp.XlFixedFormatType.xlTypePDF, outputEPPdf);
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


                        fs.shell(lPathDestino);

                    }
                    else
                        error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                    //validacion.Append(fs.error.Message);
                }
            }
            catch (Exception exc)
            {
                error = string.Concat(error, Environment.NewLine, fs.error.Message.ToString());
                //validacion.Append(exc.Message);
            }




        }



        private string ObtenerPrecioUnitarioPorServicio(DataTable iTbl, string iServicio)
        {
            string lRes = "";
            DataView lVista = new DataView(iTbl, string.Concat("Servicio='", iServicio, "'"), "", DataViewRowState.CurrentRows);
            if (lVista.Count > 0)
            {
                lRes = lVista[0]["ImporteServicio"].ToString();
            }
            return lRes;
        }

        private string RevisaTiposDeGuiaINET(DataTable iTbl , string iTipo )
        {
             int i = 0; int lTotal = 0;int lTotalTP = 0;
            for (i = 0; i < iTbl.Rows.Count; i++)
            {
                if (iTipo == "S")  // Suministro = F + FE 
                {
                    if ((iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("FE"))  || (iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("F")) || (iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("FC")) || (iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("LC")))
                    {
                        lTotalTP = lTotalTP + int.Parse(iTbl.Rows[i]["Total_Kgs"].ToString());
                    }
               }
                if (iTipo == "P")  //Preparacion =F 
                {
                    if ((iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("F")))
                    {
                        lTotalTP = lTotalTP + int.Parse(iTbl.Rows[i]["Total_Kgs"].ToString());
                    }                  
                }
                if (iTipo == "R")  // Repocisiones = R
                {
                    if ((iTbl.Rows[i]["TipoGuia_INET"].ToString().ToUpper().Equals("R")))
                    {
                        lTotalTP = lTotalTP + int.Parse(iTbl.Rows[i]["Total_Kgs"].ToString());
                    }
                }
            }

            // return (lTotal- lTotalTP).ToString ();
            return lTotalTP.ToString();
        }


        private Boolean ExisteArchivo(string lViaje, string lPathBase)
        {
            Boolean lRes = false; string lPathPortada = ""; string lPathDetalle = ""; int lCont = 0;
            string[] separators = { "-" };string lPathFinal = "";
            string[] lPartes = lViaje.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (lPartes.Length > 1)
            {
                lPathFinal = string.Concat(lPathBase, "\\");//, lPartes[0], "\\");
            }

                lPathPortada = string.Concat(lPathFinal, lViaje.Replace("/", "_"), "P.pdf");
            lPathDetalle = string.Concat(lPathFinal, lViaje.Replace("/", "_"), "D.pdf");


            if (File.Exists(lPathPortada) == true)
                lCont++;


            if (File.Exists(lPathDetalle) == true)
                lCont++;

            if (lCont < 2) //No estan los 2 archivos por viaje
                lRes = false;
            else
                lRes = true;


            return lRes;

        }

        public void CreaInforme(string iViaje, Boolean iEliminaArchivo, string iPathDestino)
        {
            Informes.Informes lInf = new Informes.Informes();
            lInf.ImprimirInforme(iViaje, iEliminaArchivo, iPathDestino);
        }

        public void CreaInforme_Autom(string iViaje, Boolean iEliminaArchivo, string iPathDestino)
        {
            Informes.Informes lInf = new Informes.Informes();
            lInf.ImprimirInforme_Automatico(iViaje, iEliminaArchivo, iPathDestino);
        }

        //

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

        public int Val(string iDato)
        {
            int lRes = 0;
            try
            {
                if ((iDato.IndexOf(".") > -1))  //|| (iDato.IndexOf(",") > -1))
                {
                    iDato = iDato.Replace(".", "");
                }
                lRes= int.Parse (iDato);

            }
            catch (Exception exc)
            {
                lRes = 0;
            }
            return lRes;

        }

        public Boolean EsNumeroDesdeINET(string iDato)
        {
            Boolean lRes = true;
              string lTmp = "";
            Char Delimitador = ',';
            try
            {
                String[] lPartes = iDato.Split(Delimitador);
                if (lPartes.Length > 1)
                {
                    lTmp = lPartes[0].ToString();
                    Convert.ToInt64(lTmp);
                }

            }
            catch (Exception exc)
            {
                lRes = false;
            }
            return lRes;
        }

        public Boolean ServicioEnEP(string iServicio, string  idObra)
        {
            Boolean lRes = false;   string lSql = ""; DataTable lTbTmp = new DataTable();
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();

            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0,",idObra,", ' ',0,0,'", iServicio, "','','','',13,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbTmp = lDts.Tables[0].Copy();
                if (lTbTmp.Rows.Count > 0)
                {
                    lRes = true;
                }                
            }
            return lRes;
        }

        public Boolean EliminarRegistro(string iServicio, string idObra,string iCantidad, string iUnidad)
        {
            Boolean lRes = false; string lSql = ""; DataTable lTbTmp = new DataTable();
            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();

            lSql = string.Concat("  SP_CRUD_EP_OTROS  0,0,", idObra, ", ' ',0,0,'", iServicio, "','",iCantidad ,"','",iUnidad , "','',14,''");
            lDts = lPx.ObtenerDatos(lSql);
            if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
            {
                lTbTmp = lDts.Tables[0].Copy();
                if (lTbTmp.Rows.Count > 0)
                {
                    lRes = true;
                }
            }
            return lRes;
        }

        public int ValDesdeINET(string iDato)
        {
            int lRes = 0;string lTmp = "";
            Char Delimitador = ',';

            try
            {
                String[] lPartes = iDato.Split(Delimitador);
                if (lPartes.Length > 1)
                {
                    lTmp = lPartes[0].ToString();
                    lRes = int.Parse(lTmp);
                }
              

            }
            catch (Exception exc)
            {
                lRes = 0;
            }
            return lRes;

        }



        public DataTable ObtenerOtrosCobros(string iIdEP, string iIdObra)
        {

            WsMensajeria.Ws_To lPx = new WsMensajeria.Ws_To(); DataSet lDts = new DataSet();
            string lSql = ""; int i = 0; int lTotal = 0;
            DataTable lTbTmp = new DataTable(); Utils lUti = new Utils();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lSql = string.Concat("  SP_CRUD_EP_OTROS  0,", iIdEP, ", ", iIdObra, ",' ',0,0, '','','','',9,''");
              
                lDts = lPx.ObtenerDatos(lSql);
                if ((lDts.Tables.Count > 0) && (lDts.Tables[0].Rows.Count > 0))
                {
                    lTbTmp = lDts.Tables[0].Copy();
                }

               

            }
            catch (Exception iEx)
            {
                MessageBox.Show(string.Concat(" Ha ocurrido el siguiente error: ", iEx.Message.ToString()), "Avisos Sistema");
            }

            return lTbTmp;
        }



    }
}