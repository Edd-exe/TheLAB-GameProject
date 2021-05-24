using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.Business.Abstract
{
    public interface ICommentService
    {
        IQueryable<Comment> GetAll();
        IQueryable<Comment> GetByBlogId(int commentId);
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(int commentId);
    }
}
