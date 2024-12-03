using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using DotNetBatch14PKK.Kpay.Features.User;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    public class DapperTranServices : IDapperTranServices
    {
        private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
        private readonly DapperUserService _userService = new();

        public TranResponseModel Transaction(
            string fromMobileNo,
            string toMobileNo,
            int amount,
            DateTime transactionDate,
            string notes,
            string password)
        {
            #region Check Sender (From User)
            var fromUser = _userService.GetUser(fromMobileNo);
            if (fromUser is null)
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

            #region Check Receiver (To User) and Balance
            var toUser = _userService.GetUser(toMobileNo);
            if (toUser is null)
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

            using IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string updateFromQuery = @"
                    UPDATE [dbo].[tblUser]
                    SET [Balance] = @Balance
                    WHERE [MobileNo] = @MobileNo";
                connection.Execute(updateFromQuery, new { Balance = fromUser.Balance, MobileNo = fromMobileNo }, transaction);

                string updateToQuery = @"
                    UPDATE [dbo].[tblUser]
                    SET [Balance] = @Balance
                    WHERE [MobileNo] = @MobileNo";
                connection.Execute(updateToQuery, new { Balance = toUser.Balance, MobileNo = toMobileNo }, transaction);

                string insertTransactionQuery = @"
                    INSERT INTO [dbo].[tblTransaction]
                    ([FromMobileNo], [ToMobileNo], [Amount], [TransactionDate], [Notes])
                    VALUES (@FromMobileNo, @ToMobileNo, @Amount, @TransactionDate, @Notes)";
                connection.Execute(insertTransactionQuery, new
                {
                    FromMobileNo = fromMobileNo,
                    ToMobileNo = toMobileNo,
                    Amount = amount,
                    TransactionDate = transactionDate,
                    Notes = notes
                }, transaction);

                transaction.Commit();

                return new TranResponseModel
                {
                    IsSuccessful = true,
                    Message = "Transaction successful."
                };
            }
            catch
            {
                transaction.Rollback();
                return new TranResponseModel
                {
                    IsSuccessful = false,
                    Message = "Transaction failed."
                };
            }
        }

        public List<TranModel> GetTransactionHistory(string mobileNo)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT * FROM [dbo].[tblTransaction]
                WHERE [FromMobileNo] = @MobileNo OR [ToMobileNo] = @MobileNo
                ORDER BY [TransactionDate] DESC";

            var transactions = connection.Query<TranModel>(query, new { MobileNo = mobileNo }).ToList();
            return transactions;
        }
    }
}
