using System;
using System.Windows.Forms;
using CommonLibrary2;

namespace EstadosdePagos
{
    static class Program
    {
        public static CurrentUser currentUser = new CurrentUser();
        public const string ENVIRONMENT = "DEBUG"; //DEBUG,PRODUCTION

        private const string userRoot = "HKEY_CURRENT_USER";
        private const string subkey = "Software\\VB and VBA Program Settings\\Metalurgica";
        public static string regeditKeyName = userRoot + "\\" + subkey;
        //WsOperacion
        //http://localhost:4880/Operacion.asmx
        //http://cubigest.torresocaranza.cl/pruebas/ws/operacion.asmx

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());

            string accion = "ESTADO";
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
                accion = arguments[1];
            Form form = null;

            switch (accion.ToUpper())
            {
                case "ESTADO":
                    form = new frmMain();
                    break;
                case "FACTURACION":
                    form = new frmFacturacion();
                    break;
            }

            if (form != null)
            {
                frmLogin f = new frmLogin();
                f.ShowDialog();
                if (f.logon)
                {
                    f.Dispose();

                    try
                    {
                        //Login -> currentUser.Login 
                        currentUser.Machine = Convert.ToInt16(new Registry().GetValue(regeditKeyName, "Maquina", -1));
                        currentUser.ComputerName = System.Environment.MachineName;
                        //currentUser.Login = "CARAYA"; // "MCANDIA";
                        //currentUser.PerfilUsuario = "";

                        WsSesion.WS_Sesion wsSesion = new WsSesion.WS_Sesion();
                        WsSesion.Sesion sesion = wsSesion.Iniciar(currentUser.Login, currentUser.Machine, currentUser.ComputerName, form.Name);

                        form.Text += " - versión: " + Application.ProductVersion;
                        Application.Run(form);

                        sesion = wsSesion.Terminar(sesion.Id);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("La acción especificada es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}