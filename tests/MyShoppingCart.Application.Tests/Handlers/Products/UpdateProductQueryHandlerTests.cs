namespace MyShoppingCart.Application.Tests.Handlers.Products;

public class UpdateProductQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenAllParametersAreValid()
    {
        //Arrange
        const string NEW_NAME = "New And Improved Nike Tennis Shoes";
        var request = QueryProvider.GetUpdateProductQuery() with { Name = NEW_NAME };
        var originalProduct = DataProvider.GetProduct();
        var updatedProduct = DataProvider.GetProduct();
        updatedProduct.Name = NEW_NAME;

        _mockMapper.Setup(x => x.Map(request, originalProduct)).Returns(updatedProduct);

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithSingleResponse(originalProduct, _cancellationToken);
        mockProductRepository
            .Setup(x => x.UpdateAsync(updatedProduct, _cancellationToken));

        var handler = new UpdateProductQueryHandler(mockProductRepository.Object, _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(updatedProduct);
        mockProductRepository
           .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), _cancellationToken), Times.Once);
        mockProductRepository
             .Verify(x => x.UpdateAsync(updatedProduct, _cancellationToken), Times.Once);
        _mockMapper
            .Verify(x => x.Map(request, originalProduct), Times.Once);
    }

    #endregion

    #region ProductId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenProductIdIsNotFound()
    {
        //Arrange
        const string NEW_NAME = "New And Improved Nike Tennis Shoes";
        var request = QueryProvider.GetUpdateProductQuery() with { Name = NEW_NAME };
        var originalProduct = DataProvider.GetProduct();
        var updatedProduct = DataProvider.GetProduct();
        updatedProduct.Name = NEW_NAME;

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithNullResponse(_cancellationToken);

        var handler = new UpdateProductQueryHandler(mockProductRepository.Object, _mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockProductRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), _cancellationToken), Times.Once);
        mockProductRepository
            .Verify(x => x.UpdateAsync(updatedProduct, _cancellationToken), Times.Never);
        _mockMapper
            .Verify(x => x.Map(request, originalProduct), Times.Never);
    }

    #endregion

}
