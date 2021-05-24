using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI.Models
{
    public class DiscussionModel
    {
        public IQueryable<Comment> Comments { get; set; }
        public Discussion Discussion { get; set; }
    }
}
