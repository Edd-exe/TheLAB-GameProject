using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI
{
    public class HomeBlogModel
    {
        public List<Blog> SliderBlog { get; set; }
        public List<Blog> HomeBlog { get; set; }
    }
}
