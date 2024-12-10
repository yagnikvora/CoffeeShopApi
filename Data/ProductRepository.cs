using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CoffeeShopApi.Data
{
    namespace WebApiDemo.Data
    {
        public class ProductRepository
        {
            private IConfiguration _configuration;
            public ProductRepository(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            #region GetAllProducts
            public List<ProductModel> GetAllProducts()
            {
                var products = new List<ProductModel>();
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_SelectAll";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = Convert.ToDouble(reader["ProductPrice"]),
                        ProductCode = reader["ProductCode"].ToString(),
                        Description = reader["Description"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                connection.Close();
                return products;
            }
            #endregion

            #region GetProductByID
            public List<ProductModel> GetProductByID(int ProductID)
            {
                var product = new List<ProductModel>();
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_SelectByPk";
                command.Parameters.AddWithValue("ProductID", ProductID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = Convert.ToDouble(reader["ProductPrice"]),
                        ProductCode = reader["ProductCode"].ToString(),
                        Description = reader["Description"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                connection.Close();
                return product;
            }
            #endregion

            #region DeleteByID
            public bool DeleteProductByID(int ProductID)
            {
                bool isDeleted = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_DeleteByPk";
                command.Parameters.AddWithValue("ProductID", ProductID);
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
                return isDeleted;
            }
            #endregion

            #region InsertProduct
            public bool InsertProduct(ProductModel product)
            {
                bool isInserted = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_Insert";
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("ProductPrice", product.ProductPrice);
                command.Parameters.AddWithValue("ProductCode", product.ProductCode);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("UserID", product.UserID);
                int rowsAffected = command.ExecuteNonQuery();
                isInserted = rowsAffected > 0;
                return isInserted;
            }

            #endregion

            #region UpdateProduct
            public bool UpdateProduct(ProductModel product)
            {
                bool isUpdate = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_UpdateByPk";
                command.Parameters.AddWithValue("ProductID", product.ProductID);
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("ProductPrice", product.ProductPrice);
                command.Parameters.AddWithValue("ProductCode", product.ProductCode);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("UserID", product.UserID);
                int rowsAffected = command.ExecuteNonQuery();
                isUpdate = rowsAffected > 0;
                return isUpdate;
            }
            #endregion
        }
    }
}
