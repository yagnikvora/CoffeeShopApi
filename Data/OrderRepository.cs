using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CoffeeShopApi.Data
{
    namespace WebApiDemo.Data
    {
        public class OrderRepository
        {
            private IConfiguration _configuration;
            public OrderRepository(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            #region GetAllOrders
            public List<OrderModel> GetAllOrders()
            {
                var orders = new List<OrderModel>();
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_SelectAll";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderNumber = reader["OrderNumber"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        TotalAmount = reader["TotalAmount"].ToString(),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                connection.Close();
                return orders;
            }
            #endregion

            #region GetOrderByID
            public List<OrderModel> GetOrderByID(int OrderID)
            {
                var order = new List<OrderModel>();
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_SelectByPk";
                command.Parameters.AddWithValue("OrderID", OrderID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    order.Add(new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderNumber = reader["OrderNumber"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        TotalAmount = reader["TotalAmount"].ToString(),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                connection.Close();
                return order;
            }
            #endregion

            #region DeleteByID
            public bool DeleteOrderByID(int OrderID)
            {
                bool isDeleted = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_DeleteByPk";
                command.Parameters.AddWithValue("OrderID", OrderID);
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
                return isDeleted;
            }
            #endregion

            #region InsertOrder
            public bool InsertOrder(OrderModel order)
            {
                bool isInserted = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_Insert";
                command.Parameters.AddWithValue("OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("OrderNumber", order.OrderNumber);
                command.Parameters.AddWithValue("CustomerID", order.CustomerID);
                command.Parameters.AddWithValue("PaymentMode", order.PaymentMode);
                command.Parameters.AddWithValue("TotalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("ShippingAddress", order.ShippingAddress);
                command.Parameters.AddWithValue("UserID", order.UserID);
                int rowsAffected = command.ExecuteNonQuery();
                isInserted = rowsAffected > 0;
                return isInserted;
            }

            #endregion

            #region UpdateOrder
            public bool UpdateOrder(OrderModel order)
            {
                bool isUpdate = false;
                string connectionString = _configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_UpdateByPk";
                command.Parameters.AddWithValue("OrderID", order.OrderID);
                command.Parameters.AddWithValue("OrderDate",order.OrderDate);
                command.Parameters.AddWithValue("OrderNumber", order.OrderNumber);
                command.Parameters.AddWithValue("CustomerID", order.CustomerID);
                command.Parameters.AddWithValue("PaymentMode", order.PaymentMode);
                command.Parameters.AddWithValue("TotalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("ShippingAddress", order.ShippingAddress);
                command.Parameters.AddWithValue("UserID", order.UserID);
                int rowsAffected = command.ExecuteNonQuery();
                isUpdate = rowsAffected > 0;
                return isUpdate;
            }
            #endregion
        }
    }
}
