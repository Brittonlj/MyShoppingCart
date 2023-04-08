using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.Entities;

public sealed class LineItem : IEntity<Guid>, IEquatable<LineItem>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }

    public LineItem()
    {
    }

    [SetsRequiredMembers]
    public LineItem(Guid orderId, Guid productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    [SetsRequiredMembers]
    public LineItem(Guid id, Guid orderId, Guid productId, int quantity)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    #region Equatable
    public bool Equals(LineItem? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            OrderId == other.OrderId &&
            ProductId == other.ProductId &&
            Quantity == other.Quantity;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LineItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OrderId, ProductId, Quantity);
    }

    public static bool operator ==(LineItem obj1, LineItem obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(LineItem obj1, LineItem obj2) => !(obj1 == obj2);

    #endregion
}
