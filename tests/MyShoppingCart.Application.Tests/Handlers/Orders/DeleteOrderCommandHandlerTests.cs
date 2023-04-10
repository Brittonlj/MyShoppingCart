namespace MyShoppingCart.Application.Tests.Handlers.Orders;

public class DeleteOrderCommandHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IRepository<Order>> _mockOrderRepository = new Mock<IRepository<Order>>();
    private readonly DeleteOrderCommandHandler _unitUnderTest;

    public DeleteOrderCommandHandlerTests()
    {
        _unitUnderTest = new DeleteOrderCommandHandler(_mockOrderRepository.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        //Arrange
        var order = DataProvider.GetOrder();
        var request = new DeleteOrderCommand(order.CustomerId, order.Id);

        _mockOrderRepository.Setup(x => x
        .FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken))
            .ReturnsAsync(order);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(Success.Instance);
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken), Times.Once);
        _mockOrderRepository
            .Verify(x => x.DeleteAsync(order, _cancellationToken), Times.Once);
    }

    #endregion

    #region Handle

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenOrderIsNotFound()
    {
        //Arrange
        var order = DataProvider.GetOrder();
        var request = new DeleteOrderCommand(order.CustomerId, order.Id);

        _mockOrderRepository.Setup(x => x
        .FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken))
            .ReturnsAsync(() => null);

        //Act
        var results = await _unitUnderTest.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        _mockOrderRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetOrderByIdSpec>(), _cancellationToken), Times.Once);
        _mockOrderRepository
            .Verify(x => x.DeleteAsync(It.IsAny<Order>(), _cancellationToken), Times.Never);
    }

    #endregion

}
