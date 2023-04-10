namespace MyShoppingCart.Domain.Specifications;

public class GetAllSecurityClaimsByCustomerIdSpec : BaseSpecification<SecurityClaim>
{
    public GetAllSecurityClaimsByCustomerIdSpec(Guid customerId)
    {
        Query
            .Where(x => x.CustomerId == customerId);
    }
}
