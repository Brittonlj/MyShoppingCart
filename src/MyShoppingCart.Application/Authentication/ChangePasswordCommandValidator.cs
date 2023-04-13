namespace MyShoppingCart.Application.Authentication;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
	public ChangePasswordCommandValidator()
	{
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.CurrentPassword).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NewPassword).NotEmpty().MaximumLength(50);
    }
}
