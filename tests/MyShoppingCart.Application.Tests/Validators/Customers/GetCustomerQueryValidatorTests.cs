namespace MyShoppingCart.Application.Tests.Validators.Customers;

public class GetCustomerQueryValidatorTests
{
    private readonly IValidator<GetCustomerQuery> _validator = new GetCustomerQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new GetCustomerQuery(Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

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
        var request = new GetCustomerQuery(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomerQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion
}
