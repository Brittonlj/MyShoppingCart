namespace MyShoppingCart.Application.Tests.Validators.Orders;

public class CreateOrderQueryValidatorTests
{
    private readonly IValidator<CreateOrderQuery> _validator;

    public CreateOrderQueryValidatorTests()
    {
        _validator = new CreateOrderQueryValidator(GetRepository());
    }

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = GetCreateOrderQuery();

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustoerIdIsEmpty()
    {
        //Arrange
        var query = GetCreateOrderQuery() with { CustomerId = Guid.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateOrderQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion

    #region LineItems

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLineItemsAreEmpty()
    {
        //Arrange
        var query = GetCreateOrderQuery() with { LineItems = new List<LineItemModel>() };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateOrderQuery.LineItems),
            "'Line Items' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLineItemsContainUnknownProducts()
    {
        //Arrange
        var lineItems = LineItemModelsData.GetLineItemModels();
        var newProductId = Guid.NewGuid();
        lineItems.Add(new LineItemModel(newProductId, 12));
        var query = GetCreateOrderQuery() with { LineItems = lineItems };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            "ProductId",
            $"The ProductId '{newProductId}' was not found.");
    }

    #endregion

    #region Private Helpers

    private static IRepository<Product> GetRepository()
    {
        var mockProductRepository = new Mock<IRepository<Product>>();
        mockProductRepository.Setup(x => x.ListAsync(It.IsAny<QueryAllProductsByProductIds>(), It.IsAny<CancellationToken>())).ReturnsAsync(ProductsData.GetProducts());

        return mockProductRepository.Object;
    }

    private static CreateOrderQuery GetCreateOrderQuery()
    {
        return new CreateOrderQuery(
            Guid.NewGuid(),
            LineItemModelsData.GetLineItemModels());
    }

    #endregion
}
