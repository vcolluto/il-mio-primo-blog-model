using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        [Authorize(Roles = "ADMIN,USER")]
        [HttpGet]
        public IActionResult Index(string? category, string? message)
        {
            _logger.WriteLog( "************************* Index - start *********************");
            
                if (message!=null)
                    ViewData["message"]=message;
                List<Post> posts;
            //    if (category == null)
                     posts = _dbContext.posts.ToList<Post>();
          //      else
        //             posts = _dbContext.posts.Where(post => post.Category == category).ToList<Post>();
                _logger.WriteLog("************************* Index - end *********************");
                return View(posts);
            
        }



        [Authorize(Roles = "ADMIN,USER")]  //tutte le richieste devono provenire da un utente autenticato appartenente al ruolo admin o user
        [HttpGet]
        // gestisce richieste del tipo /Posts/Detail?Id=<id>
        public IActionResult Detail(int Id)
        {
            

                Post? post = _dbContext.posts
                    .Where(p => p.Id == Id)
                    .Include(p => p.Category)
                    .Include(p => p.Tags)
                    .FirstOrDefault();
                if (post == null)
                    // return NotFound($"Il post {postId} non esiste!");
                    return View("NotFound", Id);    //vista NotFound.cshtml
                else
                    return View(post);  //vista Detail.cshtml
            
        }

        [Authorize(Roles = "ADMIN")]    //tutte le richieste devono provenire da un utente autenticato appartenente al ruolo admin
        [HttpGet]
        public IActionResult Create()           //visualizza la vista di inserimento Post
        {
            List<Category> categories = _dbContext.categories.ToList();
            PostFormModel model = new PostFormModel();
            model.Post = new Post();
            model.Categories = categories;

            List<Tag> listTags = _dbContext.tags.ToList();      //tutti i tag dai quali posso scegliere (istanze dell'entity)
            
            /*
            List<SelectListItem> selectListTags = new List<SelectListItem>();       //questa lista di SelectListItem verrà utilizzata dalla View per mostrare i tag dai quali posso scegliere (un SelectListItem per ogni istanza di Tag)

            foreach (Tag tag in listTags)
            {
                selectListTags.Add(new SelectListItem() { Text = tag.Title, Value = tag.Id.ToString() });
            }
            model.Tags = selectListTags;
            */
            model.Tags = listTags;      //passo la lista dei tag da cui posso scegliere

            return View(model);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _dbContext.categories.ToList();
                data.Categories = categories;

                //passo di nuovo l'elenco dei tag da cui posso scegliere
                List<Tag> listTags = _dbContext.tags.ToList();      //tutti i tag dai quali posso scegliere (istanze dell'entity)
                /*
                 List<SelectListItem> selectListTags = new List<SelectListItem>();       //questa lista di SelectListItem verrà utilizzata dalla View per mostrare i tag dai quali posso scegliere (un SelectListItem per ogni istanza di Tag)
                 foreach (Tag tag in listTags)
                 {
                     selectListTags.Add(new SelectListItem() { Text = tag.Title, Value = tag.Id.ToString() });
                 }
                 data.Tags = selectListTags;
                */

                data.Tags = listTags;
                return View(data);
            }

            
           
            Post postToCreate = new Post();
            postToCreate.Tags = new List<Tag>();
            postToCreate.Title = data.Post.Title;
            postToCreate.CategoryId = data.Post.CategoryId;
            postToCreate.Description = data.Post.Description;
            postToCreate.Image = data.Post.Image;


            if (data.SelectedTags != null)
            {
                foreach (string selectedBookId in data.SelectedTags)       //data.SelectedTags contiene gli id dei tag scelti dall'utente
                {
                    int selectedIntBookId = int.Parse(selectedBookId);      //per ogni id selezionato recupero il corrispettivo oggetto Tag
                    Tag? tag = _dbContext.tags
                                .Where(t => t.Id == selectedIntBookId)
                                .FirstOrDefault();
                    postToCreate.Tags.Add(tag);
                }
            }
            _dbContext.posts.Add(postToCreate);

            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { message = "Post inserito correttamente" });
            
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Edit(int Id)           //visualizza la vista di inserimento Post
        {
           

            Post? post = _dbContext.posts.Include(p => p.Tags).FirstOrDefault(p => p.Id == Id);     // per ogni post carica anche i tag associati (Include)

            if (post == null)

                // return NotFound($"Il post {postId} non esiste!");
                return View("NotFound", Id);    //vista NotFound.cshtml
            else
            {
                List<Category> categories = _dbContext.categories.ToList();

                PostFormModel postFormModel = new PostFormModel();

                postFormModel.Post = post;
                postFormModel.Categories = categories;

                List<Tag> listTags = _dbContext.tags.ToList();      //tutti i tag dai quali posso scegliere (istanze dell'entity)
                postFormModel.Tags = listTags;

                postFormModel.SelectedTags = new List<string>();        //passo al modello l'elenco degli id dei tag associati al post 
                foreach (Tag tag in post.Tags) 
                {
                    postFormModel.SelectedTags.Add(tag.Id.ToString());
                }

                return View(postFormModel);  //vista Edit.cshtml
            }
                   
            
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _dbContext.categories.ToList();
                data.Categories = categories;
                //passo di nuovo l'elenco dei tag da cui posso scegliere
                List<Tag> listTags = _dbContext.tags.ToList();      //tutti i tag dai quali posso scegliere (istanze dell'entity)
                data.Tags = listTags;
                return View(data);
            }    
            

            Post postToEdit = _dbContext.posts.Include(p => p.Tags).First(p => p.Id == data.Post.Id);       //carico il post da modificare includendo anche i tag associati
            postToEdit.Title = data.Post.Title;
            postToEdit.CategoryId = data.Post.CategoryId;
            postToEdit.Description = data.Post.Description;
            postToEdit.Image = data.Post.Image;
            postToEdit.Tags.Clear();        //svuoto i tag già associati precedentemente alla modifica (perché l'utente potrebbe aver selezionato altri tag)

           
            if (data.SelectedTags != null)      //ripopolo l'insieme dei tag associati in base all'elenco di id che mi restituisce la View
            {
                foreach (string selectedBookId in data.SelectedTags)       //data.SelectedTags contiene gli id dei tag scelti dall'utente
                {
                    int selectedIntBookId = int.Parse(selectedBookId);      //per ogni id selezionato recupero il corrispettivo oggetto Tag
                    Tag? tag = _dbContext.tags
                                .Where(t => t.Id == selectedIntBookId)
                                .FirstOrDefault();
                    postToEdit.Tags.Add(tag);
                }
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { message = "Post aggiornato correttamente" });
            
        }

        [Authorize(Roles = "ADMIN")]
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