using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SqlMapper.Framework.MapTypes
{
 
    public abstract class TypeMapers<T>
    { 
        
        protected Object[] _params ;

        public Object[] getParams()
        {
            
            return _params;
        }

    }

}
