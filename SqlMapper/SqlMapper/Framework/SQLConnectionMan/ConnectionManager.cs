using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlMapper.SQLConnection;

namespace SqlMapper.Framework.SQLConnectionMan
{
    public abstract class ConnectionManager
    {
        protected String _connectionStr;
        public SqlConnection _sqlConnection;
        protected SqlCommand _SqlCommand;
        protected SqlDataReader _reader;
        protected int _affectedRows;
        protected object _id;

        protected ConnectionManager(SqlConnection sqlConnection, String connectionStr)
        {
            _connectionStr = connectionStr;
            _sqlConnection = sqlConnection;
            
        }

        public abstract int ExecuteNonQuery(string stmt, Dictionary<string, object> parameters);

        public abstract SqlDataReader ExecuteReader(String stmt);
        public abstract object ExecuteScalar(String stmt, Dictionary<string, object> parameters);

        public int GetAffectedRows()
        {
            return _affectedRows;
        }

        public SqlDataReader GetReader()
        {
            return _reader;
        }

        public object getId()
        {
            return _id;
        }

    }
}
