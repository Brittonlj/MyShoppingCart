namespace MyShoppingCart.Application.Tests.Customers;

public class GetCustomersQueryValidatorTests
{
    private readonly IValidator<GetCustomersQuery> _validator = new GetCustomersQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery();

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenNamesLikeEmpty()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { NamesLike = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenNamesLikeIsNull()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { NamesLike = null };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenEmailLikeIsEmpty()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { EmailLike = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenEmailLikeIsNull()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { EmailLike = null };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenSortColumnIsLeftOff()
    {
        //Arrange
        var request = new GetCustomersQuery(
            "Fred",
            "test.com",
            1,
            20);

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

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
        var request = DataHelper.GetGetCustomersQuery() with { SortColumn = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.SortColumn),
            "'Sort Column' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenSortColumnIsInvalid()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { SortColumn = "BadSortColumn" };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.SortColumn),
            "'BadSortColumn' is an invalid value for 'Sort Column'.  Please use one of 'LastName, FirstName, Email'.");
    }

    #endregion

    #region PageNumber

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageNumberIsZero()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { PageNumber = 0 };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.PageNumber),
            "'Page Number' must not be empty.");
    }

    #endregion

    #region PageSize

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsZero()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { PageSize = 0 };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.PageSize),
            "'Page Size' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPageSizeIsTooLarge()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { PageSize = 51 };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.PageSize),
            "'Page Size' must be less than or equal to '50'.");
    }

    #endregion

    #region NamesLike

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenNamesLikeIsTooLarge()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { NamesLike = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.NamesLike),
            "The length of 'Names Like' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region EmailLike

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailLikeIsTooLarge()
    {
        //Arrange
        var request = DataHelper.GetGetCustomersQuery() with { EmailLike = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomersQuery.EmailLike),
            "The length of 'Email Like' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion
}
