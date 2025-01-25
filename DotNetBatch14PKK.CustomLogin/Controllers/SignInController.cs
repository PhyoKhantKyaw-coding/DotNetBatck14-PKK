using DotNetBatch14PKK.CustomLogin.DevCode;
using DotNetBatch14PKK.CustomLogin.Models;
using Microsoft.AspNetCore.Mvc;
namespace DotNetBatch14PKK.CustomLogin.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult Index(UserModel user)
        {
            // Hash the password for comparison
            string password = user.Password.ToHashPassword(user.Email, "123");

            // Hardcoded user credentials for demonstration
            if (user.Email == "slh@gmail.com" &&
                password == "192193cce00ee219a27de7a7fd4ee5d53f4cc93dd2a1120eab528b2c32b08228")
            {
                // Store the user's email in the session
                HttpContext.Session.SetString("email", user.Email);

                // Store the user's email in a cookie
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1), // Cookie expires in 1 minute
                    HttpOnly = true // Prevent client-side script access to the cookie
                };
                HttpContext.Response.Cookies.Append("email", user.Email, options);

                // Redirect to the home page after successful login
                return RedirectToAction("Index", "Home");
            }

            // If login fails, return to the login page with the user model
            return View(user);
        }

        [HttpPost]
        public IActionResult Logout() 
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Expire the authentication cookie
            Response.Cookies.Delete("email");

            // Redirect to the login page
            return RedirectToAction("SIndex", "SignIn");
        }
    }
}
