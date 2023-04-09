namespace MyShoppingCart.Application.Tests.Customers;

public class UpdateCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        const string NEW_EMAIL = "changed@gmail.com";
        var request = DataHelper.GetUpdateCustomerQuery() with { Email = NEW_EMAIL };
        var originalCustomer = DataHelper.GetCustomer();
        var updatedCustomer = DataHelper.GetCustomer();
        updatedCustomer.Email = NEW_EMAIL;

        var mapper = new Mapper();

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(updatedCustomer, _cancellationToken);
        mockCustomerRepository
            .Setup(x => x.UpdateAsync(updatedCustomer, _cancellationToken));

        var handler = new UpdateCustomerQueryHandler(mockCustomerRepository.Object, mapper);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(updatedCustomer);
        mockCustomerRepository
           .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockCustomerRepository
             .Verify(x => x.UpdateAsync(updatedCustomer, _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIdIsNotFound()
    {
        //Arrange
        const string NEW_EMAIL = "changed@gmail.com";
        var request = DataHelper.GetUpdateCustomerQuery() with { Email = NEW_EMAIL };
        var originalCustomer = DataHelper.GetCustomer();
        var updatedCustomer = DataHelper.GetCustomer();
        updatedCustomer.Email = NEW_EMAIL;

        var mapper = new Mapper();

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

        var handler = new UpdateCustomerQueryHandler(mockCustomerRepository.Object, mapper);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockCustomerRepository
            .Verify(x => x.UpdateAsync(updatedCustomer, _cancellationToken), Times.Never);
    }

    #endregion
}
