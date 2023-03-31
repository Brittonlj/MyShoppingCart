namespace MyShoppingCart.Api.Endpoints;

public sealed class OrderEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/order")
            .RequireAuthorization();

        // default getter is excluded on purpose
        group.MapGet("/{orderId}", GetOrderById);
        group.MapPost("/", CreateOrder);
        group.MapPut("/", UpdateOrder);
        group.MapDelete("/{orderId}", DeleteOrder);

        return app;
    }


    public static async Task<IResult> GetOrderById(
        [FromServices] IMediator mediator,
        [FromRoute] string orderId,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(orderId, out var orderGuid))
        {
            return Problem(Error.InvalidOrderId.ToJson());
        }

        var customerId = context.GetCustomerId();

        var request = new GetOrderQuery(orderGuid, customerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateOrder(
        [FromServices] IMediator mediator,
        [FromBody] Order order,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        var requestingCustomerId = context.GetCustomerId();

        var request = new CreateOrderCommand(order, requestingCustomerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> UpdateOrder(
        [FromServices] IMediator mediator,
        [FromBody] Order order,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        var requestingCustomerId = context.GetCustomerId();

        var request = new UpdateOrderCommand(order, requestingCustomerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteOrder(
        [FromServices] IMediator mediator,
        [FromRoute] string orderId,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(orderId, out var orderGuid))
        {
            return Problem(Error.InvalidOrderId.ToJson());
        }

        var requestingCustomerId = context.GetCustomerId();

        var request = new DeleteOrderCommand(orderGuid, requestingCustomerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
