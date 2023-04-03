namespace MyShoppingCart.Domain.Models;

public sealed record LineItemModel(Guid ProductId, int Quantity)
{
}
