using System;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Orders")]
    class Order
    {
        [Pk("OrderID")]
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

        public override string ToString()
        {
            return string.Format("OrderID: {0}," +
                                 " OrderDate: {1}," +
                                 " RequiredDate: {2}," +
                                 " ShippedDate: {3}," +
                                 " Freight: {4}," +
                                 " ShipName: {5}," +
                                 " ShipCity: {6}," +
                                 " ShipRegion: {7}," +
                                 " ShipPostalCode: {8}," +
                                 " ShipCountry: {9}",
                                 OrderID,
                                 OrderDate,
                                 RequiredDate,
                                 ShippedDate,
                                 Freight,
                                 ShipName,
                                 ShipCity,
                                 ShipRegion,
                                 ShipPostalCode,
                                 ShipCountry);
        }
    }
}
