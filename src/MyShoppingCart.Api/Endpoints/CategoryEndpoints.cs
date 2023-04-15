using MyShoppingCart.Application.Categories;

namespace MyShoppingCart.Api.Endpoints;

public sealed class CategoryEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/category", GetAllCategories)
            .AllowAnonymous();

        return app;
    }

    public async static Task<IResult> GetAllCategories(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var request = new GetCategoriesQuery();

        var response = await mediator.Send(request);

        return response.MatchResult();
    }
}
