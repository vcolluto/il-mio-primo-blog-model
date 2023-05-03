using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;

namespace NetCore_01
{
    public class PostContext : DbContext
    {
        public DbSet<Post> posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True;TrustServerCertificate=true");
        }
    }
}
