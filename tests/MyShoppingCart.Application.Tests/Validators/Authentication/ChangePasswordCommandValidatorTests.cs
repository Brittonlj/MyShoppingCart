using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Application.Tests.Validators.Authentication;

public sealed class ChangePasswordCommandValidatorTests
{
    private readonly IValidator<ChangePasswordCommand> _validator = new ChangePasswordCommandValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "currentPassword", "newPassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var query = new ChangePasswordCommand(Guid.Empty, "currentPassword", "newPassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(ChangePasswordCommand.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion

    #region CurrentPassword

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCurrentPasswordIsEmpty()
    {
        //Arrange
        var query = new ChangePasswordCommand(DataProvider.DefaultCustomerId, string.Empty, "newPassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(ChangePasswordCommand.CurrentPassword),
            "'Current Password' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCurrentPasswordIsTooLong()
    {
        //Arrange
        var query = new ChangePasswordCommand(DataProvider.DefaultCustomerId, LongStrings.LONG_STRING_51, "newPassword");

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(ChangePasswordCommand.CurrentPassword),
            "The length of 'Current Password' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region NewPassword

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenNewPasswordIsEmpty()
    {
        //Arrange
        var query = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "currentPassword", string.Empty);

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(ChangePasswordCommand.NewPassword),
            "'New Password' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenNewPasswordIsTooLong()
    {
        //Arrange
        var query = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "currentPassword", LongStrings.LONG_STRING_51);

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(ChangePasswordCommand.NewPassword),
            "The length of 'New Password' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion
}
