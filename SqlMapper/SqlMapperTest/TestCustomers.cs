﻿using System;
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
        public void AssertDataMapper()
        {
            mapType = new MapWithFields(typeof(Customer));
            connMan = new PersistentConnection(new SqlConnection());
            Dictionary<String, object> mapOfObjects = mapType.getParams();
            Builder b = new Builder(connMan, mapType);//mapOfObjects);
            custMapper = b.Build<Customer>();
            Assert.IsNotNull(custMapper);
            //Assert.IsInstanceOfType(custMapper, IDataMapper<Customer>());
        }

        [TestMethod]
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
        public void CustomersUpdate()
        {
            Customer cust = new Customer();
            cust.CustomerID = "ALFKI";
            cust.CompanyName = "Testing stuff";
            custMapper.Update(cust);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        public void CustomersInsert()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "SERRA";
            cust1.CompanyName = "Insert test kjd";
            custMapper.Insert(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        public void CustomersDelete()
        {
            Customer cust1 = new Customer();
            cust1.CustomerID = "SERRA";
            custMapper.Delete(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
    }
}
