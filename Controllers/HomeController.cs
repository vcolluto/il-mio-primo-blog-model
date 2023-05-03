using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Diagnostics;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Notizia
    {
        public string Titolo { get; set; }
        public string Testo { get; set; }

        public Autore Autore { get; set; }


        public Notizia(string titolo, string testo)
        {
            Titolo = titolo;
            Testo = testo;
        }

    }


    public class Autore
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }


        public Autore(string nome, string cognome)
        {
            Nome = nome;
            Cognome = cognome;
        }

    }


}