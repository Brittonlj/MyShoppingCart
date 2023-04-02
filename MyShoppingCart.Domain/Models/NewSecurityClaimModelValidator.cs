namespace MyShoppingCart.Domain.Models;

public sealed class NewSecurityClaimModelValidator : AbstractValidator<NewSecurityClaimModel>
{
	public NewSecurityClaimModelValidator()
	{
        RuleFor(x => x.Type).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
    }
}
