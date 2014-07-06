using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapperTest.Framework;


namespace SqlMapper.Framework
{
    public class Mapper<T> : IDataMapper<T>
    {
        private SqlCommand _command;
        private ConnectionManager _conMan;
        private Dictionary<String,object>_mapOfObjects;
        private string _tableName;
        private string _pkName;
        protected SqlDataReader _reader;
        protected int _affectedRows;
        Dictionary<Type, IDataMapper> _fkmappers;
        private Dictionary<String, Type> _fkNames;


        public Mapper(ConnectionManager conMan, SqlCommand command, Dictionary<String, object> mapOfObjects, string tableName, string pkName, Dictionary<Type, IDataMapper> fkmappers, Dictionary<String, Type> fkNames)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _tableName = tableName;
            _pkName = pkName;
            _fkmappers = fkmappers;
            _fkNames = fkNames;
        }


        public ISqlEnumerable<T> GetAll()
        {
            GetAll_query();
            return new SqlEnumerable<T>(_conMan, _command, _mapOfObjects, _fkmappers, _fkNames);     
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
            foreach (KeyValuePair<string, object> dic in _mapOfObjects)
            {
                String name = dic.Key;
                if (!_pkName.Equals(name))
                {
                    aux += name + " = @" + name + ", ";
                   
                    object value = GetValue(val, name);
                    if (value == null)
                        value = DBNull.Value;
                        
                    _command.Parameters.AddWithValue("@" + name, value);
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
            foreach (KeyValuePair<string, object> dic in _mapOfObjects)
            {
                String name = dic.Key;
                aux += name + ", ";
                parameters += " @" + name + ", ";
                Object value = GetValue(val, name);
                if (value == null)
                    value = DBNull.Value;

                _command.Parameters.AddWithValue("@" + name, value);
            }
            aux = aux.Remove(aux.Length - 2);
            parameters = parameters.Remove(parameters.Length - 2);
            _command.CommandText = "INSERT INTO " + _tableName + " ( " + aux + " ) Values (" + parameters + " )";
        }

        private Object GetValue(object val, string name)
        {

            Object value = null;
            MemberInfo[] myMember = val.GetType().GetMember(name);
            if (myMember.Length < 1) return null;

            switch (myMember[0].MemberType)
            {
                case MemberTypes.Field:
                    value = ((FieldInfo)myMember[0]).GetValue(val);
                    break;
                case MemberTypes.Property:
                    value = ((PropertyInfo)myMember[0]).GetValue(val, null);
                    break;
            }
            return value;
        }

        ISqlEnumerable IDataMapper.GetAll()
        {
            return GetAll();
        }

        public void Update(object val)
        {
            Update((T)val);
        }

        public void Delete(object val)
        {
            Delete((T)val);
        }

        public void Insert(object val)
        {
           Insert((T)val);
        }
    }
}