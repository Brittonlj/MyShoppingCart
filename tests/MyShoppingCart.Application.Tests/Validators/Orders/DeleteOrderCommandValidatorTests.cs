namespace MyShoppingCart.Application.Tests.Validators.Orders;

public class DeleteOrderCommandValidatorTests
{
    private readonly IValidator<DeleteOrderCommand> _validator = new DeleteOrderCommandValidator();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region OrderId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenOrderIdIsEmpty()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(DeleteOrderCommand.OrderId),
            "'Order Id' must not be empty.");
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.Empty, Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request);

        //Assert
        results.AssertValidationErrors(
            nameof(DeleteOrderCommand.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion

}
