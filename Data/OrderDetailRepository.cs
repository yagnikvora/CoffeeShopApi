using CoffeeShopApi.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeeShopApi.Data
{
    public class OrderDetailRepository
    {
        private IConfiguration _configuration;
        public OrderDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllOrderDetails
        public List<OrderDetailModel> GetAllOrderDetails()
        {
            var orderDetail = new List<OrderDetailModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderDetail.Add(new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                });
            }
            connection.Close();
            return orderDetail;
        }
        #endregion

        #region GetOrderDetailByID
        public List<OrderDetailModel> GetOrderDetailByID(int OrderDetailID)
        {
            var orderDetail = new List<OrderDetailModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectByPk";
            command.Parameters.AddWithValue("OrderDetailID", OrderDetailID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderDetail.Add(new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                });
            }
            connection.Close();
            return orderDetail;
        }
        #endregion

        #region DeleteByID
        public bool DeleteOrderDetailByID(int OrderDetailID)
        {
            bool isDeleted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_DeleteByPk";
            command.Parameters.AddWithValue("OrderDetailID", OrderDetailID);
            int rowsAffected = command.ExecuteNonQuery();
            isDeleted = rowsAffected > 0;
            return isDeleted;
        }
        #endregion

        #region InsertOrderDetail
        public bool InsertOrderDetail(OrderDetailModel orderDetail)
        {
            bool isInserted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_Insert";
            command.Parameters.AddWithValue("OrderID", orderDetail.OrderID);
            command.Parameters.AddWithValue("ProductID", orderDetail.ProductID);
            command.Parameters.AddWithValue("Quantity", orderDetail.Quantity);
            command.Parameters.AddWithValue("Amount", orderDetail.Amount);
            command.Parameters.AddWithValue("TotalAmount", orderDetail.TotalAmount);
            command.Parameters.AddWithValue("UserID", orderDetail.UserID);
            int rowsAffected = command.ExecuteNonQuery();
            isInserted = rowsAffected > 0;
            return isInserted;
        }

        #endregion

        #region UpdateOrderDetail
        public bool UpdateOrderDetail(OrderDetailModel orderDetail)
        {
            bool isUpdate = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_UpdateByPk";
            command.Parameters.AddWithValue("OrderDetailID", orderDetail.OrderDetailID);
            command.Parameters.AddWithValue("OrderID", orderDetail.OrderID);
            command.Parameters.AddWithValue("ProductID", orderDetail.ProductID);
            command.Parameters.AddWithValue("Quantity", orderDetail.Quantity);
            command.Parameters.AddWithValue("Amount", orderDetail.Amount);
            command.Parameters.AddWithValue("TotalAmount", orderDetail.TotalAmount);
            command.Parameters.AddWithValue("UserID", orderDetail.UserID);
            int rowsAffected = command.ExecuteNonQuery();
            isUpdate = rowsAffected > 0;
            return isUpdate;
        }
        #endregion
    }
}
