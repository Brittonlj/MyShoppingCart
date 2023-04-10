namespace MyShoppingCart.Domain.ValueObjects;

public interface IQueryManyPaged<TEntity> : IRequest<Response<IReadOnlyList<TEntity>>>
    where TEntity : class
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
    string SortColumn { get; init; }
    bool SortAscending { get; init; }

}
