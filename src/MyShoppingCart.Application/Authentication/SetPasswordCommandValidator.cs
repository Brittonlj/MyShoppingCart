namespace MyShoppingCart.Application.Authentication;

public sealed class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
{
	public SetPasswordCommandValidator()
	{
		RuleFor(x => x.CustomerId).NotEmpty();
		RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
	}
}
