namespace MyShoppingCart.Application.Products;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;
    public DeleteProductCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id,  cancellationToken);

        if (product is null)
        {
            return NotFound.Instance;
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return Success.Instance;
    }
}
