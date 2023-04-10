namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    public UpdateCustomerQueryHandler(IRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Response<Customer>> Handle(UpdateCustomerQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetCustomerByIdSpec(request.CustomerId);
        var customer = await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        customer = _mapper.Map(request, customer);

        await _customerRepository.UpdateAsync(customer, cancellationToken);

        return customer;
    }
}
