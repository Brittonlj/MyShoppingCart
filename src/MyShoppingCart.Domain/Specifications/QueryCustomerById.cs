using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryCustomerById : BaseSpecification<Customer>, ISingleResultSpecification
{
    public QueryCustomerById(Guid customerId)
    {
        Query
            .Where(x => x.Id == customerId)
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress)
            .AsSplitQuery();
    }
}
