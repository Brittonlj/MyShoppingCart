using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Validators;

public sealed class SecurityClaimValidator : AbstractValidator<SecurityClaim>
{
	public SecurityClaimValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Type).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
    }
}
