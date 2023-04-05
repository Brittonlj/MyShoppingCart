namespace MyShoppingCart.Application.Products;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand
{
}
