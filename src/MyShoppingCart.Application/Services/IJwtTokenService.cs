using System.Security.Claims;

namespace MyShoppingCart.Application.Services;

public interface IJwtTokenService
{
    string GenerateToken(List<Claim> claims);
}