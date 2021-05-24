using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{

    public class EfDiscussionDal : EfEntityRepositoryBase<Discussion>, IDiscussionDal
    {
        private readonly MIUBlogDbContext _context;
        public EfDiscussionDal(MIUBlogDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Discussion> GetAllIncludeEntities()
        {

            return _context.Discussions.Include(b => b.ApplicationUser).ToList();
        }

        public Discussion GetByIncludeEntity(int id)
        {
            

            return _context.Discussions.Include(i => i.ApplicationUser).FirstOrDefault(d=>d.Id==id);
        }
    }
}
