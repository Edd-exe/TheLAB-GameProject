using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.Business.Abstract
{
    public interface IProjectService
    {
        List<Project> GetAll();

        Project Get(int id);
        public List<Project> GetByUserId(string userId);
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
    }
}
