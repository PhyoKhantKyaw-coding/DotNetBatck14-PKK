using Microsoft.EntityFrameworkCore;
using DotNetBatch14PKK.Kpay.Features.KpayTransaction;
using DotNetBatch14PKK.Kpay.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNet_Batch14PKK.Kpay.Features.Users;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    public class EfcoreTranServices : IDapperTranServices
    {
        private readonly AppDbContent _db;
        private readonly EfcoreUserService _userServices;

        public EfcoreTranServices()
        {
            _db = new AppDbContent();
            _userServices = new EfcoreUserService(); 
        }

        public TranResponseModel Transaction(
            string fromMobileNo,
            string toMobileNo,
            int amount,
            DateTime transactionDate,
            string notes,
            string password)
        {
            #region Check From User and Password
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

            #region Check To User and Balance
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

            #region Update Database using EF Core
            _db.users.Update(fromUser);
            _db.users.Update(toUser);

            var transaction = new TranModel
            {
                TransactionId = Guid.NewGuid().ToString(),
                FromMobileNo = fromMobileNo,
                ToMobileNo = toMobileNo,
                Amount = amount.ToString(),
                TransactionDate = transactionDate,
                Notes = notes
            };
            _db.trans.Add(transaction);

            _db.SaveChanges();
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
            try
            {
                var transactionHistory = _db.trans
                    .Where(t => t.FromMobileNo == mobileNo || t.ToMobileNo == mobileNo)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();

                return transactionHistory;
            }
            catch (Exception ex)
            {
                return new List<TranModel> { new TranModel { Notes = $"Error: {ex.Message}" } };
            }
        }
        #endregion
    }
}
