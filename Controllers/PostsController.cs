using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace NetCore_01.Controllers
{

    //richieste /Posts/*
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
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

    


        [HttpGet]
        // gestisce richieste del tipo /Posts/Detail?Id=<id>
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