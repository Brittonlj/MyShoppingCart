namespace MyShoppingCart.Domain.Entities;

public sealed class Customer : IEquatable<Customer>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public required Guid BillingAddressId { get; set; }
    public required Address ShippingAddress { get; set; }
    public required Address BillingAddress { get; set; }


    #region Equatable
    public bool Equals(Customer? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            FirstName == other.FirstName &&
            LastName == other.LastName &&
            Email == other.Email &&
            ShippingAddressId == other.ShippingAddressId &&
            BillingAddressId == other.BillingAddressId &&
            ShippingAddress == other.ShippingAddress &&
            BillingAddress == other.BillingAddress;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Customer);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Email, ShippingAddressId, BillingAddressId);
    }

    public static bool operator ==(Customer obj1, Customer obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Customer obj1, Customer obj2) => !(obj1 == obj2);

    #endregion
}