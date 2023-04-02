namespace MyShoppingCart.Application.Customers;

public sealed class CreateCustomerQueryHandler : IRequestHandler<CreateCustomerQuery, Response<CustomerModel>>
{
    private readonly IUnitOfWork _context;

    public CreateCustomerQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<CustomerModel>> Handle(CreateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = request.ToEntity();

        await _context.Customers.AddAsync(customer, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return customer.ToModel();
    }
}
