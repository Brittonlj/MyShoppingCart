using MyShoppingCart.Application.Addresses;

namespace MyShoppingCart.Application.Customers;

public sealed record CustomerModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    AddressModel BillingAddress,
    AddressModel ShippingAddress)
{
}
