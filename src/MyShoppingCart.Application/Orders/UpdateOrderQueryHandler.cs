namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<Order>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderQueryHandler(IRepository<Order> orderRepository, IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(orderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Response<Order>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var query = new QueryOrderById(request.OrderId, request.CustomerId);
        var order = await _orderRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        _mapper.From(request).AdaptTo(order);

        MergeLineItemChanges(order, request);

        await _orderRepository.UpdateAsync(order, cancellationToken);

        return order;
    }

    private void MergeLineItemChanges(Order original, UpdateOrderQuery request)
    {
        var lineItemsToDelete = original.LineItems.Where(x => !request.LineItems.Any(y => x.Id == y.Id)).ToList();

        original.RemoveLineItemRange(lineItemsToDelete);

        original.AddUpdateLineItemRange(request.LineItems);
    }

    
}
