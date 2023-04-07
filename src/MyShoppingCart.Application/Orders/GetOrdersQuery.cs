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
}
