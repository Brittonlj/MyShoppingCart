using System.ComponentModel.DataAnnotations;

namespace MyShoppingCart.Domain.Configuration;

public sealed class MyShoppingCartSettings
{
    public const string SECTION_NAME = "MyShoppingCartSettings";

    [Required]
    public int DefaultPageSize { get; init; }

    [Required]
    public DefaultPageSorting DefaultPageSorting = new();

}
