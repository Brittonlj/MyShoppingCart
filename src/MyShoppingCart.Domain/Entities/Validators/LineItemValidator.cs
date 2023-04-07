namespace MyShoppingCart.Domain.Entities.Validators;

public sealed class LineItemValidator : AbstractValidator<LineItem>
{
	public LineItemValidator()
	{
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}
