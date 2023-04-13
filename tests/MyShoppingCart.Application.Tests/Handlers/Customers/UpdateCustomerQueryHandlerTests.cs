using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class UpdateCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        const string NEW_EMAIL = "changed@gmail.com";
        var request = QueryProvider.GetUpdateCustomerQuery() with { Email = NEW_EMAIL };
        var originalCustomer = DataProvider.GetCustomer();
        var updatedCustomer = DataProvider.GetCustomer();
        updatedCustomer.Email = NEW_EMAIL;
        var customerModel = DataProvider.GetCustomerModel();
        customerModel.Email = NEW_EMAIL;
        var identityResult = IdentityResult.Success;

        _mockMapper.Setup(x => x.Map(request, originalCustomer)).Returns(updatedCustomer);
        _mockMapper.Setup(x => x.Map<CustomerModel>(updatedCustomer)).Returns(customerModel);

        _mockUUserManager
            .Setup(x => x.FindByIdAsync(originalCustomer.Id, _cancellationToken))
            .ReturnsAsync(originalCustomer);
        _mockUUserManager
            .Setup(x => x.UpdateAsync(updatedCustomer, null, _cancellationToken))
            .ReturnsAsync(identityResult);

        var handler = new UpdateCustomerQueryHandler(_mockUUserManager.Object, _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(customerModel);
        _mockMapper
            .Verify(x => x.Map(request, originalCustomer), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(updatedCustomer), Times.Once);
        _mockUUserManager
           .Verify(x => x.FindByIdAsync(originalCustomer.Id, _cancellationToken), Times.Once);
        _mockUUserManager
             .Verify(x => x.UpdateAsync(updatedCustomer, null, _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIdIsNotFound()
    {
        //Arrange
        const string NEW_EMAIL = "changed@gmail.com";
        var request = QueryProvider.GetUpdateCustomerQuery() with { Email = NEW_EMAIL };
        var originalCustomer = DataProvider.GetCustomer();
        var updatedCustomer = DataProvider.GetCustomer();
        updatedCustomer.Email = NEW_EMAIL;

        _mockUUserManager
            .Setup(x => x.FindByIdAsync(originalCustomer.Id, _cancellationToken))
            .ReturnsAsync(() => null);

        var handler = new UpdateCustomerQueryHandler(_mockUUserManager.Object, _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        _mockMapper
            .Verify(x => x.Map(request, originalCustomer), Times.Never);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(updatedCustomer), Times.Never);
        _mockUUserManager
           .Verify(x => x.FindByIdAsync(originalCustomer.Id, _cancellationToken), Times.Once);
        _mockUUserManager
             .Verify(x => x.UpdateAsync(updatedCustomer, null, _cancellationToken), Times.Never);
    }

    #endregion
}
