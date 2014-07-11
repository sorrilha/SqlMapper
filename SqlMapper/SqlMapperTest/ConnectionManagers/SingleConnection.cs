using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapperTest.ConnectionManagers
{
    public class SingleConnection : ConnectionManager
    {
         private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;

        public SingleConnection(SqlConnection sqlConnection) : base(sqlConnection, _conStr)
        {
        }

        public override int ExecuteNonQuery(string stmt, Dictionary<string, object> parameters)
        {

            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                SqlCommand cmd = new SqlCommand(stmt);
                cmd.Transaction = trans;
                try
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    cmd.Connection = connection;
                    _affectedRows = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);

                    try
                    {
                        // Attempt to roll back the transaction.
                        trans.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection  
                        // is closed or the transaction has already been rolled  
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }
                }
                 return _affectedRows;
            }
        }

        public override SqlDataReader ExecuteReader(string stmt)
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection connection = new SqlConnection(_conStr);
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                
                
                SqlCommand cmd = new SqlCommand(stmt);
                cmd.Transaction = trans;
                try
                {
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);
                }
                
            }
            catch(Exception ex){Console.WriteLine(ex.Message);}
            return reader;
        }

        public override int ExecuteScalar(string stmt, Dictionary<string, object> parameters)
        {
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                SqlCommand cmd = new SqlCommand(stmt);
                cmd.Transaction = trans;
                try
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    cmd.Connection = connection;
                    _affectedRows = (Int32)cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);

                    try
                    {
                        // Attempt to roll back the transaction.
                        trans.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection  
                        // is closed or the transaction has already been rolled  
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }
                }
                return _affectedRows;
            }
        }
    }


}
