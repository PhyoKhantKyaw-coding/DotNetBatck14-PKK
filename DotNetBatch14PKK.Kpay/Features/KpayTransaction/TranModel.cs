using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    [Table("tblTransaction")]
    public class TranModel
    {
        [Key]
        public string? TransactionId { get; set; }
        public string? FromMobileNo { get; set; }
        public string? ToMobileNo { get; set; }
        public string? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Notes { get; set; }
    }

    public class TranResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
