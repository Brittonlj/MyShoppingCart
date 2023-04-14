using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Application.Tests.Validators.Authentication;

public sealed class LoginQueryValidatorTests
{
    private readonly IValidator<LoginQuery> _validator = new LoginQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = new LoginQuery("fred.flintstone", "somePassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region UserName

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenUserNameIsEmpty()
    {
        //Arrange
        var query = new LoginQuery("", "somePassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(LoginQuery.UserName),
            "'User Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenUserNameIsTooLong()
    {
        //Arrange
        var query = new LoginQuery(LongStrings.LONG_STRING_51, "somePassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(LoginQuery.UserName),
            "The length of 'User Name' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Password

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPasswordIsEmpty()
    {
        //Arrange
        var query = new LoginQuery("fred.flintstone", "");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(LoginQuery.Password),
            "'Password' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenPasswordIsTooLong()
    {
        //Arrange
        var query = new LoginQuery("fred.flintstone", LongStrings.LONG_STRING_51);

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(LoginQuery.Password),
            "The length of 'Password' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion
}
