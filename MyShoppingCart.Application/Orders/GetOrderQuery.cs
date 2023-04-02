namespace MyShoppingCart.Application.Orders;

public sealed record GetOrderQuery(Guid CustomerId, Guid OrderId) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}
