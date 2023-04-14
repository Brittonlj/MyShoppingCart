namespace MyShoppingCart.Domain.Entities;

public sealed class Product : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public List<Category> Categories { get; } = new List<Category>();

    #region Equatable
    public bool Equals(Product? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            Name == other.Name &&
            Price == other.Price &&
            ImageUrl == other.ImageUrl;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Product);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, Price, ImageUrl);
    }

    public static bool operator ==(Product obj1, Product obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Product obj1, Product obj2) => !(obj1 == obj2);

    #endregion
}