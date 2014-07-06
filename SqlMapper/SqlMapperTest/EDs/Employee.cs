using System;
using SqlMapper.Framework.CustomAttributes;

namespace  SqlMapperTest.EDs
{
    [Table("Employees")]
    class Employee : IDEntity
    {

        [Pk("EmployeeID")]
        public int EmployeeID { get; set; }
        public String LastName { get; set; } //NOtNull
        public String FirstName { get; set; } //NotNull
        public String Title { get; set; }
        public String TitleOfCourtesy { get; set; }
        public DateTime ? BirthDate { get; set; }
        public DateTime ? HireDate { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }        
        public String PostalCode { get; set; }

        private String[] mapMapName = { "EmployeeID", "LastName", "FirstName", "Title", "TitleOfCourtesy", 
                                           "BirthDate", "HireDate", "Address", "City", "Region", "PostalCode" };

        public String[] getMapNames()
        {
            return mapMapName;
        }   

    }
}
