using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.DataAccess.Abstract
{
   public interface IProjectUserDal : IEntityRepository<ProjectUser>
    {
        public ProjectUser GetByIncludeEntity(int id);
        public List<ProjectUser> GetAllIncludeEntities();
        public ProjectUser GetByProjectIdAndUserId(int projectId, string userId);
        public List<ProjectUser> GetByProjectId(int projectId);


    }
  
}
