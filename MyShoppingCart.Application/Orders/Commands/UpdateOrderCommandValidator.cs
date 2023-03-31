namespace MyShoppingCart.Application.Orders.Commands;

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .SetValidator(new OrderValidator());
    }
}
