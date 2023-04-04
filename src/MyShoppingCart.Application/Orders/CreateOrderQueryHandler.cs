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

        var order = new Order { CustomerId = customer.Id };

        foreach (var lineItem in request.LineItems)
        {
            order.LineItems.Add(new LineItem(order.Id, lineItem.ProductId, lineItem.Quantity));
        }

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
