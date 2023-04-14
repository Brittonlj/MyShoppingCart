using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Models;

public sealed class CategoryModel
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
}
