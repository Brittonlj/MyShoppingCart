namespace MyShoppingCart.Domain.Setup;

public sealed class DefaultPageSorting
{
    public string Customer { get; init; } = "LastName";
    public string Product { get; init; } = "Name";
}
