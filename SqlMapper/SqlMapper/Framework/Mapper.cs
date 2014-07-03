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

        /*public Mapper(ConnectionManager conMan, TypeMapers<T> typemapper, Object tObject)
        {
            _conMan = conMan;
            _command = new SqlCommand();
            _typemapper = typemapper;
            _tObject = tObject;
            Table table;
            table = _tObject.GetType().GetCustomAttribute<Table>();
            tableName = table.getTableName();
            Pk pkAtribute = null;
            PropertyInfo[] pinfo = _tObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in pinfo)
            {
                Pk aux = propertyInfo.GetCustomAttribute<Pk>();
                if (aux != null)
                    pkAtribute = aux;
            }

            pkName = pkAtribute.getPkName();
             
        }*/

        public Mapper(ConnectionManager conMan,SqlCommand command, object[] mapOfObjects,  string tableName, string pkName)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _tableName = tableName;
            _pkName = pkName;
        }

        public IEnumerable<T> GetAll()
        {
            GetAll_query();
            _reader = _conMan.ExecuteReader(_command);
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    Object instance = Activator.CreateInstance<T>();
                    foreach (var p in _mapOfObjects)
                    {
                        String column = p.ToString().Split(' ')[1];
                        int columnOrder = _reader.GetOrdinal(column);
                        Object value = _reader.GetValue(columnOrder);
                        if (_reader.IsDBNull(columnOrder))
                            value = null;
                        instance.GetType().GetProperty(column).SetValue(instance, value);
                    }
                    yield return (T) instance;
                }
            }
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
            var key = val.GetType().GetProperty(_pkName);
            foreach (var p in val.GetType().GetProperties())
            {
                String propName = p.Name;
                if (!key.Name.Equals(propName))
                {
                    aux += propName + " = @" + propName + ", ";
                    var value = p.GetValue(val);
                    if (value == null)
                        value = DBNull.Value;
                    _command.Parameters.AddWithValue("@" + propName, value);
                }
            }
            aux = aux.Remove(aux.Length - 2);
            aux += " WHERE " + _pkName + " = @" + _pkName;
            _command.Parameters.AddWithValue("@" + key.Name, key.GetValue(val));
            _command.CommandText = "UPDATE " + _tableName + "  SET " + aux;

        }

        public void Delete(T val)
        {
            Delete_query(val);
             _conMan.ExecuteNonQuery(_command);
        }

        public  void Delete_query(T val)
        {
            var key = val.GetType().GetProperty(_pkName).GetValue(val);
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
            foreach (var p in val.GetType().GetProperties())
            {
                String propName = p.Name;
                aux += propName + ", ";
                parameters += " @" + propName + ", ";
                Object value = p.GetValue(val);
                if (value == null)
                    value = DBNull.Value;
                _command.Parameters.AddWithValue("@" + propName, value);
            }
            aux = aux.Remove(aux.Length - 2);
            parameters = parameters.Remove(parameters.Length - 2);
            _command.CommandText = "INSERT INTO " + _tableName + " ( " + aux + " ) Values (" + parameters + " )";
        }
    }
}