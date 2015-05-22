using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Blog.Domain;

namespace Blog.Data
{
    public class PostContext : DbContext
    {
        public PostContext()
            : base("Post")
        {
            
        }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<PostComments> PostComments { get; set; }
    }
}
