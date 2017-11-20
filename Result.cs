using System;
using System.Data;

namespace EstadosdePagos
{
    public class Result
    {
        private DataSet _dataSet = null;
        private DataTable _dataTable = null;
        private string _stringValue = ""; //getString(""), getInt(""), getFloat("")
        private Int32 _intValue = 0;
        private DataRow[] _dataRows = null;

        public DataTable DataTable
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }

        public DataRow[] DataRows
        {
            get { return _dataRows; }
            set { _dataRows = value; }
        }

        public Int32 IntValue
        {
            get { return _intValue; }
            set { _intValue = value; }
        }

        private string _error = "";

        public string StringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; }
        }
        
        public DataSet DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        public string MensajeError
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}