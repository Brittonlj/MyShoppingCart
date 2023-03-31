using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Commands;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public DeleteOrderCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Where(x => x.Id == request.OrderId)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        if (request.RequestingCustomerId.HasValue &&
            order.CustomerId != request.RequestingCustomerId.Value)
        {
            return Unauthorized.Instance;
        }

        await _context.OrderProducts
            .Where(o => o.OrderId == request.OrderId)
            .ExecuteDeleteAsync();

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
