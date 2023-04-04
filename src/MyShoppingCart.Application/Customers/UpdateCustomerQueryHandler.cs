namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public UpdateCustomerQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<Customer>> Handle(UpdateCustomerQuery request, CancellationToken cancellationToken)
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

    //private async void UpdateClaims(UpdateCustomerQuery request)
    //{
    //    var originals = await _context
    //        .Claims
    //        .Where(x => x.CustomerId == request.CustomerId)
    //        .ToListAsync();

    //    var requests = request.Claims;

    //    var claimsToAdd = requests.Where(x => !originals.Any(y => x.Id == y.Id)).ToList();

    //    var claimsToDelete = originals.Where(x => !requests.Any(y => x.Id == y.Id))
    //        .ToList();

    //    var matches = originals.Join(requests,
    //        originalId => originalId.Id,
    //        requestId => requestId.Id,
    //        (original, request) => new
    //        {
    //            Original = original,
    //            Request = request
    //        });

    //    //Update
    //    matches
    //        .Where(x => x.Original.Id == x.Request.Id && x.Original != x.Request)
    //        .ToList()
    //        .ForEach(x => _context.Entry(x.Original).CurrentValues.SetValues(x.Request));

    //    //Add
    //    originals.AddRange(claimsToAdd);

    //    //Delete
    //    claimsToDelete.ForEach(x => originals.Remove(x));

    //}

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
