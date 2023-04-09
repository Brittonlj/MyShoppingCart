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
        _orderRepository = Guard.Against.Null(orderRepository, nameof(orderRepository));
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _dateTimeProvider = Guard.Against.Null(dateTimeProvider, nameof(dateTimeProvider));
    }

    public async Task<Response<Order>> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        var custoerQuery = new QueryCustomerById(request.CustomerId).WithNoTracking();
        var customer = await _customerRepository.FirstOrDefaultAsync(custoerQuery, cancellationToken);

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
