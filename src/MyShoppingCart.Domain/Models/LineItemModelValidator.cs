namespace MyShoppingCart.Domain.Models;

public class LineItemModelValidator : AbstractValidator<LineItemModel>
{
    public LineItemModelValidator() 
	{
        RuleFor(x => x.ProductId).NotEmpty();
		RuleFor(x => x.Quantity).NotEmpty();
	}
}
