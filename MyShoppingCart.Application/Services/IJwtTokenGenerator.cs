namespace MyShoppingCart.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(List<SecurityClaim> securityClaims);
    }
}