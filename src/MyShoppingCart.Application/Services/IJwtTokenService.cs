namespace MyShoppingCart.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(List<SecurityClaim> securityClaims);
    }
}