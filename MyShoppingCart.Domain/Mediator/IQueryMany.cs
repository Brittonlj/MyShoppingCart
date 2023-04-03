namespace MyShoppingCart.Domain.Mediator;

public interface IQueryMany<TEntity> : IRequest<Response<IReadOnlyList<TEntity>>>
    where TEntity : class
{
}
