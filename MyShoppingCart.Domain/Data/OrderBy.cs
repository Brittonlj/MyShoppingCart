using MyShoppingCart.Domain.Entities;
using System.Linq.Expressions;

namespace MyShoppingCart.Domain.Data;

public sealed class OrderBy<TEntity, T> : IOrderBy where TEntity : class, IEntity<TEntity>
{
    private readonly Expression<Func<TEntity, T>> expression;

    public OrderBy(Expression<Func<TEntity, T>> expression)
    {
        this.expression = expression;
    }

    public dynamic Expression => expression;
}

