namespace MyShoppingCart.Application.Customers.Commands;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull()
            .SetValidator(new CustomerValidator());

    }
}
