using Microsoft.EntityFrameworkCore;
using MUIBlog.Core.DataAccess.Abstract;
using MUIBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MUIBlog.Core.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    { 
       private DbContext _context;
    
    public EfEntityRepositoryBase(DbContext context)
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
