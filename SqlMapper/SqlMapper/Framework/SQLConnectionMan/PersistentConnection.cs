using System;
using System.Configuration;
using System.Data.SqlClient;


namespace SqlMapper.Framework.SQLConnectionMan
{
    public class PersistentConnection : ConnectionManager
    {
        
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        public PersistentConnection(SqlConnection sqlConnection) : base(sqlConnection, _conStr)
        {
            _sqlConnection = new SqlConnection(_connectionStr);
            _sqlConnection.Open();
        }

        public override int ExecuteNonQuery(SqlCommand cmd)
        {
            if (_reader != null)
            {
                if(!_reader.IsClosed)
                    _reader.Close();
                _reader.Dispose();
            }
           
            cmd.Connection = _sqlConnection;
            _affectedRows = cmd.ExecuteNonQuery(); 
            return _affectedRows;
        }


        public override SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            cmd.Connection = _sqlConnection;
            if (_reader != null)
            {
                if (!_reader.IsClosed)
                    _reader.Close();
                _reader.Dispose();
            }
           
            _reader = cmd.ExecuteReader();
            return _reader;
        }

    }
}
