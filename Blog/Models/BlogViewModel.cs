using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Blog.Domain;

namespace Blog.Models
{

    public class PostViewModel
    {

        public ICollection<PostComments> PostComments { get; set; }
        public PostViewModel(Posts post)
        {
            this.PostId = post.PostId;
            this.PostAuthor = post.PostAuthor;
            this.PostTitle = post.PostTitle;
            this.PostTease = post.PostTease;
            this.PostDate = post.PostDate;
            this.PostBody = post.PostBody;
            this.PostComments = post.BlogComments;
        }

        public PostViewModel(Posts post, PostViewModel blogvm)
        {
            post.PostId = blogvm.PostId;
            post.PostAuthor = blogvm.PostAuthor;
            post.PostTitle = blogvm.PostTitle;
            post.PostTease = blogvm.PostTease;
            post.PostDate = blogvm.PostDate;
            post.PostBody = blogvm.PostBody;

        }

        public PostViewModel()
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