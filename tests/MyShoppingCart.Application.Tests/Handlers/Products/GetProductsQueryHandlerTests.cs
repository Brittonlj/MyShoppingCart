namespace MyShoppingCart.Application.Tests.Handlers.Products;

public class GetProductsQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnProducts_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetGetProductsQuery();
        var products = DataProvider.GetProducts();

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithManyResponses(products, _cancellationToken);

        var handler = new GetProductsQueryHandler(mockProductRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(products);
        mockProductRepository
            .Verify(x => x.ListAsync(It.IsAny<GetProductsSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region ProductId

    [Fact]
    public async Task Handle_ShouldReturnNoProducts_WhenNoParametersMatch()
    {
        //Arrange
        var request = QueryProvider.GetGetProductsQuery();
        var products = new List<Product>();

        var mockProductRepository = MockProvider.GetMockProductRepositoryWithManyResponses(products, _cancellationToken);

        var handler = new GetProductsQueryHandler(mockProductRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(products);
        mockProductRepository
            .Verify(x => x.ListAsync(It.IsAny<GetProductsSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

}
