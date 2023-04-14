using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.Entities;

public sealed class ProductCategory
{
    public required Guid ProductId { get; init; }
    public required Guid CategoryId { get; init; }

    public ProductCategory()
    {
    }

    [SetsRequiredMembers]
    public ProductCategory(Guid productId, Guid categoryId)
    {
        ProductId = productId; 
        CategoryId = categoryId;
    }
}
