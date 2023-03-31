using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Application.Authentication.Queries;

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
        var customer = await _context.Customers.FindAsync(request.CustomerId);

        // add authentication here

        if (customer is null)
        {
            return Unauthorized.Instance;
        }

        var token = _tokenGenerator.GenerateToken(request.CustomerId, request.Role);

        return token;
    }
}
