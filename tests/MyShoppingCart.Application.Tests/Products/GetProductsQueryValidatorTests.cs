namespace MyShoppingCart.Application.Tests.Products;

public class GetProductsQueryValidatorTests
{
    private readonly IValidator<GetProductsQuery> _validator = new GetProductsQueryValidator();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = GetGetProductsQuery();

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenSearchStringEmpty()
    {
        //Arrange
        var request = GetGetProductsQuery() with { SearchString = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenSearchStringIsNull()
    {
        //Arrange
        var request = GetGetProductsQuery() with { SearchString = null };

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
        var request = GetGetProductsQuery() with { SortColumn = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.SortColumn),
            "'Sort Column' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenSortColumnIsInvalid()
    {
        //Arrange
        var request = GetGetProductsQuery() with { SortColumn = "BadSortColumn" };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.SortColumn),
            "'BadSortColumn' is an invalid value for 'Sort Column'.  Please use one of 'Name, Description, Price'.");
    }

    #endregion

    #region PageNumber

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageNumberIsZero()
    {
        //Arrange
        var request = GetGetProductsQuery() with { PageNumber = 0 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.PageNumber),
            "'Page Number' must not be empty.");
    }

    #endregion

    #region PageSize

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsZero()
    {
        //Arrange
        var request = GetGetProductsQuery() with { PageSize = 0 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.PageSize),
            "'Page Size' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsTooLarge()
    {
        //Arrange
        var request = GetGetProductsQuery() with { PageSize = 51 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.PageSize),
            "'Page Size' must be less than or equal to '50'.");
    }

    #endregion

    #region SearchString

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenSearchStringIsTooLarge()
    {
        //Arrange
        var request = GetGetProductsQuery() with { SearchString = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(GetProductsQuery.SearchString),
            "The length of 'Search String' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Private Helpers

    private static GetProductsQuery GetGetProductsQuery()
    {
        return new GetProductsQuery(
            "Tennis Shoes",
            1,
            20,
            "Name");

    }

    #endregion

}
