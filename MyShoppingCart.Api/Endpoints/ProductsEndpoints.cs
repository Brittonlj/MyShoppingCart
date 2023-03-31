using MyShoppingCart.Application.Products.Queries;

namespace MyShoppingCart.Api.Endpoints;

public static class ProductsEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/product");

        group.MapGet("/", GetAllProducts)
            .AllowAnonymous();

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

}
