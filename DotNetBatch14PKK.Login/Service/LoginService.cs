
using DotNetBatch14PKK.Login.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DotNetBatch14PKK.Login.Service;

public class LoginService
{
    private readonly AppDbContext _context;
    public LoginService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel> RegisterAsync(string userName, string password, string email,string rolecode)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Username and password cannot be empty");

        // Remove whitespace from username
        string trimmedUserName = userName.Replace(" ", "");

        // Hash (username + password)
        string combined = $"{trimmedUserName}{password}";
        string hashedPassword = DevCode.HashPassword(combined);

        // Create user entity
        var newUser = new UserModel
        {
            UserID = Guid.NewGuid(),
            UserName = trimmedUserName,
            Email = email,
            RoleCode = rolecode,
            Password = hashedPassword
        };

        // Save to database
        await _context.Users.AddAsync(newUser);
        var result = await _context.SaveChangesAsync();
        return new ResponseModel
        {
            Success = result > 0,
            Message = result > 0 ? "User registered successfully" : "Failed to register user",
            Data = newUser
        };

    }
    public async Task<ResponseModel> SignInAsync(string email, string password)
    {
        // Find user by email
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            user.UserID = Guid.Parse(user.UserID.ToString()); // Assuming UserID is stored as a string in the database
        }
        if (user == null)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Invalid email or password",
                Data = null
            };
        }

        // Combine username + password
        string combined = $"{user.UserName}{password}";
        string hashedInputPassword = DevCode.HashPassword(combined);

        // Compare with stored hashed password
        if (user.Password != hashedInputPassword)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Invalid email or password",
                Data = null
            };
        }

        // Generate Expiry Time (1 minute from now)
        DateTime expireTime = DateTime.Now.AddMinutes(1);

        // Create Token Model
        var tokenModel = new TokenModel
        {
            UserId = user.UserID,
            UserName = user.UserName,
            Email = user.Email,
            UserRoleCode = user.RoleCode,
            ExpireTime = expireTime
        };

        // Convert token model to JSON and encrypt it
        string tokenJson = tokenModel.ToJson();
        string encryptedToken = tokenJson.ToEncrypt();

        // Return encrypted token
        return new ResponseModel
        {
            Success = true,
            Message = "Login successful",
            Data = encryptedToken
        };
    }




}
