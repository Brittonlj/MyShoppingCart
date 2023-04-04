using Microsoft.AspNetCore.Authorization;
using MyShoppingCart.Domain.Configuration;

namespace MyShoppingCart.Api.Endpoints;

public static class ProductEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/product")
            .RequireAuthorization();

        group.MapGet("/", GetAllProducts)
            .AllowAnonymous();
        group.MapPost("/", CreateProduct);
        group.MapPut("/", UpdateProduct);
        group.MapDelete("/{productId}", DeleteProduct);

        return app;
    }

    public static async Task<IResult> GetAllProducts(
        [FromServices] IMediator mediator,
        [FromServices] IOptionsSnapshot<MyShoppingCartSettings> settings,
        [FromQuery] string? searchString,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? sortColumn,
        [FromQuery] bool? sortAscending,
        CancellationToken cancellationToken)
    {
        var defaultPageSize = settings.Value.DefaultPageSize;
        var defaultSortColumn = settings.Value.DefaultPageSorting.Product;

        var request = new GetProductsQuery(
            searchString,
            pageNumber ?? 1,
            pageSize ?? defaultPageSize,
            sortColumn ?? defaultSortColumn,
            sortAscending ?? true
            );

        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    [Authorize(Roles = Roles.Admin)]
    public static async Task<IResult> CreateProduct(
        [FromServices] IMediator mediator,
        [FromBody] CreateProductQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    [Authorize(Roles = Roles.Admin)]
    public static async Task<IResult> UpdateProduct(
    [FromServices] IMediator mediator,
    [FromBody] UpdateProductQuery request,
    CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    [Authorize(Roles = Roles.Admin)]
    public static async Task<IResult> DeleteProduct(
        [FromServices] IMediator mediator,
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        var request = new DeleteProductCommand(productId);
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
