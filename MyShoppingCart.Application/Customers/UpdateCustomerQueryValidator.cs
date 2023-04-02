
namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryValidator : AbstractValidator<UpdateCustomerQuery>
{
    public UpdateCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(x => x.ShippingAddress).NotNull().SetValidator(new AddressModelValidator());
        RuleFor(x => x.BillingAddress).NotNull().SetValidator(new AddressModelValidator());
    }
}
