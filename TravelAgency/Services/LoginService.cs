using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;
using System.Security.Cryptography;
using System.Text;

namespace TravelAgency.Services
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        // Hashing password using SHA256
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        // Register User (Customer)
        public async Task<ResponseLoginModel> RegisterUser(UserRequestModel user)
        {
            var existingUser = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return new ResponseLoginModel { Success = false, Message = "Email already exists", Data = null, Role = null };
            }

            var model = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = user.Name,
                Email = user.Email,
                PasswordHash = HashPassword(user.PasswordHash),
                Phone = user.Phone,
                Role = "User"
            };

            _context.User.Add(model);
            await _context.SaveChangesAsync();

            return new ResponseLoginModel { Success = true, Message = "User registered successfully", Data = model, Role = model.Role };
        }

        // Register Admin
        public async Task<ResponseLoginModel> RegisterAdmin(UserRequestModel admin)
        {
            var existingAdmin = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == admin.Email);
            if (existingAdmin != null)
            {
                return new ResponseLoginModel { Success = false, Message = "Admin email already exists", Data = null, Role = null };
            }

            var model = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = admin.Name,
                Email = admin.Email,
                PasswordHash = HashPassword(admin.PasswordHash),
                Phone = admin.Phone,
                Role = "Admin"
            };

            _context.User.Add(model);
            await _context.SaveChangesAsync();

            return new ResponseLoginModel { Success = true, Message = "Admin registered successfully", Data = model, Role = model.Role };
        }

        // Sign In Method
        public async Task<ResponseLoginModel> SignIn(string email, string password)
        {
            var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new ResponseLoginModel { Success = false, Message = "Invalid email or password", Data = null, Role = null };
            }

            // Verify password
            var hashedPassword = HashPassword(password);
            if (user.PasswordHash != hashedPassword)
            {
                return new ResponseLoginModel { Success = false, Message = "Invalid email or password", Data = null, Role = null };
            }

            // Set role-specific message
            string message = user.Role switch
            {
                "Admin" => "Admin login successful",
                "User" => "User login successful",
                _ => "Login successful"
            };

            return new ResponseLoginModel { Success = true, Message = message, Data = user, Role = user.Role };
        }
    }
}
