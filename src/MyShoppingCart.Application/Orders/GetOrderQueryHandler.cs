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
            var query = new QueryOrderById(request.OrderId, request.CustomerId)
                .WithProducts()
                .WithNoTracking();

            var order = await _orderRepository.FirstOrDefaultAsync(query);

            if (order is null)
            {
                return NotFound.Instance;
            }

            return order;
        }
    }
}
