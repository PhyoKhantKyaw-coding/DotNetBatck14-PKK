using DotNet_Batch14PKK.Kpay.Features.Users;
using DotNetBatch14PKK.Kpay.Features.User;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    public class EfcoreUserService : IDapperUserService
    {
        private readonly AppDbContent _db;

        public EfcoreUserService()
        {
            _db = new AppDbContent();
        }

        public List<UserModel> GetUsers()
        {
            return _db.users.AsNoTracking().ToList();
        }

        public UserModel GetUser(string mobileNo)
        {
            return _db.users.AsNoTracking().FirstOrDefault(user => user.MobileNo == mobileNo)!;
        }

        public ResponseModel CreateUser(UserModel user)
        {
            user.UserId = Guid.NewGuid().ToString();
            _db.users.Add(user);
            var result = _db.SaveChanges();
            string message = result > 0 ? "User created successfully." : "User creation failed.";
            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }

        public ResponseModel UpdateUser(UserModel user)
        {
            var existingUser = _db.users.AsNoTracking().FirstOrDefault(u => u.MobileNo == user.MobileNo);
            if (existingUser == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "User not found."
                };
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                existingUser.UserName = user.UserName;
            }
            if (user.Balance.HasValue)
            {
                existingUser.Balance = user.Balance;
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = user.Password;
            }

            _db.Entry(existingUser).State = EntityState.Modified;
            var result = _db.SaveChanges();
            string message = result > 0 ? "User updated successfully." : "User update failed.";
            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }

        public ResponseModel Deposit(string mobileNo, int amount, string password)
        {
            var user = _db.users.FirstOrDefault(u => u.MobileNo == mobileNo);
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
                    Message = "Incorrect password."
                };
            }

            user.Balance = (user.Balance ?? 0) + amount;
            _db.Entry(user).State = EntityState.Modified;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Deposit successful." : "Deposit failed.";
            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }

        public ResponseModel Withdraw(string mobileNo, int amount, string password)
        {
            var user = _db.users.FirstOrDefault(u => u.MobileNo == mobileNo);
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
                    Message = "Incorrect password."
                };
            }
            if ((user.Balance ?? 0) < amount)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Insufficient balance."
                };
            }

            user.Balance = (user.Balance ?? 0) - amount;
            _db.Entry(user).State = EntityState.Modified;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Withdrawal successful." : "Withdrawal failed.";
            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }
    }
}
