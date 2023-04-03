namespace MyShoppingCart.Domain.Models;

public sealed class SecurityClaimModelValidator : AbstractValidator<SecurityClaimModel>
{
	public SecurityClaimModelValidator()
	{
        RuleFor(x => x.Type).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
    }
}
