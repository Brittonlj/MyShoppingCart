namespace MyShoppingCart.Domain.Models;

public sealed class NewLineItemModel
{
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
}
