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

    public string GenerateToken(Customer customer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = customer.Claims.Select(x => new Claim(x.Type, x.Value)).ToList();
        
        if (!claims.Any(x => x.Type == ClaimTypes.NameIdentifier))
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()));
        }
        
        if (!claims.Any(x => x.Type == ClaimTypes.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
        }
        
        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
