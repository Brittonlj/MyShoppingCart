namespace MyShoppingCart.Domain.Utilities;

public sealed class UtcDateTimeProvider : IUtcDateTimeProvider
{
    public DateTime GetUtcDateTime()
    {
        return DateTime.UtcNow;
    }
}
