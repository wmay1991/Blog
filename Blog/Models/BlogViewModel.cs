using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace Blog.Models {

    public class BlogViewModel{

        public int postId { get; set; }
        public string postName { get; set; }
        public string postTease { get; set; }
        public string postBody { get; set; }
    }
}