using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<CustomerModel>>
{
    private readonly IUnitOfWork _context;

    public UpdateCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<CustomerModel>> Handle(
        UpdateCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .Include(x => x.BillingAddress)
            .Include(x => x.ShippingAddress)
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        _context.Entry(customer).CurrentValues.SetValues(request);

        await _context.SaveChangesAsync(cancellationToken);

        return customer.ToModel();
    }
}
