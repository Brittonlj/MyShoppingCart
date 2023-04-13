using Microsoft.AspNetCore.Identity;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class CreateCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetCreateCustomerQuery();
        var customer = DataProvider.GetCustomer();
        var customerModel = DataProvider.GetCustomerModel();
        var identityResult = IdentityResult.Success;

        _mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);
        _mockMapper.Setup(x => x.Map<CustomerModel>(customer)).Returns(customerModel);

        _mockUserManager.Setup(x => x.CreateAsync(customer, request.Password, _cancellationToken))
            .ReturnsAsync(identityResult);

        var handler = new CreateCustomerQueryHandler(
            _mockMapper.Object,
            _mockUserManager.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(customerModel);
        _mockMapper
            .Verify(x => x.Map<Customer>(request), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CustomerModel>(customer), Times.Once);
        _mockUserManager
            .Verify(x => x.CreateAsync(customer, request.Password, _cancellationToken), Times.Once);
    }

    #endregion
}
