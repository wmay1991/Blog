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

        public BlogViewModel(Blogs blog,  BlogViewModel blogvm)
        {
            blog.PostId = blogvm.BlogId;
            blog.PostAuthor = blogvm.PostAuthor;
            blog.PostTitle = blogvm.PostTitle;
            blog.PostTease = blogvm.PostTease;
            blog.PostDate = blogvm.PostDate;
            blog.PostBody = blogvm.PostBody;
        }

        public BlogViewModel()
        {

        }

        public Guid BlogId { get; set; }
        public DateTime PostDate { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostAuthor { get; set; }
        public string PostTease { get; set; }
        [Required]
        public string PostBody { get; set; }
    }
}