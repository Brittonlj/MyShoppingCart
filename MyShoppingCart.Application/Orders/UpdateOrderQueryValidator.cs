namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryValidator : AbstractValidator<UpdateOrderQuery>
{
    public UpdateOrderQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.ProductIds).NotEmpty();
        RuleForEach(x => x.ProductIds).NotEmpty();
    }
}
