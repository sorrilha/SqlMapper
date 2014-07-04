using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapperTest.Framework.CustomAttributes
{

    public class Fk : Attribute
    {
        private readonly String _fkName;
        private String _tableName;
        private Type _type;

        public Fk(String fkName, String tableName, Type type)
        {
            _fkName = fkName;
            _tableName = tableName;
            _type = type;
        }

        public Type getFkType()
        {
            return _type;
        }
        public string getFkName()
        {
            return _fkName;
        }

        public String getTableName()
        {
            return _tableName;
        }

    }
}
