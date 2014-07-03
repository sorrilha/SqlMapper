using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapperTest.Framework
{
    class SqlEnumerable<T> :ISqlEnumerable<T>
    {
        private ConnectionManager _conMan;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private Object[] _mapOfObjects;
        private List<string> _clauses;
        private String _query; 
        public SqlEnumerable(ConnectionManager conMan, SqlCommand command, Object[] mapOfObjects)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _clauses = new List<String>();
            _query  = _command.CommandText;
            
        }

        public ISqlEnumerable<T> Where(string clause)
        {
            _clauses.Add(clause);
            
            foreach(String s in _clauses)
            {
                if(s.Equals(_clauses.First()) && _clauses.Count==1)
                    _query += " WHERE " + s;
                else
                {
                    _query += " AND " + s;
                }
            }
            
            _command.CommandText = _query;
           
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
