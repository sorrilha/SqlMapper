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
        private Category category;
        private Supplier supplier;
        private int _id;
        [TestInitialize]
        [TestMethod]
        [Priority(0)]
        public void AssertDataMapper()
        {
            mapType = new MapWithProperties(typeof(Product));
            connMan = new PersistentConnection(new SqlConnection());
            Builder b = new Builder(connMan, mapType);
            prodMapper = b.Build<Product>();
            product = new Product();
            category = new Category();
            supplier = new Supplier();
            Assert.IsNotNull(prodMapper);
        }

        [TestMethod]
        [Priority(1)]
        public void ProductsGetAll()
        {
            int expected = 0;
            ISqlEnumerable<Product> prods = prodMapper.GetAll();
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
        [Priority(2)]
        public void ProductsInsert()
        {

            supplier.SupplierID = 1;
            category.CategoryID = 1;
            product.supplier = supplier;
            product.category = category;
            product.ProductName = "Inserted Product";
            prodMapper.Insert(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }

        [TestMethod]
        [Priority(3)]
        public void ProductsGetAll_Where()
        {
            int expected = 0;
            ISqlEnumerable<Product> prods = prodMapper.GetAll().Where("ProductName = 'Inserted Product'");
            IEnumerator<Product> pr = prods.GetEnumerator();
            if (pr.MoveNext())
                product = pr.Current;
            else
                product = null;
            Assert.IsTrue(product != null);

        }
        [TestMethod]
        [Priority(4)]
        public void ProductsUpdate()
        {
            ISqlEnumerable<Product> prods = prodMapper.GetAll().Where("ProductName = 'Inserted Product'");
            IEnumerator<Product> pr = prods.GetEnumerator();
            if (pr.MoveNext())
                product = pr.Current;
            product.ProductName = "Updated Product";
            prodMapper.Update(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
        
        [TestMethod]
        [Priority(5)]
        public void ProductsDelete()
        {
            ISqlEnumerable<Product> prods = prodMapper.GetAll().Where("ProductName = 'Updated Product'");
            IEnumerator<Product> pr = prods.GetEnumerator();
            if (pr.MoveNext())
                product = pr.Current;
            prodMapper.Delete(product);
            int real = connMan.GetAffectedRows();
            Assert.IsTrue(real == 1);
        }
    }
}
