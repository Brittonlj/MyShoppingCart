namespace MyShoppingCart.Application.Orders
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Response<Order>>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetOrderQueryHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = Guard.Against.Null(orderRepository, nameof(orderRepository));
        }

        public async Task<Response<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetOrderByIdSpec(request.OrderId, request.CustomerId)
                .WithNoTracking();

            var order = await _orderRepository.FirstOrDefaultAsync(spec);

            if (order is null)
            {
                return NotFound.Instance;
            }

            return order;
        }
    }
}
