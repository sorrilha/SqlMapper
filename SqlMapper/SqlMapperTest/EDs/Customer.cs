using System;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Customers")]
    public class Customer
    {
        [Pk("CustomerID")]
        public String CustomerID { get; set; }
        public String CompanyName { get; set; } //NotNull
        public String ContactName { get; set; }
        public String ContactTitle { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }

        public override string ToString()
        {
            return string.Format("CustomerID: {0}," +
                                 " CompanyName: {1}," +
                                 " ContactName: {2}," +
                                 " ContactTitle: {3}," +
                                 " Address: {4}," +
                                 " City: {5}," +
                                 " Region: {6}," +
                                 " PostalCode: {7}," +
                                 " Country: {8}," +
                                 " Phone: {9}," +
                                 " Fax: {10}",
                                 CustomerID,
                                 CompanyName,
                                 ContactName,
                                 ContactTitle,
                                 Address,
                                 City,
                                 Region,
                                 PostalCode,
                                 Country,
                                 Phone,
                                 Fax);
        }
    }
}
