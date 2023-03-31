namespace MyShoppingCart.Domain.Entities;

public abstract class EntityBase : IEquatable<EntityBase>
{
    public Guid Id { get; init; } = Guid.NewGuid();

    #region IEquatable
    public bool Equals(EntityBase? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityBase);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    #endregion
}
