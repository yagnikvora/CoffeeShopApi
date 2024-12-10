using CoffeeShopApi.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeeShopApi.Data
{
    public class CustomerRepository
    {
        private IConfiguration _configuration;
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllCustomers
        public List<CustomerModel> GetAllCustomers()
        {
            var customers = new List<CustomerModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new CustomerModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString(),
                    Email = reader["Email"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    GSTNO = reader["GSTNO"].ToString(),
                    CityName = reader["CityName"].ToString(),
                    PinCode = reader["PinCode"].ToString(),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                });
            }
            connection.Close();
            return customers;
        }
        #endregion

        #region GetCustomerByID
        public List<CustomerModel> GetCustomerByID(int CustomerID)
        {
            var customer = new List<CustomerModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectByPk";
            command.Parameters.AddWithValue("CustomerID", CustomerID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer.Add(new CustomerModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString(),
                    Email = reader["Email"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    GSTNO = reader["GSTNO"].ToString(),
                    CityName = reader["CityName"].ToString(),
                    PinCode = reader["PinCode"].ToString(),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                });
            }
            connection.Close();
            return customer;
        }
        #endregion

        #region DeleteByID
        public bool DeleteCustomerByID(int CustomerID)
        {
            bool isDeleted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_DeleteByPk";
            command.Parameters.AddWithValue("CustomerID", CustomerID);
            int rowsAffected = command.ExecuteNonQuery();
            isDeleted = rowsAffected > 0;
            return isDeleted;
        }
        #endregion

        #region InsertCustomer
        public bool InsertCustomer(CustomerModel customer)
        {
            bool isInserted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_Insert";
            command.Parameters.AddWithValue("CustomerName", customer.CustomerName);
            command.Parameters.AddWithValue("HomeAddress", customer.HomeAddress);
            command.Parameters.AddWithValue("Email", customer.Email);
            command.Parameters.AddWithValue("MobileNo", customer.MobileNo);
            command.Parameters.AddWithValue("GSTNO", customer.GSTNO);
            command.Parameters.AddWithValue("CityName", customer.CityName);
            command.Parameters.AddWithValue("PinCode", customer.PinCode);
            command.Parameters.AddWithValue("NetAmount", customer.NetAmount);
            command.Parameters.AddWithValue("UserID", customer.UserID);
            int rowsAffected = command.ExecuteNonQuery();
            isInserted = rowsAffected > 0;
            return isInserted;
        }

        #endregion

        #region UpdateCustomer
        public bool UpdateCustomer(CustomerModel customer)
        {
            bool isUpdate = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_UpdateByPk";
            command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            command.Parameters.AddWithValue("CustomerName", customer.CustomerName);
            command.Parameters.AddWithValue("HomeAddress", customer.HomeAddress);
            command.Parameters.AddWithValue("Email", customer.Email);
            command.Parameters.AddWithValue("MobileNo", customer.MobileNo);
            command.Parameters.AddWithValue("GSTNO", customer.GSTNO);
            command.Parameters.AddWithValue("CityName", customer.CityName);
            command.Parameters.AddWithValue("PinCode", customer.PinCode);
            command.Parameters.AddWithValue("NetAmount", customer.NetAmount);
            command.Parameters.AddWithValue("UserID", customer.UserID);
            int rowsAffected = command.ExecuteNonQuery();
            isUpdate = rowsAffected > 0;
            return isUpdate;
        }
        #endregion
    }
}
