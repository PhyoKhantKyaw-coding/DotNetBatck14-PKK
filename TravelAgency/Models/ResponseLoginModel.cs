namespace TravelAgency.Models
{
    public class ResponseLoginModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public UserModel? Data { get; set; }
        public string? Role { get; set; }
    }
}
