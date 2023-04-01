namespace MyShoppingCart.Application.Orders.Commands;

public sealed record DeleteOrderCommand(Guid CustomerId, Guid OrderId) : 
    IRequest<Response<Success>>,
    IAuthorizedCustomerRequest
{
}
