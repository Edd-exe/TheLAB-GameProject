using Microsoft.EntityFrameworkCore;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog>, IBlogDal
    {
        private readonly MIUBlogDbContext _context;
        public EfBlogDal(MIUBlogDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetAllIncludeEntities()
        {
            
            return _context.Blogs.Include(b => b.ApplicationUser).ToList();
        }

        public Blog GetByIncludeEntity(int id)
        {
            return _context.Blogs.Include(b => b.ApplicationUser).FirstOrDefault(b => b.BlogId == id);
        }
    }
}
