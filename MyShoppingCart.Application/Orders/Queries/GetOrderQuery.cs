namespace MyShoppingCart.Application.Orders.Queries;

public sealed record GetOrderQuery(Guid CustomerId, Guid OrderId) : 
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}
