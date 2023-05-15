using Microsoft.AspNetCore.Mvc;

namespace NetCore_01.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }

}
