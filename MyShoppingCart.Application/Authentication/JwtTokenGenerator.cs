using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyShoppingCart.Application.Authentication;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenGenerator(IOptionsSnapshot<JwtConfig> config)
    {
        _jwtConfig = config.Value;
    }

    public string GenerateToken(Guid customerId, string? role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, customerId.ToString()),
            new Claim(ClaimTypes.Role, role ?? "Customer")
        };
        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
