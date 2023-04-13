using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyShoppingCart.Application.Services;

public sealed class UserSecurityService : IUserSecurityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSecurityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = Guard.Against.Null(httpContextAccessor);
    }

    public bool IsInRole(string roleName)
    {
        ArgumentException.ThrowIfNullOrEmpty(roleName);

        var user = _httpContextAccessor.HttpContext.User;
        return user.IsInRole(roleName);
    }

    public Guid? GetCustomerId()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var customerIdClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (customerIdClaim is null || !Guid.TryParse(customerIdClaim.Value, out var customerIdGuid))
        {
            return null;
        }

        return customerIdGuid;
    }

}
