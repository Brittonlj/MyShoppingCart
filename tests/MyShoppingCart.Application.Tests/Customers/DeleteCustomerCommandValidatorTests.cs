namespace MyShoppingCart.Application.Tests.Customers;

public class DeleteCustomerCommandValidatorTests
{
    private readonly IValidator<DeleteCustomerCommand> _validator = new DeleteCustomerCommandValidator();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request);

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
        var request = new DeleteCustomerCommand(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(DeleteCustomerCommand.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion
}
