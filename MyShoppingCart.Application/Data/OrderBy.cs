using System.Linq.Expressions;

namespace MyShoppingCart.Application.Data;

public sealed class OrderBy<TEntity, T> : IOrderBy
{
    private readonly Expression<Func<TEntity, T>> expression;

    public OrderBy(Expression<Func<TEntity, T>> expression)
    {
        this.expression = expression;
    }

    public dynamic Expression => this.expression;
}

