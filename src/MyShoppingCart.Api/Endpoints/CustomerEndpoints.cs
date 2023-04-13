namespace MyShoppingCart.Api.Endpoints;

public sealed class CustomerEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/customer")
            .RequireAuthorization();

        group.MapGet("/", GetAllCustomers)
            .RequireAuthorization(Policies.AdminAccess);

        group.MapGet("/{customerId}", GetCustomerById)
            .RequireAuthorization(Policies.CustomerAccess);

        group.MapPost("/", CreateCustomer)
            .RequireAuthorization(Policies.AdminAccess);

        group.MapPut("/", UpdateCustomer)
            .RequireAuthorization(Policies.CustomerAccess);

        group.MapDelete("/{customerId}", DeleteCustomer)
            .RequireAuthorization(Policies.AdminAccess);

        return app;
    }

    public static async Task<IResult> GetAllCustomers(
        [FromServices] IMediator mediator,
        [AsParameters] GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetCustomerById(
        [FromServices] IMediator mediator,
        [AsParameters] GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] CreateCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> UpdateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] UpdateCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteCustomer(
        [FromServices] IMediator mediator,
        [AsParameters] DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
}
