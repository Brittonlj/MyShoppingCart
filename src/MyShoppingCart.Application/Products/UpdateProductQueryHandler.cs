namespace MyShoppingCart.Application.Products;

public sealed class UpdateProductQueryHandler : IRequestHandler<UpdateProductQuery, Response<Product>>
{
    private readonly IRepository<Product> _productRepository;

    public UpdateProductQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
    }

    public async Task<Response<Product>> Handle(UpdateProductQuery request, CancellationToken cancellationToken)
    {
        var query = new QueryProductById(request.ProductId);
        var product = await _productRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (product is null)
        {
            return NotFound.Instance;
        }

        _productRepository.UpdateEntityProperties(product, request);

        await _productRepository.UpdateAsync(product, cancellationToken);

        return product;

    }
}
