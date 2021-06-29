using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Abstruct;
using Data.Contexts;
using Data.Models;
using Entities.Abstruct;
using Microsoft.EntityFrameworkCore;

namespace Data.Concreate
{
    public class Repository<TEntity> : IRepository<TEntity>         
        where TEntity : class, IEntity, new()
    {
        private readonly RepositoryContext _context;
        
        public Repository()
        {
            _context = new RepositoryContext();
        }

        public async Task<TEntity> Get(params Expression<Func<TEntity, bool>>[] filters)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            
            if (filters != null || filters.Length > 0)
                foreach (var filter in filters)
                    query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetList(IList<Expression<Func<TEntity, bool>>> filters = null, OrderByExpression<TEntity> orderBy = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (filters != null || filters.Count > 0)
                foreach (var filter in filters)
                    query = query.Where(filter);
            
            if (orderBy != null)
            {
                if (orderBy.Direction == OrderBy.desc)
                    if (orderBy.PredicateThenBy != null)
                        if (orderBy.PredicateThenBy.Direction == OrderBy.desc)
                            query = query.OrderByDescending(orderBy.Predicate).ThenByDescending(orderBy.PredicateThenBy.Predicate);
                        else
                            query = query.OrderByDescending(orderBy.Predicate).ThenBy(orderBy.PredicateThenBy.Predicate);
                    else
                        query = query.OrderByDescending(orderBy.Predicate);
                else
                    if (orderBy.PredicateThenBy != null)
                        if (orderBy.PredicateThenBy.Direction == OrderBy.desc)
                            query = query.OrderBy(orderBy.Predicate).ThenByDescending(orderBy.PredicateThenBy.Predicate);
                        else
                            query = query.OrderBy(orderBy.Predicate).ThenBy(orderBy.PredicateThenBy.Predicate);
                    else
                        query = query.OrderBy(orderBy.Predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<IQueryable<TEntity>> GetQuery(IList<Expression<Func<TEntity, bool>>> filters = null, OrderByExpression<TEntity> orderBy = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            
            if (filters != null)
                foreach (var filter in filters)
                    query = query.Where(filter);
            
            if (orderBy != null)
            {
                if (orderBy.Direction == OrderBy.desc)
                    if (orderBy.PredicateThenBy != null)
                        if (orderBy.PredicateThenBy.Direction == OrderBy.desc)
                            query = query.OrderByDescending(orderBy.Predicate).ThenByDescending(orderBy.PredicateThenBy.Predicate);
                        else
                            query = query.OrderByDescending(orderBy.Predicate).ThenBy(orderBy.PredicateThenBy.Predicate);
                    else
                        query = query.OrderByDescending(orderBy.Predicate);
                else
                if (orderBy.PredicateThenBy != null)
                    if (orderBy.PredicateThenBy.Direction == OrderBy.desc)
                        query = query.OrderBy(orderBy.Predicate).ThenByDescending(orderBy.PredicateThenBy.Predicate);
                    else
                        query = query.OrderBy(orderBy.Predicate).ThenBy(orderBy.PredicateThenBy.Predicate);
                else
                    query = query.OrderBy(orderBy.Predicate);
            }

            return await Task.Run(()=> query);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}