using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Application.Customers;

public sealed record CreateCustomerQuery(
    string FirstName,
    string LastName,
    string Email,
    NewAddressModel? BillingAddress,
    NewAddressModel? ShippingAddress,
    List<NewSecurityClaimModel> Claims) :
    IQuery<Customer>
{
}
