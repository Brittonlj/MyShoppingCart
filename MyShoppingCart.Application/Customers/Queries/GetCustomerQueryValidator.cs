namespace MyShoppingCart.Application.Customers.Queries;

public sealed class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
