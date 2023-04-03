using System.Security.Claims;

namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryHandler : IRequestHandler<CreateCustomerQuery, Response<Customer>>
{
    private readonly IUnitOfWork _context;

    public CreateCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Customer>> Handle(CreateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = MapToCustomer(request);

        var claims = request.Claims.Select(x => new SecurityClaim{ Type = x.Type, Value = x.Value }).ToList();

        ValidateClaims(claims, customer.Id);
        
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.Claims.AddRangeAsync(claims, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    private Customer MapToCustomer(CreateCustomerQuery request)
    {
        var customer = new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        if (request.BillingAddress is not null)
        {
            customer.BillingAddress = new Address
            {
                Street = request.BillingAddress.Street,
                City = request.BillingAddress.City,
                State = request.BillingAddress.State,
                PostalCode = request.BillingAddress.PostalCode
            };
        }

        if (request.ShippingAddress is not null)
        {
            customer.ShippingAddress = new Address
            {
                Street = request.ShippingAddress.Street,
                City = request.ShippingAddress.City,
                State = request.ShippingAddress.State,
                PostalCode = request.ShippingAddress.PostalCode
            };
        }

        return customer;
    }

    private void ValidateClaims(List<SecurityClaim> claims, Guid customerId)
    {
        var invalidUserClaims = claims.Where(x => x.Type == ClaimTypes.NameIdentifier && x.Value != customerId.ToString());

        foreach(var invalidClaim in invalidUserClaims)
        {
            claims.Remove(invalidClaim);
        }

        if (!claims.Any(x => x.Type == ClaimTypes.NameIdentifier))
        {
            claims.Add(new SecurityClaim
            {
                CustomerId = customerId,
                Type = ClaimTypes.NameIdentifier,
                Value = customerId.ToString()
            });
        }

        if (!claims.Any(x => x.Type == ClaimTypes.Role))
        {
            claims.Add(new SecurityClaim
            {
                CustomerId = customerId,
                Type = ClaimTypes.Role,
                Value = "Customer"
            });
        }
    }
}
