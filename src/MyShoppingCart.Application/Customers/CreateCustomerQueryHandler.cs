using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryHandler : IRequestHandler<CreateCustomerQuery, Response<CustomerModel>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly IMapper _mapper;

    public CreateCustomerQueryHandler(
        IMapper mapper,
        IUserManagerFacade userManager)
    {
        _mapper = Guard.Against.Null(mapper);
        _userManager = Guard.Against.Null(userManager); ;
    }

    public async Task<Response<CustomerModel>> Handle(CreateCustomerQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = _mapper.Map<Customer>(request);

        IdentityResult result = await _userManager.CreateAsync(customer, request.Password);

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
