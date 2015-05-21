using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain
{
   [Table("BlogComments")]
    public class BlogComments
    {
        [Key]
        public Guid CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentBody { get; set; }

        public Guid PostId { get; set; }
        public Blogs Blog { get; set; }

        public BlogComments()
        {
        }

    }
}
