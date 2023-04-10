namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomersQueryHandler :
    IRequestHandler<GetCustomersQuery, Response<IReadOnlyList<Customer>>>
{
    private readonly IRepository<Customer> _customerRepository;

    public GetCustomersQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository)); ;
    }

    public async Task<Response<IReadOnlyList<Customer>>> Handle(
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

        return customers;
    }
}
