using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Blog.Domain;

namespace Blog.Models {

    public class BlogViewModel{


        public BlogViewModel(Blogs blog)
        {
            this.BlogId = blog.PostId;
            this.PostAuthor = blog.PostAuthor;
            this.PostTitle = blog.PostTitle;
            this.PostTease = blog.PostTease;
            this.PostDate = blog.PostDate;
            this.PostBody = blog.PostBody;
        }

        public BlogViewModel()
        {

        }

        public Guid BlogId;
        public DateTime PostDate;
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostAuthor { get; set; }
        public string PostTease { get; set; }
        [Required]
        public string PostBody { get; set; }
    }
}