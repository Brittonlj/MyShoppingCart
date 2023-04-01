namespace MyShoppingCart.Api.Endpoints;

public class CustomerOrderEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/customer")
            .RequireAuthorization();

        group.MapGet("/{customerId}/order", GetOrders);
        group.MapGet("/{customerId}/order/{orderId}", GetOrder);
        group.MapPost("/{customerId}/order", CreateOrder);
        group.MapPut("/{customerId}/order", UpdateOrder);
        group.MapDelete("/{customerId}/order/{orderId}", DeleteOrder);

        return app;
    }

    public static async Task<IResult> GetOrder(
         [FromServices] IMediator mediator,
         IOptionsSnapshot<MyShoppingCartSettings> settings,
         [FromRoute] string customerId,
         [FromRoute] string orderId,
         CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }
        if (!Guid.TryParse(orderId, out var orderGuid))
        {
            return Problem(Error.InvalidOrderId.ToJson());
        }

        var request = new GetOrderQuery(
            customerGuid,
            orderGuid);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetOrders(
        [FromServices] IMediator mediator,
        IOptionsSnapshot<MyShoppingCartSettings> settings,
        [FromRoute] string customerId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortColumn,
        [FromQuery] bool? sortAscending,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var defaultPageSize = settings.Value.DefaultPageSize;

        var request = new GetOrdersQuery(
            customerGuid,
            pageNumber ?? 1,
            pageSize ?? defaultPageSize,
            sortAscending ?? true
            );

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateOrder(
         [FromServices] IMediator mediator,
         [FromRoute] string customerId,
         [FromBody] Order order,
         CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var request = new CreateOrderCommand(customerGuid, order);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
    public static async Task<IResult> UpdateOrder(
        [FromServices] IMediator mediator,
        [FromRoute] string customerId,
        [FromBody] Order order,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var request = new UpdateOrderCommand(customerGuid, order);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteOrder(
            [FromServices] IMediator mediator,
            [FromRoute] string customerId,
            [FromRoute] string orderId,
            CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }
        if (!Guid.TryParse(orderId, out var orderGuid))
        {
            return Problem(Error.InvalidOrderId.ToJson());
        }

        var request = new DeleteCustomerOrderCommand(customerGuid, orderGuid);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
