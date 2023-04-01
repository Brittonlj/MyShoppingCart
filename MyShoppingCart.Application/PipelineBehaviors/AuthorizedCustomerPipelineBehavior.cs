using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyShoppingCart.Application.PipelineBehaviors;

public sealed class AuthorizedCustomerPipelineBehavior<TRequest, TEntity> :
    IPipelineBehavior<TRequest, Response<TEntity>>
    where TRequest : IRequest<Response<TEntity>>
    where TEntity : class
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizedCustomerPipelineBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<TEntity>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Response<TEntity>> next, 
        CancellationToken cancellationToken)
    {
        if (request is not IAuthorizedCustomerRequest authorizedCustomerRequest)
        {
            return await next();
        }

        var user = _httpContextAccessor.HttpContext.User;

        var isAdmin = user.IsInRole("Admin");

        if (isAdmin)
        {
            return await next();
        }

        var customerIdClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (customerIdClaim is null ||
            !Guid.TryParse(customerIdClaim.Value, out var customerId) ||
            authorizedCustomerRequest.CustomerId != customerId)
        {
            return Unauthorized.Instance;
        }

        return await next();
    }
}
