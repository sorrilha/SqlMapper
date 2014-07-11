using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlMapper.Framework.SQLConnectionMan
{
    public abstract class ConnectionManager
    {
        public String _connectionStr;
        public SqlConnection _sqlConnection;
        protected SqlCommand _SqlCommand;
        public SqlDataReader _reader;
        public int _affectedRows;
        protected int _id;

        public ConnectionManager(SqlConnection sqlConnection, String connectionStr)
        {
            _connectionStr = connectionStr;
            _sqlConnection = sqlConnection;
            
        }

        public abstract int ExecuteNonQuery(string stmt, Dictionary<string, object> parameters);

        public abstract SqlDataReader ExecuteReader(String stmt);
        public abstract int ExecuteScalar(String stmt, Dictionary<string, object> parameters);

        public int GetAffectedRows()
        {
            return _affectedRows;
        }

        public SqlDataReader GetReader()
        {
            return _reader;
        }

        public int GetId()
        {
            return _id;
        }

    }
}
