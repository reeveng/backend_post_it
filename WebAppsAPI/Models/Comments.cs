using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsAPI.Models
{
    public class Comments
    {
        #region properties
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region constructors
        public Comments()
        {
            DateAdded = DateTime.Now;
        }
        public Comments(string text) : this()
        {
            this.Text = text;
        }
        #endregion
    }
}
