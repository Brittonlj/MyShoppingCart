using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryHandler : IRequestHandler<JwtTokenQuery, Response<string>>
{
    private readonly IRepository<SecurityClaim> _claimsRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IJwtTokenService _tokenService;

    public JwtTokenQueryHandler(IRepository<SecurityClaim> claimsRepository, IRepository<Customer> customerRepository, IJwtTokenService tokenService)
    {
        _claimsRepository = Guard.Against.Null(claimsRepository, nameof(claimsRepository));
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _tokenService = Guard.Against.Null(tokenService, nameof(tokenService));
    }

    public async Task<Response<string>> Handle(JwtTokenQuery request, CancellationToken cancellationToken)
    {
        var customerQuery = new QueryCustomerById(request.CustomerId).WithNoTracking();
        var customer = await _customerRepository.FirstOrDefaultAsync(customerQuery, cancellationToken);

        if (customer is null)
        {
            return Unauthorized.Instance;
        }

        var claimsQuery = new QuerySecurityClaims(request.CustomerId).WithNoTracking();
        var claims = await _claimsRepository.ListAsync(claimsQuery, cancellationToken);

        if (claims.Count == 0) 
        {
            return Unauthorized.Instance;
        }

        var token = _tokenService.GenerateToken(claims);
        var bearer = $"Bearer {token}";
        return bearer;
    }
}
