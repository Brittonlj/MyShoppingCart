using MediatR;

namespace MyShoppingCart.Domain.Mediator;

public interface IQuery<TEntity> : IRequest<Response<TEntity>>, IRequestMarker
    where TEntity : class
{
}
