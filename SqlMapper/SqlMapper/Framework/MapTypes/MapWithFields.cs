using System;
using System.Data.SqlClient;
using System.Reflection;

namespace SqlMapper.Framework.MapTypes
{
    class MapWithFields<T> : TypeMapers<T>
    {
        public MapWithFields()
        {
           Type type = typeof (T);
           _params = type.GetFields();
        }

    }
}
