using MyShoppingCart.Application.Services;
using System.Security.Claims;

namespace MyShoppingCart.Application.Authentication;

public sealed class LoginQueryHandler :
    IRequestHandler<LoginQuery, Response<AuthenticationResponseModel>>
{
    private readonly IUserManagerFacade _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;
    public LoginQueryHandler(
        IUserManagerFacade userManager,
        IJwtTokenService jwtTokenService,
        IMapper mapper)
    {
        _userManager = Guard.Against.Null(userManager);
        _jwtTokenService = Guard.Against.Null(jwtTokenService);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<Response<AuthenticationResponseModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var customer = await _userManager.FindByNameAsync(request.UserName, cancellationToken);

        if (customer is null || !await _userManager.CheckPasswordAsync(customer, request.Password))
        {
            return Unauthorized.Instance;
        }

        var customerModel = _mapper.Map<CustomerModel>(customer);
        var claims = await _userManager.GetClaimsAsync(customer);
        var roles = await _userManager.GetRolesAsync(customer);

        foreach ( var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var token = _jwtTokenService.GenerateToken(claims.ToList());

        return new AuthenticationResponseModel
        {
            Token = token,
            Customer = customerModel
        };
    }
}
