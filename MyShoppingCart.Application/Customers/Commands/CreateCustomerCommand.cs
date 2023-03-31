namespace MyShoppingCart.Application.Customers.Commands;

public sealed record CreateCustomerCommand(Customer Customer) : 
    IRequest<Response<Success>>
{
}
