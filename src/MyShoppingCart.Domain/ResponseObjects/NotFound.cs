namespace MyShoppingCart.Domain.ResponseObjects;

public sealed class NotFound
{
    public static readonly NotFound Instance = new NotFound();

    public object? Message { get; }

	private NotFound() { }

    public NotFound(object? message = null)
    {
        Message = message;
    }
}
