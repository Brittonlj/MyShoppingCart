namespace MyShoppingCart.Application.Authentication
{
    public interface IJwtConfig
    {
        string Audience { get; init; }
        string Issuer { get; init; }
        string Key { get; init; }
    }
}