namespace MyShoppingCart.Application.Customers;

public sealed record UpdateCustomerQuery(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Email,
    Address BillingAddress,
    Address ShippingAddress) :
    IQuery<Customer>,
    IAuthorizedCustomerRequest
{
}
