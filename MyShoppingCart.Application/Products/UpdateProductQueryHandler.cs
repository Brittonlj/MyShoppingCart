namespace MyShoppingCart.Application.Products;

public sealed class UpdateProductQueryHandler : IRequestHandler<UpdateProductQuery, Response<Product>>
{
    private readonly IUnitOfWork _context;

    public UpdateProductQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Product>> Handle(UpdateProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return NotFound.Instance;
        }

        _context.Entry(product).CurrentValues.SetValues(request);

        await _context.SaveChangesAsync();

        return product;

    }
}
