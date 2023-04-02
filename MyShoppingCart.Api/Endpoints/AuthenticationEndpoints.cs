using MyShoppingCart.Application.Authentication.Queries;

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
    IOptionsSnapshot<MyShoppingCartSettings> settings,
    [FromRoute] Guid customerId,
    [FromQuery] string? role,
    CancellationToken cancellationToken
)
    {
        var request = new JwtTokenQuery(customerId, role);
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
