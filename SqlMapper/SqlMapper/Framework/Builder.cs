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
using SqlMapperTest.Framework;
using SqlMapperTest.Framework.CustomAttributes;


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
            //Dictionary<String, String> foreignKeyMap = new Dictionary<string, string>();
           // List<Fk> fkList = new List<Fk>();
            Fk fk = instance.GetType().GetCustomAttribute<Fk>();
            if (fk != null)
            {
                String NameOfFk = fk.getFkName();
                String tableOfFk = fk.getTableName();
                Type type = fk.getFkType();
                Object instOfFk = Activator.CreateInstance(type);
                var dataMapper = new DataMapper(instOfFk);
                SqlEnumerable sqlEnumerable = dataMapper.GetAll();
            }

            foreach (Object  obj in _mapOfObjects)
            {
                Pk aux;
                
                String name = obj.ToString().Split(' ')[1];
                MemberInfo ob = instance.GetType().GetProperty(name);
                if(ob == null)
                    ob = instance.GetType().GetField(name);
                aux = ob.GetCustomAttribute<Pk>();
               
                if (aux != null)
                    pkAtribute = aux;

               
            }

            String pkName = pkAtribute.getPkName();
            
            
            return new Mapper<T>(_connectionManager,_command, _mapOfObjects, tableName, pkName);
        }
    }
}


