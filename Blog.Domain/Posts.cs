using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using a = System.ComponentModel.DataAnnotations.Schema;



namespace Blog.Domain
{
    [Table(Name = "Post")]
    public class Posts
    {

        public Posts()
        {
            this.BlogComments = new HashSet<PostComments>();
        }

        [Key]
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostAuthor { get; set; }
        public DateTime PostDate { get; set; }
        public string PostTease { get; set; }
        public string PostBody { get; set; }

        [a.ForeignKey("PostId")]
        public virtual ICollection<PostComments> BlogComments { get; set; }


    }
}
