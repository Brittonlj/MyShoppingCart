using FluentAssertions;
using FluentValidation;
using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Application.Tests.Authentication;

public class JwtTokenQueryValidatorTests
{
    private readonly IValidator<JwtTokenQuery> _validator = new JwtTokenQueryValidator();

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

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var jwtTokenQuery = new JwtTokenQuery(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(jwtTokenQuery);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors.First().PropertyName.Should().Be(nameof(JwtTokenQuery.CustomerId));
        results.Errors.First().ErrorMessage.Should().Be("'Customer Id' must not be empty.");
        results.RuleSetsExecuted.Should().NotBeNull().And.NotBeEmpty();
    }
}
