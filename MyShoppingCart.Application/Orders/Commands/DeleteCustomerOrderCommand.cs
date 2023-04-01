namespace MyShoppingCart.Application.Orders.Commands;

public sealed record DeleteCustomerOrderCommand(Guid CustomerId, Guid OrderId) : 
    IQuery<Success>,
    IAuthorizedCustomerRequest
{
}
