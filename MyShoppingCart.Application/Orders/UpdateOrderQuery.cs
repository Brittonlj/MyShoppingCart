namespace MyShoppingCart.Application.Orders;

public sealed record UpdateOrderQuery(
    Guid CustomerId,
    Guid OrderId,
    IReadOnlyList<Guid> ProductIds) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}
