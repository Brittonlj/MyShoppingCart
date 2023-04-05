namespace MyShoppingCart.Application.Tests.Validators.Orders;

public class GetOrdersQueryValidatorTests
{
    private readonly IValidator<GetOrdersQuery> _validator = new GetOrdersQueryValidator();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = GetGetOrdersQuery();

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region SortColumn

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenSortColumnIsEmpty()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { SortColumn = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.SortColumn),
            "'Sort Column' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenSortColumnIsInvalid()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { SortColumn = "BadSortColumn" };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.SortColumn),
            "'BadSortColumn' is an invalid value for 'Sort Column'");
    }

    #endregion

    #region PageNumber

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageNumberIsZero()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { PageNumber = 0 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.PageNumber),
            "'Page Number' must not be empty.");
    }

    #endregion

    #region PageSize

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsZero()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { PageSize = 0 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.PageSize),
            "'Page Size' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsTooLarge()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { PageSize = 51 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.PageSize),
            "'Page Size' must be less than or equal to '50'.");
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var request = GetGetOrdersQuery() with { CustomerId = Guid.Empty };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrdersQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion

    #region Private Helpers

    private static GetOrdersQuery GetGetOrdersQuery()
    {
        return new GetOrdersQuery(
            Guid.NewGuid(),
            1,
            20,
            "OrderDateTimeUtc");

    }

    #endregion
}
