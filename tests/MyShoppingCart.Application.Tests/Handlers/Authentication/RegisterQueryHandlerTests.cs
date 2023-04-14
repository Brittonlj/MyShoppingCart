using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.Services;
using System.Security.Claims;

namespace MyShoppingCart.Application.Tests.Handlers.Authentication;

public sealed class RegisterQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private readonly RegisterQueryHandler _unitUnderTest;

    public RegisterQueryHandlerTests()
    {
        _unitUnderTest = new RegisterQueryHandler(_mockUserManager.Object, _mockMapper.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnAuthenticationResponseModel_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var customer = DataProvider.GetCustomer();
        var customerModel = DataProvider.GetCustomerModel();

        _mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);
        _mockMapper.Setup(x => x.Map<CustomerModel>(customer)).Returns(customerModel);

        _mockUserManager.Setup(x => x.CreateAsync(customer, request.Password, _cancellationToken))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(x => x.AddClaimAsync(customer, It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(x => x.AddToRoleAsync(customer, It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(customerModel);
        _mockMapper
            .Verify(x => x.Map<Customer>(request), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Once);
        _mockUserManager
            .Verify(x => x.CreateAsync(customer, request.Password, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.AddClaimAsync(customer, It.IsAny<Claim>()), Times.Once);
        _mockUserManager
            .Verify(x => x.AddToRoleAsync(customer, It.IsAny<string>()), Times.Once);
    }

    #endregion

    #region Error Handling

    [Fact]
    public async Task Handle_ShouldReturnErrors_WhenFailToAddCustomer()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var customer = DataProvider.GetCustomer();
        var identityResult = IdentityResult.Failed(
            new IdentityError
            {
                Code = "AlreadyExists",
                Description = "User already exists."                
            });
        var errors = new ErrorList
        {
            new Error("AlreadyExists", "User already exists.")
        };

        _mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);

        _mockUserManager.Setup(x => x.CreateAsync(customer, request.Password, _cancellationToken))
            .ReturnsAsync(identityResult);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.ErrorList.Should().NotBeNull().And.BeEquivalentTo(errors);
        _mockMapper
            .Verify(x => x.Map<Customer>(request), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Never);
        _mockUserManager
            .Verify(x => x.CreateAsync(customer, request.Password, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.AddClaimAsync(customer, It.IsAny<Claim>()), Times.Never);
        _mockUserManager
            .Verify(x => x.AddToRoleAsync(customer, It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrors_WhenFailToAddClaim()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var customer = DataProvider.GetCustomer();
        var identityResult = IdentityResult.Failed(
            new IdentityError
            {
                Code = "ClaimDoesNotExist",
                Description = "Claim does not exist."
            });
        var errors = new ErrorList
        {
            new Error("ClaimDoesNotExist", "Claim does not exist.")
        };

        _mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);

        _mockUserManager.Setup(x => x.CreateAsync(customer, request.Password, _cancellationToken))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(x => x.AddClaimAsync(customer, It.IsAny<Claim>()))
            .ReturnsAsync(identityResult);
        _mockUserManager.Setup(x => x.AddToRoleAsync(customer, It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.ErrorList.Should().NotBeNull().And.BeEquivalentTo(errors);
        _mockMapper
            .Verify(x => x.Map<Customer>(request), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Never);
        _mockUserManager
            .Verify(x => x.CreateAsync(customer, request.Password, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.AddClaimAsync(customer, It.IsAny<Claim>()), Times.Once);
        _mockUserManager
            .Verify(x => x.AddToRoleAsync(customer, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrors_WhenFailToAddRole()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var customer = DataProvider.GetCustomer();
        var identityResult = IdentityResult.Failed(
            new IdentityError
            {
                Code = "RoleDoesNotExist",
                Description = "Role does not exist."
            });
        var errors = new ErrorList
        {
            new Error("RoleDoesNotExist", "Role does not exist.")
        };

        _mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);

        _mockUserManager.Setup(x => x.CreateAsync(customer, request.Password, _cancellationToken))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(x => x.AddToRoleAsync(customer, It.IsAny<string>()))
            .ReturnsAsync(identityResult);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.ErrorList.Should().NotBeNull().And.BeEquivalentTo(errors);
        _mockMapper
            .Verify(x => x.Map<Customer>(request), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Never);
        _mockUserManager
            .Verify(x => x.CreateAsync(customer, request.Password, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.AddClaimAsync(customer, It.IsAny<Claim>()), Times.Never);
        _mockUserManager
            .Verify(x => x.AddToRoleAsync(customer, It.IsAny<string>()), Times.Once);
    }

    #endregion
}
