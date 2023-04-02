namespace MyShoppingCart.Application.Addresses;

public sealed record AddressModel
(
    string Street,
    string City,
    string State,
    string PostalCode 
)
{
}
