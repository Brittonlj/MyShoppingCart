namespace MyShoppingCart.Application.Customers;

public sealed class UpdateCustomerQueryHandler : IRequestHandler<UpdateCustomerQuery, Response<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;

    public UpdateCustomerQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = Guard.Against.Null(customerRepository, nameof(customerRepository));
    }

    public async Task<Response<Customer>> Handle(UpdateCustomerQuery request, CancellationToken cancellationToken)
    {
        var customerQuery = new QueryCustomerById(request.CustomerId);
        var customer = await _customerRepository.FirstOrDefaultAsync(customerQuery, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        _customerRepository.UpdateEntityProperties(customer, request);

        UpdateAddress(customer.BillingAddress, request.BillingAddress);
        UpdateAddress(customer.ShippingAddress, request.ShippingAddress);

        await _customerRepository.UpdateAsync(customer, cancellationToken);

        return customer;
    }

    private void UpdateAddress(Address? original, Address? request)
    {
        if (original is null && request is not null)
        {
            original = request;
        }
        if (original is not null && request is null)
        {
            original = null;
        }
        if (original is not null && request is not null)
        {
            _customerRepository.UpdateEntityProperties(original, request);
        }
    }
}
