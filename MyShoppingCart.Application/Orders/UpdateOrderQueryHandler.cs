namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<Order>>
{
    private readonly IUnitOfWork _context;

    public UpdateOrderQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Order>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(x => x.Products)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x =>
                x.Id == request.OrderId && x.CustomerId == request.CustomerId,
                cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        var productGuids = request.ProductIds.Select(x => x).ToList();
        var requestProducts = await _context.Products.Where(x => productGuids.Contains(x.Id)).ToListAsync();

        // Only update the product lists
        EntityUtility.MergeEntityLists(_context.Products, order.Products, requestProducts);

        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
