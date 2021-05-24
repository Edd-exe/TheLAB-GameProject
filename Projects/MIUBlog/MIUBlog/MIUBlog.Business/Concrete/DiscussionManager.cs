using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUDiscussion.Business.Concrete
{
    public class DiscussionManager : IDiscussionService
    {
        private IDiscussionDal _discussionDal;

        public DiscussionManager(IDiscussionDal discussionDal)
        {
            _discussionDal = discussionDal;
        }

        public List<Discussion> GetAll()
        {
            return _discussionDal.GetAllIncludeEntities();
        }

        

        public void Add(Discussion discussion)
        {
            _discussionDal.Add(discussion);
        }

        public void Update(Discussion discussion)
        {
            _discussionDal.Update(discussion);
        }

        public void Delete(int id)
        {
            var result = _discussionDal.Get(d => d.Id == id);
            _discussionDal.Delete(result);
        }

        public Discussion Get(int id)
        {
            return _discussionDal.GetByIncludeEntity(id);
        }
    }
}
