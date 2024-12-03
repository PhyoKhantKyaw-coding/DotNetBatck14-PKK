using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    public class TranServices : IDapperTranServices
    {
        private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
        private readonly UserServices _userServices = new();

        public TranResponseModel Transaction(
            string fromMobileNo,
            string toMobileNo,
            int amount,
            DateTime transactionDate,
            string notes,
            string password)
        {
            #region check from mobile no user and password
            var fromUser = _userServices.GetUser(fromMobileNo);
            if (fromUser == null)
            {
                return new TranResponseModel
                {
                    IsSuccessful = false,
                    Message = "From user not found."
                };
            }
            if (fromUser.Password != password)
            {
                return new TranResponseModel
                {
                    IsSuccessful = false,
                    Message = "The password is not correct."
                };
            }
            #endregion
            #region check to mobile no user and check fromuser has sufficient balance
            var toUser = _userServices.GetUser(toMobileNo);
            if (toUser == null)
            {
                return new TranResponseModel
                {
                    IsSuccessful = false,
                    Message = "To user not found."
                };
            }
            if ((fromUser.Balance ?? 0) < amount)
            {
                return new TranResponseModel
                {
                    IsSuccessful = false,
                    Message = "Insufficient balance in the sender's account."
                };
            }
            #endregion
            fromUser.Balance -= amount;
            toUser.Balance += amount;

            #region update database 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string updateFromQuery = @"
                    UPDATE [dbo].[tblUser]
                    SET [Balance] = @Balance
                    WHERE [MobileNo] = @MobileNo";
                using (SqlCommand cmd = new(updateFromQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MobileNo", fromMobileNo);
                    cmd.Parameters.AddWithValue("@Balance", fromUser.Balance);
                    cmd.ExecuteNonQuery();
                }

                string updateToQuery = @"
                    UPDATE [dbo].[tblUser]
                    SET [Balance] = @Balance
                    WHERE [MobileNo] = @MobileNo";
                using (SqlCommand cmd = new(updateToQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MobileNo", toMobileNo);
                    cmd.Parameters.AddWithValue("@Balance", toUser.Balance);
                    cmd.ExecuteNonQuery();
                }

                string insertTransactionQuery = @"
                    INSERT INTO [dbo].[tblTransaction]
                    ([FromMobileNo], [ToMobileNo], [Amount], [TransactionDate], [Notes])
                    VALUES (@FromMobileNo, @ToMobileNo, @Amount, @TransactionDate, @Notes)";
                using (SqlCommand cmd = new(insertTransactionQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@FromMobileNo", fromMobileNo);
                    cmd.Parameters.AddWithValue("@ToMobileNo", toMobileNo);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.ExecuteNonQuery();
                }

            }
            #endregion
            return new TranResponseModel
            {
                IsSuccessful = true,
                Message = "Transaction successful."
            };
        }
        #region History
        public List<TranModel> GetTransactionHistory(string mobileNo)
        {
            List<TranModel> transactionHistory = new();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT * FROM [dbo].[tblTransaction]
                    WHERE [FromMobileNo] = @MobileNo OR [ToMobileNo] = @MobileNo
                    ORDER BY [TransactionDate] DESC";
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);

                    using SqlDataAdapter adapter = new(cmd);
                    DataTable dt = new();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        transactionHistory.Add(new TranModel
                        {
                            TransactionId = row["TransactionId"].ToString(),
                            FromMobileNo = row["FromMobileNo"].ToString(),
                            ToMobileNo = row["ToMobileNo"].ToString(),
                            Amount = row["Amount"].ToString(),
                            TransactionDate = Convert.ToDateTime(row["TransactionDate"]),
                            Notes = row["Notes"].ToString()
                        });
                    }
                }
            }
            #endregion
            return transactionHistory;
        }
    }
}
