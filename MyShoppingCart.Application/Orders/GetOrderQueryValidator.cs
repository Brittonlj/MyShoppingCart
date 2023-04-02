namespace MyShoppingCart.Application.Orders;

public sealed class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
