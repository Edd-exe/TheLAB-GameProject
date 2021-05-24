using Microsoft.EntityFrameworkCore;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{
    public class EfProjectUserDal : EfEntityRepositoryBase<ProjectUser>, IProjectUserDal
    {
        private readonly MIUBlogDbContext _context;
        public EfProjectUserDal(MIUBlogDbContext context) : base(context)
        {
            _context = context;
        }





        public List<ProjectUser> GetByProjectId(int projectId)
        {
            return _context.ProjectUsers
                .Where(p=>p.ProjectId==projectId)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Project)                
                .ToList();
        }

        public ProjectUser GetByProjectIdAndUserId(int projectId, string userId)
        {

            return _context.ProjectUsers
                .Include(p => p.Project)
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.ProjectId == projectId && p.ApplicationUserId == userId);
        }

        public List<ProjectUser> GetAllIncludeEntities()
        {
            return _context.ProjectUsers
                .Include(p => p.Project)
                .Include(p => p.ApplicationUser)
                .ToList();
        }

       public  ProjectUser GetByIncludeEntity(int id)
        {
            return _context.ProjectUsers
                .Include(p => p.Project)
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
