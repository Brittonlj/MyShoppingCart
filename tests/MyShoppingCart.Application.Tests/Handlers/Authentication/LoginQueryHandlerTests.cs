using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.Services;
using System.Security.Claims;

namespace MyShoppingCart.Application.Tests.Handlers.Authentication;

public class LoginQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<IJwtTokenService> _mockJwtTokenService = new Mock<IJwtTokenService>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnAuthenticationResponseModel_WhenAllParametersAreValid()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "somePassword");
        var customer = DataProvider.GetCustomer();
        var customerModel = DataProvider.GetCustomerModel();
        var identityResult = IdentityResult.Success;
        var authenticationResponseModel = new AuthenticationResponseModel
        {
            Customer = customerModel,
            Token = DataProvider.DEFAULT_TOKEN
        };
        var claims = new List<Claim>();

        _mockMapper.Setup(x => x.Map<CustomerModel>(customer)).Returns(customerModel);

        _mockUserManager.Setup(x => x.FindByNameAsync(request.UserName, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(customer, request.Password))
            .ReturnsAsync(true);
        _mockUserManager.Setup(x => x.GetClaimsAsync(customer))
            .ReturnsAsync(claims);
        _mockUserManager.Setup(x => x.GetRolesAsync(customer))
            .ReturnsAsync(new List<string>());

        _mockJwtTokenService.Setup(x => x.GenerateToken(claims))
            .Returns(DataProvider.DEFAULT_TOKEN);

        var handler = new LoginQueryHandler(
            _mockUserManager.Object,
            _mockJwtTokenService.Object,
            _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(authenticationResponseModel);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Once);
        _mockUserManager
            .Verify(x => x.FindByNameAsync(request.UserName, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(customer, request.Password), Times.Once);
        _mockUserManager
            .Verify(x => x.GetClaimsAsync(customer), Times.Once);
        _mockUserManager
            .Verify(x => x.GetRolesAsync(customer), Times.Once);
    }

    #endregion

    #region Invalid Login


    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenLoginFails()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "somePassword");
        var customer = DataProvider.GetCustomer();
        var customerModel = DataProvider.GetCustomerModel();
        var identityResult = IdentityResult.Success;
        var authenticationResponseModel = new AuthenticationResponseModel
        {
            Customer = customerModel,
            Token = DataProvider.DEFAULT_TOKEN
        };
        var claims = new List<Claim>();

        _mockMapper.Setup(x => x.Map<CustomerModel>(customer)).Returns(customerModel);

        _mockUserManager.Setup(x => x.FindByNameAsync(request.UserName, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(customer, request.Password))
            .ReturnsAsync(false);

        var handler = new LoginQueryHandler(
            _mockUserManager.Object,
            _mockJwtTokenService.Object,
            _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Unauthorized.Should().NotBeNull();
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Never);
        _mockUserManager
            .Verify(x => x.FindByNameAsync(request.UserName, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(customer, request.Password), Times.Once);
        _mockUserManager
            .Verify(x => x.GetClaimsAsync(customer), Times.Never);
        _mockUserManager
            .Verify(x => x.GetRolesAsync(customer), Times.Never);
    }

    #endregion
}
