using System;
using System.Data.SqlClient;
using System.Reflection;

namespace SqlMapper.Framework.MapTypes
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
