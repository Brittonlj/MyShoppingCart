namespace Scratch;

internal class Person : IEquatable<Person>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Equals(Person? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public static bool operator ==(Person lhs, Person rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }
            return false;
        }
        
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Person lhs, Person rhs)
    {
        return !(lhs == rhs);
    }

}
