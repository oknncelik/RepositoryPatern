using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Models;
using Entities.Abstruct;

namespace Data.Abstruct
{
    public interface IRepository<TEntity> where TEntity: class, IEntity, new()
    {
        Task<TEntity> Get(params Expression<Func<TEntity, bool>>[] filters);
        Task<IList<TEntity>> GetList(IList<Expression<Func<TEntity, bool>>> filters = null, OrderByExpression<TEntity> orderBy = null);
        Task<IQueryable<TEntity>> GetQuery(IList<Expression<Func<TEntity, bool>>> filters = null, OrderByExpression<TEntity> orderBy = null);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
    }
}