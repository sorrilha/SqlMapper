using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SqlMapper.Framework.SQLConnectionMan;

namespace SqlMapperTest.Framework
{
    public class SqlEnumerable : ISqlEnumerable
    {
        private ConnectionManager _conMan;
        private SqlCommand _command;
        private object[] _mapOfObjects;

        public SqlEnumerable(ConnectionManager conMan, SqlCommand command, object[] mapOfObjects)
        {

            _conMan = conMan;
            _command = command;
            _mapOfObjects = mapOfObjects;
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public ISqlEnumerable Where(string clause)
        {
            bool whereIsPresent = _command.CommandText.Contains("WHERE");
            if (whereIsPresent) _command.CommandText = _command.CommandText + " AND " + clause;
            else _command.CommandText += " WHERE " + clause;
                 return this;
        }
    }
}
