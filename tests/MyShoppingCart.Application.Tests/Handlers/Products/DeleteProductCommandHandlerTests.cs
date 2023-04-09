namespace MyShoppingCart.Application.Tests.Handlers.Products;

public class DeleteProductCommandHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        //Arrange
        var product = DataProvider.GetProduct();
        var request = new DeleteProductCommand(product.Id);

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithSingleResponse(product, _cancellationToken);

        var handler = new DeleteProductCommandHandler(mockProductRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(Success.Instance);
        mockProductRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryProductById>(), _cancellationToken), Times.Once);
        mockProductRepository
            .Verify(x => x.DeleteAsync(product, _cancellationToken), Times.Once);
    }

    #endregion

    #region Handle

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenProductIsNotFound()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());
        var mockProductRepository = MockProvider.GetMockProductRepositoryWithNullResponse(_cancellationToken);

        var handler = new DeleteProductCommandHandler(mockProductRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockProductRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryProductById>(), _cancellationToken), Times.Once);
        mockProductRepository
            .Verify(x => x.DeleteAsync(It.IsAny<Product>(), _cancellationToken), Times.Never);
    }

    #endregion

}
