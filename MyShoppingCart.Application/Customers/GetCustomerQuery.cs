namespace MyShoppingCart.Application.Customers;

public sealed record GetCustomerQuery(Guid CustomerId) :
    IQuery<CustomerModel>,
    IAuthorizedCustomerRequest
{
}
