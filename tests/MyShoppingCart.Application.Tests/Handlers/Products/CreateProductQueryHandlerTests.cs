namespace MyShoppingCart.Application.Tests.Handlers.Products;

public class CreateProductQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenAllParametersAreValid()
    {
        //Arrange
        var request = QueryProvider.GetCreateProductQuery();
        var product = DataProvider.GetProduct();

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<Product>(request)).Returns(product);

        var mockProductRepository = new Mock<IRepository<Product>>();
        mockProductRepository
            .Setup(x => x.AddAsync(product, _cancellationToken))
            .ReturnsAsync(product);

        var handler = new CreateProductQueryHandler(
            mockProductRepository.Object,
            mockMapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(product);
        mockProductRepository
            .Verify(x => x.AddAsync(product, _cancellationToken), Times.Once);
    }

    #endregion
}
