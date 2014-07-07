using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlMapper.Framework;
using SqlMapper.Framework.MapTypes;
using SqlMapper.Framework.SQLConnectionMan;
using SqlMapper.SQLConnection;
using SqlMapperTest.EDs;
using SqlMapperTest.Framework;

namespace SqlMapperTest
{
    [TestClass]
    public class TestProducts
    {
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        private IDataMapper<Product> prodMapper;
        private TypeMapers mapType;
        private ConnectionManager connMan;
        private int affectedRows;
        private SqlDataReader reader;
        private SqlConnection connecttion;
        private SqlCommand command;
        private Product product;
        [TestInitialize]
        [TestMethod]
        public void AssertDataMapper()
        {
            mapType = new MapWithProperties(typeof(Product));
            connMan = new PersistentConnection(new SqlConnection());
           // Dictionary<String, object> mapOfObjects = mapType.getParams();

            Builder b = new Builder(connMan, mapType);//mapOfObjects);
            prodMapper = b.Build<Product>();
            product = new Product();
            Assert.IsNotNull(prodMapper);
        }

        [TestMethod]
        public void ProductsGetAll()
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
            IEnumerator<Product> pr = prods.GetEnumerator();
            int real = prods.Count();
            Assert.IsTrue(real == expected);

        }

        [TestMethod]
        public void ProductsUpdate()
        {
            product.ProductName = "Updated Product";
            prodMapper.Update(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        [TestMethod]
        public void ProductsInsert()
        {
            Supplier supplier=new Supplier();
            supplier.SupplierID = 1;
            product.supplier = supplier;
            product.ProductName = "Inserted Product";
            prodMapper.Insert(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(product.ProductID>0);
        }
        [TestMethod]
        public void ProductsDelete()
        {
            prodMapper.Delete(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
    }
}
