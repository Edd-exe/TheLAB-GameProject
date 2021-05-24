using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Business.Concrete
{

    public class ProjectManager:IProjectService
    {
        private IProjectDal _projectDal;

        public ProjectManager(IProjectDal ProjectDal)
        {
            _projectDal = ProjectDal;
        }

        public List<Project> GetAll()
        {
            return _projectDal.GetAllIncludeEntities();
        }



        public void Add(Project Project)
        {
            _projectDal.Add(Project);
        }

        public void Update(Project Project)
        {
            _projectDal.Update(Project);
        }

        public void Delete(int id)
        {
            var result = _projectDal.Get(d => d.Id == id);
            _projectDal.Delete(result);
        }

        public Project Get(int id)
        {
            return _projectDal.GetByIncludeEntity(id);
        }

        public List<Project> GetByUserId(string userId)
        {
            return _projectDal.GetByUserId(userId);
        }
    }
}
