namespace MyShoppingCart.Application.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid customerId, string? role);
    }
}