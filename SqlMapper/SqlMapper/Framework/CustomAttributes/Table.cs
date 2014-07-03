using System;

namespace SqlMapper.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Table: Attribute
    {
        private readonly String _tableName;

        public Table(String tableName)
        {
            _tableName = tableName;
        }

        public string getTableName()
        {
            return _tableName;
        }

    }
}
