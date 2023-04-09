namespace MyShoppingCart.Application.Tests.Validators.Customers;

public class GetCustomerSecurityQueryValidatorTests
{
    private readonly IValidator<GetCustomerSecurityQuery> _validator = new GetCustomerSecurityQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new GetCustomerSecurityQuery(Guid.NewGuid());

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
        var request = new GetCustomerSecurityQuery(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(GetCustomerSecurityQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }

    #endregion
}
