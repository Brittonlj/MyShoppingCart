namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
