namespace MyShoppingCart.Domain.Specifications;

public class QuerySecurityClaims : BaseSpecification<SecurityClaim>
{
    public QuerySecurityClaims(Guid customerId)
    {
        Query
            .Where(x => x.CustomerId == customerId);
    }
}
