using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
