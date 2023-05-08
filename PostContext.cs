using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Diagnostics;

namespace NetCore_01
{
    public class PostContext : DbContext
    {
        public DbSet<Post> posts { get; set; }

        public PostContext(DbContextOptions<PostContext> dbContextOptions) : base(dbContextOptions) { }

    }
}
