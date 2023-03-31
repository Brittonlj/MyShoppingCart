namespace MyShoppingCart.Domain.Setup;

public sealed class MyShoppingCartSettings
{
    public const string SECTION_NAME = "MyShoppingCartSettings";

    public required int DefaultPageSize { get; init; }

    public DefaultPageSorting DefaultPageSorting = new();

}
