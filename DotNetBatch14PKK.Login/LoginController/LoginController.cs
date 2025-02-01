using DotNetBatch14PKK.Login.Service;
using DotNetBatch14PKK.Login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.Login.LoginController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new ResponseModel { Success = false, Message = "Invalid input data." });
            }

            var response = await _loginService.RegisterAsync(request.UserName, request.Password, request.Email, request.RoleCode);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // User login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new ResponseModel { Success = false, Message = "Invalid email or password." });
            }

            var response = await _loginService.SignInAsync(request.Email, request.Password);
            return response.Success ? Ok(response) : Unauthorized(response);
        }
    }



}