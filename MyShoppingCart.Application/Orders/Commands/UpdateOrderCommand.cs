namespace MyShoppingCart.Application.Orders.Commands;

public sealed record UpdateOrderCommand(Guid CustomerId, Order Order) : 
    IQuery<Success>,
    IAuthorizedCustomerRequest
{
}
