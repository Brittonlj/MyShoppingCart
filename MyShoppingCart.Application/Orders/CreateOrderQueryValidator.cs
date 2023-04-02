namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryValidator : AbstractValidator<CreateOrderQuery>
{
    public CreateOrderQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.ProductIds).NotEmpty();
        RuleForEach(x => x.ProductIds).NotEmpty();
    }
}
