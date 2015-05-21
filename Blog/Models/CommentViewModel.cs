using Blog.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CommentViewModel
    {

        public Guid CommentId { get; set; }
        public DateTime CommentDate { get; set; }

        [Required]
        [Display(Name = "Comment Author")]
        public string CommentAuthor { get; set; }

        [Required]
        [Display(Name = "Comment Body")]
        public string CommentBody { get; set; }

        public Guid BlogId { get; set; }
        public Blogs Blog { get; set; }
    }
}