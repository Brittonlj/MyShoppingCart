namespace MyShoppingCart.Application.Orders;

public sealed record GetOrdersQuery(
    Guid CustomerId,
    int PageNumber,
    int PageSize,
    string SortColumn,
    bool SortAscending = true
    ) :
    IQueryManyPaged<Order>,
    IAuthorizedCustomerRequest
{
    public static readonly IReadOnlyDictionary<string, IOrderBy> OrderByClauses =
        new Dictionary<string, IOrderBy>
        {
            { nameof(Order.OrderDateTimeUtc), new OrderBy<Order, DateTime>(x => x.OrderDateTimeUtc) }
        };
}
