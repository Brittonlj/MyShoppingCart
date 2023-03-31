using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Products.Queries;

public sealed class GetProductsQueryHandler :
    IRequestHandler<GetProductsQuery, Response<IReadOnlyList<Product>>>
{
    private readonly IUnitOfWork _context;
    public GetProductsQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<IReadOnlyList<Product>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context
            .Products
            .AsNoTracking()
            .Paginate(request.PageNumber, request.PageSize);

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(x =>
                x.Name.Contains(request.SearchString) ||
                x.Description.Contains(request.SearchString));
        }

        var orderByClause = OrderByClauses.Products[request.SortColumn];

        query = request.SortAscending ?
            query.OrderBy(orderByClause) :
            query.OrderByDescending(orderByClause);

        var products = await query.ToListAsync(cancellationToken);

        return products;
    }
}
