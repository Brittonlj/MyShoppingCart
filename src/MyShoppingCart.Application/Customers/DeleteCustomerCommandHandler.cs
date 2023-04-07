namespace MyShoppingCart.Application.Customers;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<Success>>
{
    private readonly IRepository<Customer> _customerRepository;

    public DeleteCustomerCommandHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
    }

    public async Task<Response<Success>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var spec = new QueryCustomerById(request.CustomerId);
        var customer = await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        await _customerRepository.DeleteAsync(customer, cancellationToken);

        return Success.Instance;
    }
}
