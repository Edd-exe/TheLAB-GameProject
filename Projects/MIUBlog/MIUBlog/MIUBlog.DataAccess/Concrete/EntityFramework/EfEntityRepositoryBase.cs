using Microsoft.EntityFrameworkCore;
using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private MIUBlogDbContext _context;

        public EfEntityRepositoryBase(MIUBlogDbContext context)
        {
            _context = context;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {

            return _context.Set<TEntity>().FirstOrDefault(filter);

        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {

            return filter == null ? _context.Set<TEntity>() : _context.Set<TEntity>().Where(filter);

        }

        public void Add(TEntity entity)
        {

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();

        }

        public void Delete(TEntity entity)
        {

            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();

        }

        public void Update(TEntity entity)
        {

            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();

        }
    }

}
