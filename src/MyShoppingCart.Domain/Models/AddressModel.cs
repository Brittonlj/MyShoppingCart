namespace MyShoppingCart.Domain.Models;

public sealed record AddressModel(
    string Street,
    string City,
    string State,
    string PostalCode)
{
}
