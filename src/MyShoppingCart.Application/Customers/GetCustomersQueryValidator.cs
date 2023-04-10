namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
{
    private readonly string _validSortColumns = string.Join(", ", Enum.GetNames(typeof(GetCustomersSpec.SortColumns)));
    
    public GetCustomersQueryValidator()
    {
        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .IsEnumName(typeof(GetCustomersSpec.SortColumns), caseSensitive: false)
            .WithErrorCode("InvalidSortColumn")
            .WithMessage("'{PropertyValue}' is an invalid value for '{PropertyName}'.  Please use one of '" + _validSortColumns + "'.");
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);
        RuleFor(x => x.NamesLike).MaximumLength(50);
        RuleFor(x => x.EmailLike).MaximumLength(50);
    }
}
