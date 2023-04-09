namespace MyShoppingCart.Application.Products;

public sealed class CreateProductQueryHandler : IRequestHandler<CreateProductQuery, Response<Product>>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Response<Product>> Handle(CreateProductQuery request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product);

        return product;
    }
}
