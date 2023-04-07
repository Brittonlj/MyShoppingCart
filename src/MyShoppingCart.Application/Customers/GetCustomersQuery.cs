namespace MyShoppingCart.Application.Customers;

public sealed record GetCustomersQuery(
    string? NamesLike,
    string? EmailLike,
    int PageNumber,
    int PageSize,
    string SortColumn = "LastName",
    bool SortAscending = true
    ) : IQueryManyPaged<Customer>
{
}
