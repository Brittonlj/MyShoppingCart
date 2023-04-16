namespace MyShoppingCart.Domain.ResponseObjects;

public interface IQueryMany<TEntity> : IRequest<Response<IReadOnlyList<TEntity>>>
    where TEntity : class
{
}
