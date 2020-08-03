using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppsAPI.Models;

namespace WebAppsAPI.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne()
                .IsRequired()
                .HasForeignKey("UserId");

            builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(50);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne()
                .IsRequired()
                .HasForeignKey("PostId");

            builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(50);
            builder.Entity<Post>().Property(p => p.Text).IsRequired().HasMaxLength(200);


            builder.Entity<Comments>().Property(c => c.Text).IsRequired().HasMaxLength(200);

            //Another way to seed the database
            builder.Entity<User>().HasData(
               new User { Id = 1, FirstName = "Ree", LastName = "Govaert", Email = "govaertr@gmail.com", DateAdded = new DateTime(2019, 2, 20) },
               new User { Id = 2, FirstName = "Ven", LastName = "Gov", Email = "VenGov@gmail.com", DateAdded = new DateTime(2019, 1, 18) },
               new User { Id = 3, FirstName = "Web4", LastName = "Web4", Email = "Web4@gmail.com", DateAdded = new DateTime(2018, 1, 18) }
            );

            builder.Entity<Post>().HasData(
               new { Id = 1, Title = "Marilyn Monroe quote", Text = "“I'm selfish, impatient and a little insecure. I make mistakes, I am out of control and at times hard to handle.” ― Marilyn Monroe", DateAdded = new DateTime(2019, 2, 21), UserId = 1 },
               new { Id = 2, Title = "Oscar Wilde quote", Text = "“Be yourself; everyone else is already taken.” ― Oscar Wilde", DateAdded = new DateTime(2019, 2, 22), UserId = 1 },
               new { Id = 3, Title = "My first post", Text = "Hello, I am new here :)", DateAdded = new DateTime(2019, 1, 19), UserId = 2 },
               new { Id = 4, Title = "What is the meaning of life?", Text = "The meaning of life, or the answer to the question: 'What is the meaning of life?'", DateAdded = new DateTime(2019, 1, 19), UserId = 3 },
               new { Id = 5, Title = "Name the world's biggest island?", Text = "What is the world's biggest island, without continents technically being an island?", DateAdded = new DateTime(2019, 1, 19), UserId = 3 },
               new { Id = 6, Title = "What is the diameter of Earth?", Text = "I would like to know what the diameter of the Earth is?", DateAdded = new DateTime(2019, 1, 19), UserId = 3 });
            builder.Entity<Comments>().HasData(
               new { Id = 1, Text = "What a beautiful quote.", DateAdded = new DateTime(2019, 3, 11), PostId = 1 },
               new { Id = 2, Text = "This is just astonishing!", DateAdded = new DateTime(2019, 3, 14), PostId = 1 },
               new { Id = 3, Text = "My first comment in this comment section", DateAdded = new DateTime(2019, 1, 20), PostId = 2 },
               new { Id = 4, Text = "Beautifully said!", DateAdded = new DateTime(2019, 1, 20), PostId = 5 }
            );
        }

        public DbSet<User> Users { get; set; }
    }
}

