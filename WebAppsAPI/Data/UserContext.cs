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
                new User { Id = 1, FirstName = "Reeven", LastName = "Govaert", Email = "govaertr@gmail.com", DateAdded = new DateTime(2019, 2, 20) },
                new User { Id = 2, FirstName = "Random", LastName = "Dude", Email = "randomdude@gmail.com", DateAdded = new DateTime(2019, 1, 18) },
                new User { Id = 3, FirstName = "Web4", LastName = "Web4", Email = "Web4@gmail.com", DateAdded = new DateTime(2018, 1, 18) }
  );

            builder.Entity<Post>().HasData(

                    new { Id = 1, Title = "Hello man", Text = "This is my first post, i got it working :)!", DateAdded = new DateTime(2019, 2, 21), UserId = 1 },
                    new { Id = 2, Title = "Yeay", Text = "This is my second post!", DateAdded = new DateTime(2019, 2, 22), UserId = 1 },
                    new { Id = 3, Title = "Random", Text = "Hey, I am a random dude!", DateAdded = new DateTime(2019, 1, 19), UserId = 2 },
                    new { Id = 4, Title = "Hey", Text = "Nice to meet ya", DateAdded = new DateTime(2019, 1, 19), UserId = 3 },
                    new { Id = 5, Title = "Lorem", Text = "Lorem ipsum dolor sit amet.", DateAdded = new DateTime(2019, 1, 19), UserId = 3 },
                    new { Id = 6, Title = "Lorem ipsum", Text = "Lorem ipsum dolor sit.", DateAdded = new DateTime(2019, 1, 19), UserId = 3 }                    );
            builder.Entity<Comments>().HasData(
                    new { Id = 1, Text = "First Comment!", DateAdded = new DateTime(2019, 3, 11), PostId = 1 },
                    new { Id = 2, Text = "Second comment", DateAdded = new DateTime(2019, 3, 14), PostId = 1 },
                    new { Id = 3, Text = "Random comment from a random guy", DateAdded = new DateTime(2019, 1, 20), PostId = 2 },
                    new { Id = 4, Text = "Privacy? Big Brother is watching you...", DateAdded = new DateTime(2019, 1, 20), PostId = 5 }
                  );
        }

        public DbSet<User> Users { get; set; }
    }
}

