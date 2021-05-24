using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Business.Abstract
{
    public interface IProjectUserService
    {
        List<ProjectUser> GetAll();

        ProjectUser Get(int id);

        public ProjectUser GetByProjectIdAndUserId(int projectId, string userId);

        public List<ProjectUser> GetByProjectId(int projectId);
        void Add(ProjectUser projectUser);
        void Update(ProjectUser projectUser);
        void Delete(int id);

    }
}
