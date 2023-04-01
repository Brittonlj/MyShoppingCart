namespace MyShoppingCart.Application.Customers.Commands;

public sealed record DeleteCustomerCommand(Guid CustomerId) :
    IQuery<Success>,
    IAuthorizedCustomerRequest
{
}
