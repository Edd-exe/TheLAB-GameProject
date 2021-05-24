using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public IQueryable<Blog> GetAll()
        {
            return _blogDal.GetList();
        }


        public IQueryable<Blog> GetCategoryId(int categoryId)
        {
            return _blogDal.GetList(i => i.CategoryId == categoryId);
        }

        public void Add(Blog blog)
        {
            _blogDal.Add(blog);
        }

        public void Update(Blog blog)
        {
            _blogDal.Update(blog);
        }

        public void Delete(int blogId)
        {
            var result = _blogDal.GetList(i => i.BlogId == blogId).FirstOrDefault();
            _blogDal.Delete(result);
        }

        public Blog Get(int id)
        {
            return _blogDal.GetByIncludeEntity(id);
        }
    }
}
