using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinFu.DynamicProxy;
using System.Data.SqlClient;
using System.Data;
using SqlMapper.EDs;
using SqlMapper.Framework.EDsDataMappers;


namespace SqlMapper.Framework
{
    public class MyInterceptor<T>: IInterceptor
    {

        private readonly string _connStr;
        private SqlConnection _sqlCon;
      
        private SqlTransaction _transaction;
        private Dictionary<Type, Type> mappValues = new Dictionary<Type, Type>();  

        public MyInterceptor(String connection)
        {
            _connStr = connection;
            populateDic();

        }

        public MyInterceptor(SqlConnection sqlCon, SqlTransaction transaction)
        {
            _sqlCon = sqlCon;
            _transaction = transaction;
            populateDic();

        }

        void populateDic()
        {
            mappValues.Add(typeof(Product), typeof(ProductMapper));
            mappValues.Add(typeof(Employee), typeof(EmployeeMapper));
            mappValues.Add(typeof(Customer), typeof(CustomerMapper));
            mappValues.Add(typeof(Order), typeof(OrderMapper));
        }

        public object Intercept(InvocationInfo info)
        {
             _sqlCon = new SqlConnection(_connStr);
            MethodInfo mi = info.TargetMethod;
            var paramList = new List<Object>();
            paramList.Add(_sqlCon);
            /*if (_transaction != null)
            {
                paramList.Add(_transaction);
            }*/
            Object o=null;

            foreach (KeyValuePair<Type, Type> pair in mappValues)
            {
                if (pair.Key.Equals(typeof (T)))
                {
                    o = Activator.CreateInstance(pair.Value, paramList.ToArray());
                    break;
                }
            }

            MethodInfo[] mis = o.GetType().GetMethods();
            MethodInfo m=null;
            var parametersForInvocation = new List<object>();
            for (int i =0; i<mis.Length; i++)
            {
                if (mis[i].ReturnType.Equals(info.TargetMethod.ReturnType) && mis[i].Name.Equals(mi.Name))
                {
                    m = o.GetType().GetMethod(mis[i].Name);
                    break;
                }
            }
       
            
            return m.Invoke(o, info.Arguments);
        }
    }
}
