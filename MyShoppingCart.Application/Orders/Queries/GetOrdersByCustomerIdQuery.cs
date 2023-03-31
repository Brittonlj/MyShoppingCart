namespace MyShoppingCart.Application.Orders.Queries;

public sealed record GetOrdersByCustomerIdQuery(
    Guid CustomerId,
    int PageNumber,
    int PageSize,
    bool SortAscending = true,
    Guid? RequestingCustomerId = null
    ) : IRequest<Response<IReadOnlyList<Order>>>
{
}
