namespace MyShoppingCart.Application.Customers;

public sealed record DeleteCustomerCommand(Guid CustomerId) :
    ICommand,
    IAuthorizedCustomerRequest
{
}
