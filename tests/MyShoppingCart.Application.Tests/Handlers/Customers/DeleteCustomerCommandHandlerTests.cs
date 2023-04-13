using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public sealed class DeleteCustomerCommandHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var request = new DeleteCustomerCommand(customer.Id);
        var identityResult = IdentityResult.Success;

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(customer);
        _mockUserManager.Setup(x => x.DeleteAsync(customer, _cancellationToken))
                    .ReturnsAsync(identityResult);

        var handler = new DeleteCustomerCommandHandler(_mockUserManager.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(Success.Instance);
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.DeleteAsync(customer, _cancellationToken), Times.Once);
    }

    #endregion

    #region Handle

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.NewGuid());

        _mockUserManager.Setup(x => x.FindByIdAsync(request.CustomerId, _cancellationToken))
            .ReturnsAsync(() => null);

        var handler = new DeleteCustomerCommandHandler(_mockUserManager.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        _mockUserManager
            .Verify(x => x.FindByIdAsync(request.CustomerId, _cancellationToken), Times.Once);
        _mockUserManager
            .Verify(x => x.DeleteAsync(It.IsAny<Customer>(), _cancellationToken), Times.Never);
    }

    #endregion
}