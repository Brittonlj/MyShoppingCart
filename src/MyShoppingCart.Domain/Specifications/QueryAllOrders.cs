using MyShoppingCart.Domain.Data;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryAllOrders : BaseSpecification<Order>
{
    public QueryAllOrders(
        Guid customerId,
        int pageNumber,
        int pageSize,
        bool sortAscending = false
    )
    {
        Query
            .Where(x => x.CustomerId == customerId);

        if (sortAscending)
        {
            Query.OrderBy(x => x.OrderDateTimeUtc);
        }
        else
        {
            Query.OrderByDescending(x => x.OrderDateTimeUtc);
        }

        Query
            .AsSplitQuery()
            .Paginate(pageNumber, pageSize)
            .Include(x => x.LineItems)
            .ThenInclude(x => x.Product);
    }
}
