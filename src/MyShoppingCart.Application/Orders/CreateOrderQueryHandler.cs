using MyShoppingCart.Domain.Utilities;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, Response<Order>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly IUtcDateTimeProvider _dateTimeProvider;

    public CreateOrderQueryHandler(
        IRepository<Order> orderRepository, 
        IRepository<Customer> customerRepository, 
        IMapper mapper, 
        IUtcDateTimeProvider dateTimeProvider)
    {
        _orderRepository = Guard.Against.Null(orderRepository);
        _customerRepository = Guard.Against.Null(customerRepository);
        _mapper = Guard.Against.Null(mapper);
        _dateTimeProvider = Guard.Against.Null(dateTimeProvider);
    }

    public async Task<Response<Order>> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetCustomerByIdSpec(request.CustomerId).WithNoTracking();
        var customer = await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = _mapper.Map<Order>(request);
        order.OrderDateTimeUtc = _dateTimeProvider.GetUtcDateTime();

        order = await _orderRepository.AddAsync(order);

        return order;
    }
}
