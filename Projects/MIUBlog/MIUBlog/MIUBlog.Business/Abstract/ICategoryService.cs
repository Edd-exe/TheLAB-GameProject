using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.Business.Abstract
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        IQueryable<Category> GetByBlogId(int categoryId);
        void Add(Category category);
        void Update(Category category);
        void Delete(int categoryId);
    }
}
