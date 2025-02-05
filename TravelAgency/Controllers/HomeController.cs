using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TravelService _travelService;
        private readonly LoginService _loginService;

        public HomeController(TravelService travelService, LoginService log)
        {
            _travelService = travelService;
            _loginService = log;
        }

        [HttpGet("travel-packages")]
        public async Task<IActionResult> GetTravelPackages()
        {
            try
            {
                var packages = await _travelService.GetTravelPackages();
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }
        [HttpGet("travel-package/{id}")]
        public async Task<IActionResult> GetTravelPackage(string id)
        {
            try
            {
                var package = await _travelService.GetPackagebyId(id);
                return Ok(package);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        // Register User
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestModel user)
        {
            var response = await _loginService.RegisterUser(user);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // Register Admin
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRequestModel admin)
        {
            var response = await _loginService.RegisterAdmin(admin);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // Sign In
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequestModel loginRequest)
        {
            var response = await _loginService.SignIn(loginRequest.Email, loginRequest.Password);
            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }
        [HttpGet("User-id")]
        public async Task<IActionResult> GetUserId(string id)
        {
            try
            {
                var user = await _loginService.GetUserbyId(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }
    }
}
