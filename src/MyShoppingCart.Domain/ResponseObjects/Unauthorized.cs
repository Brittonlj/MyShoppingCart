namespace MyShoppingCart.Domain.ResponseObjects;

public sealed class Unauthorized
{
    public readonly static Unauthorized Instance = new Unauthorized();

    private Unauthorized() { }
}
