namespace MyShoppingCart.Application.Products;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.SearchString).MaximumLength(50);
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .Must(x => OrderByClauses.Products.Keys.Contains(x));
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);

    }
}
