namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class GetCustomersQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
 
    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomers_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetGetCustomersQuery();
        var customers = DataProvider.GetCustomers();
        var customerModels = DataProvider.GetCustomerModels();

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithManyResponses(customers, _cancellationToken);

        var handler = new GetCustomersQueryHandler(mockCustomerRepository.Object, new Mapper());

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(customerModels);
        mockCustomerRepository
            .Verify(x => x.ListAsync(It.IsAny<GetCustomersSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNoCustomers_WhenNoParametersMatch()
    {
        //Arrange
        var request = QueryProvider.GetGetCustomersQuery();
        var customers = new List<Customer>();
        var customerModels = new List<CustomerModel>();

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithManyResponses(customers, _cancellationToken);

        var handler = new GetCustomersQueryHandler(mockCustomerRepository.Object, _mapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(customerModels);
        mockCustomerRepository
            .Verify(x => x.ListAsync(It.IsAny<GetCustomersSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

}
