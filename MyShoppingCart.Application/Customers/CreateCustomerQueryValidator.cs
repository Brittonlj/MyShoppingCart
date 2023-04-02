using MyShoppingCart.Domain.Models;
using MyShoppingCart.Domain.Validators;

namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryValidator : AbstractValidator<CreateCustomerQuery>
{
    public CreateCustomerQueryValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(x => x.BillingAddress).SetValidator(new NewAddressModelValidator());
        RuleFor(x => x.ShippingAddress).SetValidator(new NewAddressModelValidator());
        RuleForEach(x => x.Claims).SetValidator(new NewSecurityClaimModelValidator());
    }
}
