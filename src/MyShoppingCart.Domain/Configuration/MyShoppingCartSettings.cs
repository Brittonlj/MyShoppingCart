using System.ComponentModel.DataAnnotations;

namespace MyShoppingCart.Domain.Configuration;

public sealed class MyShoppingCartSettings
{
    public const string SECTION_NAME = "MyShoppingCartSettings";

    [Required]
    public int DefaultPageSize { get; set; }

    [Required]
    public required DefaultPageSorting DefaultPageSorting { get; init; }

}
