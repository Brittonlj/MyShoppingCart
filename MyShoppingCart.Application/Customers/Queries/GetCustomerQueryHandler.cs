using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Customers.Queries;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public GetCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Customer>> Handle(
        GetCustomerQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query.RequestingUser.HasValue && query.CustomerId != query.RequestingUser.Value)
        {
            return Unauthorized.Instance;
        }

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
