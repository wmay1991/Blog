﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Blog.Domain;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext()
            : base("BlogContext")
        {
            
        }
        public virtual DbSet<Blogs> Blogs { get; set; }
    }
}
