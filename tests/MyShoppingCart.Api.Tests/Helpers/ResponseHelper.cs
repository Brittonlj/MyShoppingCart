namespace MyShoppingCart.Api.Tests.Helpers;

public static class ResponseHelper
{
    public static Response<T> GetSampleErrorResponse<T>()
        where T : class
    {
        return Response<T>.FromErrorList(new ErrorList
        {
            new Error("Exception", "An error has occured")
        });
    }

    public static Response<T> GetSampleValidationErrorResponse<T>(string key, string errorMessage)
        where T : class
    {
        return Response<T>.FromValidationFailure(
            new Dictionary<string, string[]>
            {
                { key, new string[]{ errorMessage } }
            });
    }
}
