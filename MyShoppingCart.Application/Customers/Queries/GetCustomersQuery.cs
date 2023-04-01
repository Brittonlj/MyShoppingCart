namespace MyShoppingCart.Application.Customers.Queries;

public sealed record GetCustomersQuery(
    string? NamesLike,
    string? EmailLike,
    int PageNumber,
    int PageSize,
    string SortColumn = "LastName",
    bool SortAscending = true
    ) : IQueryMany<Customer>
{
}
