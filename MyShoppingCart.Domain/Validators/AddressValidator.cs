using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
	public AddressValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.Street).NotEmpty().MaximumLength(50);
		RuleFor(x => x.City).NotEmpty().MaximumLength(50);
		RuleFor(x => x.State).NotEmpty().MaximumLength(50);
		RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10);
	}
}
