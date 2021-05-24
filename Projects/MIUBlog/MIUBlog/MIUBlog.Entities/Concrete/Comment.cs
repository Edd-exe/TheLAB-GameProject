using MIUBlog.Entities.Abstract;

using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public bool isApproved { get; set; }

        public DateTime Date { get; set; }

        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
       
        public int? DiscussionId { get; set; }
        public virtual Discussion Discussion { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
