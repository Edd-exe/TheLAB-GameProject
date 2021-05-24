using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI
{
    public class BlogListViewModel
    {
        public IQueryable<Blog> Blogs { get; set; }
    }
}
