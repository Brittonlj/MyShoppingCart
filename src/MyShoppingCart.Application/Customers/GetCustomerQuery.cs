namespace MyShoppingCart.Application.Customers;

public sealed record GetCustomerQuery(Guid CustomerId) :
    IQuery<Customer>,
    IAuthorizedCustomerRequest
{
}
