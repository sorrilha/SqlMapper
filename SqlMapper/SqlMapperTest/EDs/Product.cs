using System;
using SqlMapper.Framework;
using SqlMapper.Framework.CustomAttributes;


namespace SqlMapperTest.EDs
{
    [Table("Products")]
    public class Product : IEntity
    {
       
        [Pk("ProductID", true)]
        public int ProductID{ get; set; }
        public String  ProductName { get; set; }
        [Fk("SupplierID", "Suppliers", typeof(Supplier))]
        public Supplier supplier { get; set; }
        [Fk("CategoryID", "Categories", typeof(Category))]
        public Category category { get; set; }
        public String  QuantityPerUnit { get; set; }
        public decimal ? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
       
      
        

        private String[] mapMapName = { "ProductID", "ProductName", "QuantityPerUnit", "UnitPrice", "UnitsInStock", 
                                           "UnitsOnOrder", "supplier", "category" };

        

        public String[] getMapNames()
        {
            return mapMapName;
        }

        public int getId()
        {
            return ProductID;
        }
    }
}
