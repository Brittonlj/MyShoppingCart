using FluentValidation;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.EntityValidators;

public sealed class ProductValidator : AbstractValidator<Product>
{
	public ProductValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x =>x.Name)
			.NotEmpty()
			.MaximumLength(50);
		RuleFor(x => x.Description)
			.NotEmpty()
			.MaximumLength(500);
		RuleFor(x => x.Price)
			.NotEmpty();
		RuleFor(x => x.ImageUrl)
			.MaximumLength(50);

	}
}
