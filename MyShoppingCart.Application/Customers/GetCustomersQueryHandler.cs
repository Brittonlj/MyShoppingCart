namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomersQueryHandler :
    IRequestHandler<GetCustomersQuery, Response<IReadOnlyList<Customer>>>
{
    private readonly IUnitOfWork _context;

    public GetCustomersQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<IReadOnlyList<Customer>>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context
            .Customers
            .Include(x => x.BillingAddress)
            .Include(x => x.ShippingAddress)
            .AsSplitQuery()
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.NamesLike))
        {
            query = query.Where(
                x => x.FirstName.Contains(request.NamesLike) ||
                x.LastName.Contains(request.NamesLike));
        }

        if (!string.IsNullOrWhiteSpace(request.EmailLike))
        {
            query = query.Where(x => x.Email.Contains(request.EmailLike));
        }

        var orderByClause = GetCustomersQuery.OrderByClauses[request.SortColumn];

        query = request.SortAscending ?
            query.OrderBy(orderByClause) :
            query.OrderByDescending(orderByClause);

        query = query.Paginate(request.PageNumber, request.PageSize);

        var customers = await query.ToListAsync(cancellationToken);

        return customers.Select(x => x).ToList();
    }
}
