using Microsoft.EntityFrameworkCore;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{
    public class EfProjectDal : EfEntityRepositoryBase<Project>, IProjectDal
    {
        private readonly MIUBlogDbContext _context;
        public EfProjectDal(MIUBlogDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Project> GetAllIncludeEntities()
        {

            return _context.Projects.Include(b => b.ApplicationUser).ToList();
        }

        public Project GetByIncludeEntity(int id)
        {
            return _context.Projects
                .Include(b => b.ApplicationUser)
                .FirstOrDefault(b => b.Id == id);
        }
        public List<Project> GetByUserId(string userId)
        {
            return _context.Projects
                .Where(p => p.ApplicationUserId == userId)
                .Include(b => b.ApplicationUser).ToList();
                
        }
    }
}
