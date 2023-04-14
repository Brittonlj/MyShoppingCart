using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;
using System.Security.Claims;

namespace MyShoppingCart.Application.Authentication;

public sealed class RegisterQueryHandler : IRequestHandler<RegisterQuery, Response<CustomerModel>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly IMapper _mapper;

    public RegisterQueryHandler(IUserManagerFacade userManager, IMapper mapper)
    {
        _mapper = Guard.Against.Null(mapper);
        _userManager = Guard.Against.Null(userManager); ;
    }

    public async Task<Response<CustomerModel>> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = _mapper.Map<Customer>(request);

        var result = await _userManager.CreateAsync(customer, request.Password);

        if (!result.Succeeded)
        {
            var errors = new ErrorList();
            foreach (IdentityError error in result.Errors)
            {
                errors.Add(new Error(error.Code, error.Description));
            }
            return errors;
        }

        result = await AddDefaultClaimsAndRoles(customer);

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

    private async Task<IdentityResult> AddDefaultClaimsAndRoles(Customer customer)
    {
        var result = await _userManager.AddToRoleAsync(customer, Roles.Customer);
        
        if (!result.Succeeded)
        {
            return result;
        }
        
        var customerIdClaim = new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString());
        
        result = await _userManager.AddClaimAsync(customer, customerIdClaim);
        
        return result;
    }
}