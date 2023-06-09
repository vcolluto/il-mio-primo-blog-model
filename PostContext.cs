﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCore_01.Models;
using System.Diagnostics;

namespace NetCore_01
{
    public class PostContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Post> posts { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Tag> tags { get; set; }

        public PostContext(DbContextOptions<PostContext> dbContextOptions) : base(dbContextOptions) { }

    }
}
