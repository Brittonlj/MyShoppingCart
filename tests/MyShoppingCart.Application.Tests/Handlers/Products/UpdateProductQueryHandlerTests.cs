namespace MyShoppingCart.Application.Tests.Handlers.Products;

public class UpdateProductQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

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

        var mapper = new Mapper();

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithSingleResponse(updatedProduct, _cancellationToken);
        mockProductRepository
            .Setup(x => x.UpdateAsync(updatedProduct, _cancellationToken));

        var handler = new UpdateProductQueryHandler(mockProductRepository.Object, mapper);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(updatedProduct);
        mockProductRepository
           .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), _cancellationToken), Times.Once);
        mockProductRepository
             .Verify(x => x.UpdateAsync(updatedProduct, _cancellationToken), Times.Once);
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

        var mapper = new Mapper();

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithNullResponse(_cancellationToken);

        var handler = new UpdateProductQueryHandler(mockProductRepository.Object, mapper);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockProductRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), _cancellationToken), Times.Once);
        mockProductRepository
            .Verify(x => x.UpdateAsync(updatedProduct, _cancellationToken), Times.Never);
    }

    #endregion

}
