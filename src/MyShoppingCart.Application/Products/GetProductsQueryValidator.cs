namespace MyShoppingCart.Application.Products;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    private readonly string _validSortColumns = string.Join(", ", Enum.GetNames(typeof(QueryAllProducts.SortColumns)));

    public GetProductsQueryValidator()
    {
        RuleFor(x => x.SearchString).MaximumLength(50);
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .IsEnumName(typeof(QueryAllProducts.SortColumns), caseSensitive: false)
            .WithErrorCode("InvalidSortColumn")
            .WithMessage("'{PropertyValue}' is an invalid value for '{PropertyName}'.  Please use one of '" + _validSortColumns + "'.");
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);

    }
}
