namespace MyShoppingCart.Application.Orders.Commands;

public sealed class DeleteCustomerOrderCommandValidator : AbstractValidator<DeleteCustomerOrderCommand>
{
    public DeleteCustomerOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
