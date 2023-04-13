using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<CustomerModel>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly IMapper _mapper;
    public UpdateCustomerQueryHandler(IUserManagerFacade userManager, IMapper mapper)
    {
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Response<CustomerModel>> Handle(UpdateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _userManager.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        customer = _mapper.Map(request, customer);

        var result = await _userManager.UpdateAsync(customer, cancellationToken: cancellationToken);

        if (!result.Succeeded)
        {
            var errors = new ErrorList();
            foreach (IdentityError error in result.Errors)
            {
                errors.Add(new Error(error.Code, error.Description));
            }
            return errors;
        }

        var customerModel = _mapper.Map<CustomerModel>(customer);
        return customerModel;
    }
}
