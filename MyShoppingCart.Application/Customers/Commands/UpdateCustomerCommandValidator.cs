namespace MyShoppingCart.Application.Customers.Commands;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull()
            .SetValidator(new CustomerValidator());
    }
}
