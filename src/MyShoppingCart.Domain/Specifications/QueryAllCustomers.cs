using MyShoppingCart.Domain.Data;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public class QueryAllCustomers : BaseSpecification<Customer>
{
    public const SortColumns DEFAULT_SORT_COLUMN = SortColumns.LastName;

    public enum SortColumns
    {
        LastName,
        FirstName,
        Email
    }

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

        if (!Enum.TryParse<SortColumns>(sortColumn, true, out var orderByEnum))
        {
            orderByEnum = DEFAULT_SORT_COLUMN;
        }

        if (sortAscending)
        {
            switch(orderByEnum)
            {
                case SortColumns.LastName:
                    Query.OrderBy(x => x.LastName);
                    break;
                case SortColumns.FirstName:
                    Query.OrderBy(x => x.FirstName);
                    break;
                case SortColumns.Email:
                    Query.OrderBy(x => x.Email);
                    break;
            }
        }
        else
        {
            switch (orderByEnum)
            {
                case SortColumns.LastName:
                    Query.OrderByDescending(x => x.LastName);
                    break;
                case SortColumns.FirstName:
                    Query.OrderByDescending(x => x.FirstName);
                    break;
                case SortColumns.Email:
                    Query.OrderByDescending(x => x.Email);
                    break;
            }
        }

        Query
            .AsSplitQuery()
            .Paginate(pageNumber, pageSize)
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress);
    }
}
