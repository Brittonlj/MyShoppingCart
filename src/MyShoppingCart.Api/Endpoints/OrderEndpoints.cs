using MyShoppingCart.Domain.Configuration;

namespace MyShoppingCart.Api.Endpoints;

public class OrderEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var customer = app.MapGroup("/customer")
            .RequireAuthorization(Policies.CustomerAccess);

        customer.MapGet("/{customerId}/order", GetAllOrders);
        customer.MapGet("/{customerId}/order/{orderId}", GetOrderById);
        customer.MapDelete("{customerId}/order/{orderId}", DeleteOrder);

        var order = app.MapGroup("/order")
            .RequireAuthorization();

        order.MapPost("/", CreateOrder);
        order.MapPut("/", UpdateOrder);

        return app;
    }

    public static async Task<IResult> GetOrderById(
         [FromServices] IMediator mediator,
         [FromRoute] Guid customerId,
         [FromRoute] Guid orderId,
         CancellationToken cancellationToken)
    {
        var request = new GetOrderQuery(customerId, orderId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetAllOrders(
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
