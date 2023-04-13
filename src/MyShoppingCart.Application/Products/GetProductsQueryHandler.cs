namespace MyShoppingCart.Application.Products;

public sealed class GetProductsQueryHandler :
    IRequestHandler<GetProductsQuery, Response<IReadOnlyList<Product>>>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductsQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository);
    }

    public async Task<Response<IReadOnlyList<Product>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new GetProductsSpec(
            request.SearchString,
            request.PageNumber,
            request.PageSize,
            request.SortColumn,
            request.SortAscending)
            .WithNoTracking();

        var products = await _productRepository.ListAsync(spec, cancellationToken);

        return products;
    }
}
