using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MyShoppingCart.Api.Tests.Helpers;

public static class AssertionExtensions
{
    public static void AssertCommonErrorConditions(this ProblemHttpResult httpResult, ErrorList errors)
    {
        var problemDetailsJson = JsonSerializer.Serialize(errors);
        httpResult.Should().NotBeNull();
        httpResult.ProblemDetails.Should().NotBeNull();
        httpResult.ProblemDetails.Detail.Should().NotBeNull().And.Be(problemDetailsJson);
    }

    public static void AssertCommonValidationErrorConditions(this ProblemHttpResult httpResult, string key, string errorMessage)
    {
        httpResult.Should().NotBeNull();
        var problemDetails = (HttpValidationProblemDetails)httpResult.ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.NotBeEmpty();
        problemDetails.Errors[key].Should().NotBeNull().And.HaveCount(1);
        problemDetails.Errors[key][0].Should().NotBeNull().And.Be(errorMessage);
    }
}
