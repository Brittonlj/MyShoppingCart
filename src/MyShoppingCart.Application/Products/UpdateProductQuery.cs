namespace MyShoppingCart.Application.Products;

public sealed record UpdateProductQuery(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    IReadOnlyCollection<Category> Categories) :
    IQuery<Product>
{
}
