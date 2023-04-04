using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class SecurityClaim : IEntity<SecurityClaim>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid CustomerId { get; set; } = Guid.Empty;
    public required string Type { get; set; }
    public required string Value { get; set; }

    #region Equatable
    public bool Equals(SecurityClaim? other)
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
            Type == other.Type &&
            Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SecurityClaim);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CustomerId, Type, Value);
    }

    public static bool operator ==(SecurityClaim obj1, SecurityClaim obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(SecurityClaim obj1, SecurityClaim obj2) => !(obj1 == obj2);
    #endregion
}
