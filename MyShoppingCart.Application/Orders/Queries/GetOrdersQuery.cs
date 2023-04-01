namespace MyShoppingCart.Application.Orders.Queries;

public sealed record GetOrdersQuery(
    Guid CustomerId,
    int PageNumber,
    int PageSize,
    bool SortAscending = true
    ) : 
    IQueryMany<Order>,
    IAuthorizedCustomerRequest
{
}
