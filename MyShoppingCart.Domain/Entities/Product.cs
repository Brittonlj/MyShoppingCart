using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Product : EntityBase, IEquatable<Product>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; } = new();

    public void CopyTo(Product other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));
        if (Id != other.Id)
        {
            throw new ArgumentException("Can only clone Products with the same ProductId");
        }
        other.Name = Name;
        other.Description = Description;
        other.Price = Price;
        other.ImageUrl = ImageUrl;
    }

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
    #endregion
}