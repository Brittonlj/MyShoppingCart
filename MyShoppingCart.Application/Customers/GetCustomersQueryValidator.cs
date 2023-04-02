namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
{
    public GetCustomersQueryValidator()
    {
        RuleFor(x => x.SortColumn).NotEmpty().Must(x => GetCustomersQuery.OrderByClauses.Keys.Contains(x));
        RuleFor(x => x.PageNumber).NotEmpty();
        RuleFor(x => x.PageSize).NotEmpty().LessThanOrEqualTo(50);
        RuleFor(x => x.NamesLike).MaximumLength(50);
        RuleFor(x => x.EmailLike).MaximumLength(50);
    }
}
