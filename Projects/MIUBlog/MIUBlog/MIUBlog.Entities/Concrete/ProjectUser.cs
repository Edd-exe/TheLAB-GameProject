using MIUBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Entities.Concrete
{
    public class ProjectUser:IEntity
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool isApproved { get; set; }

    }
}
