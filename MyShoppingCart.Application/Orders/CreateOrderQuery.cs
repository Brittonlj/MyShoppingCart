namespace MyShoppingCart.Application.Orders;

public sealed record CreateOrderQuery(
    Guid CustomerId,
    IReadOnlyList<Guid> ProductIds) :
    IQuery<OrderModel>,
    IAuthorizedCustomerRequest
{
}
