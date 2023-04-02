namespace MyShoppingCart.Application.Orders;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public DeleteOrderCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(x => x.Products)
            .Where(x => x.Id == request.OrderId && x.CustomerId == request.CustomerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
