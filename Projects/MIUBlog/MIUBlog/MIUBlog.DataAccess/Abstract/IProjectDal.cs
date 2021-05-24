using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.DataAccess.Abstract
{
   public interface IProjectDal : IEntityRepository<Project>
    {
        public Project GetByIncludeEntity(int id);
        public List<Project> GetAllIncludeEntities();
        public List<Project> GetByUserId(string userId);


    }
}
