using System.Security.Claims;

namespace MyShoppingCart.Api.Utilities;

public static class HttpContextUtilities
{
    public static Guid? GetCustomerId(this HttpContext context)
    {
        string? customerIdString = context.User?.Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(customerIdString))
        {
            return null;
        }

        if (Guid.TryParse(customerIdString, out var customerId))
        {
            return customerId;
        }

        return null;
    }
}
