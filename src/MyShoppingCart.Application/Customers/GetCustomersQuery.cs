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
    
    public static readonly IReadOnlyDictionary<string, IOrderBy> OrderByClauses = new Dictionary<string, IOrderBy>
    {
        { nameof(Customer.FirstName), new OrderBy<Customer, string>(x => x.FirstName) },
        { nameof(Customer.LastName), new OrderBy<Customer, string>(x => x.LastName) },
        { nameof(Customer.Email), new OrderBy<Customer, string>(x => x.Email) },
    };

}
