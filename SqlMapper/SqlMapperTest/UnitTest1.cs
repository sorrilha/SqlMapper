using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Framework;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapperTest.EDs;
using SqlMapperTest.Framework;


namespace SqlMapperTest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        private IDataMapper<Customer> custMapper;
        private TypeMapers<Customer> mapType;
        private ConnectionManager connMan;
        private int affectedRows;
        private SqlDataReader reader;
        private SqlConnection connecttion;
        private SqlCommand command;
        [TestInitialize]
        [TestMethod]
        public void AssertDataMapper()
        {
            mapType = new MapWithProperties<Customer>();
            connMan = new PersistentConnection(new SqlConnection());
            Object[] mapOfObjects = mapType.getParams();
            Builder b = new Builder(connMan, mapOfObjects);
            custMapper = b.Build<Customer>();
            Assert.IsNotNull(custMapper);
            //Assert.IsInstanceOfType(custMapper, IDataMapper<Customer>());
        }

        [TestMethod]
        public void CustomersGetAll()
        {
            int result=0;
            ISqlEnumerable<Customer> custs = custMapper.GetAll().Where("CustomerID = 'SERRA'").Where("CompanyName = 'Insert test kjd'");
            connecttion = new SqlConnection(_conStr);
            connecttion.Open();
            command = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = 'SERRA' AND CompanyName = 'Insert test kjd' ");
            command.Connection = connecttion;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                result++;
            connecttion.Close();
            connecttion.Dispose();
            command.Dispose();
            int real = custs.Count();
            Assert.IsTrue(real == result);
        }

        [TestMethod]
        public void CustomersUpdate()
        {
            Customer cust = new Customer();
            cust.CustomerID = "ALFKI";
            cust.CompanyName = "Testing stuff";
            custMapper.Update(cust);
            int res = connMan.GetAffectedRows();
            Assert.IsTrue(res == 1);
        }
        [TestMethod]
        public void CustomersInsert()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "SERRA";
            cust1.CompanyName = "Insert test kjd";
            custMapper.Insert(cust1);
            int res = connMan.GetAffectedRows();
            Assert.IsTrue(res == 1);
        }
        [TestMethod]
        public void CustomersDelete()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "SERRA";
            custMapper.Delete(cust1);
            int res = connMan.GetAffectedRows();
            Assert.IsTrue(res == 1);
        }
    }
}
