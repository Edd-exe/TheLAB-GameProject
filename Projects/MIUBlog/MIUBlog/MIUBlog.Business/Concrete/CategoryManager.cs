using MIUBlog.Business.Abstract;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIUBlog.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


        public IQueryable<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }

        public void Delete(int categoryId)
        {
            var result = _categoryDal.GetList(i => i.CategoryId == categoryId).FirstOrDefault();
            _categoryDal.Delete(result);
        }

       

        public IQueryable<Category> GetByBlogId(int categoryId)
        {
            throw new NotImplementedException();
        }

        
    }
}
