using System;
using System.Reflection;


namespace SqlMapper.Framework.MapTypes
{
    public class MapWithProperties<T> : TypeMapers<T>
    {

        public MapWithProperties()
        {
            Type type = typeof (T);
            _params = type.GetProperties();
        }

    }
}
