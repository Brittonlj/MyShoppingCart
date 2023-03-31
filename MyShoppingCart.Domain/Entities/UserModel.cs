namespace MyShoppingCart.Domain.Entities;

public class UserModel
{
    public required Guid CustomerId { get; init; }
    public required string Role { get; init; }
}
