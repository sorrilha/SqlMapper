using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SqlMapper.Framework;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.SQLConnectionMan;


namespace SqlMapper.Framework
{
    public class SqlEnumerable<T> :ISqlEnumerable<T>
    {
        private ConnectionManager _conMan;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private Dictionary<String, object> _mapOfObjects;
        private Dictionary<Type, IDataMapper> _fkmappers;
        private Dictionary<String, Type> _fkNames;

        public SqlEnumerable(ConnectionManager conMan, SqlCommand command, Dictionary<String, object> mapOfObjects, Dictionary<Type, IDataMapper> fkmappers, Dictionary<String, Type> fkNames)
        {
            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
            _fkmappers = fkmappers;
            _fkNames = fkNames;
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
            int i = 0;
            _reader = _conMan.ExecuteReader(_command);
            
            if (_reader.HasRows)
            {
                Object instance = Activator.CreateInstance<T>();
                while (_reader.Read())
                {
                    object value;
                    int columnOrder = i;
                    String column = _reader.GetName(columnOrder);
                    bool containsValue = _mapOfObjects.ContainsKey(column);
                    if (containsValue)
                    {
                        value = _reader.GetValue(columnOrder);
                        if (_reader.IsDBNull(columnOrder))
                            value = null;
                        instance.GetType().GetProperty(column).SetValue(instance, value);
                        if (i < _reader.FieldCount) i++;
                        // isto n pode ser assim , tenho de conseguir fazer set quer seja Prop/Field/ED
                    }
                    else
                    {

                        bool containsFk = _fkNames.ContainsKey(column);
                        if (containsFk)
                        {
                            Type aux = _fkNames[column];
                            ConstructorInfo ctor = aux.GetConstructor(new Type[] { });
                            object ent = ctor.Invoke(new object[] {});
                            value = _reader.GetValue(columnOrder);
                            value = CreateFkEntity(ent, column, value);
                            instance.GetType().GetProperty(column).SetValue(instance, value);
                            if (i < _reader.FieldCount) i++;
                        }

                    }
                    
                }
                yield return (T)instance;
            }
        }

        ISqlEnumerable ISqlEnumerable.Where(string clause)
        {
            return Where(clause);
        }


        private object CreateFkEntity(object ent, String column, object value)
        {
            IDataMapper dm = _fkmappers[ent.GetType()];
           
            ISqlEnumerable enumerable = dm.GetAll().Where(column + " = " + value);
            IEnumerator enumer = enumerable.GetEnumerator();
            bool next = enumer.MoveNext();
            if (next)
                ent = enumer.Current;
            
           // enumer.Reset();
            
            return ent;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
