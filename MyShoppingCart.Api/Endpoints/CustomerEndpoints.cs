using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MyShoppingCart.Api.Endpoints;

public sealed class CustomerEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/customer")
            .RequireAuthorization();

        group.MapGet("/", GetAllCustomers);
        group.MapGet("/{customerId}", GetCustomerById);
        group.MapGet("/{customerId}/Orders", GetOrdersForCustomer);
        group.MapDelete("/{customerId}/Orders/{orderId}", DeleteCustomerOrder);
        group.MapPost("/", CreateCustomer);
        group.MapPut("/", UpdateCustomer);
        group.MapDelete("/{customerId}", DeleteCustomer);

        return app;
    }

    [Authorize(Roles = "Admin")]
    public static async Task<IResult> GetAllCustomers(
        [FromServices] IMediator mediator,
        IOptionsSnapshot<MyShoppingCartSettings> settings,
        [FromQuery] string? namesLike,
        [FromQuery] string? emailLike,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortColumn,
        [FromQuery] bool? sortAscending,
        CancellationToken cancellationToken
    )
    {
        var defaultPageSize = settings.Value.DefaultPageSize;
        var defaultSortColumn = settings.Value.DefaultPageSorting.Customer;

        var request = new GetCustomersQuery(
            namesLike,
            emailLike,
            pageNumber ?? 1,
            pageSize ?? defaultPageSize,
            sortColumn ?? defaultSortColumn,
            sortAscending ?? true
            );
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetCustomerById(
        [FromServices] IMediator mediator,
        [FromRoute] string customerId,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var requestingCustomerId = context.GetCustomerId();

        var request = new GetCustomerQuery(customerGuid, requestingCustomerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> GetOrdersForCustomer(
    [FromServices] IMediator mediator,
    IOptionsSnapshot<MyShoppingCartSettings> settings,
    [FromRoute] string customerId,
    [FromQuery] int? pageNumber,
    [FromQuery] int? pageSize,
    [FromQuery] string? sortColumn,
    [FromQuery] bool? sortAscending,
    HttpContext context,
    CancellationToken cancellationToken
)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var requestingCustomerId = context.GetCustomerId();

        var defaultPageSize = settings.Value.DefaultPageSize;

        var request = new GetOrdersByCustomerIdQuery(
            customerGuid, 
            pageNumber ?? 1,
            pageSize ?? defaultPageSize,
            sortAscending ?? true,
            requestingCustomerId
            );

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }


    public static async Task<IResult> CreateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] Customer customer,
        CancellationToken cancellationToken
    )
    {
        var request = new CreateCustomerCommand(customer);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> UpdateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] Customer customer,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        var requestingCustomerId = context.GetCustomerId();

        var request = new UpdateCustomerCommand(customer, requestingCustomerId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
    public static async Task<IResult> DeleteCustomer(
            [FromServices] IMediator mediator,
            [FromRoute] string customerId,
            HttpContext context,
            CancellationToken cancellationToken
        )
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var requestingUserId = context.GetCustomerId();

        var request = new DeleteCustomerCommand(customerGuid, requestingUserId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteCustomerOrder(
            [FromServices] IMediator mediator,
            [FromRoute] string customerId,
            [FromRoute] string orderId,
            HttpContext context,
            CancellationToken cancellationToken
        )
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }
        if (!Guid.TryParse(orderId, out var orderGuid))
        {
            return Problem(Error.InvalidOrderId.ToJson());
        }

        var requestingUserId = context.GetCustomerId();

        var request = new DeleteCustomerOrderCommand(customerGuid, orderGuid, requestingUserId);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
}
