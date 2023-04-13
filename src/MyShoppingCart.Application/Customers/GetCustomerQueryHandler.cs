namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<CustomerModel>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    public GetCustomerQueryHandler(IRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<CustomerModel>> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken = default)
    {
        var spec = new GetCustomerByIdSpec(request.CustomerId).WithNoTracking();

        var customer = await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        var customerModel = _mapper.Map<CustomerModel>(customer);
        return customerModel;
    }
}
