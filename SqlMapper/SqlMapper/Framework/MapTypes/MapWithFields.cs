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
            String[] paramsName = (string[]) mi.Invoke(type, null);
            object[] paramis = type.GetFields();
            foreach (String s in paramsName)
            {
                foreach (object o in paramis)
                {
                    if (o.ToString().Contains(s))
                        _params.Add(s, o);
                }

            }
        }
    }
}
