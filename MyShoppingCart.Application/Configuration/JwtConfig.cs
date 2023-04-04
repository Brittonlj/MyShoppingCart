namespace MyShoppingCart.Application.Configuration;

public sealed class JwtConfig
{
    public const string SECTION_NAME = "Jwt";

    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TimeoutInMinutes { get; set; }
}
