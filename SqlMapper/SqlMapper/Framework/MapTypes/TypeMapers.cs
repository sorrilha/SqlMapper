using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
