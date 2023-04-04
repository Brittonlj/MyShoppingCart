using System.ComponentModel.DataAnnotations;

namespace MyShoppingCart.Domain.Configuration;

public sealed class DefaultPageSorting
{
    [Required]
    public string Customer { get; init; } = string.Empty;
    [Required]
    public string Product { get; init; } = string.Empty;
    [Required]
    public string Order { get; init; } = string.Empty;
}
