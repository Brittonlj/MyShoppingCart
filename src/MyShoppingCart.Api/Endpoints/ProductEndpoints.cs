namespace MyShoppingCart.Api.Endpoints;

public static class ProductEndpoints
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/product")
            .RequireAuthorization(Policies.AdminAccess);

        group.MapGet("/", GetProducts)
            .AllowAnonymous();

        group.MapPost("/", CreateProduct);
        
        group.MapPut("/", UpdateProduct);
        
        group.MapDelete("/{productId}", DeleteProduct);

        return app;
    }

    public static async Task<IResult> GetProducts(
        [FromServices] IMediator mediator,
        [AsParameters] GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> CreateProduct(
        [FromServices] IMediator mediator,
        [FromBody] CreateProductQuery request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> UpdateProduct(
    [FromServices] IMediator mediator,
    [FromBody] UpdateProductQuery request,
    CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

    public static async Task<IResult> DeleteProduct(
        [FromServices] IMediator mediator,
        [AsParameters] DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return response.MatchResult();
    }

}
