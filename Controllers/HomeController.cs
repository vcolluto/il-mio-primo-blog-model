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

      
   

        public IActionResult Index(string? category)
        {
            using (PostContext postContext = new PostContext())
            {
                List<Post> posts;
                if (category == null)
                     posts = postContext.posts.ToList<Post>();
                else
                    posts = postContext.posts.Where(post => post.Category == category).ToList<Post>();
                return View(posts);
            }
        }


        // gestisce /Home/Privacy
        public IActionResult Privacy()
        {
            return View();                       // restituisce la Vista /Home/Privacy.cshtml
        }

        // gestisce richieste del tipo /Home/Detail?Id=<id>
        public IActionResult Detail(int Id)
        {
            using (PostContext postContext = new PostContext())
            {

                Post post = postContext.posts.First(p => p.Id == Id);
                if (post == null)
                    // return NotFound($"Il post {postId} non esiste!");
                    return View("NotFound", Id);    //vista NotFound.cshtml
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