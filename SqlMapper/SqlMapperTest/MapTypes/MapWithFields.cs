using System;
using System.Reflection;
using SqlMapper.Framework.MapTypes;

namespace SqlMapperTest.MapTypes
{
    public class MapWithFields : TypeMapers
    {
        public MapWithFields(Type type)
        {
             object x = Activator.CreateInstance(type, null);
            MethodInfo mi = type.GetMethod("getMapNames");
           
            String [] paramsName = (string[]) mi.Invoke(x, null);
            foreach(String s in paramsName)
            {    
                    object o = type.GetField(s);
                     _params.Add(s,o);
            }
        }

    }
    
}
