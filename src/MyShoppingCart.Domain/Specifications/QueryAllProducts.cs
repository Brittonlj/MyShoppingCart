using MyShoppingCart.Domain.Data;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryAllProducts : BaseSpecification<Product>
{
    public const string DEFAULT_SORT_COLUMN = "Name";

    public QueryAllProducts(
        string? searchString,
        int pageNumber,
        int pageSize,
        string sortColumn,
        bool sortAscending = true
    )
    {
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            Query
                .Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString));
        }

        if (!SortColumns.Products.TryGetValue(sortColumn, out var orderByClause))
        {
            orderByClause = SortColumns.Products[DEFAULT_SORT_COLUMN];
        }

        if (sortAscending )
        {
            Query.OrderBy(orderByClause);
        }
        else
        {
            Query.OrderByDescending(orderByClause);
        }

        Query.Paginate(pageNumber, pageSize);
    }
}
