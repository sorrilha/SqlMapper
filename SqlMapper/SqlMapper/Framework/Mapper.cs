using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;


namespace SqlMapper.Framework
{
    public abstract class Mapper<T> : IDataMapper<T>
    {


        protected SqlTransaction _transaction;
        protected SqlCommand _command;
      
        protected readonly SqlConnection _connection;

        public Mapper(SqlConnection connection)
        {
            _connection = connection;
        }

        public Mapper(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        protected abstract void getAll_query();
        protected abstract void update_query(T val);
        protected abstract void delete_query(T val);
        protected abstract void insert_query(T val);

        public abstract IEnumerable<T> GetAll();

        public void Update(T val)
        {
            BeginConn(val);
            update_query(val);
            
            var affectdedRows = _command.ExecuteNonQuery();

            Console.WriteLine(affectdedRows);
            if (affectdedRows == 0)
            {
                Console.WriteLine("No rows where affected");
            }
            EndConn(); 
        }

        public void Delete(T val)
        {
            
            BeginConn(val);
            delete_query(val);
            var affectdedRows = _command.ExecuteNonQuery();

            Console.WriteLine(affectdedRows);
            if (affectdedRows == 0)
            {
                Console.WriteLine("No rows where affected");
            }
            EndConn(); 
        }

        public void Insert(T val)
        {
            
            BeginConn(val);
            insert_query(val);
            var affectdedRows = _command.ExecuteNonQuery();

            Console.WriteLine(affectdedRows);
            if (affectdedRows == 0)
            {
                Console.WriteLine("No rows where affected");
            }

            EndConn(); 
        }

        protected void BeginConn(T val)
        {
               _connection.Open();
            _command = new SqlCommand(null, _connection);
            _command.Prepare();
            _transaction = _connection.BeginTransaction();
            _command.Transaction = _transaction;
            _transaction.Commit();

        }
        protected void EndConn()
        {

            _command.Dispose();
            _transaction.Dispose();
            _connection.Close();
            _connection.Dispose();

        }

        public string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

    }
}
 