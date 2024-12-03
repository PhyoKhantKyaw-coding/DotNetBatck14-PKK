using Dapper;
using DotNetBatch14PKK.Kpay.Features.KpayTransaction;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14PKK.Kpay.Features.User
{
    public class DapperUserService : IDapperUserService
    {
        private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;

        public List<UserModel> GetUsers()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM tblUser";
            var users = connection.Query<UserModel>(query).ToList();
            return users;
        }

        public UserModel GetUser(string mobileNo)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM tblUser WHERE MobileNo = @MobileNo";
            var user = connection.QueryFirstOrDefault<UserModel>(query, new { MobileNo = mobileNo });
            return user!;
        }

        public ResponseModel CreateUser(UserModel requestModel)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = @"
                INSERT INTO tblUser (UserName, MobileNo, Balance, Password)
                VALUES (@UserName, @MobileNo, @Balance, @Password)";
            var result = connection.Execute(query, requestModel);

            string message = result > 0 ? "Saving successful." : "Saving failed.";
            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }

        public ResponseModel Deposit(string mobileNo, int depositAmount, string password)
        {
            var user = GetUser(mobileNo);
            if (user == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "User not found."
                };
            }

            if (user.Password != password)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "The password is not correct."
                };
            }

            user.Balance += depositAmount;
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = @"
                UPDATE tblUser
                SET Balance = @Balance
                WHERE MobileNo = @MobileNo";
            var result = connection.Execute(query, new { Balance = user.Balance, MobileNo = mobileNo });

            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = result > 0 ? "Deposit successful." : "Deposit failed."
            };
        }

        public ResponseModel Withdraw(string mobileNo, int withdrawAmount, string password)
        {
            var user = GetUser(mobileNo);
            if (user == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "User not found."
                };
            }

            if (user.Password != password)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "The password is not correct."
                };
            }

            if (user.Balance < withdrawAmount)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Insufficient balance."
                };
            }

            user.Balance -= withdrawAmount;
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = @"
                UPDATE tblUser
                SET Balance = @Balance
                WHERE MobileNo = @MobileNo";
            var result = connection.Execute(query, new { Balance = user.Balance, MobileNo = mobileNo });

            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = result > 0 ? "Withdrawal successful." : "Withdrawal failed."
            };
        }
    }
}
