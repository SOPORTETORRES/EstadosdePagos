using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelApp = Microsoft.Office.Interop.Excel;
using CommonLibrary2;


namespace EstadosdePagos.Pruebas
{
    public partial class FrmExcel : Form
    {
        private FileSystemUtility fs = new FileSystemUtility();
        public FrmExcel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeneraExcel_EP();
        }


        private void GeneraExcel_EP( )
        {
            string selectedPath = ""; string obra = "";
            WsOperacion.Operacion wsOperacion = new WsOperacion.Operacion();
            WsOperacion.ListaDataSet listaDataSetOp = new WsOperacion.ListaDataSet();
            WsOperacion.ListaDataSet listaDataTMP = new WsOperacion.ListaDataSet();
            WsOperacion.ListaDataSet lDatos = new WsOperacion.ListaDataSet();
            WsOperacion.EP_Generado lEP = new WsOperacion.EP_Generado();
            List<string> listArchivosGuiasDespacho = new List<string>(); int lCont = 0;
            WsOperacion.Operacion ws = new WsOperacion.Operacion(); string lRangoEx = "";
            DataTable dtResumenxGuiaDespacho = null; int i = 0; DataTable lTbl = new DataTable();
            string error = ""; string lPathDestino = ""; DataTable ltblServicios = new DataTable();
            string lHora = ""; int lTotalCD = 0; int lFE = 0; string lSubTitulo = "";
            string lValorSum = "0"; string lValorPrep = "0"; string lStrTmp = "";
            long totalKgSum = 0; long totalKgPrep = 0;
            try
            {


                lHora = DateTime.Now.ToShortTimeString().Replace(":", "_");
                //Copia la plantilla al directorio de destino
                string plantillaEP = Path.Combine(@"C:\Roberto Becerra\TO\Desarrollos Externos\EP\EstadosdePagos20170914\EstadosdePagos\Pruebas\", "Plantilla.xlsx");
                string outputEP = Path.Combine(lPathDestino, string.Concat("PlantillaEP_", lHora, ".xls"));  //selectedPath + (!su.Right(selectedPath, 1).Equals("\\") ? "\\" : "") + "PlantillaEP.xls";
                string outputEPPdf = Path.Combine(lPathDestino, "PlantillaEP.Pdf");


                if (fs.error == null)
                {
                    //Inserta los datos en el excel de salida
                    object paramMissing = Type.Missing;
                    ExcelApp.Application excelApplication = new ExcelApp.Application();
                    excelApplication.DisplayAlerts = false;
                    excelApplication.Visible = true;
                    ExcelApp.Workbook excelWorkBook = excelApplication.Workbooks.Open(plantillaEP); //.Add(paramMissing);
                    ExcelApp.Worksheet excelSheet = null;

                    if (excelWorkBook != null)
                    {
                        //LLenaremos primero el detalle de la pestaña de  Resumen GD, de la plantilla
                        //Hoja: Resumen
                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[2]).Select();

                        excelSheet.Range["A5"].Value = lSubTitulo.ToString();

                        lTotalCD = 0; lFE = 0;
                        lCont = 7;

                        //Sigue todo como estaba 
                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[1]).Select();

                        excelSheet.Rows["9"].Insert();


                        (excelSheet = (ExcelApp.Worksheet)excelWorkBook.Worksheets[3]).Select();
                        lCont = 4;



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
                //validacion.Append(exc.Message);
            }

        }


    }
}
