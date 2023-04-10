using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryHandler : IRequestHandler<JwtTokenQuery, Response<string>>
{
    private readonly IRepository<SecurityClaim> _claimsRepository;
    private readonly IJwtTokenService _tokenService;

    public JwtTokenQueryHandler(IRepository<SecurityClaim> claimsRepository, IJwtTokenService tokenService)
    {
        _claimsRepository = Guard.Against.Null(claimsRepository, nameof(claimsRepository));
        _tokenService = Guard.Against.Null(tokenService, nameof(tokenService));
    }

    public async Task<Response<string>> Handle(JwtTokenQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetAllSecurityClaimsByCustomerIdSpec(request.CustomerId).WithNoTracking();
        var claims = await _claimsRepository.ListAsync(spec, cancellationToken);

        if (claims.Count == 0) 
        {
            return Unauthorized.Instance;
        }

        var token = _tokenService.GenerateToken(claims);
        var bearer = $"Bearer {token}";
        return bearer;
    }
}
