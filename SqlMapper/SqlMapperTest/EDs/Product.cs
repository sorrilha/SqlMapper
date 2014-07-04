using System;
using SqlMapper.Framework.CustomAttributes;
using SqlMapperTest.Framework.CustomAttributes;


namespace SqlMapperTest.EDs
{
    [Table("Products")]
    public class Product
    {
       
        [Pk("ProductID")]
        public int ProductID{ get; set; }
        public String  ProductName { get; set; }
        public String  QuantityPerUnit { get; set; }
        public decimal ? UnitPrice { get; set; }
        public short ? UnitsInStock { get; set; }
        public short ? UnitsOnOrder { get; set; }
        [Fk("SupplierID", "Suppliers", typeof(Supplier))] 
        public Supplier supplier;
        [Fk("CategoryID", "Categories", typeof(Category))] 
        public Category categoy;
       
        public override string ToString()
        {
            return string.Format("ProductID: {0}, " +
                                 "ProductName: {1}, " +
                                 "QuantityPerUnit: {2}, " +
                                 "UnitPrice: {3}, " +
                                 "UnitsInStock: {4}, " +
                                 "UnitsOnOrder: {5}", 
                                 ProductID, 
                                 ProductName, 
                                 QuantityPerUnit, 
                                 UnitPrice, 
                                 UnitsInStock, 
                                 UnitsOnOrder);
        }
            
    }
}
