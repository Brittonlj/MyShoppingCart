namespace MyShoppingCart.Application.Authentication;

public sealed record JwtTokenQuery(Guid CustomerId, string? Role) : IQuery<string>
{
}
