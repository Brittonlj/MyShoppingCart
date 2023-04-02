namespace MyShoppingCart.Application.Orders;

public sealed class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .Must(x => OrderByClauses.Orders.Keys.Contains(x));
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);
    }
}
