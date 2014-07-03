using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapper.SQLConnection;
using SqlMapperTest.Framework;


namespace SqlMapper.Framework
{
    public class Mapper<T> : IDataMapper<T>
    {
        private SqlCommand _command;
        private ConnectionManager _conMan;
        private object[] _mapOfObjects;
        private string _tableName;
        private string _pkName;
        private object _instance;
        protected SqlDataReader _reader;
        protected int _affectedRows;


        public Mapper(ConnectionManager conMan,SqlCommand command, object[] mapOfObjects,  string tableName, string pkName)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _tableName = tableName;
            _pkName = pkName;
        }

        public ISqlEnumerable<T> GetAll()
        {
            GetAll_query();
            return new SqlEnumerable<T>(_conMan, _command, _mapOfObjects);
           
        }

        public void GetAll_query()
        {

            _command.CommandText = "SELECT * FROM " + _tableName;

        }

        public void Update(T val)
        {
            Update_query(val);
             _conMan.ExecuteNonQuery(_command);
        }

        public  void Update_query(T val)
        {
            String aux = "";
            var key = GetValue(val, _pkName);
            foreach (var p in _mapOfObjects)
            {
                String propName = p.ToString().Split(' ')[1];
                if (!_pkName.Equals(propName))
                {
                    aux += propName + " = @" + propName + ", ";
                    object value = GetValue(val, propName);
                    if (value == null)
                        value = DBNull.Value;
                    _command.Parameters.AddWithValue("@" + propName, value);
                }
            }
            aux = aux.Remove(aux.Length - 2);
            aux += " WHERE " + _pkName + " = @" + _pkName;
            _command.Parameters.AddWithValue("@" + _pkName, key);
            _command.CommandText = "UPDATE " + _tableName + "  SET " + aux;

        }

        public void Delete(T val)
        {
            Delete_query(val);
             _conMan.ExecuteNonQuery(_command);
        }

        public  void Delete_query(T val)
        {
            var key = GetValue(val, _pkName);
            _command.Parameters.AddWithValue("@" + _pkName, key);
            _command.CommandText = "DELETE FROM " + _tableName + "  WHERE " + _pkName + " = @" + _pkName;

        }

        public void Insert(T val)
        {
            Insert_query(val);
             _conMan.ExecuteNonQuery(_command);
        }

        public void Insert_query(T val)
        {
            String aux = "";
            String parameters = "";
            foreach (var p in _mapOfObjects)
            {
                String propName = p.ToString().Split(' ')[1];
                aux += propName + ", ";
                parameters += " @" + propName + ", ";
                Object value = GetValue(val, propName);
                if (value == null)
                    value = DBNull.Value;
                _command.Parameters.AddWithValue("@" + propName, value);
            }
            aux = aux.Remove(aux.Length - 2);
            parameters = parameters.Remove(parameters.Length - 2);
            _command.CommandText = "INSERT INTO " + _tableName + " ( " + aux + " ) Values (" + parameters + " )";
        }

        private Object GetValue(T val, string propName)
        {
            Object o = val.GetType().GetProperty(propName).GetValue(val);
            //if (o != null)
                return o;
            //return val.GetType().GetField(propName).GetValue(val);
        }
    }
}