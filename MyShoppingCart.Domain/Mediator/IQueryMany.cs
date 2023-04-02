namespace MyShoppingCart.Domain.Mediator;

public interface IQueryMany<TEntity> : IRequest<Response<IReadOnlyList<TEntity>>>
    where TEntity : class
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
    string SortColumn { get; init; }
    bool SortAscending { get; init; }

}
