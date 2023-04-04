using System.ComponentModel.DataAnnotations;

namespace MyShoppingCart.Domain.Configuration;

public sealed class DefaultPageSorting
{
    [Required]
    public required string Customer { get; init; }
    [Required]
    public required string Product { get; init; }
    [Required]
    public required string Order { get; init; }
}
