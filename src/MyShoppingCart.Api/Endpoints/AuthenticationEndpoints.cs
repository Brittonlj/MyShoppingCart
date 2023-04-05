using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Api.Endpoints;

public class AuthenticationEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/authentication").AllowAnonymous();

        group.MapGet("/token/{customerId}", GetToken); //This is just a dummy authentication method to test with

        return app;
    }

    public static async Task<IResult> GetToken(
    [FromServices] IMediator mediator,
    [FromRoute] Guid customerId,
    CancellationToken cancellationToken
)
    {
        var request = new JwtTokenQuery(customerId);
        var response = await mediator.Send(request, cancellationToken);

        return response.Match(
            success => Content(success),
            unauthorized => Unauthorized(),
            notFound => NotFound(),
            error => Problem(error.ToJson()),
            validationFailed => ValidationProblem(validationFailed.Results));
    }

}
