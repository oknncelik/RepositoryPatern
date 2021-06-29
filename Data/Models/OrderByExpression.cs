using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Models
{
    public class OrderByExpression<TEntity>
    {
        public Expression<Func<TEntity, object>> Predicate { get; set; }
        public ThenByExpression<TEntity> PredicateThenBy { get; set; }
        public OrderBy Direction { get; set; } = OrderBy.asc;
    }

    public class ThenByExpression<TEntity>
    {
        public Expression<Func<TEntity, object>> Predicate { get; set; }
        public OrderBy Direction { get; set; } = OrderBy.asc;
    }

    public enum OrderBy
    {
        asc,
        desc
    }
}