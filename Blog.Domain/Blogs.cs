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
       public Guid PostId { get; set; }
       public string PostTitle {get;set;}
       public string PostAuthor { get; set; }
       public DateTime PostDate { get; set; }
       public string PostTease {get;set;}
       public string PostBody {get; set;}

    }
}
