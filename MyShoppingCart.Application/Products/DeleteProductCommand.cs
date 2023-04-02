namespace MyShoppingCart.Application.Products;

public sealed record DeleteProductCommand(Guid Id) : ICommand
{
}
