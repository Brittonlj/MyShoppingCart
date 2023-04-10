namespace MyShoppingCart.Domain.Specifications;

public class GetSecurityClaimsByCustomerIdSpec : BaseSpecification<SecurityClaim>
{
    public GetSecurityClaimsByCustomerIdSpec(Guid customerId)
    {
        Query
            .Where(x => x.CustomerId == customerId);
    }
}
