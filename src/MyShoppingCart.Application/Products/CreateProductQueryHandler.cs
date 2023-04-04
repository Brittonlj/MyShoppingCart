namespace MyShoppingCart.Application.Products;

public sealed class CreateProductQueryHandler : IRequestHandler<CreateProductQuery, Response<Product>>
{
    private readonly IUnitOfWork _context;

    public CreateProductQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
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

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }
}
