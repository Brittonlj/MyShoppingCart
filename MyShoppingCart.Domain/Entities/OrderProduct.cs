namespace MyShoppingCart.Domain.Entities;

public sealed class OrderProduct : IEquatable<OrderProduct>
{
    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }

    #region Equatable
    public bool Equals(OrderProduct? other)
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
        return Equals(obj as OrderProduct);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OrderId, ProductId, Quantity);
    }

    public static bool operator ==(OrderProduct obj1, OrderProduct obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(OrderProduct obj1, OrderProduct obj2) => !(obj1 == obj2);

    #endregion
}
