namespace MyShoppingCart.Application.Products;

public sealed class CreateProductQueryHandler : IRequestHandler<CreateProductQuery, Response<Product>>
{
    private readonly IRepository<Product> _productRepository;

    public CreateProductQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
    }

    public async Task<Response<Product>> Handle(CreateProductQuery request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
        };

        await _productRepository.AddAsync(product);

        return product;
    }
}
