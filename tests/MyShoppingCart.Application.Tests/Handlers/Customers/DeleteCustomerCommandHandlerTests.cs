namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public sealed class DeleteCustomerCommandHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var request = new DeleteCustomerCommand(customer.Id);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var handler = new DeleteCustomerCommandHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(Success.Instance);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockCustomerRepository
            .Verify(x => x.DeleteAsync(customer, _cancellationToken), Times.Once);
    }

    #endregion

    #region Handle

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.NewGuid());
        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

        var handler = new DeleteCustomerCommandHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockCustomerRepository
            .Verify(x => x.DeleteAsync(It.IsAny<Customer>(), _cancellationToken), Times.Never);
    }

    #endregion
}