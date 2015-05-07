using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain
{
   [Table(Name = "Blogs")]
    public class Blogs
    {
        public Blogs()
        {

        }

        [Key] // Primary Key
        public int blogId { get; set; }

        public string blogName {get;set;}
        public string tease {get;set;}
        public string body {get; set;}

    }
}
