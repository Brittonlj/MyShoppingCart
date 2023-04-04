namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public GetCustomerQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<Customer>> Handle(
        GetCustomerQuery query,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context
            .Customers
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        return customer;
    }
}
