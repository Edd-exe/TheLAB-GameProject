using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.Business.Concrete
{
    public class CommetManager : ICommentService
    {
        private ICommentDal _commentDal;

        public CommetManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }


        public IQueryable<Comment> GetAll()
        {
            return _commentDal.GetList();
        }

        public void Add(Comment comment)
        {
            _commentDal.Add(comment);
        }

        public void Update(Comment comment)
        {
            _commentDal.Update(comment);
        }

        public void Delete(int commentId)
        {
            var result = _commentDal.GetList(i => i.CommentId == commentId).FirstOrDefault();
            _commentDal.Delete(result);
        }

        public IQueryable<Comment> GetByBlogId(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
