namespace MyShoppingCart.Domain.Configuration;

public sealed class DefaultPageSorting
{
    public string Customer { get; init; } = "LastName";
    public string Product { get; init; } = "Name";
    public string Order { get; init; } = "OrderDateTimeUtc";
}
