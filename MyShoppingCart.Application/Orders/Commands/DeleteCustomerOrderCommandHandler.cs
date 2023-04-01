using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Orders.Commands;

public sealed class DeleteCustomerOrderCommandHandler : IRequestHandler<DeleteCustomerOrderCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public DeleteCustomerOrderCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(DeleteCustomerOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Where(x => x.Id == request.OrderId && x.CustomerId == request.CustomerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return NotFound.Instance;
        }

        await _context.OrderProducts
            .Where(o => o.OrderId == request.OrderId)
            .ExecuteDeleteAsync();

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
