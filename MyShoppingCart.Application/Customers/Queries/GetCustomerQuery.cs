namespace MyShoppingCart.Application.Customers.Queries;

public sealed record GetCustomerQuery(Guid CustomerId) : 
    IQuery<Customer>,
    IAuthorizedCustomerRequest
{
}
