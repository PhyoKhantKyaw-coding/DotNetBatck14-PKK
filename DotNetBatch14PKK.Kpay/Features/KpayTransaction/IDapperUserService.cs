using DotNetBatch14PKK.Kpay.Features.KpayTransaction;

namespace DotNetBatch14PKK.Kpay.Features.User
{
    public interface IDapperUserService
    {
        ResponseModel CreateUser(UserModel requestModel);
        ResponseModel Deposit(string mobileNo, int depositAmount, string password);
        UserModel GetUser(string mobileNo);
        List<UserModel> GetUsers();
        ResponseModel Withdraw(string mobileNo, int withdrawAmount, string password);
    }
}