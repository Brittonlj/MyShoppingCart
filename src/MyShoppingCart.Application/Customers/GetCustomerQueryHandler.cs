﻿namespace MyShoppingCart.Application.Customers;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;

    public GetCustomerQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Response<Customer>> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken = default)
    {
        var query = new QueryCustomerById(request.CustomerId).WithNoTracking();

        var customer = await _customerRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (customer is null)
        {
            return NotFound.Instance;
        }

        return customer;
    }
}
