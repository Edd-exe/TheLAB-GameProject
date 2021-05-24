using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI
{
    public class BlogModel
    {
        public IQueryable<Comment> Comments { get; set; }
        public Blog Blog { get; set; }

    }
}
