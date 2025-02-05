namespace TravelAgency.Models
{
    public class LoginRequestModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class UserRequestModel
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Phone { get; set; } = null!;

    }
}
