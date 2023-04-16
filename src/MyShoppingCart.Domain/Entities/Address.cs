using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.Entities;

public sealed class Address : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }

    public Address()
    {
    }

    [SetsRequiredMembers]
    public Address(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    [SetsRequiredMembers]
    public Address(Guid id, string street, string city, string state, string postalCode)
    {
        Id = id;
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    #region Equatable
    public bool Equals(Address? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            string.Equals(Street, other.Street) &&
            string.Equals(City, other.City) &&
            string.Equals(State, other.State) &&
            string.Equals(PostalCode, other.PostalCode);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as Address);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Street, City, State, PostalCode);
    }

    public static bool operator ==(Address obj1, Address obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Address obj1, Address obj2) => !(obj1 == obj2);
    #endregion
}
