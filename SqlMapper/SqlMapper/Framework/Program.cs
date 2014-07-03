using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlMapper.EDs;


namespace SqlMapper.Framework
{
    public  class Program
    {
        private static readonly String _conStr = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        public static void Main()
        {

            /*Builder b = new Builder();
            IDataMapper<Product> prodMapper = b.Build<Product>(_conStr);

            IEnumerable<Product> prods = prodMapper.GetAll();
            Console.WriteLine(prods.First().ToString());
            Product prod = new Product();
            prod.ProductID=74;
            prod.ProductName = "Teste de Update1";
            prodMapper.Update(prod);

            Product prod1 = new Product();
            prod1.ProductID = 70;
            prod1.ProductName = "Insert test76";
            prodMapper.Insert(prod1);
            
           // prodMapper.Delete(prod1);
             * Console.WriteLine(prodMapper.ToString());*/
            
            Builder b = new Builder();
            IDataMapper<Customer> custMapper = b.Build<Customer>(_conStr);

            IEnumerable<Customer> custs = custMapper.GetAll();
            Console.WriteLine(custs.First().ToString());
            Customer cust = new Customer();
            cust.CustomerID = "ALFKI";
            cust.CompanyName = "Teste de Update1";
            custMapper.Update(cust);

            Customer cust1 = new Customer();
            cust1.CustomerID = "SURRA";
            cust1.CompanyName = "Insert test";
            custMapper.Insert(cust1);
            
            custMapper.Delete(cust1);
             
            


            Console.ReadLine();

        }
    }
}
