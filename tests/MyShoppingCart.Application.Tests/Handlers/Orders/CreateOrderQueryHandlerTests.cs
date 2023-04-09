namespace MyShoppingCart.Application.Tests.Handlers.Orders;

public class CreateOrderQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetCreateOrderQuery();
        var customer = DataProvider.GetCustomer();
        var order = DataProvider.GetOrder();

        var dateTimeProvider = MockProvider.GetUtcDateTimeProvider();

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<Order>(request)).Returns(order);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var mockOrdersRepository = new Mock<IRepository<Order>>();
        mockOrdersRepository.Setup(x => x.AddAsync(It.IsAny<Order>(), _cancellationToken))
            .ReturnsAsync(order);

        var handler = new CreateOrderQueryHandler(
            mockOrdersRepository.Object,
            mockCustomerRepository.Object,
            mockMapper.Object,
            dateTimeProvider);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(order);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockOrdersRepository
            .Verify(x => x.AddAsync(order, _cancellationToken), Times.Once);
    }

    #endregion

        #region CustomerId

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
        {
            //Arrange
            var request = QueryProvider.GetCreateOrderQuery();
            var mapper = new Mapper();

            var dateTimeProvider = MockProvider.GetUtcDateTimeProvider();

            var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

            var mockOrdersRepository = new Mock<IRepository<Order>>();

            var handler = new CreateOrderQueryHandler(
                mockOrdersRepository.Object,
                mockCustomerRepository.Object,
                mapper,
                dateTimeProvider);

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
