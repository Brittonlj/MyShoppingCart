using FluentValidation;
using FluentValidation.Results;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.EntityValidators;

public sealed class OrderValidator : AbstractValidator<Order>
{
	public OrderValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.OrderDateTimeUtc)
			.NotEmpty()
			.LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(5))
			.WithMessage("Order Date Time Utc may not be in the future");
		RuleFor(x => x.Products)
			.NotEmpty()
			.ForEach(x => x.SetValidator(new ProductValidator()));
		RuleFor(x => x.CustomerId)
			.NotNull();
	}

    public override async Task<ValidationResult> ValidateAsync(
        ValidationContext<Order> context, 
        CancellationToken cancellation = default)
    {
        CustomValidate(context);
        return await base.ValidateAsync(context, cancellation);
    }

    public override ValidationResult Validate(ValidationContext<Order> context)
    {
        CustomValidate(context);
        return base.Validate(context);
    }

    private void CustomValidate(ValidationContext<Order> context)
    {
        var order = context.InstanceToValidate;

        if (!order.OrderProducts.Select(x => x.OrderId).Contains(order.Id))
        {
            context.AddFailure(new ValidationFailure("OrderProducts", "There are one or more invalid OrderIds in OrderProducts"));
        }
    }
}
