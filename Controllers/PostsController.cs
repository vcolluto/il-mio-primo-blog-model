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
        //private readonly ILogger<PostsController> _logger;
        private ICustomLogger _logger;
        private PostContext _dbContext;

        public PostsController(ICustomLogger logger, PostContext dbContext)        //mi viene passata da qualcuno (il framework)
        {
            // _logger = logger;
            _logger = logger;        //non faccio la new
            _dbContext = dbContext; //non faccio la new
        }



        [HttpGet]
        public IActionResult Index(string? category, string? message)
        {
            _logger.WriteLog( "************************* Index - start *********************");
            
                if (message!=null)
                    ViewData["message"]=message;
                List<Post> posts;
                if (category == null)
                     posts = _dbContext.posts.ToList<Post>();
                else
                     posts = _dbContext.posts.Where(post => post.Category == category).ToList<Post>();
                _logger.WriteLog("************************* Index - end *********************");
                return View(posts);
            
        }

    


        [HttpGet]
        // gestisce richieste del tipo /Posts/Detail?Id=<id>
        public IActionResult Detail(int Id)
        {
            

                Post post = _dbContext.posts.First(p => p.Id == Id);
                if (post == null)
                    // return NotFound($"Il post {postId} non esiste!");
                    return View("NotFound", Id);    //vista NotFound.cshtml
                else
                    return View(post);  //vista Detail.cshtml
            
        }

        [HttpGet]
        public IActionResult Create()           //visualizza la vista di inserimento Post
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            
           
            Post postToCreate = new Post();
            postToCreate.Title = data.Title;
            postToCreate.Category = data.Category;
            postToCreate.Description = data.Description;
            postToCreate.Image = data.Image;

            _dbContext.posts.Add(postToCreate);

            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { message = "Post inserito correttamente" });
            
        }

        [HttpGet]
        public IActionResult Edit(int Id)           //visualizza la vista di inserimento Post
        {
           

            Post? post = _dbContext.posts.FirstOrDefault(p => p.Id == Id);
            if (post == null)
                // return NotFound($"Il post {postId} non esiste!");
                return View("NotFound", Id);    //vista NotFound.cshtml
            else
            {
                List<string> elencoCategorie = new List<string>() { "Informazioni", "Saluti", "Generico" };
                ViewData["elencoCategorie"]=elencoCategorie;
                return View(post);  //vista Edit.cshtml
            }
                   
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post data)
        {
            if (!ModelState.IsValid)
            {
                List<string> elencoCategorie = new List<string>() { "Informazioni", "Saluti", "Generico" };
                ViewData["elencoCategorie"] = elencoCategorie;
                return View(data);
            }

            
            Post postToEdit = _dbContext.posts.First(p => p.Id == data.Id);
            postToEdit.Title = data.Title;
            postToEdit.Category = data.Category;
            postToEdit.Description = data.Description;
            postToEdit.Image = data.Image;

            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { message = "Post aggiornato correttamente" });
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
                Post? postToDelete = _dbContext.posts.Where(post => post.Id == id).FirstOrDefault();

                if (postToDelete != null)
                {
                    _dbContext.posts.Remove(postToDelete);

                    _dbContext.SaveChanges();
                   

                    return RedirectToAction("Index",new { message= "Post eliminato correttamente" });
                }
                else
                {
                    return NotFound();
                }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}