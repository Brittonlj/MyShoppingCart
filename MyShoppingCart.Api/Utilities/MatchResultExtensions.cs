namespace MyShoppingCart.Api.Utilities;

public static class MatchResultExtensions
{
    public static IResult MatchResult<T>(this Response<T> response) where T : class
    {
        return response.Match(
            success => (success is Success) ? Ok() : Ok(success),
            unauthorized => Unauthorized(),
            notFound => (string.IsNullOrWhiteSpace(notFound.Message)) ? NotFound() : NotFound(notFound.Message),
            error => Problem(error.ToJson()),
            validationFailed => ValidationProblem(validationFailed.Results));
    }
}
