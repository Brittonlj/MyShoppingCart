using System.Text.Json;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, Response<Order>>
{
    private readonly IUnitOfWork _context;

    public CreateOrderQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Order>> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FindAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = new Order
        {
            Customer = customer,
            CustomerId = customer.Id,
            OrderDateTimeUtc = DateTime.UtcNow,
        };

        var productsList = await _context.Products.Where(x => request.ProductIds.Contains(x.Id)).ToListAsync(cancellationToken);

        var missingProducts = request.ProductIds.Where(x => !productsList.Select(x => x.Id).Contains(x));

        if (missingProducts.Any())
        {
            var json = JsonSerializer.Serialize(missingProducts);

            return new NotFound(json);
        }

        foreach (var productId in request.ProductIds)
        {
            order.Products.Add(productsList.First(x => x.Id == productId));
        }
        
        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
