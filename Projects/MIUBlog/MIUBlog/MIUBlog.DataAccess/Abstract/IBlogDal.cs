using MIUBlog.DataAccess.Concrete.EntityFramework;
using MIUBlog.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.DataAccess.Abstract
{
 
    
    public interface IBlogDal :IEntityRepository<Blog>
    {
        public Blog GetByIncludeEntity(int id);
        public List<Blog> GetAllIncludeEntities();
        

    }
}
