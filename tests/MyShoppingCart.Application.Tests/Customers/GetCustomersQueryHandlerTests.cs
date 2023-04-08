namespace MyShoppingCart.Application.Tests.Customers;

public class GetCustomersQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomers_WhenAllParametersAreValid()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery();
        var customers = DataHelper.GetCustomers();

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.ListAsync(It.IsAny<QueryAllCustomers>(), _cancellationToken))
            .ReturnsAsync(customers);

        var handler = new GetCustomersQueryHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(customers);
        mockCustomerRepository
            .Verify(x => x.ListAsync(It.IsAny<QueryAllCustomers>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNoCustomers_WhenNoParametersMatch()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery();
        var customers = new List<Customer>();

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.ListAsync(It.IsAny<QueryAllCustomers>(), _cancellationToken))
            .ReturnsAsync(customers);

        var handler = new GetCustomersQueryHandler(mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(customers);
        mockCustomerRepository
            .Verify(x => x.ListAsync(It.IsAny<QueryAllCustomers>(), _cancellationToken), Times.Once);
    }

    #endregion

}
