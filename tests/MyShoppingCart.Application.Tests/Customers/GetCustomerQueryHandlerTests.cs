namespace MyShoppingCart.Application.Tests.Customers;

public class GetCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataHelper.GetCustomer();
        var request = new GetCustomerQuery(Guid.NewGuid());

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var handler = new GetCustomerQueryHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(customer);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var customer = DataHelper.GetCustomer();
        var request = new GetCustomerQuery(Guid.NewGuid());

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

        var handler = new GetCustomerQueryHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
    }

    #endregion


}
