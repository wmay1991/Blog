using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CommentViewModel
    {

        public Guid CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentAuthor { get; set; }

        public string CommentBody { get; set; }

        public Guid BlogId { get; set; }
        public Blogs Blog { get; set; }
    }
}