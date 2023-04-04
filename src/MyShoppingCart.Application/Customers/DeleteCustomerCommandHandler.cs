﻿namespace MyShoppingCart.Application.Customers;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;

    public DeleteCustomerCommandHandler(IUnitOfWork context)
    {
        _context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task<Response<Success>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FindAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        _context.Customers.Remove(customer);

        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}