namespace MyShoppingCart.Application.Products;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
	public DeleteProductCommandValidator()
	{
		RuleFor(x => x.ProductId).NotEmpty();
	}
}
