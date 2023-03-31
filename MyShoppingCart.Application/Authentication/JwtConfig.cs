namespace MyShoppingCart.Application.Authentication;

public sealed class JwtConfig : IJwtConfig
{
    public const string SECTION_NAME = "Jwt";

    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
}
