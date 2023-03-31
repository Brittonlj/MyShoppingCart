namespace MyShoppingCart.Domain.Entities;

public sealed class Address : EntityBase, IEquatable<Address>
{
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
 
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
            Street == other.Street &&
            City == other.City &&
            State == other.State &&
            PostalCode == other.PostalCode;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as Address);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Street, City, State, PostalCode);
    }
    #endregion
}
