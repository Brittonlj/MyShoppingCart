namespace MyShoppingCart.Application.Products;

public sealed record UpdateProductQuery(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl) :
    IQuery<Product>
{
}
