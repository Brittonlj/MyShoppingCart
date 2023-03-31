namespace MyShoppingCart.Domain.Mediator;

public sealed class NotFound
{
    public static readonly NotFound Instance = new NotFound();

    public string Message { get; } = "Not Found";

	private NotFound() { }

    public NotFound(string message)
    {
        Message = message;
    }
}
