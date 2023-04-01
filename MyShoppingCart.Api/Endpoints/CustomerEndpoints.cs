using Microsoft.AspNetCore.Authorization;

namespace MyShoppingCart.Api.Endpoints;

public sealed class CustomerEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/customer")
            .RequireAuthorization();

        group.MapGet("/", GetAllCustomers);
        group.MapGet("/{customerId}", GetCustomerById);
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
        CancellationToken cancellationToken)
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
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var request = new GetCustomerQuery(customerGuid);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] Customer customer,
        CancellationToken cancellationToken)
    {
        var request = new CreateCustomerCommand(customer);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

 
    public static async Task<IResult> UpdateCustomer(
        [FromServices] IMediator mediator,
        [FromBody] Customer customer,
        CancellationToken cancellationToken)
    {
        var request = new UpdateCustomerCommand(customer, customer.Id);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteCustomer(
        [FromServices] IMediator mediator,
        [FromRoute] string customerId,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            return Problem(Error.InvalidCustomerId.ToJson());
        }

        var request = new DeleteCustomerCommand(customerGuid);

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }
}
