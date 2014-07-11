﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapperTest.ConnectionManagers
{
    public class PersistentConnection : ConnectionManager
    {
        
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        public PersistentConnection(SqlConnection sqlConnection) : base(sqlConnection, _conStr)
        {
            _sqlConnection = new SqlConnection(_connectionStr);
            _sqlConnection.Open();
        }

        public override int ExecuteNonQuery(string stmt, Dictionary<string, object> parameters)
        {
            SqlCommand cmd = new SqlCommand(stmt);

            foreach (KeyValuePair<string,object> p in parameters)
            {
                cmd.Parameters.AddWithValue(p.Key,p.Value);
            }
           
            SqlConnection connection = new SqlConnection(_connectionStr);
            connection.Open();
            cmd.Connection = connection;
            _affectedRows = cmd.ExecuteNonQuery(); 
            return _affectedRows;
        }


        public override SqlDataReader ExecuteReader(String stmt)
        {

           SqlCommand cmd = new SqlCommand(stmt);
           SqlConnection connection = new SqlConnection(_connectionStr);
            connection.Open();
            cmd.Connection = connection;
            _reader = cmd.ExecuteReader();
            return _reader;
        }

        public override int ExecuteScalar(string stmt, Dictionary<string, object> parameters)
        {
            int newID = 0;
            SqlCommand cmd = new SqlCommand(stmt);

            foreach (KeyValuePair<string, object> p in parameters)
            {
                cmd.Parameters.AddWithValue(p.Key, p.Value);
            }

            SqlConnection connection = new SqlConnection(_connectionStr);
            connection.Open();
            cmd.Connection = connection;
            newID = Convert.ToInt32(cmd.ExecuteScalar());
            return newID;
        }
    }
}