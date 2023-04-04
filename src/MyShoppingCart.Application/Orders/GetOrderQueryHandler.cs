namespace MyShoppingCart.Application.Orders
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Response<Order>>
    {
        private readonly IUnitOfWork _context;

        public GetOrderQueryHandler(IUnitOfWork context)
        {
            _context = Guard.Against.Null(context, nameof(context)); ;
        }

        public async Task<Response<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _context
                .Orders
                .Include(x => x.LineItems)
                .ThenInclude(x => x.Product)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(x => x.Id == request.OrderId && x.CustomerId == request.CustomerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                return NotFound.Instance;
            }

            return order;
        }
    }
}
