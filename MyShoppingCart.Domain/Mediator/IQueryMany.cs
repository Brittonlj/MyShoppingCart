using MediatR;

namespace MyShoppingCart.Domain.Mediator;

public interface IQueryMany<TEntity> : IQuery<IReadOnlyList<TEntity>>
    where TEntity : class
{
}
