using System.Security.Claims;
using System.Transactions;

namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryHandler : IRequestHandler<CreateCustomerQuery, Response<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<SecurityClaim> _securityClaimRepository;
    private readonly IMapper _mapper;

    public CreateCustomerQueryHandler(
        IRepository<Customer> customerRepository, 
        IRepository<SecurityClaim> securityClaimRepository,
        IMapper mapper)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _securityClaimRepository = Guard.Against.Null(securityClaimRepository, nameof(securityClaimRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Response<Customer>> Handle(CreateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);

        var claims = GetDefaultClaims(customer.Id);

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        customer = await _customerRepository.AddAsync(customer, cancellationToken);

        await _securityClaimRepository.AddRangeAsync(claims, cancellationToken);

        transaction.Complete();

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
