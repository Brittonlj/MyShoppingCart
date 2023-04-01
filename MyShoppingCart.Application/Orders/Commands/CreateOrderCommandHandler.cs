using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Commands;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public CreateOrderCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FirstOrDefaultAsync(x => x.Id == request.Order.CustomerId, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = request.Order;

        _context.Orders.Add(order);

        var orderProducts = order.Products
            .Select(x => new OrderProduct
            {
                OrderId = order.Id,
                ProductId = x.Id,
            });

        await _context.OrderProducts.AddRangeAsync(orderProducts);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
