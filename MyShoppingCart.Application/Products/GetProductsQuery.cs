namespace MyShoppingCart.Application.Products;

public sealed record GetProductsQuery(
    string? SearchString,
    int PageNumber,
    int PageSize,
    string SortColumn,
    bool SortAscending = true
    ) : IQueryMany<ProductModel>
{
}
