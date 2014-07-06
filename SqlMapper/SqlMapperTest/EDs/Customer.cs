using System;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Customers")]
    public class Customer : IDEntity
    {
        [Pk("CustomerID")] //public String CustomerID { get; set; }
        public String CustomerID;

        public String CompanyName;
        //public String CompanyName { get; set; } //NotNull
        public String ContactName;
        //public String ContactName { get; set; }
        public String ContactTitle;
       // public String ContactTitle { get; set; }
        public String Address;
        //public String Address { get; set; }
        public String City;
        //public String City { get; set; }
        public String Region;
        //public String Region { get; set; }
        public String PostalCode;
        //public String PostalCode { get; set; }
        public String Country;
        //public String Country { get; set; }
        public String Phone;
        //public String Phone { get; set; }
        public String Fax;
        //public String Fax { get; set; }


        private String[] mapMapName = { "CustomerID", "CompanyName", "ContactName", "ContactTitle", "Address", 
                                           "City", "Region", "PostalCode", "Country", "Phone", "Fax" };

        public String[] getMapNames()
        {
            return mapMapName;
        }   
    }
}
