using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace Blog.Models {

    public class BlogViewModel{

        public Guid id;
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostAuthor { get; set; }
        public string PostTease { get; set; }
        [Required]
        public string PostBody { get; set; }
    }
}