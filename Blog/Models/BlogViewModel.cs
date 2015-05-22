using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Blog.Domain;

namespace Blog.Models
{

    public class BlogViewModel
    {

        public ICollection<BlogComments> BlogComments { get; set; }
        public BlogViewModel(Blogs blog)
        {
            this.PostId = blog.PostId;
            this.PostAuthor = blog.PostAuthor;
            this.PostTitle = blog.PostTitle;
            this.PostTease = blog.PostTease;
            this.PostDate = blog.PostDate;
            this.PostBody = blog.PostBody;
            this.BlogComments = blog.BlogComments;
        }

        public BlogViewModel(Blogs blog, BlogViewModel blogvm)
        {
            blog.PostId = blogvm.PostId;
            blog.PostAuthor = blogvm.PostAuthor;
            blog.PostTitle = blogvm.PostTitle;
            blog.PostTease = blogvm.PostTease;
            blog.PostDate = blogvm.PostDate;
            blog.PostBody = blogvm.PostBody;

        }

        public BlogViewModel()
        {

        }

        public CommentViewModel CommentViewModel { get; set; }

        public Guid PostId { get; set; }
        public DateTime PostDate { get; set; }

        [Required]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }
        [Required]
        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }
        [Display(Name = "Post Tease")]
        public string PostTease { get; set; }
        [Required]
        [Display(Name = "Post Body")]
        public string PostBody { get; set; }


    }
}