namespace MyShoppingCart.Application.Authentication;

public sealed class RegisterQueryValidator : AbstractValidator<RegisterQuery>
{
	public RegisterQueryValidator()
	{
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
    }
}
