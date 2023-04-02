namespace MyShoppingCart.Application.Products;

public sealed class CreateProductQueryHandler : IRequestHandler<CreateProductQuery, Response<ProductModel>>
{
    private readonly IUnitOfWork _context;

    public CreateProductQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<ProductModel>> Handle(CreateProductQuery request, CancellationToken cancellationToken)
    {
        var product = request.ToEntity();

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product.ToModel();
    }
}
