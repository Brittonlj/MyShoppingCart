namespace MyShoppingCart.Application.Customers;

public sealed record CreateCustomerQuery(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    AddressModel? BillingAddress,
    AddressModel? ShippingAddress) :
    IQuery<CustomerModel>
{
}
