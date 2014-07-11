using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.MapTypes;

namespace SqlMapper.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class Fk : Attribute
    {
        private readonly String _fkName;
        private String _tableName;
        private Type _type;
        private Pk pk;

        public Fk(String fkName, String tableName, Type type)
        {
            _fkName = fkName;
            _tableName = tableName;
            _type = type;
            Object inst = Activator.CreateInstance(type);
            MemberInfo m = inst.GetType().GetMember(fkName)[0];
           

            pk =m.GetCustomAttribute<Pk>();

        }

        public IEntity getFkType()
        {
            Object o =  Activator.CreateInstance(_type);
            return (IEntity)o ;
        }
        public string getFkName()
        {
            return _fkName;
        }

        public String getTableName()
        {
            return _tableName;
        }

        public object[] getParams()
        {
            return _type.GetProperties();
        }

        public string getFkPkName()
        {
            return pk.getPkName();
        }

    }
}
