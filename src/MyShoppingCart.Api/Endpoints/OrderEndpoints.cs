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
         [AsParameters] GetOrderQuery request,
         CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetAllOrders(
        [FromServices] IMediator mediator,
        [AsParameters] GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
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
            [AsParameters] DeleteOrderCommand request,
            CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
