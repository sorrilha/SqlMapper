using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SqlMapper.EDs;

namespace SqlMapper.Framework.EDsDataMappers
{
    public class ProductMapper :Mapper<Product>
    {
        
        public ProductMapper(SqlConnection connection) :base(connection) 
        {
          
        }



        public  override IEnumerable<Product> GetAll()
        {
            BeginConn(null);
            getAll_query();
            
            SqlDataReader reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var p = new Product();
                    p.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                    p.ProductName = SafeGetString(reader, reader.GetOrdinal("ProductName"));
                    p.QuantityPerUnit = SafeGetString(reader, reader.GetOrdinal("QuantityPerUnit"));
                    p.UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"));
                    p.UnitsInStock = reader.GetInt16(reader.GetOrdinal("UnitsInStock"));
                    p.UnitsOnOrder = reader.GetInt16(reader.GetOrdinal("UnitsOnOrder"));

                    yield return p;
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
            _command.CommandText = "SELECT * FROM Products";
        }

        protected override void update_query(Product product)
        {
            //VER MELHOR
            _command.CommandText=
            "UPDATE Products SET ProductName = @ProductName WHERE ProductID = @ProductID";
            SqlParameter pNameParam = new SqlParameter("@ProductName", SqlDbType.NVarChar, 40);
            pNameParam.Value = product.ProductName ?? SqlString.Null;
            SqlParameter pIDParam = new SqlParameter("@ProductID", SqlDbType.Int, 4);
            pIDParam.Value =  product.ProductID;
            _command.Parameters.Add(pNameParam);
            _command.Parameters.Add(pIDParam);

        }

        protected override void delete_query(Product product)
        {
            _command.CommandText = "DELETE [Order Details] FROM [Order Details] INNER JOIN Products ON  [Order Details].ProductID=Products.ProductID Where Products.ProductID =@ProductID; ";
            SqlParameter pIDParam = new SqlParameter("@ProductID", SqlDbType.Int, 4);
            _command.Parameters.Add(pIDParam);


        }

        protected override void insert_query(Product product)
        {
             _command.CommandText  = "INSERT INTO Products (ProductName, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder) " +
                  "VALUES (@ProductName, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder)  ";
             SqlParameter pNameParam = new SqlParameter("@ProductName", SqlDbType.NVarChar,40);
             pNameParam.Value = product.ProductName ?? SqlString.Null;

             SqlParameter pQuantityParam = new SqlParameter("@QuantityPerUnit", SqlDbType.NVarChar, 20);
             pQuantityParam.Value= product.QuantityPerUnit ?? SqlString.Null;

             SqlParameter punitPriceParam = new SqlParameter("@UnitPrice", SqlDbType.Money,8);
            punitPriceParam.Value =  product.UnitPrice ?? SqlMoney.Null;
            SqlParameter pUnitsInStockParam = new SqlParameter("@UnitsInStock", SqlDbType.SmallInt,2);
               pUnitsInStockParam.Value = product.UnitsInStock ?? SqlDecimal.Null;
            SqlParameter pUnitsOnStockParam = new SqlParameter("@UnitsOnOrder", SqlDbType.SmallInt,2);
            pUnitsOnStockParam.Value = product.UnitsOnOrder ?? SqlDecimal.Null;

                _command.Parameters.Add(pNameParam);
                _command.Parameters.Add(pQuantityParam);
                _command.Parameters.Add(punitPriceParam);
                _command.Parameters.Add(pUnitsInStockParam);
                _command.Parameters.Add(pUnitsOnStockParam);
            
        }
    }
}