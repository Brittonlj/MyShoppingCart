using Azure.Core;

namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public UpdateCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Customer>> Handle(
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

        UpdateAddresses(customer, request);

        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    private void UpdateAddresses(Customer customer, UpdateCustomerQuery request)
    {
        if (customer.BillingAddress is not null)
        {
            if (request.BillingAddress is not null)
            {
                _context.Entry(customer.BillingAddress).CurrentValues.SetValues(request.BillingAddress);
            }
            else
            {
                customer.BillingAddress = null;
            }
        }

        if (customer.ShippingAddress is not null)
        {
            if (request.ShippingAddress is not null)
            {
                _context.Entry(customer.ShippingAddress).CurrentValues.SetValues(request.ShippingAddress);
            }
            else
            {
                customer.ShippingAddress = null;
            }
        }
    }
}
