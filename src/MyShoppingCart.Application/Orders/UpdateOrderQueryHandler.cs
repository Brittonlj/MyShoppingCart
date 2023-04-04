namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<Order>>
{
    private readonly IUnitOfWork _context;

    public UpdateOrderQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<Order>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(x => x.LineItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x =>
                x.Id == request.OrderId && 
                x.CustomerId == request.CustomerId,
                cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        MergeLineItemChanges(order.LineItems, request);

        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    private void MergeLineItemChanges(List<LineItem> originals, UpdateOrderQuery request)
    {
        //Update (only the quantities can be updated)
        var matches = originals.Join(request.LineItems,
            originalId => originalId.ProductId,
            requestId => requestId.ProductId,
            (original, request) => new { Original = original, Request = request });

        matches
            .Where(x => x.Original.ProductId == x.Request.ProductId && x.Original.Quantity != x.Request.Quantity)
            .ToList()
            .ForEach(x => x.Original.Quantity = x.Request.Quantity);

        //Add
        var lineItemsToAdd = request.LineItems.Where(x => !originals.Any(y => x.ProductId == y.ProductId))
        .Select(x => new LineItem(request.OrderId, x.ProductId, x.Quantity))
        .ToList();

        originals.AddRange(lineItemsToAdd);

        //Delete
        var lineItemsToDelete = originals.Where(x => !request.LineItems.Any(y => x.ProductId == y.ProductId))
            .ToList();

        lineItemsToDelete.ForEach(x => originals.Remove(x));
    }

    
}
