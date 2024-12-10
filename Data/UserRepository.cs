using CoffeeShopApi.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeeShopApi.Data
{
    public class UserRepository
    {
        private IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllUsers
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = reader["UserName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Address = reader["Address"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                });
            }
            connection.Close();
            return users;
        }
        #endregion

        #region GetUserByID
        public List<UserModel> GetUserByID(int UserID)
        {
            var user = new List<UserModel>();
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectByPk";
            command.Parameters.AddWithValue("UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = reader["UserName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Address = reader["Address"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                });
            }
            connection.Close();
            return user;
        }
        #endregion

        #region DeleteByID
        public bool DeleteUserByID(int UserID)
        {
            bool isDeleted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_DeleteByPk";
            command.Parameters.AddWithValue("UserID", UserID);
            int rowsAffected = command.ExecuteNonQuery();
            isDeleted = rowsAffected > 0;
            return isDeleted;
        }
        #endregion

        #region InsertUser
        public bool InsertUser(UserModel user)
        {
            bool isInserted = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Insert";
            command.Parameters.AddWithValue("UserName", user.UserName);
            command.Parameters.AddWithValue("Email", user.Email);
            command.Parameters.AddWithValue("Password", user.Password);
            command.Parameters.AddWithValue("MobileNo", user.MobileNo);
            command.Parameters.AddWithValue("Address", user.Address);
            command.Parameters.AddWithValue("IsActive", user.IsActive);
            int rowsAffected = command.ExecuteNonQuery();
            isInserted = rowsAffected > 0;
            return isInserted;
        }

        #endregion

        #region UpdateUser
        public bool UpdateUser(UserModel user)
        {
            bool isUpdate = false;
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_UpdateByPk";
            command.Parameters.AddWithValue("UserID", user.UserID);
            command.Parameters.AddWithValue("UserName", user.UserName);
            command.Parameters.AddWithValue("Email", user.Email);
            command.Parameters.AddWithValue("Password", user.Password);
            command.Parameters.AddWithValue("MobileNo", user.MobileNo);
            command.Parameters.AddWithValue("Address", user.Address);
            command.Parameters.AddWithValue("IsActive", user.IsActive);
            int rowsAffected = command.ExecuteNonQuery();
            isUpdate = rowsAffected > 0;
            return isUpdate;
        }
        #endregion
    }
}
