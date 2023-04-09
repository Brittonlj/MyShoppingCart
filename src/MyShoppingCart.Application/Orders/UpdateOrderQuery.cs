namespace MyShoppingCart.Application.Orders;

public sealed record UpdateOrderQuery(
    Guid CustomerId,
    Guid OrderId,
    IReadOnlyList<LineItem> LineItems) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}
