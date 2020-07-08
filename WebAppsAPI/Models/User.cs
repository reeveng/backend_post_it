using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsAPI.Models
{
    public class User
    {
        #region properties
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public User()
        {
            Posts = new List<Post>();
            DateAdded = DateTime.Now;
        }

        public User(string firstName, string lastName, string email) : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }
        #endregion

        #region Methods
        public void AddPost(Post post) => Posts.Add(post);

        public Post GetPost(int id) => Posts.SingleOrDefault(i => i.Id == id);
        public Post GetPostByTitle(string title) => Posts.SingleOrDefault(p => p.Title == title);
        #endregion
    }
}

