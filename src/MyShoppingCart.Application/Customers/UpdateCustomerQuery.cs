
namespace MyShoppingCart.Application.Customers;

public sealed record UpdateCustomerQuery(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    Address BillingAddress,
    Address ShippingAddress) :
    IQuery<CustomerModel>,
    IAuthorizedCustomerRequest
{
}
