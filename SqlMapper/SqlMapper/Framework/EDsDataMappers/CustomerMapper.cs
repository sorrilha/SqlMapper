using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SqlMapper.EDs;

namespace SqlMapper.Framework.EDsDataMappers
{
   public  class CustomerMapper: Mapper<Customer>
    {

        public CustomerMapper(SqlConnection connection): base(connection) 
        {
          
        }

        public override IEnumerable<Customer> GetAll()
        {
            
            BeginConn(null);
            getAll_query();
            SqlDataReader reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var c = new Customer();
                    c.CustomerID = reader.GetString(reader.GetOrdinal("CustomerID"));
                    c.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                    c.ContactName = SafeGetString(reader, reader.GetOrdinal("ContactName"));
                    c.ContactTitle = SafeGetString(reader, reader.GetOrdinal("ContactTitle"));
                    c.Address = SafeGetString(reader, reader.GetOrdinal("Address"));
                    c.City = SafeGetString(reader, reader.GetOrdinal("City"));
                    c.Region = SafeGetString(reader, reader.GetOrdinal("Region"));
                    c.PostalCode = SafeGetString(reader, reader.GetOrdinal("PostalCode"));
                    c.Country = SafeGetString(reader, reader.GetOrdinal("Country"));
                    c.Phone = SafeGetString(reader, reader.GetOrdinal("Phone"));
                    c.Fax = SafeGetString(reader, reader.GetOrdinal("Fax"));
                    yield return c;
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
            _command.CommandText = "SELECT * FROM Customers";
        }

        protected override void update_query(Customer customer)
        {

            _command.CommandText =
           "UPDATE Customers SET CompanyName = @CompanyName WHERE CustomerID = @CustomerID";
            SqlParameter cNameParam = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 40);
            cNameParam.Value = customer.CompanyName ?? SqlString.Null;
            SqlParameter cIDParam = new SqlParameter("@CustomerID", SqlDbType.NChar, 5);
            cIDParam.Value = customer.CustomerID;
            _command.Parameters.Add(cNameParam);
            _command.Parameters.Add(cIDParam);
        }

        protected override void delete_query(Customer customer)
        {
            _command.CommandText = "DELETE  FROM Customers Where CustomerID =@CustomerID; ";
            SqlParameter cIDParam = new SqlParameter("@CustomerID", SqlDbType.NChar, 5);
            cIDParam.Value = customer.CustomerID;
            _command.Parameters.Add(cIDParam);

            
        }

        protected override void insert_query(Customer customer)
        {

            _command.CommandText = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle,Address, City, Region, PostalCode, Country, Phone, Fax) " +
                  "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle,@Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)  ";

            SqlParameter cIDParam = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 40);
            cIDParam.Value = customer.CustomerID;

            
            SqlParameter cNameParam = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 40);
            cNameParam.Value = customer.CompanyName ;

            SqlParameter contactNameParam = new SqlParameter("@ContactName", SqlDbType.NVarChar, 30);
            contactNameParam.Value = customer.ContactName ?? SqlString.Null;

            SqlParameter contactTitleParam = new SqlParameter("@ContactTitle", SqlDbType.NVarChar, 30);
            contactTitleParam.Value = customer.ContactTitle ?? SqlString.Null;

            SqlParameter cAddressParam = new SqlParameter("@Address", SqlDbType.NVarChar, 60);
            cAddressParam.Value = customer.Address ?? SqlString.Null;

            SqlParameter cCityParam = new SqlParameter("@City", SqlDbType.NVarChar, 15);
            cCityParam.Value = customer.City ?? SqlString.Null;

            SqlParameter cRegionParam = new SqlParameter("@Region", SqlDbType.NVarChar, 15);
            cRegionParam.Value = customer.Region ?? SqlString.Null;

            SqlParameter cPostalCodeParam = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
            cPostalCodeParam.Value = customer.PostalCode ?? SqlString.Null;

            SqlParameter cCountryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 15);
            cCountryParam.Value = customer.Country ?? SqlString.Null;

            SqlParameter cPhoneParam = new SqlParameter("@Phone", SqlDbType.NVarChar, 24);
            cPhoneParam.Value = customer.Phone ?? SqlString.Null;

            SqlParameter cFaxParam = new SqlParameter("@Fax", SqlDbType.NVarChar, 24);
            cFaxParam.Value = customer.Fax ?? SqlString.Null;

            _command.Parameters.Add(cIDParam);
            _command.Parameters.Add(cNameParam);
            _command.Parameters.Add(contactNameParam);
            _command.Parameters.Add(contactTitleParam);
            _command.Parameters.Add(cAddressParam);
            _command.Parameters.Add(cCityParam);
            _command.Parameters.Add(cRegionParam);
            _command.Parameters.Add(cPostalCodeParam);
            _command.Parameters.Add(cCountryParam);
            _command.Parameters.Add(cPhoneParam);
            _command.Parameters.Add(cFaxParam);
        }
    }
    
}
