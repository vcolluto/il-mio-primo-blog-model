using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace NetCore_01.Controllers
{

    //richieste /Home/*
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

      
        // gestisce /Home/Index     oppure    /Home     oppure /
        public IActionResult Index()
        {
            using(PostContext postContext=new PostContext())
            {
                List<Post> posts = postContext.posts.ToList<Post>();
                return View(posts);
            }
        }


        // gestisce /Home/Privacy
        public IActionResult Privacy()
        {
            return View();                       // restituisce la Vista /Home/Privacy.cshtml
        }

        // gestisce richieste del tipo /Home/Detail?postId=<id>
        public IActionResult Detail(int postId)
        {
            using (PostContext postContext = new PostContext())
            {

                Post post = postContext.posts.First(p => p.Id == postId);
                if (post == null)
                    // return NotFound($"Il post {postId} non esiste!");
                    return View("NotFound", postId);    //vista NotFound.cshtml
                else
                    return View(post);  //vista Detail.cshtml
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}