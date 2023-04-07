namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<Order>>
{
    private readonly IRepository<Order> _orderRepository;

    public UpdateOrderQueryHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(orderRepository));
    }

    public async Task<Response<Order>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var query = new QueryOrderById(request.OrderId, request.CustomerId);
        var order = await _orderRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        if (order.OrderDateTimeUtc != request.OrderDateTimeUtc)
        {
            order.OrderDateTimeUtc = request.OrderDateTimeUtc;
        }

        MergeLineItemChanges(order.LineItems, request);

        await _orderRepository.UpdateAsync(order, cancellationToken);

        return order;
    }

    private void MergeLineItemChanges(List<LineItem> originals, UpdateOrderQuery request)
    {
        var matches = originals.Join(request.LineItems,
            originalId => originalId.Id,
            requestId => requestId.Id,
            (original, request) => new { Original = original, Request = request });

        matches
            .Where(x => x.Original != x.Request)
            .ToList()
            .ForEach(x =>
            {
                x.Original.Quantity = x.Request.Quantity;
                x.Original.ProductId = x.Request.ProductId;
            });

        //Add
        var lineItemsToAdd = request.LineItems.Where(x => !originals.Any(y => x.Id == y.Id));
        originals.AddRange(lineItemsToAdd);

        //Delete
        var lineItemsToDelete = originals.Where(x => !request.LineItems.Any(y => x.Id == y.Id)).ToList();

        lineItemsToDelete.ForEach(x => originals.Remove(x));
    }

    
}
