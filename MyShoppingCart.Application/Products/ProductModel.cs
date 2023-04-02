namespace MyShoppingCart.Application.Products;

public sealed record ProductModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl
)
{
}
