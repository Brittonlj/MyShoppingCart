namespace MyShoppingCart.Application.Tests.Handlers.Orders;

public class GetOrdersQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IRepository<Order>> _mockOrderRepository = new Mock<IRepository<Order>>();
    private readonly GetOrdersQueryHandler _unitUnderTest;
    public GetOrdersQueryHandlerTests()
    {
        _unitUnderTest = new GetOrdersQueryHandler(_mockOrderRepository.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnOrders_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        var orders = DataProvider.GetOrders();

        _mockOrderRepository.Setup(x => x.ListAsync(It.IsAny<GetOrdersSpec>(), _cancellationToken))
            .ReturnsAsync(orders);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(orders);
        _mockOrderRepository
            .Verify(x => x.ListAsync(It.IsAny<GetOrdersSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region OrderId

    [Fact]
    public async Task Handle_ShouldReturnNoOrders_WhenNoParametersMatch()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        var orders = new List<Order>();

        _mockOrderRepository.Setup(x => x.ListAsync(It.IsAny<GetOrdersSpec>(), _cancellationToken))
            .ReturnsAsync(orders);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(orders);
        _mockOrderRepository
            .Verify(x => x.ListAsync(It.IsAny<GetOrdersSpec>(), _cancellationToken), Times.Once);
    }

    #endregion
}
