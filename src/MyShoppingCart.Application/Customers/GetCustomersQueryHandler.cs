namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomersQueryHandler :
    IRequestHandler<GetCustomersQuery, Response<IReadOnlyList<CustomerModel>>>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    public GetCustomersQueryHandler(IRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = Guard.Against.Null(customerRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<Response<IReadOnlyList<CustomerModel>>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new GetCustomersSpec(
            request.NamesLike,
            request.EmailLike,
            request.PageNumber,
            request.PageSize,
            request.SortColumn,
            request.SortAscending)
            .WithNoTracking();

        var customers = await _customerRepository.ListAsync(spec, cancellationToken);

        var customerModels = customers.Select(x => _mapper.Map<CustomerModel>(x)).ToList();

        return customerModels;
    }
}
