using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Authentication;

public sealed class ChangePasswordCommandHandler :
    IRequestHandler<ChangePasswordCommand, Response<Success>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly IRepository<Customer> _customerRepository;
    public ChangePasswordCommandHandler(IUserManagerFacade userManager, IRepository<Customer> customerRepository)
    {
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
    }

    public async Task<Response<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetCustomerByIdSpec(request.CustomerId);
        var customer = await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);

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
            return Unauthorized.Instance;
        }

        return Success.Instance;
    }
}