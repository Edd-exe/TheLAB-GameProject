using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Business.Concrete
{
    public class ProjectUserManager : IProjectUserService
    {
        private readonly IProjectUserDal _projectUserDal;
        public ProjectUserManager(IProjectUserDal projectUserDal)
        {
            _projectUserDal = projectUserDal;
        }
        public void Add(ProjectUser projectUser)
        {
            _projectUserDal.Add(projectUser);
        }

        public void Delete(int id)
        {
            ProjectUser projectUser = _projectUserDal.Get(p => p.Id == id);
            _projectUserDal.Delete(projectUser);
        }

        public ProjectUser Get(int id)
        {
           return _projectUserDal.GetByIncludeEntity(id);
        }

        public List<ProjectUser> GetAll()
        {
            return _projectUserDal.GetAllIncludeEntities();
        }

        public List<ProjectUser> GetByProjectId(int projectId)
        {
            return _projectUserDal.GetByProjectId(projectId);
        }

        public ProjectUser GetByProjectIdAndUserId(int projectId, string userId)
        {
            return _projectUserDal.GetByProjectIdAndUserId(projectId, userId);
        }

        public void Update(ProjectUser projectUser)
        {
            _projectUserDal.Update(projectUser);
        }
    }
}
