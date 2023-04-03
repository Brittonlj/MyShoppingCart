namespace MyShoppingCart.Application.Configuration;

public sealed class JwtConfig
{
    public const string SECTION_NAME = "Jwt";

    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int TimeoutInMinutes { get; init; }
}
