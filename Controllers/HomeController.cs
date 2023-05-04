using Microsoft.AspNetCore.Mvc;

namespace NetCore_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // gestisce /Home/Privacy
        public IActionResult Privacy()
        {
            return View();                       // restituisce la Vista /Home/Privacy.cshtml
        }
    }
}
