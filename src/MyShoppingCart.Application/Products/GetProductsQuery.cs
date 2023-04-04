namespace MyShoppingCart.Application.Products;

public sealed record GetProductsQuery(
    string? SearchString,
    int PageNumber,
    int PageSize,
    string SortColumn,
    bool SortAscending = true
    ) : IQueryManyPaged<Product>
{
    public static readonly IReadOnlyDictionary<string, IOrderBy> OrderByClauses =
        new Dictionary<string, IOrderBy>
        {
            { nameof(Product.Name), new OrderBy<Product, string>(x => x.Name) },
            { nameof(Product.Price), new OrderBy<Product, decimal>(x => x.Price) },
        };

}
