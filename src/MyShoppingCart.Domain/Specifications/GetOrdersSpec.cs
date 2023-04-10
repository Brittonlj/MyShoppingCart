namespace MyShoppingCart.Domain.Specifications;

public sealed class GetOrdersSpec : BaseSpecification<Order>
{
    public const string DEFAULT_SORT_COLUMN = "OrderDateTimeUtc";

    public enum SortColumns
    {
        OrderDateTimeUtc
    }

    public GetOrdersSpec(
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
