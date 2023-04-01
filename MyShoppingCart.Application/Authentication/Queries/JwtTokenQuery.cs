namespace MyShoppingCart.Application.Authentication.Queries;

public sealed record JwtTokenQuery(Guid CustomerId, string? Role) : IQuery<string>
{
}
