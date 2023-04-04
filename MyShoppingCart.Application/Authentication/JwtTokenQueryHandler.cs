using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryHandler : IRequestHandler<JwtTokenQuery, Response<string>>
{
    private readonly IUnitOfWork _context;
    private readonly IJwtTokenService _tokenGenerator;

    public JwtTokenQueryHandler(IUnitOfWork context, IJwtTokenService tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
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

        var token = _tokenGenerator.GenerateToken(claims);
        var bearer = $"Bearer {token}";
        return bearer;
    }
}
