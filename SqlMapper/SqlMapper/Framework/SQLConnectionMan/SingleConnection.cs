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

        public override int ExecuteNonQuery(SqlCommand cmd)
        {
           int affectedRows = 0;
           using (SqlConnection connection = new SqlConnection(_conStr)) 
            try    
            {

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

        public override SqlDataReader ExecuteReader(SqlCommand cmd)
        {
           
            using (SqlConnection connection = new SqlConnection(_conStr))
                try
                {   
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
    }
}
