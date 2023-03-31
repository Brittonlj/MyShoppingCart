namespace MyShoppingCart.Domain.Entities;

public sealed class OrderProduct : IEquatable<OrderProduct>
{
    public required Guid OrderId { get; init; }
    public required Guid ProductId { get; init; }

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
            ProductId == other.ProductId;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OrderProduct);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OrderId, ProductId);
    }
    #endregion
}
