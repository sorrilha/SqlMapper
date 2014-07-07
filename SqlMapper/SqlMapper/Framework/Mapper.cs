using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapper.Framework.CustomAttributes;



namespace SqlMapper.Framework
{
    public class Mapper<T> : IDataMapper<T>
    {
        private SqlCommand _command;
        private ConnectionManager _conMan;
        private Dictionary<String,object>_mapOfObjects;
        private string _tableName;
        private Pk _pk;
        protected SqlDataReader _reader;
        protected int _affectedRows;
        Dictionary<Type, IDataMapper> _fkmappers;
        private Dictionary<String, IEntity> _fkNames;
        private Dictionary<String, object> parameterDictionary;
        Dictionary<String, String> _foreignToPrimary;

        public Mapper(ConnectionManager conMan, SqlCommand command, 
            Dictionary<String, object> mapOfObjects, string tableName, 
            Pk pk, Dictionary<Type, IDataMapper> fkmappers, 
            Dictionary<String, IEntity> fkNames,Dictionary<String, String> foreignToPrimary)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _tableName = tableName;
            _pk = pk;
            _fkmappers = fkmappers;
            _fkNames = fkNames;
            _foreignToPrimary = foreignToPrimary;

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
            _conMan.ExecuteNonQuery(_command.CommandText, parameterDictionary);
        }

        public  void Update_query(T val)
        {
            parameterDictionary = new Dictionary<string, object>();
            String aux = "";
            String pkName = _pk.getPkName();
            bool isIdentity = _pk.isIdentity();
            var key = GetValue(val, pkName);
            object value = null;
            foreach (KeyValuePair<string, object> dic in _mapOfObjects)
            {
                String name = dic.Key;

                if (_foreignToPrimary.ContainsKey(name))
                {
                    name = _foreignToPrimary[name];
                    aux += name + " = @" + name + ", ";
                    value = GetValue(val, name);
                    if (value == null)
                        value = DBNull.Value;


                    parameterDictionary.Add("@" + name, value);
                }
                else if (!pkName.Equals(name))
                {
                    aux += name + " = @" + name + ", ";
                    value = GetValue(val, name);
                    if (value == null)
                        value = DBNull.Value;


                    parameterDictionary.Add("@" + name, value);
                }

            }
            aux = aux.Remove(aux.Length - 2);
            aux += " WHERE " + pkName + " = @" + pkName;
            parameterDictionary.Add("@" + pkName, key);
            _command.CommandText = "UPDATE " + _tableName + " SET " + aux;

        }

        public void Delete(T val)
        {
            Delete_query(val);
             _conMan.ExecuteNonQuery(_command.CommandText, parameterDictionary);
        }

        public  void Delete_query(T val)
        {
            String pkName = _pk.getPkName();
            parameterDictionary = new Dictionary<string, object>();
            var key = GetValue(val, pkName);
            parameterDictionary.Add("@" + pkName, key);
            //_command.Parameters.AddWithValue("@" + _pkName, key);
            _command.CommandText = "DELETE FROM " + _tableName + "  WHERE " + pkName + " = @" + pkName;

        }

        public void Insert(T val)
        {
            Insert_query(val);
            _conMan.ExecuteNonQuery(_command.CommandText, parameterDictionary);
        }

        public void Insert_query(T val)
        {
            parameterDictionary = new Dictionary<string, object>();
            String aux = "";
            String parameters = "";
            String pkName = _pk.getPkName();
            bool isIdentity = _pk.isIdentity();
            var key = GetValue(val, pkName);
            
            foreach (KeyValuePair<string, object> dic in _mapOfObjects)
            {
                String name = dic.Key;
                Object value = null;
                if (_foreignToPrimary.ContainsKey(name))
                {
                    //String nameOfKey = name;
                    name = _foreignToPrimary[name];
                    aux += name + ", ";
                    IEntity t = _fkNames[name];
                    
                    value = t.getId();
                    if (value == null)
                        value = DBNull.Value;
                    parameters += " @" + name + ", ";

                    parameterDictionary.Add("@" + name, value);
                }
                else if (name.Equals(pkName))
                {
                    if (!isIdentity)
                    {
                        aux += name + ", ";
                        value = GetValue(val, name);
                        if (value == null)
                            value = DBNull.Value;
                        parameters += " @" + name + ", ";

                        parameterDictionary.Add("@" + name, value);
                    }
                }
                else
                {
                    aux += name + ", ";
                    value = GetValue(val, name);
                    if (value == null)
                        value = DBNull.Value;
                    parameters += " @" + name + ", ";

                    parameterDictionary.Add("@" + name, value);
                }
               
                //_command.Parameters.AddWithValue("@" + name, value);
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