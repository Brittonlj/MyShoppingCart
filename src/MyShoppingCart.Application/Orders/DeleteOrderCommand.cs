namespace MyShoppingCart.Application.Orders;

public sealed record DeleteOrderCommand(Guid CustomerId, Guid OrderId) :
    IRequest<Response<Success>>,
    IAuthorizedCustomerRequest
{
}
