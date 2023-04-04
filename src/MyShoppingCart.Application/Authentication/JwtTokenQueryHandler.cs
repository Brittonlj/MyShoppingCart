using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryHandler : IRequestHandler<JwtTokenQuery, Response<string>>
{
    private readonly IUnitOfWork _context;
    private readonly IJwtTokenService _tokenService;

    public JwtTokenQueryHandler(IUnitOfWork context, IJwtTokenService tokenService)
    {
        _context = Guard.Against.Null(context, nameof(context));
        _tokenService = Guard.Against.Null(tokenService, nameof(tokenService));
    }

    public async Task<Response<string>> Handle(JwtTokenQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Unauthorized.Instance;
        }

        var claims = await _context
            .Claims
            .Where(x => x.CustomerId  == request.CustomerId)
            .ToListAsync(cancellationToken);

        var token = _tokenService.GenerateToken(claims);
        var bearer = $"Bearer {token}";
        return bearer;
    }
}
