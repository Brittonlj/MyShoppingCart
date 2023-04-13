using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Application.Products;

public sealed class UpdateProductQueryHandler : IRequestHandler<UpdateProductQuery, Response<Product>>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<Response<Product>> Handle(UpdateProductQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new GetProductByIdSpec(request.ProductId);
        var product = await _productRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (product is null)
        {
            return NotFound.Instance;
        }

        _mapper.From(request).AdaptTo(product);

        await _productRepository.UpdateAsync(product, cancellationToken);

        return product;

    }
}
