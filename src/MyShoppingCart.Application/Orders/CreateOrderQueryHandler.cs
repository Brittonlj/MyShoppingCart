using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, Response<Order>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Customer> _customerRepository;

    public CreateOrderQueryHandler(IRepository<Order> orderRepository, IRepository<Customer> customerRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(orderRepository));
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
    }

    public async Task<Response<Order>> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        var custoerQuery = new QueryCustomerById(request.CustomerId).WithNoTracking();
        var customer = await _customerRepository.FirstOrDefaultAsync(custoerQuery, cancellationToken);

        if (customer is null)
        {
            return new NotFound(Error.CustomerNotFound.Message);
        }

        var order = new Order { CustomerId = customer.Id };

        order.AddUpdateLineItemRange(request.LineItems);

        await _orderRepository.AddAsync(order);

        return order;
    }
}
