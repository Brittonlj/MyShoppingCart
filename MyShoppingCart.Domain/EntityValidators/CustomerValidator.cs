using FluentValidation;
using FluentValidation.Results;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.EntityValidators;

public sealed class CustomerValidator : AbstractValidator<Customer>
{
	public CustomerValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
		RuleFor(x => x.ShippingAddressId).NotEmpty();
		RuleFor(x => x.BillingAddressId).NotEmpty();
        RuleFor(x => x.ShippingAddress).NotNull().SetValidator(new AddressValidator());
        RuleFor(x => x.BillingAddress).NotNull().SetValidator(new AddressValidator());
    }

    public override async Task<ValidationResult> ValidateAsync(
        ValidationContext<Customer> context, 
        CancellationToken cancellation = default)
    {
        CustomValidate(context);
        return await base.ValidateAsync(context, cancellation);
    }

    public override ValidationResult Validate(ValidationContext<Customer> context)
    {
        CustomValidate(context);
        return base.Validate(context);
    }

    private void CustomValidate(ValidationContext<Customer> context)
    {
        var customer = context.InstanceToValidate;
        if (customer.BillingAddressId is not null &&
            customer.BillingAddress is not null &&
            customer.BillingAddressId.Value != customer.BillingAddress.Id)
        {
            context.AddFailure(new ValidationFailure(
                "BillingAddressId", "BillingAddressId does not match BillingAddress.AddressId"));
        }

        if (customer.ShippingAddressId is not null &&
                    customer.ShippingAddress is not null &&
                    customer.ShippingAddressId.Value != customer.ShippingAddress.Id)
        {
            context.AddFailure(new ValidationFailure(
                "ShippingAddressId", "ShippingAddressId does not match ShippingAddress.AddressId"));
        }

        foreach (var order in customer.Orders)
        {
            if (order.CustomerId != customer.Id)
            {
                context.AddFailure(new ValidationFailure(
                    "Order.CustomerId",
                    $"OrderId {order.Id} has a CustomerId of {order.CustomerId} which does not match the root CustomerId of {customer.Id}."));
            }
        }
    }
}
