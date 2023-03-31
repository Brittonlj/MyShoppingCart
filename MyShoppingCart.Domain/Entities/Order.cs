using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Order : EntityBase, IEquatable<Order>
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDateTimeUtc { get; set; }
    public List<Product> Products { get; } = new();
    [JsonIgnore]
    public List<OrderProduct> OrderProducts { get; } = new();

    public void CopyTo(Order other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));
        if (Id != other.Id)
        {
            throw new ArgumentException("Can only clone Orders with the same OrderId");
        }
        other.CustomerId = CustomerId;
        other.OrderDateTimeUtc = OrderDateTimeUtc;
        other.Products.Clear();
        other.Products.AddRange(Products);
        other.OrderProducts.Clear();
        other.OrderProducts.AddRange(OrderProducts);
    }

    #region Equatable
    public bool Equals(Order? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            CustomerId == other.CustomerId &&
            OrderDateTimeUtc == other.OrderDateTimeUtc &&
            Products.SequenceEqual(other.Products) &&
            OrderProducts.SequenceEqual(other.OrderProducts);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Order);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CustomerId, OrderDateTimeUtc, Products, OrderProducts);
    }
    #endregion
}
