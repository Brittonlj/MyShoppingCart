namespace MyShoppingCart.Application.Orders;

public sealed class GetOrdersQueryHandler :
    IRequestHandler<GetOrdersQuery, Response<IReadOnlyList<Order>>>
{
    private readonly IUnitOfWork _context;

    public GetOrdersQueryHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<IReadOnlyList<Order>>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context
            .Orders
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.CustomerId == request.CustomerId);

        query = request.SortAscending ?
            query.OrderBy(x => x.OrderDateTimeUtc) :
            query.OrderByDescending(x => x.OrderDateTimeUtc);

        query = query
            .Paginate(request.PageNumber, request.PageSize)
            .Include(x => x.LineItems)
            .ThenInclude(x => x.Product)
;
        var orders = await query.ToListAsync(cancellationToken);

        return orders;

    }
}
