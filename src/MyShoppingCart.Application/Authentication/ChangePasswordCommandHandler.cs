using Microsoft.Extensions.Logging;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public sealed class ChangePasswordCommandHandler :
    IRequestHandler<ChangePasswordCommand, Response<Success>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    public ChangePasswordCommandHandler(IUserManagerFacade userManager, ILogger<ChangePasswordCommandHandler> logger)
    {
        _userManager = Guard.Against.Null(userManager);
        _logger = Guard.Against.Null(logger);
    }

    public async Task<Response<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await _userManager.FindByIdAsync(request.CustomerId);

        if (customer is null || !await _userManager.CheckPasswordAsync(customer, request.CurrentPassword))
        {
            return Unauthorized.Instance;
        }

        var result = await _userManager.ChangePasswordAsync(
            customer, 
            request.CurrentPassword, 
            request.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogError("Error changing password for customer ID {customerId}: [{code}] {description}", request.CustomerId, error.Code, error.Description);
            }
            return Unauthorized.Instance;
        }

        return Success.Instance;
    }
}