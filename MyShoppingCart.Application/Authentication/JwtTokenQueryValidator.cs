namespace MyShoppingCart.Application.Authentication;

public class JwtTokenQueryValidator : AbstractValidator<JwtTokenQuery>
{
    public JwtTokenQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Role).MaximumLength(50);
    }
}
