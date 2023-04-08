namespace MyShoppingCart.Application.Tests.Products;

public class CreateProductQueryValidatorTests
{
    private readonly IValidator<CreateProductQuery> _validator = new CreateProductQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = GetCreateProductQuery();

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenImageUrlIsNull()
    {
        //Arrange
        var query = GetCreateProductQuery() with { ImageUrl = null };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenImageUrlIsEmpty()
    {
        //Arrange
        var query = GetCreateProductQuery() with { ImageUrl = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenPriceIsZero()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Price = 0.0M };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenDecimalIsOneDigit()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Price = 1.0M };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenDecimalIsZeroDigits()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Price = 100 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region Name

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenNameIsEmpty()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Name = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Name),
            "'Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenNameIsTooLong()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Name = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Name),
            "The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
    }
    #endregion

    #region Description

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenDescriptionIsEmpty()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Description = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Description),
            "'Description' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenDescriptionIsTooLong()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Description = LongStrings.LONG_STRING_501 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Description),
            "The length of 'Description' must be 500 characters or fewer. You entered 501 characters.");
    }

    #endregion

    #region Price

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPriceHasTooManyDecimals()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Price = 1.100M };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Price),
            "'Price' must not be more than 7 digits in total, with allowance for 2 decimals. 1 digits and 3 decimals were found.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPriceHasTooManyDigits()
    {
        //Arrange
        var query = GetCreateProductQuery() with { Price = 10000000.00M };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.Price),
            "'Price' must not be more than 7 digits in total, with allowance for 2 decimals. 8 digits and 2 decimals were found.");
    }

    #endregion

    #region ImageUrl

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenImageUrlIsTooLong()
    {
        //Arrange
        var query = GetCreateProductQuery() with { ImageUrl = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.ImageUrl),
            "The length of 'Image Url' must be 50 characters or fewer. You entered 51 characters.");
        results.AssertValidationErrors(
            nameof(CreateProductQuery.ImageUrl),
            "'Image Url' must be a valid absolute URL.", 1);
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenImageUrlIsInvalidUri()
    {
        //Arrange
        var query = GetCreateProductQuery() with { ImageUrl = "sometext" };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateProductQuery.ImageUrl),
            "'Image Url' must be a valid absolute URL.");
    }

    #endregion

    #region Private Helpers

    private static CreateProductQuery GetCreateProductQuery()
    {
        return new CreateProductQuery(
            "Tennis Shoes",
            "These are some Tennis Shoes",
            10.00M,
            "https://myimages.com/image.jpg");
    }

    #endregion

}
