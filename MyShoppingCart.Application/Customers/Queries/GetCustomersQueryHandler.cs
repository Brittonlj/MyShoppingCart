using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Customers.Queries;

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
            .AsNoTracking()
            .Paginate(request.PageNumber, request.PageSize);

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

        var orderByClause = OrderByClauses.Customers[request.SortColumn];

        query = request.SortAscending ?
            query.OrderBy(orderByClause) :
            query.OrderByDescending(orderByClause);

        var customers = await query.ToListAsync(cancellationToken);

        return new List<Customer>(customers);
    }
}
