using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Order : IEntity<Order>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid CustomerId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  
    public Customer? Customer { get; set; }
    public DateTime OrderDateTimeUtc { get; set; } = DateTime.UtcNow;
    public List<LineItem> LineItems { get; } = new();

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
            LineItems.SequenceEqual(other.LineItems);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Order);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CustomerId, OrderDateTimeUtc, LineItems);
    }

    public static bool operator ==(Order obj1, Order obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Order obj1, Order obj2) => !(obj1 == obj2);

    #endregion
}
