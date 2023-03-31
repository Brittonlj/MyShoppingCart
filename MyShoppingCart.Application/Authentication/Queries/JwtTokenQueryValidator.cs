namespace MyShoppingCart.Application.Authentication.Queries;

public class JwtTokenQueryValidator : AbstractValidator<JwtTokenQuery>
{
    public JwtTokenQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Role).MaximumLength(50);
    }
}
