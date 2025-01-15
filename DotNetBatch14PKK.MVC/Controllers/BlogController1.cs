using DotNet_Batch14PKK.BlogShare;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.MVC.Controllers
{
    public class BlogController1 : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogController1(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        [ActionName("Index")]
        public IActionResult BlogList()
        {
            var lst = _blogServices.GetBlogs();
            return View("BlogList",lst);
        }
    }
}
