using System;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapper.EDs
{
    [Table("Employees")]
    class Employee
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

        public override string ToString()
        {
            return string.Format("EmployeeID: {0}," +
                                 " LastName: {1}," +
                                 " FirstName: {2}," +
                                 " Title: {3}," +
                                 " TitleOfCourtesy: {4}," +
                                 " BirthDate: {5}," +
                                 " HireDate: {6}," +
                                 " Address: {7}," +
                                 " City: {8}," +
                                 " Region: {9}," +
                                 " PostalCode: {10}",
                                 EmployeeID,
                                 LastName,
                                 FirstName,
                                 Title,
                                 TitleOfCourtesy,
                                 BirthDate,
                                 HireDate,
                                 Address,
                                 City,
                                 Region,
                                 PostalCode);
        }
    }
}
