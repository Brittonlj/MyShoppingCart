namespace MyShoppingCart.Application.Customers.Commands;

public sealed record UpdateCustomerCommand(Customer Customer, Guid? RequestingCustomerId = null) : 
    IRequest<Response<Success>>
{
}
