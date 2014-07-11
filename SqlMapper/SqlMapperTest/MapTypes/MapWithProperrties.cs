using System;
using System.Reflection;
using SqlMapper.Framework.CustomAttributes;
using SqlMapper.Framework.MapTypes;

namespace SqlMapperTest.MapTypes
{
    public class MapWithProperties : TypeMapers
    {

        public MapWithProperties(Type type)
        {

            object x = Activator.CreateInstance(type, null);
            MethodInfo mi = type.GetMethod("getMapNames");
           
            String [] paramsName = (string[]) mi.Invoke(x, null);
            foreach(String s in paramsName)
            {    
                    object o = type.GetProperty(s);
                     _params.Add(s,o);
            }

            PropertyInfo []pi = type.GetProperties();
            foreach (PropertyInfo p in pi)
            {
                Fk fk = p.GetCustomAttribute<Fk>();
                if(fk!= null)
                    getForeignToPrimary().Add(p.Name, fk.getFkPkName());

            }
           
        }

    }
}
