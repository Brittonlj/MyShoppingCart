namespace MyShoppingCart.Application.Tests.Handlers.Orders;

public class UpdateOrderQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IRepository<Order>> _mockOrderRepository = new Mock<IRepository<Order>>();
    private readonly UpdateOrderQueryHandler _unitUnderTest;
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    public UpdateOrderQueryHandlerTests()
    {
        _unitUnderTest = new UpdateOrderQueryHandler(_mockOrderRepository.Object, _mockMapper.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetUpdateOrderQuery();
        var originalOrder = DataProvider.GetOrder();
        var updatedOrder = DataProvider.GetOrder();
        updatedOrder.OrderDateTimeUtc = new DateTime(2022, 1, 1, 1, 1, 1, DateTimeKind.Utc);
        
        _mockMapper.Setup(x => x.Map(request, originalOrder)).Returns(updatedOrder);

        _mockOrderRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryOrderById>(), _cancellationToken))
            .ReturnsAsync(originalOrder);
        _mockOrderRepository
            .Setup(x => x.AddAsync(updatedOrder, _cancellationToken))
            .ReturnsAsync(updatedOrder);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(updatedOrder);
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryOrderById>(), _cancellationToken), Times.Once);
        _mockOrderRepository
            .Verify(x => x.UpdateAsync(updatedOrder, _cancellationToken), Times.Once);
    }

    #endregion

    #region OrderId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenOrderIdIsNotFound()
    {
        //Arrange
        var request = QueryProvider.GetUpdateOrderQuery();
        var originalOrder = DataProvider.GetOrder();
        var updatedOrder = DataProvider.GetOrder();
        updatedOrder.OrderDateTimeUtc = new DateTime(2022, 1, 1, 1, 1, 1, DateTimeKind.Utc);

        _mockMapper.Setup(x => x.Map(updatedOrder, originalOrder)).Returns(updatedOrder);

        _mockOrderRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryOrderById>(), _cancellationToken))
            .ReturnsAsync(() => null);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryOrderById>(), _cancellationToken), Times.Once);
        _mockOrderRepository
            .Verify(x => x.UpdateAsync(updatedOrder, _cancellationToken), Times.Never);
    }

    #endregion

}
