namespace MyShoppingCart.Application.Addresses;

public static class AddressMapper
{
    public static Address ToEntity(this AddressModel other)
    {
        return new Address
        {
            Street = other.Street,
            City = other.City,
            State = other.State,
            PostalCode = other.PostalCode
        };
    }

    public static AddressModel ToModel(this Address other)
    {
        return new AddressModel(
            other.Street,
            other.City,
            other.State,
            other.PostalCode);
    }
}
