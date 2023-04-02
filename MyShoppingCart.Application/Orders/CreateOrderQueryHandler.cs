using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, Response<OrderModel>>
{
    private readonly IUnitOfWork _context;

    public CreateOrderQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<OrderModel>> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = request.ToShallowEntity(customer);
        var products = _context.Products.Where(x => request.ProductIds.Contains(x.Id)).ToList();
        order.Products.AddRange(products);

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.ToModel();
    }
}
