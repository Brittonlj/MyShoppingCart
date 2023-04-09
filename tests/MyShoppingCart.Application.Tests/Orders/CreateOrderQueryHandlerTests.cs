namespace MyShoppingCart.Application.Tests.Orders;

public class CreateOrderQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact(Skip = "Incomplete")]
    public async Task Handle_ShouldReturnOrder_WhenAllParametersAreValid()
    {
        //Arrange
        var request = DataHelper.GetCreateOrderQuery();
        var customer = DataHelper.GetCustomer();
        var order = DataHelper.GetOrder();

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken))
            .ReturnsAsync(customer);

        var mockOrdersRepository = new Mock<IRepository<Order>>();
        mockOrdersRepository.Setup(x => x.AddAsync(It.IsAny<Order>(), _cancellationToken))
            .ReturnsAsync(order);

        var handler = new CreateOrderQueryHandler(
            mockOrdersRepository.Object,
            mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(order);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockOrdersRepository
            .Verify(x => x.AddAsync(It.IsAny<Order>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var request = DataHelper.GetCreateOrderQuery();

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken))
            .ReturnsAsync(() => null);

        var mockOrdersRepository = new Mock<IRepository<Order>>();

        var handler = new CreateOrderQueryHandler(
            mockOrdersRepository.Object,
            mockCustomerRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockOrdersRepository
            .Verify(x => x.AddAsync(It.IsAny<Order>(), _cancellationToken), Times.Never);
    }

    #endregion
}
