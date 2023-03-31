using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Customer : EntityBase, IEquatable<Customer>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public Guid? ShippingAddressId { get; set; }
    public Guid? BillingAddressId { get; set; }
    public Address? ShippingAddress { get; set; }
    public Address? BillingAddress { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; } = new();


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
            BillingAddress == other.BillingAddress &&
            Orders.SequenceEqual(other.Orders);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Customer);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Email, ShippingAddressId, BillingAddressId);
    }
    #endregion
}