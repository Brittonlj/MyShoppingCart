using FluentValidation.Results;

namespace MyShoppingCart.Application.Tests.Helpers;

public static class ValidationAssertionExtensions
{
    public static void AssertValidationErrors(this ValidationResult results, string columnName, string errorMessage, int errorIndex = 0)
    {
        ;

        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[errorIndex].PropertyName.Should().Be(columnName);
        results.Errors[errorIndex].ErrorMessage.Should().Be(errorMessage);

    }
}
