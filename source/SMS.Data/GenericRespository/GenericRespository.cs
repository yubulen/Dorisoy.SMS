﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SMS.Data
{
  /// <summary>
  /// 用于表示泛型仓储
  /// </summary>
  /// <typeparam name="TC"></typeparam>
  /// <typeparam name="TContext"></typeparam>
  public class GenericRepository<TC, TContext> : IGenericRepository<TC>
      where TC : class
      where TContext : DbContext
  {
    protected readonly TContext Context;

    internal readonly DbSet<TC> DbSet;
    protected IUnitOfWork<TContext> _uow;
    protected GenericRepository(IUnitOfWork<TContext> uow)
    {

      this._uow = uow;

      try
      {
        Context = uow.Context;
        DbSet = Context.Set<TC>();
      }
      catch (Exception ex)
      {
        throw new System.InvalidOperationException("数据链接异常，你可以通过覆盖 DbContext.OnConfiguring 或在应用程序服务提供者上使用 AddDbContext 来配置提供者！");
      }
    }


    public IQueryable<TC> All => Context.Set<TC>();
    public void Add(TC entity)
    {
      Context.Add(entity);
    }

    public IQueryable<TC> AllIncluding(params Expression<Func<TC, object>>[] includeProperties)
    {
      return GetAllIncluding(includeProperties);
    }

    public IQueryable<TC> FindByInclude(Expression<Func<TC, bool>> predicate, params Expression<Func<TC, object>>[] includeProperties)
    {
      var query = GetAllIncluding(includeProperties);
      return query.Where(predicate);
    }

    public IQueryable<TC> FindBy(Expression<Func<TC, bool>> predicate)
    {
      IQueryable<TC> queryable = DbSet.AsNoTracking();
      return queryable.Where(predicate);
    }

    private IQueryable<TC> GetAllIncluding(params Expression<Func<TC, object>>[] includeProperties)
    {
      IQueryable<TC> queryable = DbSet.AsNoTracking();

      return includeProperties.Aggregate
        (queryable, (current, includeProperty) => current.Include(includeProperty));
    }
    public TC Find(Guid id)
    {
      return Context.Set<TC>().Find(id);
    }

    public async Task<TC> FindAsync(Guid id)
    {
      return await Context.Set<TC>().FindAsync(id);
    }

    public virtual void Update(TC entity)
    {
      Context.Update(entity);
    }

    public virtual void UpdateRange(List<TC> entities)
    {
      Context.UpdateRange(entities);
    }

    public void RemoveRange(IEnumerable<TC> lstEntities)
    {
      Context.Set<TC>().RemoveRange(lstEntities);
    }

    public void AddRange(IEnumerable<TC> lstEntities)
    {
      Context.Set<TC>().AddRange(lstEntities);
    }

    public void InsertUpdateGraph(TC entity)
    {
      Context.Set<TC>().Add(entity);
      //Context.ApplyStateChanges(user);
    }

    public virtual void Delete(Guid id)
    {
      var entity = Context.Set<TC>().Find(id) as BaseEntity;
      if (entity != null)
      {
        entity.IsDeleted = true;
        Context.Update(entity);
      }
    }

    public virtual void Delete(TC entityData)
    {
      var entity = entityData as BaseEntity;
      entity.IsDeleted = true;
      Context.Update(entity);
    }
    public virtual void Remove(TC entity)
    {
      Context.Remove(entity);
    }

    public void Dispose()
    {
      Context.Dispose();
    }
  }
}
