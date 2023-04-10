namespace MyShoppingCart.Domain.Specifications;

public sealed class GetCustomerByIdSpec : BaseSpecification<Customer>, ISingleResultSpecification
{
    public GetCustomerByIdSpec(Guid customerId)
    {
        Query
            .Where(x => x.Id == customerId)
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress)
            .AsSplitQuery();
    }
}
