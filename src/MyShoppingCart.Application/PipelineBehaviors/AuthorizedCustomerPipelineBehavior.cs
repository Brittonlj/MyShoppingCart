using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.PipelineBehaviors;

public sealed class AuthorizedCustomerPipelineBehavior<TRequest, TEntity> :
    IPipelineBehavior<TRequest, Response<TEntity>>
    where TRequest : IRequest<Response<TEntity>>
    where TEntity : class
{
    private readonly IUserSecurityService _userSecurityService;

    public AuthorizedCustomerPipelineBehavior(IUserSecurityService userSecurityService)
    {
        _userSecurityService = Guard.Against.Null(userSecurityService);
    }

    public async Task<Response<TEntity>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Response<TEntity>> next, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        if (request is not IAuthorizedCustomerRequest authorizedCustomerRequest)
        {
            return await next();
        }

        var isAdmin = _userSecurityService.IsInRole(Roles.Admin);

        if (isAdmin)
        {
            return await next();
        }

        var customerId = _userSecurityService.GetCustomerId();

        if (customerId is null || authorizedCustomerRequest.CustomerId != customerId)
        {
            return Unauthorized.Instance;
        }

        return await next();
    }
}
