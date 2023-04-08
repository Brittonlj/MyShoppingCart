namespace MyShoppingCart.Application.Tests.Authentication;

public class JwtTokenQueryValidatorTests
{
    private readonly IValidator<JwtTokenQuery> _validator = new JwtTokenQueryValidator();

    #region Happy Path
    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var jwtTokenQuery = new JwtTokenQuery(Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(jwtTokenQuery);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
        results.RuleSetsExecuted.Should().NotBeNull().And.NotBeEmpty();
    }
    #endregion

    #region CustomerId
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var jwtTokenQuery = new JwtTokenQuery(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(jwtTokenQuery);

        //Assert
        results.AssertValidationErrors(
            nameof(JwtTokenQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }
    #endregion
}
