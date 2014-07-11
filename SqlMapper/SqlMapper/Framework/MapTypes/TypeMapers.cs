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
        protected Dictionary<String, String> foreignToPrimary = new Dictionary<string, string>(); 
        public  Dictionary<String, object> getParams()
        {

            return _params;
        }
        public Dictionary<String, String> getForeignToPrimary()
        {

            return foreignToPrimary;
        }
    }

}
