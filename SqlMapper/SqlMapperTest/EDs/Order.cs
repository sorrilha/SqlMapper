using System;
using SqlMapper.Framework;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Orders")]
    class Order : IEntity
    {
        [Pk("OrderID",true)]
        public int OrderID { get; set; }
        public DateTime ? OrderDate { get; set; }
        public DateTime ? RequiredDate { get; set; }
        public DateTime ? ShippedDate { get; set; }
        public decimal ? Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }

        private String[] mapMapName = { "OrderID", "OrderDate", "RequiredDate", "ShippedDate", "Freight", 
                                           "ShipName", "ShipCity", "ShipRegion", "ShipRegion", "ShipPostalCode", "ShipCountry" };

        public String[] getMapNames()
        {
            return mapMapName;
        }

        public int getId()
        {
            return OrderID;
        }
    }
}
