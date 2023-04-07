namespace MyShoppingCart.Application.Orders;

public sealed record UpdateOrderQuery(
    Guid CustomerId,
    Guid OrderId,
    DateTime OrderDateTimeUtc,
    IReadOnlyList<LineItem> LineItems) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}
