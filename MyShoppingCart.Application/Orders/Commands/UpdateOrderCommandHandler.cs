using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Commands;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public UpdateOrderCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = request.Order.Id;

        var order = await _context
            .Orders
            .FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        if (order != request.Order)
        {
            _context.Entry(order).CurrentValues.SetValues(request.Order);
        }

        var original = await _context.OrderProducts.Where(x => x.OrderId == orderId).ToListAsync();
        var updated = request.Order.Products
            .Select(x => new OrderProduct { OrderId = orderId, ProductId = x.Id }).ToList();

        if (!original.SequenceEqual(updated))
        {
            var orderProductsToAdd = updated.Where(x => !original.Contains(x));
            _context.OrderProducts.AddRange(orderProductsToAdd);

            var orderProductsToDelete = original.Where(x => !updated.Contains(x));
            _context.OrderProducts.RemoveRange(orderProductsToDelete); //TODO: Refactor to use ExecuteDelete
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
