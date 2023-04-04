namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerSecurityQueryHandler : 
    IRequestHandler<GetCustomerSecurityQuery, Response<IReadOnlyList<SecurityClaim>>>
{
    private readonly IUnitOfWork _context;

    public GetCustomerSecurityQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<IReadOnlyList<SecurityClaim>>> Handle(
        GetCustomerSecurityQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        var claims = await _context
            .Claims
            .Where(x => x.CustomerId == request.CustomerId)
            .ToListAsync(cancellationToken);

        return claims;
    }
}
