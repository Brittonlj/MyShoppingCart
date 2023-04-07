namespace MyShoppingCart.Application.Products;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Success>>
{
    private readonly IRepository<Product> _productRepository;

    public DeleteProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
    }

    public async Task<Response<Success>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var query = new QueryProductById(request.ProductId);
        var product = await _productRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (product is null)
        {
            return NotFound.Instance;
        }

        await _productRepository.DeleteAsync(product);

        return Success.Instance;
    }
}
