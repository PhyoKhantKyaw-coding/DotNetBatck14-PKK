
namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    public interface IDapperTranServices
    {
        List<TranModel> GetTransactionHistory(string mobileNo);
        TranResponseModel Transaction(string fromMobileNo, string toMobileNo, int amount, DateTime transactionDate, string notes, string password);
    }
}