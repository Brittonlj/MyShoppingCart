using MyShoppingCart.Domain.Data;

namespace MyShoppingCart.Application.Customers;

public sealed record GetCustomersQuery(
    string? NamesLike = null,
    string? EmailLike = null,
    int PageNumber = Constants.DEFAULT_PAGE_NUMBER,
    int PageSize = Constants.DEFAULT_PAGE_SIZE,
    string SortColumn = GetCustomersSpec.DEFAULT_SORT_COLUMN,
    bool SortAscending = true
    ) : IQueryManyPaged<Customer>
{
}
