using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Customer : IEntity<Customer>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public Guid? ShippingAddressId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Address? ShippingAddress { get; set; }
    public Guid? BillingAddressId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Address? BillingAddress { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<SecurityClaim> Claims { get; } = new();

    #region Equatable
    public bool Equals(Customer? other)
    {
        if (other is null) 
            return false;

        if (ReferenceEquals(this, other)) 
            return true;

        return
            Id == other.Id &&
            FirstName == other.FirstName &&
            LastName == other.LastName &&
            Email == other.Email &&
            EqualityComparer<Address>.Default.Equals(ShippingAddress, other.ShippingAddress) &&
            EqualityComparer<Address>.Default.Equals(BillingAddress, other.BillingAddress) &&
            Claims.SequenceEqual(other.Claims);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Customer);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Email, ShippingAddress, BillingAddress, Claims);
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