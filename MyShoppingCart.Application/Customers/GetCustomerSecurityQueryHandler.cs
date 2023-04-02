namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerSecurityQueryHandler : 
    IRequestHandler<GetCustomerSecurityQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public GetCustomerSecurityQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Customer>> Handle(
        GetCustomerSecurityQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .Include(x => x.Claims)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        return customer;
    }
}
