namespace MyShoppingCart.Application.Orders;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<Success>>
{
    private readonly IRepository<Order> _orderRepository;

    public DeleteOrderCommandHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository);
    }

    public async Task<Response<Success>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetOrderByIdSpec(request.OrderId, request.CustomerId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        await _orderRepository.DeleteAsync(order);

        return Success.Instance;
    }
}
