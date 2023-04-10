namespace MyShoppingCart.Domain.ValueObjects;

public interface IQuery<TEntity> : IRequest<Response<TEntity>>
    where TEntity : class
{
}
