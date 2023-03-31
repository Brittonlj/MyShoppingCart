namespace MyShoppingCart.Application.Customers.Commands;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<Success>>
{
    private readonly IUnitOfWork _context;
 
    public CreateCustomerCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<Response<Success>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .FindAsync(request.Customer.Id, cancellationToken);

        if (customer == request.Customer)
        {
            return Success.Instance; //Exact same customer exists
        }
        else if (customer is not null)
        {
            return new ErrorList { new Error("CustomerExists", "Customer Already Exists.") };
        }

        await _context.Customers.AddAsync(request.Customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Success.Instance;
    }
}
