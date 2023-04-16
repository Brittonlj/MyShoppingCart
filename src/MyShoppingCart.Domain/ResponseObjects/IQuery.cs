namespace MyShoppingCart.Domain.ResponseObjects;

public interface IQuery<TEntity> : IRequest<Response<TEntity>>
    where TEntity : class
{
}
