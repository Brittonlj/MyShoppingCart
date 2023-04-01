using MyShoppingCart.Application.Authentication.Queries;

namespace MyShoppingCart.Api.Endpoints;

public class AuthenticationEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/authentication").AllowAnonymous();

        group.MapPost("/token/{customerId}", GetToken); //This is just a dummy authentication method to test with

        return app;
    }

    public static async Task<IResult> GetToken(
    [FromServices] IMediator mediator,
    IOptionsSnapshot<MyShoppingCartSettings> settings,
    [FromRoute] string customerId,
    [FromQuery] string? role,
    CancellationToken cancellationToken
)
    {
        if (!Guid.TryParse(customerId, out var customerGuid)) 
        {
            return BadRequest("Invalid customerId");
        }

        var request = new JwtTokenQuery(customerGuid, role);
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
