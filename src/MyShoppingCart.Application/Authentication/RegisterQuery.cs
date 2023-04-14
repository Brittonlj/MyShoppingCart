namespace MyShoppingCart.Application.Authentication;

public sealed record RegisterQuery(
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
