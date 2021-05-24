using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.DataAccess.Abstract
{

    public interface IDiscussionDal : IEntityRepository<Discussion>
    {
        public Discussion GetByIncludeEntity(int id);
        public List<Discussion> GetAllIncludeEntities();


    }
}
