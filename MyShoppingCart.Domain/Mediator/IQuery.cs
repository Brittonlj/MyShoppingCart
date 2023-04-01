using MediatR;

namespace MyShoppingCart.Domain.Mediator;

public interface IQuery<TEntity> : IRequest<Response<TEntity>>
    where TEntity : class
{
}
