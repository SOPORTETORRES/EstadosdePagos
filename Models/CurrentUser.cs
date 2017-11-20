
namespace EstadosdePagos
{
    public class CurrentUser
    {
        private string _login = "";
        private string _name = "";
        private int _machine = -1;
        private string _computerName = "";
        private string _PerfilUsuario = "ADMIN"; //"ADMIN";
        private int _idTotem = 666;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
       
        public int Machine
        {
            get { return _machine; }
            set { _machine = value; }
        }
        
        public string ComputerName
        {
            get { return _computerName; }
            set { _computerName = value; }
        }

        public string PerfilUsuario
        {
            get { return _PerfilUsuario; }
            set { _PerfilUsuario = value; }
        }

        public int IdTotem
        {
            get { return _idTotem; }
            set { _idTotem = value; }
        }
    }
}