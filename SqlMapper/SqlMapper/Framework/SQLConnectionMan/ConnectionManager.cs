using System;
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

        protected ConnectionManager(SqlConnection sqlConnection, String connectionStr)
        {
            _connectionStr = connectionStr;
            _sqlConnection = sqlConnection;
            
        }

        public abstract int ExecuteNonQuery(SqlCommand cmd);

        public abstract SqlDataReader ExecuteReader(SqlCommand cmd);

        public int GetAffectedRows()
        {
            return _affectedRows;
        }

        public SqlDataReader GetReader()
        {
            return _reader;
        }

    }
}
