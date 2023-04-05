namespace MyShoppingCart.Application.Orders;

public sealed class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .Must(x => GetOrdersQuery.OrderByClauses.Keys.Contains(x))
            .WithErrorCode("InvalidSortColumn")
            .WithMessage("{PropertyValue} is an invalid value for {PropertyName}");
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);
    }
}
