using MyShoppingCart.Application.Addresses;

namespace MyShoppingCart.Application.Customers;

public sealed record CreateCustomerQuery(
    string FirstName,
    string LastName,
    string Email,
    AddressModel BillingAddress,
    AddressModel ShippingAddress
    ) :
    IQuery<CustomerModel>
{
}
