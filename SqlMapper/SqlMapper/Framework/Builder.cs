using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinFu.DynamicProxy;

using SqlMapper.Framework;
using System.Data.SqlClient;

namespace SqlMapper.Framework
{
    internal class Builder
    {

        public IDataMapper<T> Build<T>(String connStr)
        {
            if (string.IsNullOrEmpty(connStr))
                throw new ArgumentNullException("connStr");

            ProxyFactory factory = new ProxyFactory();
            IDataMapper<T> proxy = factory.CreateProxy<IDataMapper<T>>(new MyInterceptor<T>(connStr));
            return proxy;
            //return new ProxyFactory().CreateProxy<IDataMapper<T>>(new MyInterceptor());


        }

         public T Build<T>(SqlConnection connection, SqlTransaction transaction)
        {

            return new ProxyFactory().CreateProxy<T>(new MyInterceptor<T>(connection, transaction));
        }
 
    }
}


