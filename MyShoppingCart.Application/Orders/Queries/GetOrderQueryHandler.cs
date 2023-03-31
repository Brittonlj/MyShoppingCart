using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Queries
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Response<Order>>
    {
        private readonly IUnitOfWork _context;

        public GetOrderQueryHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Response<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            if (request.RequestingUserId.HasValue && request.RequestingUserId != request.CustomerId)
            {
                return Unauthorized.Instance;
            }

            var query = _context
                .Orders
                .Include(x => x.Products)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(x => x.Id == request.OrderId);

            if (request.CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == request.CustomerId.Value);
            }

            var order = await query.FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                return NotFound.Instance;
            }

            return order;
        }
    }
}
