using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.Data.Models;

namespace Twits.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id= 1,
                    Title = "Cooking"
                }, 
                new Category
                {
                    Id= 2,
                    Title = "Art"
                }, 
                new Category
                {
                    Id= 3,
                    Title = "History"
                }, 
                new Category
                {
                    Id= 4,
                    Title = "Science"
                }, 
                new Category
                {
                    Id= 5,
                    Title = "Technology"
                }, 
                new Category
                {
                    Id= 6,
                    Title = "Sport"
                }
                );
        }
    }
}
