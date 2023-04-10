using MyShoppingCart.Application.Products;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class ProductEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();


    #region GetAllProducts

    [Fact]
    public async Task GetAllProducts_ShouldReturnProducts_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new GetProductsQuery();
        var response = Response<IReadOnlyList<Product>>.FromSuccess(GetProductList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Product>>)await ProductEndpoints.GetProducts(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnNoProducts_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = new GetProductsQuery("UnavailableName");
        var response = Response<IReadOnlyList<Product>>.FromSuccess(GetEmptyProductsList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Product>>)await ProductEndpoints.GetProducts(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new GetProductsQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<IReadOnlyList<Product>>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.GetProducts(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new GetProductsQuery(SortColumn: "InvalidSortColumn");
        const string ERROR_KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<IReadOnlyList<Product>>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.GetProducts(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region CreateProduct

    [Fact]
    public async Task CreateProduct_ShouldReturnProduct_WhenValidParametersAreChosen()
    {
        //Arrange
        var product = GetProduct();
        var request = GetCreateProductQuery();
        var response = Response<Product>.FromSuccess(product); ;
        SetupMediator<CreateProductQuery>(response);

        //Act
        var httpResult = (Ok<Product>)await ProductEndpoints.CreateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be((Product)response);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = GetCreateProductQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Product>.FromErrorList(_errors);
        SetupMediator<CreateProductQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.CreateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var product = GetProduct();
        product.Name = string.Empty;
        var request = GetCreateProductQuery();
        const string ERROR_KEY = "Name";
        const string ERROR_MESSAGE = "Name cannot be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Product>.FromValidationFailure(_validationErrors);
        SetupMediator<CreateProductQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.CreateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }
    #endregion

    #region UpdateProduct

    [Fact]
    public async Task UpdateProduct_ShouldReturnProduct_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = GetUpdateProductQuery();
        var response = Response<Product>.FromSuccess(GetProduct()); ;
        SetupMediator<UpdateProductQuery>(response);

        //Act
        var httpResult = (Ok<Product>)await ProductEndpoints.UpdateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnNotFound_WhenBadProductIdIsChosen()
    {
        //Arrange
        var request = GetUpdateProductQuery();
        var response = Response<Product>.FromNotFound();
        SetupMediator<UpdateProductQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            ProductEndpoints.UpdateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = GetUpdateProductQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Product>.FromErrorList(_errors);
        SetupMediator<UpdateProductQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.UpdateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = GetUpdateProductQuery() with { ProductId = Guid.Empty };
        const string ERROR_KEY = "ProductId";
        const string ERROR_MESSAGE = "ProductId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Product>.FromValidationFailure(_validationErrors);
        SetupMediator<UpdateProductQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.UpdateProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region DeleteProduct

    [Fact]
    public async Task DeleteProduct_ShouldReturnOk_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());
        var response = Response<Success>.FromSuccess(Success.Instance); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await ProductEndpoints.DeleteProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnNotFound_WhenBadProductIdIsChosen()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            ProductEndpoints.DeleteProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.DeleteProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());
        const string ERROR_KEY = "ProductId";
        const string ERROR_MESSAGE = "ProductId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await ProductEndpoints.DeleteProduct(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Private Helpers
    private void SetupMediator(Response<IReadOnlyList<Product>> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetProductsQuery>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator(Response<Success> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator<T>(Response<Product> response)
        where T : class, IQuery<Product>
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<T>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private static CreateProductQuery GetCreateProductQuery()
    {
        return new CreateProductQuery(
            "Bubble Gum",
            "Bubble Gum",
            1.0M,
            null);
    }

    private static UpdateProductQuery GetUpdateProductQuery()
    {
        return new UpdateProductQuery(
            Guid.NewGuid(),
            "Bubble Gum",
            "Bubble Gum",
            1.0M,
            null);
    }

    private static IReadOnlyList<Product> GetEmptyProductsList()
    {
        return new List<Product>();
    }

    private static IReadOnlyList<Product> GetProductList()
    {
        var products = new List<Product>
        {
            new Product
            {
                Name = "Tennis Shoes",
                Description = "Tennis Shoes",
                Price = 100.00M,
                ImageUrl = null
            },
            new Product
            {
                Name = "Bath Robe",
                Description = "Bath Robe",
                Price = 50.00M,
                ImageUrl = null
            },
            new Product
            {
                Name = "Bubble Gum", 
                Description = "Bubble Gum", 
                Price = 1.00M, 
                ImageUrl = null
            },
        };

        return products;
    }

    private static Product GetProduct()
    {
        return new Product
        {
            Name = "Bubble Gum",
            Description = "Bubble Gum",
            Price = 1.00M,
            ImageUrl = null
        };
    }
    #endregion

}
