using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SqlMapper.Framework;
using System.Data.SqlClient;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;



namespace SqlMapper.Framework
{
    public class Builder
    {
        private ConnectionManager _connectionManager;
        private Object[] _mapOfObjects;
        private SqlCommand _command;
        public Builder(ConnectionManager connectionManager, Object[] mapOfObjects)
        {
            _connectionManager = connectionManager;
            _mapOfObjects = mapOfObjects;
        }


        public IDataMapper<T> Build<T>()
        {
           
            Object instance = Activator.CreateInstance<T>();
            _command = new SqlCommand();
            Table table;
            table = instance.GetType().GetCustomAttribute<Table>();
            String tableName = table.getTableName();
            Pk pkAtribute = null;
            foreach (Object  obj in _mapOfObjects)
            {
                Pk aux = obj.GetType().GetCustomAttribute<Pk>();
                if (aux != null)
                    pkAtribute = aux;
            }

            String pkName = pkAtribute.getPkName();
            
            
            return new Mapper<T>(_connectionManager,_command, _mapOfObjects, tableName, pkName);
        }
    }
}


