namespace MyShoppingCart.Application.Customers.Commands;

public sealed record UpdateCustomerCommand(Customer Customer, Guid CustomerId) : 
    IQuery<Success>,
    IAuthorizedCustomerRequest
{
}
