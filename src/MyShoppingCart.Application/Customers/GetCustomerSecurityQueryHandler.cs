namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerSecurityQueryHandler : 
    IRequestHandler<GetCustomerSecurityQuery, Response<IReadOnlyList<SecurityClaim>>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<SecurityClaim> _securityClaimRepository;

    public GetCustomerSecurityQueryHandler(
        IRepository<Customer> customerRepository, 
        IRepository<SecurityClaim> securityClaimRepository)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _securityClaimRepository = Guard.Against.Null(securityClaimRepository, nameof(securityClaimRepository));
    }

    public async Task<Response<IReadOnlyList<SecurityClaim>>> Handle(
        GetCustomerSecurityQuery request, 
        CancellationToken cancellationToken)
    {
        var customerQuery = new QueryCustomerById(request.CustomerId);
        var customer = await _customerRepository.FirstOrDefaultAsync(customerQuery, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        var claimsQuery = new QuerySecurityClaims(request.CustomerId);
        var claims = await _securityClaimRepository.ListAsync(claimsQuery, cancellationToken);

        return claims;
    }
}
