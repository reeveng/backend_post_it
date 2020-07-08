using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsAPI.Models
{
    public class Post
    {
        #region properties
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region constructors
        public Post()
        {
            Comments = new List<Comments>();
            DateAdded = DateTime.Now;
        }
        public Post(string title, string text) : this()
        {
            this.Title = title;
            this.Text = text;
        }
        #endregion
        #region Methods
        public void AddComments(Comments comments) => Comments.Add(comments);


        #endregion
    }
}
