namespace DotNetBatch14PKK.Login.Models
{
    public class UserRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleCode { get; set; }
    }
}
