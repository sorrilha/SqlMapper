using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SqlMapper.EDs;

namespace SqlMapper.Framework.EDsDataMappers
{
    class OrderMapper : Mapper<Order>
    {
        
        public OrderMapper(SqlConnection connection) :base(connection) 
        {
          
        }

        public  override IEnumerable<Order> GetAll()
        {
            getAll_query();
            BeginConn(null);
            SqlDataReader reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var o = new Order();
                    o.OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
                    o.OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
                    o.RequiredDate = reader.GetDateTime(reader.GetOrdinal("RequiredDate"));
                    o.ShippedDate = reader.GetDateTime(reader.GetOrdinal("ShipDate"));
                    o.Freight = reader.GetDecimal(reader.GetOrdinal("Freight"));
                    o.ShipName =  SafeGetString(reader, reader.GetOrdinal("ShipName"));
                    o.ShipCity =  SafeGetString(reader, reader.GetOrdinal("ShipCity"));
                    o.ShipRegion = SafeGetString(reader, reader.GetOrdinal("ShipRegion"));
                    o.ShipPostalCode = SafeGetString(reader, reader.GetOrdinal("ShipPostalCode"));
                    o.ShipCountry = SafeGetString(reader, reader.GetOrdinal("ShipCountry"));
                    yield return o;
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
            _command.CommandText = "SELECT * FROM Orders";
        }

        protected override void update_query(Order order)
        {
            // VER MELHOR
            /*return "UPDATE Orders SET OrderDate = "+order.OrderDate+", " +
                   "RequiredDate = "+order.RequiredDate+", " +
                   "ShippedDate = "+order.ShippedDate+", " +
                   "Freight = "+order.Freight+", " +
                   "ShipName = '"+order.ShipName+"', " +
                   "ShipCity = '"+order.ShipCity+"', " +
                   "ShipRegion = '"+order.ShipRegion+"', " +
                   "ShipPostalCode = '"+order.ShipPostalCode+"', " +
                   "ShipCountry = '"+order.ShipName+"', " +
                    "WHERE OrderID = "+order.OrderID;*/

  


        }

        protected override void delete_query(Order order)
        {
            /*return "DELETE FROM [Order Details] WHERE OrderId = "+order.OrderID+" ; " +
                   "DELETE FROM Orders WHERE OrderId = "+order.OrderID;*/
        }

        protected override void insert_query(Order order)
        {

            /*return "INSERT INTO Orders (OrderDate, RequiredDate, ShippedDate, Freight, ShipName, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) " +
                   "VALUES (" 
                    + order.OrderDate + ", "
                    + order.RequiredDate + ", "
                    + order.ShippedDate + ", "
                    + order.Freight + ",  " +
                    "'" + order.ShipName + "', " +
                    "'" + order.ShipCity + "', " +
                    "'" + order.ShipRegion + "', " +
                    "'" + order.ShipPostalCode + "', " +
                    "'" + order.ShipName + "', " +
                    "'" + order.ShipCountry + "'" +
                    ") " +
                    "WHERE OrderID = " + order.OrderID;*/
        }
    }
    
}
