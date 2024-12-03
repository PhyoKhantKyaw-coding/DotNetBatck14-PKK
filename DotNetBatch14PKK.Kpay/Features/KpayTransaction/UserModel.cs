using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    [Table("tblUser")]
    public class UserModel
    {
        [Key]
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? MobileNo { get; set; }
        public int? Balance { get; set; }
        public string? Password { get; set; }
    }

    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
