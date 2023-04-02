using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, Response<OrderModel>>
{
    private readonly IUnitOfWork _context;

    public UpdateOrderQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<OrderModel>> Handle(UpdateOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(x => x.Products)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x =>
                x.Id == request.OrderId && x.CustomerId == request.CustomerId,
                cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        var productGuids = request.ProductIds.Select(x => x).ToList();
        var requestProducts = await _context.Products.Where(x => productGuids.Contains(x.Id)).ToListAsync();

        foreach (var product in order.Products.Where(x => !productGuids.Contains(x.Id)))
        {
            order.Products.Remove(product);
        }
        foreach (var product in requestProducts.Where(x => !order.Products.Contains(x)))
        {
            order.Products.Add(product);
        }

        _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.ToModel();
    }
}
