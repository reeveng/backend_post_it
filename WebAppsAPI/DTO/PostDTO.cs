using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppsAPI.Models;

namespace WebAppsAPI.DTO
{
    public class PostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public ICollection<CommentsDTO> Comments { get; set; }
    }
}
