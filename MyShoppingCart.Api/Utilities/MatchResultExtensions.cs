namespace MyShoppingCart.Api.Utilities;

public static class MatchResultExtensions
{
    public static IResult MatchResult<T>(this Response<T> response) where T : class
    {
        return response.Match(
            success => Ok(success),
            unauthorized => Unauthorized(),
            notFound => NotFound(),
            error => Problem(error.ToString()),
            validationFailed => ValidationProblem(validationFailed.Results));
    }
}
