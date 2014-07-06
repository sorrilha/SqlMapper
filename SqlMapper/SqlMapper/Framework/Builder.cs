using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;



namespace SqlMapper.Framework
{
    public class Builder
    {
        private ConnectionManager _connectionManager;
        private readonly TypeMapers _mapTypes;
        private SqlCommand _command;
       // List<IDataMapper> dataMappers = new List<IDataMapper>();
        Dictionary<Type, IDataMapper>dataMappers = new Dictionary<Type,IDataMapper>();
        //private List<String> listOfFk = new List<string>(); 
        private Dictionary<String, Type> listOfFk = new Dictionary<String, Type>();
        public Builder(ConnectionManager connectionManager, TypeMapers mapTypes)
        {
            _connectionManager = connectionManager;
            _mapTypes = mapTypes;
        }



        public IDataMapper<T> Build<T>()
        {
          
            Object instance = Activator.CreateInstance<T>();
            _command = new SqlCommand();
            Table table;
            table = instance.GetType().GetCustomAttribute<Table>();
            String tableName = table.getTableName();
            Pk pkAtribute = null;

            ConstructorInfo ctor = _mapTypes.GetType().GetConstructor(new Type[] { typeof(Type) });
            object o = ctor.Invoke(new object[] { typeof(T) });
            TypeMapers tm = (TypeMapers) o;
            

            string pkName;
            foreach (KeyValuePair<string, object> dic in tm.getParams())
            {
                String name = dic.Key;
                object obj = dic.Value;
                    MemberInfo ob = instance.GetType().GetProperty(name);
                    if (ob == null)
                        ob = instance.GetType().GetField(name);
                    Fk fk = ob.GetCustomAttribute<Fk>();
                    if (fk != null)
                    {
                        Type type = fk.getFkType();
                        
                        listOfFk.Add(fk.getFkPkName(), fk.getFkType());
                        MethodInfo meth = GetType().GetMethod("Build");
                        MethodInfo generic = meth.MakeGenericMethod(type);
                        IDataMapper invoke = (IDataMapper) generic.Invoke(this, null);
                        dataMappers.Add(type, invoke);
                    }

                    Pk aux;
                    aux = ob.GetCustomAttribute<Pk>();
                    if (aux != null)
                        pkAtribute = aux;
                
            }

            pkName = pkAtribute.getPkName();
            
            return new Mapper<T>(_connectionManager, _command, tm.getParams(), tableName, pkName, dataMappers, listOfFk);
        }
    }


}


