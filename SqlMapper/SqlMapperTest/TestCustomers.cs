using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Framework;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapperTest.ConnectionManagers;
using SqlMapperTest.EDs;
using SqlMapperTest.Framework;
using SqlMapperTest.MapTypes;


namespace SqlMapperTest
{
    [TestClass]
    public class TestCustomers
    {
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        private IDataMapper<Customer> custMapper;
        private TypeMapers mapType;
        private ConnectionManager connMan;
        private int affectedRows;
        private SqlDataReader reader;
        private SqlConnection connecttion;
        private SqlCommand command;
        [TestInitialize]
        [TestMethod]
        [Priority(0)]
        public void AssertDataMapper()
        {
            mapType = new MapWithFields(typeof(Customer));
            connMan = new SingleConnection(new SqlConnection());
            Dictionary<String, object> mapOfObjects = mapType.getParams();
            Builder b = new Builder(connMan, mapType);//mapOfObjects);
            custMapper = b.Build<Customer>();
            Assert.IsNotNull(custMapper);
            //Assert.IsInstanceOfType(custMapper, IDataMapper<Customer>());
        }

        [TestMethod]
        [Priority(1)]
        public void CustomersGetAll()
        {
            int expected=0;
            ISqlEnumerable<Customer> custs = custMapper.GetAll().Where("CustomerID = 'SERRA'").Where("CompanyName = 'Insert test kjd'");
            connecttion = new SqlConnection(_conStr);
            connecttion.Open();
            command = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = 'SERRA' AND CompanyName = 'Insert test kjd' ");
            command.Connection = connecttion;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                expected++;
            connecttion.Close();
            connecttion.Dispose();
            command.Dispose();
            int real = custs.Count();
            Assert.IsTrue(real == expected);
        }

       
        [TestMethod]
        [Priority(2)]
        public void CustomersInsert()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "Ana";
            cust1.CompanyName = "Insert test 4 Ana";
            custMapper.Insert(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }

        [TestMethod]
        [Priority(3)]
        public void CustomersUpdate()
        {
            Customer cust = new Customer();
            cust.CustomerID = "Ana";
            cust.CompanyName = "Update test 4 Sofia";
            custMapper.Update(cust);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        [Priority(4)]
        public void CustomersDelete()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "Ana";
            custMapper.Delete(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
    }
}
