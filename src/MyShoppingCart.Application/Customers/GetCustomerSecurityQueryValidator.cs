namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerSecurityQueryValidator : AbstractValidator<GetCustomerSecurityQuery>
{
	public GetCustomerSecurityQueryValidator()
	{
		RuleFor(x => x.CustomerId).NotEmpty();
	}
}
