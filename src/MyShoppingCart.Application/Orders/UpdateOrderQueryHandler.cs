namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<Order>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderQueryHandler(IRepository<Order> orderRepository, IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<Response<Order>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetOrderByIdSpec(request.OrderId, request.CustomerId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        order = _mapper.Map(request, order);

        await _orderRepository.UpdateAsync(order, cancellationToken);

        return order;
    }
}
