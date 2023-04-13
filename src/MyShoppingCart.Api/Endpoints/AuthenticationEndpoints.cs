using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Api.Endpoints;

public class AuthenticationEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/authentication")
            .RequireAuthorization(Policies.CustomerAccess);

        group.MapPost("/login", Login)
            .AllowAnonymous();

        return app;
    }

    public static async Task<IResult> Login(
        [FromServices] IMediator mediator,
        [FromBody] LoginQuery request,
        CancellationToken cancellationToken
)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
}
