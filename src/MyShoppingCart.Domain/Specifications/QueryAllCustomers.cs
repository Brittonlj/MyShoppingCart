using MyShoppingCart.Domain.Data;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public class QueryAllCustomers : BaseSpecification<Customer>
{
    public const string DEFAULT_SORT_COLUMN = "LastName";
    public QueryAllCustomers(
        string? namesLike,
        string? emailLike,
        int pageNumber,
        int pageSize,
        string sortColumn,
        bool sortAscending = true
    )
    {
        if (!string.IsNullOrWhiteSpace(namesLike))
        {
            Query.Where(x => x.FirstName.Contains(namesLike) || x.LastName.Contains(namesLike));
        }

        if (!string.IsNullOrWhiteSpace(emailLike))
        {
            Query.Where(x => x.Email.Contains(emailLike));
        }

        if (!SortColumns.Customers.TryGetValue(sortColumn, out var orderByClause))
        {
            orderByClause = SortColumns.Customers[DEFAULT_SORT_COLUMN];
        }

        if (sortAscending)
        {
            Query.OrderBy(orderByClause);
        }
        else
        {
            Query.OrderByDescending(orderByClause);
        }

        Query
            .AsSplitQuery()
            .Paginate(pageNumber, pageSize)
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress);
    }
}
