using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlMapper.EDs;

namespace SqlMapper.Framework.EDsDataMappers
{
    class EmployeeMapper : Mapper<Employee>
    {
         public  EmployeeMapper(SqlConnection connection): base(connection) 
        {
          
        }

        public override IEnumerable<Employee> GetAll()
        {
             getAll_query();
            BeginConn(null);
            SqlDataReader reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var e = new Employee();

                    e.EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                    e.LastName  = reader.GetString(reader.GetOrdinal("LastName"));
                    e.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    e.Title = SafeGetString(reader, reader.GetOrdinal("Title"));
                    e.TitleOfCourtesy = SafeGetString(reader, reader.GetOrdinal("TitleOfCourtesy"));
                    e.BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
                    e.HireDate =  reader.GetDateTime(reader.GetOrdinal("HireDate"));
                    e.Address = SafeGetString(reader, reader.GetOrdinal("Address"));
                    e.City = SafeGetString(reader, reader.GetOrdinal("City"));
                    e.Region = SafeGetString(reader, reader.GetOrdinal("Region"));
                    e.PostalCode = SafeGetString(reader, reader.GetOrdinal("PostalCode"));

                    yield return e;
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            EndConn();
            
        }

        protected override void getAll_query()
        {
            //return "SELECT * FROM Customers";
        }

        protected override void update_query(Employee employee)
        {
            //VER MELHOR
            /*return "UPDATE Employees SET FirstName = '"+employee.FirstName+
                "'  WHERE EmployeeID = "+employee.EmployeeID;*/
        }

        protected override void delete_query(Employee employee)
        {
         /*   SELECT OrderID FROM Orders WHERE EmployeeID = 9;
            DELETE FROM [Order Details] WHERE OrderID =;
            DELETE FROM Orders WHERE EmployeeID = 9;
            DELETE FROM Employees WHERE EmployeeID = 9
            */


           // return "DELETE  FROM [Order Details] WHERE OrderID = " + product.ProductID + "; DELETE  FROM [Products] WHERE ProductID = " + product.ProductID;
           
        }

        protected override void insert_query(Employee employee)
        {
            /*return "INSERT INTO Orders (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, Region, PostalCode) " +
                   "VALUES (" +
                    "'"+ employee.LastName + "', "+
                    "'"+ employee.FirstName + "', "+
                    "'"+ employee.Title + "', "+
                    "'" + employee.TitleOfCourtesy + "', " +
                    employee.BirthDate + ", " +
                    employee.HireDate + ", " +
                    "'" + employee.Address + "', " +
                    "'" + employee.City + "', " +
                    "'" + employee.Region + "', " +
                    "'" + employee.PostalCode + "'" +
                    ") " +
                    "WHERE EmployeeID = " + employee.EmployeeID;*/
        }
    }
    
}
