using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyShoppingCart.Application.Services;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtConfig;

    public JwtTokenService(IOptionsSnapshot<JwtSettings> config)
    {
        _jwtConfig = config.Value;
    }

    public string GenerateToken(List<SecurityClaim> securityClaims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = securityClaims.Select(x => new Claim(x.Type, x.Value)).ToList();

        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.TimeoutInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
