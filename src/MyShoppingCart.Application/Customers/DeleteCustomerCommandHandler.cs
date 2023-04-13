using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Customers;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<Success>>
{
    private readonly IUserManagerFacade _userManager;

    public DeleteCustomerCommandHandler(IUserManagerFacade userManager)
    {
        _userManager = Guard.Against.Null(userManager);
    }

    public async Task<Response<Success>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _userManager.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        await _userManager.DeleteAsync(customer);

        return Success.Instance;
    }
}
