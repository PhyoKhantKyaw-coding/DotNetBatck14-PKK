
using Microsoft.AspNetCore.Mvc;
namespace DotNetBatch14PKK.Session.Controllers
{
    public class HomeController : Controller
    {
        // Action to set a session value
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("Username", "JohnDoe");
            return Content("Session value set!");
        }

        // Action to retrieve a session value
        public IActionResult GetSession()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return Content("No session value found.");
            }
            return Content($"Hello, {username}!");
        }
    }
}
