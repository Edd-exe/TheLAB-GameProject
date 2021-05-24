using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI.Models
{
    public class ProjectModel
    {
        public IQueryable<Comment> Comments { get; set; }
        public Project Project { get; set; }
        public bool Status { get; set; }
    }
}
