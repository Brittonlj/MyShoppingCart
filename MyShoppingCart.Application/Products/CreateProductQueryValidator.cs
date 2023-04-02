﻿namespace MyShoppingCart.Application.Products;

public sealed class CreateProductQueryValidator : AbstractValidator<CreateProductQuery>
{
	public CreateProductQueryValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
		RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
		RuleFor(x => x.Price).PrecisionScale(7, 2, false);
		RuleFor(x => x.ImageUrl).MaximumLength(50);
	}
}
