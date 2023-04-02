using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<CustomerModel>>
{
    private readonly IUnitOfWork _context;

    public GetCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<CustomerModel>> Handle(
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

        return customer.ToModel();
    }
}
