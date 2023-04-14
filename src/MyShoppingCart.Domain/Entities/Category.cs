using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.Entities;

public sealed class Category : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    public Category()
    {
    }

    [SetsRequiredMembers]
    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
