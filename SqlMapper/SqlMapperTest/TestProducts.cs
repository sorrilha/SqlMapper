using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Framework;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapperTest.EDs;
using SqlMapperTest.Framework;

namespace SqlMapperTest
{
    [TestClass]
    public class TestProducts
    {
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        private IDataMapper<Product> prodMapper;
        private TypeMapers<Product> mapType;
        private ConnectionManager connMan;
        private int affectedRows;
        private SqlDataReader reader;
        private SqlConnection connecttion;
        private SqlCommand command;
        [TestInitialize]
        [TestMethod]
        public void AssertDataMapper()
        {
            mapType = new MapWithProperties<Product>();
            connMan = new PersistentConnection(new SqlConnection());
            Object[] mapOfObjects = mapType.getParams();
            Builder b = new Builder(connMan, mapOfObjects);
            prodMapper = b.Build<Product>();
            Assert.IsNotNull(prodMapper);
        }

        [TestMethod]
        public void CustomersGetAll()
        {
            int expected = 0;
            ISqlEnumerable<Product> prods = prodMapper.GetAll();//.Where("CustomerID = 'SERRA'").Where("CompanyName = 'Insert test kjd'");
            connecttion = new SqlConnection(_conStr);
            connecttion.Open();
            command = new SqlCommand("SELECT * FROM Products");
            command.Connection = connecttion;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                expected++;
            connecttion.Close();
            connecttion.Dispose();
            command.Dispose();
            //int real = prods.Count();
            //Assert.IsTrue(real == expected);
            
        }

        [TestMethod]
        public void CustomersUpdate()
        {
            Product cust = new Product();
            cust.ProductID = 1;
            cust.ProductName = "Testing stuff";
            prodMapper.Update(cust);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        public void CustomersInsert()
        {
            Product cust1 = new Product();
            cust1.ProductID = 1;
            cust1.ProductName = "Insert test kjd";
            prodMapper.Insert(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        public void CustomersDelete()
        {
            Product cust1 = new Product();
            cust1.ProductID = 1;
            prodMapper.Delete(cust1);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
    }
}
