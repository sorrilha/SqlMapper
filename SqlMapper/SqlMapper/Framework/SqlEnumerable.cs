using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapperTest.Framework
{
    class SqlEnumerable<T> :ISqlEnumerable<T>
    {
        private ConnectionManager _conMan;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private Object[] _mapOfObjects;

        public SqlEnumerable(ConnectionManager conMan, SqlCommand command, Object[] mapOfObjects)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            
        }

        public ISqlEnumerable<T> Where(string clause)
        {
     
            bool whereIsPresent = _command.CommandText.Contains("WHERE");
            if (whereIsPresent) _command.CommandText = _command.CommandText + " AND " + clause;
            else _command.CommandText += " WHERE " + clause; 
                return this;

        }

        public IEnumerator<T> GetEnumerator()
        {

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
                    yield return (T)instance;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
