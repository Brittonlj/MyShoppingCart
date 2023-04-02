namespace MyShoppingCart.Api.Endpoints;

public class OrderEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/order")
            .RequireAuthorization();

        group.MapGet("/{customerId}", GetOrders);
        group.MapGet("/{orderId}/{customerId}", GetOrder);
        group.MapPost("/", CreateOrder);
        group.MapPut("/", UpdateOrder);
        group.MapDelete("/{orderId}/{customerId}", DeleteOrder);

        return app;
    }

    public static async Task<IResult> GetOrder(
         [FromServices] IMediator mediator,
         IOptionsSnapshot<MyShoppingCartSettings> settings,
         [FromRoute] Guid customerId,
         [FromRoute] Guid orderId,
         CancellationToken cancellationToken)
    {
        var request = new GetOrderQuery(customerId, orderId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetOrders(
        [FromServices] IMediator mediator,
        IOptionsSnapshot<MyShoppingCartSettings> settings,
        [FromRoute] Guid customerId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortColumn,
        [FromQuery] bool? sortAscending,
        CancellationToken cancellationToken)
    {
        var defaultPageSize = settings.Value.DefaultPageSize;
        var defaultSortColumn = settings.Value.DefaultPageSorting.Order;

        var request = new GetOrdersQuery(
            customerId,
            pageNumber ?? 1,
            pageSize ?? defaultPageSize,
            defaultSortColumn,
            sortAscending ?? true
            );

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateOrder(
         [FromServices] IMediator mediator,
         [FromBody] CreateOrderQuery request,
         CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
    public static async Task<IResult> UpdateOrder(
        [FromServices] IMediator mediator,
        [FromBody] UpdateOrderQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteOrder(
            [FromServices] IMediator mediator,
            [FromRoute] Guid customerId,
            [FromRoute] Guid orderId,
            CancellationToken cancellationToken)
    {
        var request = new DeleteOrderCommand(customerId, orderId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
