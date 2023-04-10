namespace MyShoppingCart.Domain.ValueObjects;

public interface IQueryMany<TEntity> : IRequest<Response<IReadOnlyList<TEntity>>>
    where TEntity : class
{
}
