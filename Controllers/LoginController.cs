using Microsoft.AspNetCore.Mvc;

namespace PIU.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
