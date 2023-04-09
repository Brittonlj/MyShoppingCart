namespace MyShoppingCart.Application.Tests.Validators.Orders;

public class GetOrderQueryValidatorTests
{
    private readonly IValidator<GetOrderQuery> _validator = new GetOrderQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new GetOrderQuery(Guid.NewGuid(), Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

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
        var request = new GetOrderQuery(Guid.NewGuid(), Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrderQuery.OrderId),
            "'Order Id' must not be empty.");
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var request = new GetOrderQuery(Guid.Empty, Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetOrderQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion

}
