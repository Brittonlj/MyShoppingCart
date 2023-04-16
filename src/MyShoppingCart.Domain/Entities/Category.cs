using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.Entities;

public sealed class Category : IEquatable<Category>, IEntity<Guid>
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

    #region Equatable
    public bool Equals(Category? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            string.Equals(Name, other.Name);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Category);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public static bool operator ==(Category obj1, Category obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Category obj1, Category obj2) => !(obj1 == obj2);

    #endregion
}
