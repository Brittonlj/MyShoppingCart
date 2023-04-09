namespace MyShoppingCart.Application.Services
{
    public interface IUserSecurityService
    {
        Guid? GetCustomerId();
        bool IsInRole(string roleName);
    }
}