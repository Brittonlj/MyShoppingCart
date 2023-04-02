namespace MyShoppingCart.Application.Products;

public sealed record CreateProductQuery(
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl) :
    IQuery<Product>
{
}
