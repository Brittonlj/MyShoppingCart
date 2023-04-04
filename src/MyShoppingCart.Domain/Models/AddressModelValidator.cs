namespace MyShoppingCart.Domain.Models;

public sealed class AddressModelValidator : AbstractValidator<AddressModel>
{
	public AddressModelValidator()
	{
        RuleFor(x => x.Street).NotEmpty().MaximumLength(50);
        RuleFor(x => x.City).NotEmpty().MaximumLength(50);
        RuleFor(x => x.State).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10);
    }
}
