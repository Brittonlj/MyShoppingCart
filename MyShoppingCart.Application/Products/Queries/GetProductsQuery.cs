namespace MyShoppingCart.Application.Products.Queries;

public sealed record GetProductsQuery(
    string? SearchString,
    int PageNumber,
    int PageSize,
    string SortColumn,
    bool SortAscending = true
    ) : IQueryMany<Product>
{
}
