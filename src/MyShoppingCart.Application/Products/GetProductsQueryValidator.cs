namespace MyShoppingCart.Application.Products;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.SearchString).MaximumLength(50);
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .Must(x => GetProductsQuery.OrderByClauses.Keys.Contains(x))
            .WithErrorCode("InvalidSortColumn")
            .WithMessage("{PropertyValue} is an invalid value for {PropertyName}");
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);

    }
}
