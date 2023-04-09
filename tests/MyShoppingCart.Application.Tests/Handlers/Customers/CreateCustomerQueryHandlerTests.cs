namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class CreateCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetCreateCustomerQuery();
        var customer = DataProvider.GetCustomer();

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<Customer>(request)).Returns(customer);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);
        mockCustomerRepository.Setup(x => x.AddAsync(customer, _cancellationToken)).ReturnsAsync(customer);

        var mockSecurityClaimRepository = new Mock<IRepository<SecurityClaim>>();

        var handler = new CreateCustomerQueryHandler(
            mockCustomerRepository.Object,
            mockSecurityClaimRepository.Object,
            mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(customer);
        mockCustomerRepository
            .Verify(x => x.AddAsync(customer, _cancellationToken), Times.Once);
        mockSecurityClaimRepository
            .Verify(x => x.AddRangeAsync(It.IsAny<List<SecurityClaim>>(), _cancellationToken), Times.Once);
    }

    #endregion
}
