namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryHandler : IRequestHandler<JwtTokenQuery, Response<string>>
{
    private readonly IUnitOfWork _context;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public JwtTokenQueryHandler(IUnitOfWork context, IJwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Response<string>> Handle(JwtTokenQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId);

        if (customer is null)
        {
            return Unauthorized.Instance;
        }

        var token = _tokenGenerator.GenerateToken(customer);
        var bearer = $"Bearer {token}";
        return bearer;
    }
}
