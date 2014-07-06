using System;
using System.Reflection;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapper.Framework.MapTypes
{
    public class MapWithProperties : TypeMapers
    {

        public MapWithProperties(Type type)
        {

            object x = Activator.CreateInstance(type, null);
            MethodInfo mi = type.GetMethod("getMapNames");
           
            String [] paramsName = (string[]) mi.Invoke(x, null);
            object[] paramis = type.GetProperties();
            foreach(String s in paramsName)
            {    
                    object o = type.GetProperty(s);
                     _params.Add(s,o);
            }
        }

    }
}
