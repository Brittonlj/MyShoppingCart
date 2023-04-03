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
        var customer = await _context.Customers.FindAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = new Order { Customer = customer, CustomerId = customer.Id };

        foreach (var lineItem in request.LineItems)
        {
            _context.OrderProducts.Add(
                new OrderProduct { OrderId = order.Id, ProductId = lineItem.ProductId, Quantity = lineItem.Quantity });
        }

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
