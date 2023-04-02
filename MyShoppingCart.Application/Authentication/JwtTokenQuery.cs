namespace MyShoppingCart.Application.Authentication;

public sealed record JwtTokenQuery(Guid CustomerId) : IQuery<string>
{
}
