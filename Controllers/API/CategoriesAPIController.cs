using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;

namespace NetCore_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPIController : ControllerBase
    {
        private PostContext _dbContext;

        public CategoriesAPIController(PostContext dbContext)
        {
            _dbContext = dbContext;     //viene iniettato tramite Dependency Injection
        }

        [HttpGet]
        public IActionResult Index(string? search)        // gestisce richieste GET /api/CategoriesAPI  
        {
            List<Category> categories;
            categories = _dbContext.categories.ToList<Category>();
            return Ok(categories);
        }
    }
}
