namespace MyShoppingCart.Domain.ValueObjects;

public sealed class Success
{
    public static readonly Success Instance = new Success();

	private Success() { }
}