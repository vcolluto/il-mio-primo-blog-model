using Microsoft.AspNetCore.Mvc;

namespace NetCore_01.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // gestisce /Home/Privacy
        public IActionResult Privacy()
        {
            return View();                       // restituisce la Vista /Home/Privacy.cshtml
        }
    }
}
