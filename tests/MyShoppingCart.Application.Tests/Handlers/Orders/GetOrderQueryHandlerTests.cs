namespace MyShoppingCart.Application.Tests.Handlers.Orders;

public class GetOrderQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IRepository<Order>> _mockOrderRepository = new Mock<IRepository<Order>>();
    private readonly GetOrderQueryHandler _unitUnderTest;
    public GetOrderQueryHandlerTests()
    {
        _unitUnderTest = new GetOrderQueryHandler(_mockOrderRepository.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenAllParametersAreValid()
    {
        //Arrange
        var order = DataProvider.GetOrder();
        var request = new GetOrderQuery(order.CustomerId, order.Id);

        _mockOrderRepository.Setup(x => x
        .FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken))
            .ReturnsAsync(order);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(order);
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region OrderId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenOrderIsNotFound()
    {
        var order = DataProvider.GetOrder();
        var request = new GetOrderQuery(order.CustomerId, order.Id);

        _mockOrderRepository.Setup(x => x
        .FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken))
            .ReturnsAsync(() => null);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken), Times.Once);
    }

    #endregion
}
