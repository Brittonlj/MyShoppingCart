﻿namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryValidator : AbstractValidator<CreateCustomerQuery>
{
    public CreateCustomerQueryValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        RuleFor(x => x.BillingAddress).NotNull().SetValidator(new AddressModelValidator()!);
        RuleFor(x => x.ShippingAddress).NotNull().SetValidator(new AddressModelValidator()!);
    }
}
