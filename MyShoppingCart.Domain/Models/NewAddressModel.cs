namespace MyShoppingCart.Domain.Models;

public sealed record NewAddressModel(
    string Street,
    string City,
    string State,
    string PostalCode)
{
}
