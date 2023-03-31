using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Queries;

public sealed class GetOrdersByCustomerIdQueryHandler : 
    IRequestHandler<GetOrdersByCustomerIdQuery, Response<IReadOnlyList<Order>>>
{
    private readonly IUnitOfWork _context;

    public GetOrdersByCustomerIdQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<IReadOnlyList<Order>>> Handle(
        GetOrdersByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.RequestingCustomerId.HasValue &&
            request.CustomerId != request.RequestingCustomerId)
        {
            return Unauthorized.Instance;
        }

        var query = _context
            .Orders
            .Include(x => x.Products)
            .AsSplitQuery()
            .AsNoTracking()
            .Paginate(request.PageNumber, request.PageSize)
            .Where(x => x.CustomerId == request.CustomerId);

        query = request.SortAscending ?
            query.OrderBy(x => x.OrderDateTimeUtc) :
            query.OrderByDescending(x => x.OrderDateTimeUtc);

        var orders = await query.ToListAsync(cancellationToken);

        return orders;

    }
}
