using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using  a = System.ComponentModel.DataAnnotations.Schema;



namespace Blog.Domain
{
   [Table(Name = "Blogs")]
    public class Blogs
    {
     
        public Blogs()
        {
            this.BlogComments = new HashSet<BlogComments>();
        }

       [Key] 
       public Guid PostId { get; set; }
       public string PostTitle {get;set;}
       public string PostAuthor { get; set; }
       public DateTime PostDate { get; set; }
       public string PostTease {get;set;}
       public string PostBody {get; set;}

       [a.ForeignKey("PostId")]
       public virtual ICollection<BlogComments> BlogComments { get; set; }


       //public void SetComments(IEnumerable<BlogComments> comments)
       //{
       //    var commentsToSet = (comments ?? new List<BlogComments>());
       //    AddNewComments(commentsToSet);
       //}


       //private void AddNewComments(IEnumerable<BlogComments> commentsToSet)
       //{
       //    var newComments = commentsToSet.Except(this.BlogComments);
       //    foreach (var c in newComments)
       //    {
       //        this.BlogComments.Add(c);
       //    }
       //}
    }
}
