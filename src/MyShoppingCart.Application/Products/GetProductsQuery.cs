using MyShoppingCart.Domain.Data;

namespace MyShoppingCart.Application.Products;

public sealed record GetProductsQuery(
    string? SearchString = null,
    int PageNumber = Constants.DEFAULT_PAGE_NUMBER,
    int PageSize = Constants.DEFAULT_PAGE_SIZE,
    string SortColumn = GetProductsSpec.DEFAULT_SORT_COLUMN,
    bool SortAscending = true
    ) : IQueryManyPaged<Product>
{
}
