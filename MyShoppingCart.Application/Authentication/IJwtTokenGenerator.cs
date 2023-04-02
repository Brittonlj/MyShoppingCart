namespace MyShoppingCart.Application.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Customer customer);
    }
}