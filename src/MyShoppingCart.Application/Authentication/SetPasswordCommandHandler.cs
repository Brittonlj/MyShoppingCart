using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public sealed class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, Response<Success>>
{
    private readonly IUserManagerFacade _userManager;

    public SetPasswordCommandHandler(IUserManagerFacade userManager)
    {
        _userManager = Guard.Against.Null(userManager);
    }

    public async Task<Response<Success>> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await _userManager.FindByIdAsync(request.CustomerId);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        var result = await _userManager.RemovePasswordAsync(customer);

        if (!result.Succeeded)
        {
            return BuildErrorList(result);
        }

        result = await _userManager.AddPasswordAsync(customer, request.Password);

        if (!result.Succeeded)
        {
            return BuildErrorList(result);
        }

        return Success.Instance;
    }

    private static ErrorList BuildErrorList(IdentityResult result)
    {
        var errors = new ErrorList();
        foreach (var error in result.Errors)
        {
            errors.Add(new Error(error.Code, error.Description));
        }
        return errors;

    }
}
