using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SqlMapper.Framework.MapTypes
{
 
    public abstract class TypeMapers
    {
        protected Dictionary<String, object> _params = new Dictionary<string, object>();
        public  Dictionary<String, object> getParams()
        {

            return _params;
        }

        /* protected String[] _paramsName ;
        protected object[] _params;
        protected 
       
        public String[] getParamsNames()
        {

            return _paramsName;
        }

        public object[] getParams()
        {
            
            return _params;
        }*/

    }

}
