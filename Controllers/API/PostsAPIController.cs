using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Linq;

namespace NetCore_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsAPIController : ControllerBase       // gestisce le richieste /api/PostsAPI/*
    {
        private PostContext _dbContext;

        public PostsAPIController(PostContext dbContext)
        {
            _dbContext = dbContext;     //viene iniettato tramite Dependency Injection
        }


        //ELENCO DEI POST
        [HttpGet]
        public IActionResult Index(string? search)        // gestisce richieste GET /api/PostsAPI  oppure    GET /api/PostsAPI?search=<qualcosa>
        {          
            List<Post> posts;           
            posts = _dbContext.posts.Include(p => p.Tags).ToList<Post>();  

            if (search!=null)   // se è stata specificata la chiave di ricerca
            {
                posts= posts.Where(p => p.Title.ToLower().Contains(search.ToLower())).ToList();         //prendo da tutti i post solo quelli il cui titolo contiene la stringa di ricerca
                //ToLower() : converti il titolo in minuscolo
                //Contains(): la stringa contiene...
            }
            return Ok(posts);               //restituisce un elenco di post serializzato in JSON con esito httpstatus 200 (OK)
        }


        //CREAZIONE NUOVO POST
        [HttpPost]
        public IActionResult CreaPost([FromBody] Post data)      // gestisce richieste POST /api/PostsAPI
        {
            Post postToCreate = new Post();
            postToCreate.Tags = new List<Tag>();
            postToCreate.Title = data.Title;
            postToCreate.CategoryId = data.CategoryId;
            postToCreate.Description = data.Description;
            postToCreate.Image = data.Image;


            if (data.Tags != null)
            {
                foreach (Tag t in data.Tags)       //per ogni oggetto Tag che mi arriva dal JSON prendo il suo id (t.Id)
                {
                    Tag? tag = _dbContext.tags
                                .Where(x => x.Id == t.Id)
                                .FirstOrDefault();          //recupero l'oggetto Tag dal DB attraverso il suo Id  (t.Id)
                    postToCreate.Tags.Add(tag);             //lo aggiungo al post che devo creare
                }
            }
            _dbContext.posts.Add(data);     //riporto le modifiche sul DB

            _dbContext.SaveChanges();

            return Ok();
        }


        //DETTAGLIO POST
        [HttpGet("{id}")]
        public IActionResult Detail(int Id)     // gestisce richieste POST /api/PostsAPI/<id>
        {
            Post? post = _dbContext.posts
                .Where(p => p.Id == Id)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .FirstOrDefault();
            if (post == null)
                return NotFound($"Il post {Id} non esiste!");               
            else
                return Ok(post);  //restituisce il JSON relativo al singolo post
        }

        //AGGIORNAMENTO (DI UN POST DEL BLOG)
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Post data)
        {
           

            Post? postToEdit = _dbContext.posts.Include(p => p.Tags).FirstOrDefault(p => p.Id == id);       //carico il post da modificare includendo anche i tag associati
            if (postToEdit==null)
                return NotFound($"Il post {id} non esiste!");
            else
            {
                postToEdit.Title = data.Title;
                postToEdit.CategoryId = data.CategoryId;
                postToEdit.Description = data.Description;
                postToEdit.Image = data.Image;
                postToEdit.Tags.Clear();        //svuoto i tag già associati precedentemente alla modifica (perché l'utente potrebbe aver selezionato altri tag)


                if (data.Tags != null)
                {
                    foreach (Tag t in data.Tags)       //per ogni oggetto Tag che mi arriva dal JSON prendo il suo id (t.Id)
                    {
                        Tag? tag = _dbContext.tags
                                    .Where(x => x.Id == t.Id)
                                    .FirstOrDefault();          //recupero l'oggetto Tag dal DB attraverso il suo Id  (t.Id)
                        postToEdit.Tags.Add(tag);             //lo aggiungo al post che devo creare
                    }
                }

                _dbContext.SaveChanges();

                return Ok();
            }
            

        }

        //CANCELLAZIONE POST
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            Post? postToDelete = _dbContext.posts.Where(post => post.Id == id).FirstOrDefault();

            if (postToDelete != null)
            {
                _dbContext.posts.Remove(postToDelete);

                _dbContext.SaveChanges();


                return Ok("Post eliminato correttamente");
            }
            else
            {
                return NotFound();
            }

        }

    }
}
