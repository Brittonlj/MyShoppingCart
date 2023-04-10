namespace MyShoppingCart.Domain.Specifications;

public sealed class GetAllOrdersSpec : BaseSpecification<Order>
{
    public const SortColumns DEFAULT_SORT_COLUMN = SortColumns.OrderDateTimeUtc;

    public enum SortColumns
    {
        OrderDateTimeUtc
    }

    public GetAllOrdersSpec(
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
