namespace DotNetBatch14PKK.Login.Models
{
    public class TokenModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRoleCode { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
