namespace MyShoppingCart.Application.Orders.Commands;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .SetValidator(new OrderValidator());
    }
}
