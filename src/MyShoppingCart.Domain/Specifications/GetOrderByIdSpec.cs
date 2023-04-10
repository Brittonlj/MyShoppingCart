namespace MyShoppingCart.Domain.Specifications;

public sealed class GetOrderByIdSpec : BaseSpecification<Order>, ISingleResultSpecification
{
    public GetOrderByIdSpec(Guid orderId, Guid customerId)
    {
        Query
            .Where(x => x.Id == orderId && x.CustomerId == customerId)
            .Include(x => x.LineItems)
            .ThenInclude(x => x.Product)
            .AsSplitQuery();
    }
}
