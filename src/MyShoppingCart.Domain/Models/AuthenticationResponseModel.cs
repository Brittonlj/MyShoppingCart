namespace MyShoppingCart.Domain.Models;

public sealed class AuthenticationResponseModel
{
    public required CustomerModel Customer { get; init; }
    public required string Token { get; init; }
}
