using CoffeeShopApi.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CoffeeShopApi.Data
{
    public class BillRepository
    {
        private IConfiguration _configuration;
        public BillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllBills
        public List<BillModel> GetAllBills()
        {
            var bills = new List<BillModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bills.Add(new BillModel
                {
                    BillID = Convert.ToInt32(reader["BillID"]),
                    BillNumber = reader["BillNumber"].ToString(),
                    BillDate = Convert.ToDateTime(reader["BillDate"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    NetAmount = Convert.ToInt32(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])
                });
            }
            connection.Close();
            return bills;
        }
        #endregion

        #region GetBillByID
        public List<BillModel> GetBillByID(int BillID)
        {
            var bill = new List<BillModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectByPk";
            command.Parameters.AddWithValue("BillID", BillID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bill.Add(new BillModel
                {
                    BillID = Convert.ToInt32(reader["BillID"]),
                    BillNumber = reader["BillNumber"].ToString(),
                    BillDate = Convert.ToDateTime(reader["BillDate"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                    Discount = Convert.ToInt32(reader["Discount"]),
                    NetAmount = Convert.ToInt32(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])
                });
            }
            connection.Close();
            return bill;
        }
        #endregion

        #region DeleteByID
        public bool DeleteBillByID(int BillID)
        {
            bool isDeleted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_DeleteByPk";
            command.Parameters.AddWithValue("BillID", BillID);
            int rowsAffected = command.ExecuteNonQuery();
            isDeleted = rowsAffected > 0;
            return isDeleted;
        }
        #endregion

        #region InsertBill
        public bool InsertBill(BillModel bill)
        {
            bool isInserted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_Insert";
            command.Parameters.AddWithValue("BillDate", bill.BillDate);
            command.Parameters.AddWithValue("BillNumber", bill.BillNumber);
            command.Parameters.AddWithValue("TotalAmount", bill.TotalAmount);
            command.Parameters.AddWithValue("UserID", bill.UserID);
            command.Parameters.AddWithValue("OrderID", bill.OrderID);
            command.Parameters.AddWithValue("Discount", bill.Discount);
            command.Parameters.AddWithValue("NetAmount", bill.NetAmount);
            int rowsAffected = command.ExecuteNonQuery();
            isInserted = rowsAffected > 0;
            return isInserted;
        }

        #endregion

        #region UpdateBill
        public bool UpdateBill(BillModel bill)
        {
            bool isUpdate = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_UpdateByPk";
            command.Parameters.AddWithValue("BillID", bill.BillID);
            command.Parameters.AddWithValue("BillDate", bill.BillDate);
            command.Parameters.AddWithValue("BillNumber", bill.BillNumber);
            command.Parameters.AddWithValue("TotalAmount", bill.TotalAmount);
            command.Parameters.AddWithValue("UserID", bill.UserID);
            command.Parameters.AddWithValue("OrderID", bill.OrderID);
            command.Parameters.AddWithValue("Discount", bill.Discount);
            command.Parameters.AddWithValue("NetAmount", bill.NetAmount);
            int rowsAffected = command.ExecuteNonQuery();
            isUpdate = rowsAffected > 0;
            return isUpdate;
        }
        #endregion
    }
}
