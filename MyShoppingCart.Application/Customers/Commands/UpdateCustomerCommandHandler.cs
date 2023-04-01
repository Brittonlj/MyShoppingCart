using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Customers.Commands;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public UpdateCustomerCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .Include(x => x.BillingAddress)
            .Include(x => x.ShippingAddress)
            .FirstOrDefaultAsync(x => x.Id == request.Customer.Id, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        _context.Entry(customer).CurrentValues.SetValues(request.Customer);
        _context.Addresses.HandleChanges(customer.BillingAddress, request.Customer.BillingAddress);
        _context.Addresses.HandleChanges(customer.ShippingAddress, request.Customer.ShippingAddress);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
