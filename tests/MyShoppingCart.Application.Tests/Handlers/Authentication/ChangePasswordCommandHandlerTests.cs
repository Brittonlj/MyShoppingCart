using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Authentication;

public sealed class ChangePasswordCommandHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<ILogger<ChangePasswordCommandHandler>> _logger = new Mock<ILogger<ChangePasswordCommandHandler>>();
    private readonly ChangePasswordCommandHandler _unitUnderTest;

    public ChangePasswordCommandHandlerTests()
    {
        _unitUnderTest = new ChangePasswordCommandHandler(_mockUserManager.Object, _logger.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        var customer = DataProvider.GetCustomer();

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(customer, request.CurrentPassword))
            .ReturnsAsync(true);
        _mockUserManager.Setup(x => x.ChangePasswordAsync(customer, request.CurrentPassword, request.NewPassword))
            .ReturnsAsync(IdentityResult.Success);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(Success.Instance);
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(customer, request.CurrentPassword), Times.Once);
        _mockUserManager
            .Verify(x => x.ChangePasswordAsync(customer, request.CurrentPassword, request.NewPassword), Times.Once);
    }

    #endregion

    #region Error Conditions

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenCurrentPasswordIsInvalid()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        var customer = DataProvider.GetCustomer();

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(customer, request.CurrentPassword))
            .ReturnsAsync(false);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Unauthorized.Should().NotBeNull();
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(customer, request.CurrentPassword), Times.Once);
        _mockUserManager
            .Verify(x => x.ChangePasswordAsync(customer, request.CurrentPassword, request.NewPassword), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenCustomerIsNotFound()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(() => null);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Unauthorized.Should().NotBeNull();
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(It.IsAny<Customer>(), request.CurrentPassword), Times.Never);
        _mockUserManager
            .Verify(x => x.ChangePasswordAsync(It.IsAny<Customer>(), request.CurrentPassword, request.NewPassword), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenChangePasswordFails()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        var customer = DataProvider.GetCustomer();
        var identityResult = IdentityResult.Failed(
            new IdentityError
            {
                Code = "SomeErrorCode",
                Description = "Some error has occured."
            });

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(customer, request.CurrentPassword))
            .ReturnsAsync(true);
        _mockUserManager.Setup(x => x.ChangePasswordAsync(customer, request.CurrentPassword, request.NewPassword))
            .ReturnsAsync(identityResult);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Unauthorized.Should().NotBeNull();
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.CheckPasswordAsync(customer, request.CurrentPassword), Times.Once);
        _mockUserManager
            .Verify(x => x.ChangePasswordAsync(customer, request.CurrentPassword, request.NewPassword), Times.Once);
    }



    #endregion
}
