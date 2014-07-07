using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapper.SQLConnection
{
    public class SingleConnection : ConnectionManager
    {
         private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;

        public SingleConnection(SqlConnection sqlConnection) : base(sqlConnection, _conStr)
        {
        }

        public override int ExecuteNonQuery(string stmt, Dictionary<string, object> parameters)
        {
           int affectedRows = 0;
           using (SqlConnection connection = new SqlConnection(_conStr)) 
            try    
            {
                SqlCommand cmd = new SqlCommand(stmt);
                cmd.Parameters.Add(parameters);
                connection.Open();
                cmd.Connection = connection;
                cmd.Prepare();
                SqlTransaction trans = connection.BeginTransaction();
                cmd.Transaction = trans;
                trans.Commit();
                affectedRows = cmd.ExecuteNonQuery();
                trans.Dispose();
                connection.Dispose();
                connection.Close();
                
            } 
            catch (Exception) 
             { 
             /*Handle error*/ 
             }
           return affectedRows;
        }

        public override SqlDataReader ExecuteReader(String  stmt)
        {
           
            using (SqlConnection connection = new SqlConnection(_conStr))
                try
                {
                    SqlCommand cmd = new SqlCommand(stmt);
                
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Prepare();

                    SqlTransaction trans = connection.BeginTransaction();
                    cmd.Transaction = trans;
                    trans.Commit();
                    _reader = cmd.ExecuteReader();
                    trans.Dispose();
                    connection.Dispose();
                    connection.Close();
                    
                }
                catch (Exception)
                {
                    /*Handle error*/
                }
                return _reader;
        }

        public override object ExecuteScalar(string stmt, Dictionary<string, object> parameters)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(_conStr))
                try
                {
                    SqlCommand cmd = new SqlCommand(stmt);
                    cmd.Parameters.Add(parameters);
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Prepare();
                    SqlTransaction trans = connection.BeginTransaction();
                    cmd.Transaction = trans;
                    trans.Commit();
                    id = cmd.ExecuteNonQuery();
                    trans.Dispose();
                    connection.Dispose();
                    connection.Close();

                }
                catch (Exception)
                {
                    /*Handle error*/
                }
            return id;
        }
    }
}
