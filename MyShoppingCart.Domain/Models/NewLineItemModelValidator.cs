namespace MyShoppingCart.Domain.Models;

public class NewLineItemModelValidator : AbstractValidator<NewLineItemModel>
{
    public NewLineItemModelValidator() 
	{
        RuleFor(x => x.ProductId).NotEmpty();
		RuleFor(x => x.Quantity).NotEmpty();
	}
}
