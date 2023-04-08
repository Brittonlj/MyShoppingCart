using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryOrderById : BaseSpecification<Order>, ISingleResultSpecification
{
    public QueryOrderById(Guid orderId, Guid customerId)
    {
        Query
            .Where(x => x.Id == orderId && x.CustomerId == customerId)
            .Include(x => x.LineItems)
            .ThenInclude(x => x.Product)
            .AsSplitQuery();
    }
}
