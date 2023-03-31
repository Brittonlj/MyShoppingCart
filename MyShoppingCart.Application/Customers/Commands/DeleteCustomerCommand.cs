namespace MyShoppingCart.Application.Customers.Commands;

public sealed record DeleteCustomerCommand(Guid CustomerId, Guid? RequestingCustomerId = null) : 
    IRequest<Response<Success>>
{
}
