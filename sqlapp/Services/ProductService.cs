using Microsoft.Data.SqlClient;
using sqlapp.Models;
using System.Data.SqlClient;


namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appserver30003.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Rushi123@123";
        private static string db_database = "appdb";


        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);

        }
        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> products = new List<Product>();

            string statement = "SELECT ProductId,ProductName,Quantity from Products";
            
            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)

                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }
    }
}

