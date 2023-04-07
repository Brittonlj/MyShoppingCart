using System.Security.Claims;
using System.Transactions;

namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryHandler : IRequestHandler<CreateCustomerQuery, Response<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<SecurityClaim> _securityClaimRepository;

    public CreateCustomerQueryHandler(
        IRepository<Customer> customerRepository, 
        IRepository<SecurityClaim> securityClaimRepository)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _securityClaimRepository = Guard.Against.Null(securityClaimRepository, nameof(securityClaimRepository));
    }

    public async Task<Response<Customer>> Handle(CreateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = MapToCustomer(request);

        var claims = GetDefaultClaims(customer.Id);

        using var transaction = new TransactionScope();

        await _customerRepository.AddAsync(customer, cancellationToken);

        await _securityClaimRepository.AddRangeAsync(claims, cancellationToken);

        transaction.Complete();

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

    private List<SecurityClaim> GetDefaultClaims(Guid customerId)
    {
        var claims = new List<SecurityClaim>();

        claims.Add(new SecurityClaim
        {
            CustomerId = customerId,
            Type = ClaimTypes.NameIdentifier,
            Value = customerId.ToString()
        });

        claims.Add(new SecurityClaim
        {
            CustomerId = customerId,
            Type = ClaimTypes.Role,
            Value = "Customer"
        });

        return claims;
    }
}
